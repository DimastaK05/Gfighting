using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    //переменная для управления анимациями персонажа.
    private Animator animator;

    //Переменная для скорости
    public float speed = 5f;
    //Переменная для управления физическим движением персонажа. 
    private Rigidbody rigidbody;

    //Переменная для поворота
    public float rotationSpeed = 10f;

    public Transform groundCheckerTransform;

    public LayerMask notPlayerMask;

    private CapsuleCollider collider;
    public float jumpForce = 2f;
    // Этот метод вызывается только один раз при запуске сцены.
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();//GetComponent<Animator>(): Получает компонент Animator, прикрепленный к тому же игровому объекту, что и этот скрипт.
        rigidbody = GetComponent<Rigidbody>();// GetComponent<Rigidbody>(): Получает компонент Rigidbody, прикрепленный к тому же игровому объекту, что и этот скрипт.


    }

    // Этот метод вызывается каждый кадр игры.
    void Update()
    {

        float h = Input.GetAxis("Horizontal");// Получает значение горизонтальной оси управления 
        float v = Input.GetAxis("Vertical");//Получает значение вертикальной оси управления

        Vector3 directionVector = new Vector3(-h, 0, -v); //Создается вектор, который определяет направление движения персонажа.

        if (directionVector.magnitude > Mathf.Abs(0.05f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationSpeed); //отвечает за плавный поворот персонажа в сторону движения.

        animator.SetFloat("speed", Vector3.ClampMagnitude(directionVector, 1).magnitude); //устанавливает значение “speed” для аниматора, чтобы выбрать правильную анимацию движения.\

        Vector3 moveDir = Vector3.ClampMagnitude(directionVector, 1) * speed;

        rigidbody.velocity = new Vector3(moveDir.x, rigidbody.velocity.y, moveDir.z);//устанавливает скорость персонажа в направлении движения.

        rigidbody.angularVelocity = Vector3.zero;// сбрасывает угловую скорость, чтобы предотвратить случайный поворот персонажа.


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            UnCrouch();
        }

        if (Physics.CheckSphere(groundCheckerTransform.position, 0.3f, notPlayerMask))
        {
            animator.SetBool("IsInAir", false);

        }
        else
        {
            animator.SetBool("IsInAir", true);
        }

    }
    void Jump()
    {
        if (animator.GetBool("IsCrouch") == false) return;
        RaycastHit hit;
        if (Physics.Raycast(groundCheckerTransform.position, Vector3.down, 0.2f, notPlayerMask))
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Did not find ground layer");
        }
    }

    void Crouch()
    {
        if (Physics.Raycast(groundCheckerTransform.position, Vector3.down, 0.2f, notPlayerMask))
        {
            animator.SetBool("IsCrouch", false);
            speed = 3f;
            collider.height = 2f;
            collider.center = new Vector3(collider.center.x, 1.02f, collider.center.z);

        }
    }

    void UnCrouch()
    {
        animator.SetBool("IsCrouch", true);
        speed = 5f;
        collider.height = 3.87f;
        collider.center = new Vector3(0.01230764f, 1.855963f, 0);
    }
}
