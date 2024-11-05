using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerTransform; //���������� ���� Transform ����� ������� ������ �� Transform ���������, �� ������� ������ ������ �������.
    public Vector3 offset; //��� ���������� ���� Vector3 ���������� �������� ������ ������������ ���������.
    public float camPositionSpeed = 10f; //��� ���������� ���� float ���������� �������� �������� ����������� ������ � ������� �������.
    // Start is called before the first frame update
    void Start()
    {

    }

    // ���� ����� ���������� ������ ������������� ����, ������� ������ ������������ ��� ���������� ����������.
    void FixedUpdate()
    {
        Vector3 newCamPosition = playerTransform.position + offset; //����������� ����� ������� ������, �������� offset � ������� ���������.
        transform.position = Vector3.Lerp(transform.position, newCamPosition, camPositionSpeed * Time.deltaTime); // ��� ������ ������ ���������� ������ � ����� ������� � ������� �������� ������������

    }
}
