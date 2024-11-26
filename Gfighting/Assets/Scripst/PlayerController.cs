using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    //���������� ��� ���������� ���������� ���������.
    private Animator animator;

    //���������� ��� ��������
    public float speed = 5f;
    //���������� ��� ���������� ���������� ��������� ���������. 
    private Rigidbody rigidbody;

    //���������� ��� ��������
    public float rotationSpeed = 10f;

    public Transform groundCheckerTransform;

    public LayerMask notPlayerMask;

    private CapsuleCollider collider;
    public float jumpForce = 2f;
    // ���� ����� ���������� ������ ���� ��� ��� ������� �����.
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();//GetComponent<Animator>(): �������� ��������� Animator, ������������� � ���� �� �������� �������, ��� � ���� ������.
        rigidbody = GetComponent<Rigidbody>();// GetComponent<Rigidbody>(): �������� ��������� Rigidbody, ������������� � ���� �� �������� �������, ��� � ���� ������.


    }

    // ���� ����� ���������� ������ ���� ����.
    void Update()
    {

        float h = Input.GetAxis("Horizontal");// �������� �������� �������������� ��� ���������� 
        float v = Input.GetAxis("Vertical");//�������� �������� ������������ ��� ����������

        Vector3 directionVector = new Vector3(-h, 0, -v); //��������� ������, ������� ���������� ����������� �������� ���������.

        if (directionVector.magnitude > Mathf.Abs(0.05f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationSpeed); //�������� �� ������� ������� ��������� � ������� ��������.

        animator.SetFloat("speed", Vector3.ClampMagnitude(directionVector, 1).magnitude); //������������� �������� �speed� ��� ���������, ����� ������� ���������� �������� ��������.\

        Vector3 moveDir = Vector3.ClampMagnitude(directionVector, 1) * speed;

        rigidbody.velocity = new Vector3(moveDir.x, rigidbody.velocity.y, moveDir.z);//������������� �������� ��������� � ����������� ��������.

        rigidbody.angularVelocity = Vector3.zero;// ���������� ������� ��������, ����� ������������� ��������� ������� ���������.


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
