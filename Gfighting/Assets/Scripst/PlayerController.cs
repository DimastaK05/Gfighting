using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public static float speed = 5f;
    private Rigidbody rigidbody;
    public static float rotationSpeed = 5f;
    public Transform groundCheckerTransform;
    public LayerMask notPlayerMask;
    private CapsuleCollider collider;
    public float jumpForce = 2f;

    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Поворот и анимации
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 directionVector = new Vector3(-h, 0, -v);

        if (directionVector.magnitude > 0.05f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(directionVector),
                Time.deltaTime * rotationSpeed);
        }

        animator.SetFloat("speed", directionVector.magnitude);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Приседание
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            UnCrouch();
        }

      if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        // Проверка земли
        animator.SetBool("IsInAir", !Physics.CheckSphere(groundCheckerTransform.position, 0.3f, notPlayerMask));
    }

    void FixedUpdate()
    {
        // Движение (работает с физикой)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(-h, 0, -v) * speed;
        rigidbody.velocity = new Vector3(moveDir.x, rigidbody.velocity.y, moveDir.z);
    }

    void Jump()
    {
        if (Physics.Raycast(groundCheckerTransform.position, Vector3.down, 0.2f, notPlayerMask))
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Crouch()
    {
        animator.SetBool("IsCrouch", false);
        speed = 3f;
        collider.height = 2f;
        collider.center = new Vector3(collider.center.x, 1.02f, collider.center.z);
    }

    void UnCrouch()
    {
        animator.SetBool("IsCrouch", true);
        speed = 5f;
        collider.height = 3.87f;
        collider.center = new Vector3(0.01230764f, 1.855963f, 0);
    }
}