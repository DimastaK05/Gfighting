using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerTransform; //переменная типа Transform будет хранить ссылку на Transform персонажа, за которым камера должна следить.
    public Vector3 offset; //Эта переменная типа Vector3 определяет смещение камеры относительно персонажа.
    public float camPositionSpeed = 10f; //Эта переменная типа float определяет скорость плавного перемещения камеры к целевой позиции.
    // Start is called before the first frame update
    void Start()
    {

    }

    // Этот метод вызывается каждый фиксированный кадр, который обычно используется для физических обновлений.
    void FixedUpdate()
    {
        Vector3 newCamPosition = playerTransform.position + offset; //Вычисляется новая позиция камеры, добавляя offset к позиции персонажа.
        transform.position = Vector3.Lerp(transform.position, newCamPosition, camPositionSpeed * Time.deltaTime); // Эта строка плавно перемещает камеру к новой позиции с помощью линейной интерполяции

    }
}
