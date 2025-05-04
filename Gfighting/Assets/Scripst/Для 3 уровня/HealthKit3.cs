using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthKit3 : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;
    [SerializeField] private float pickupDelay = 3f; // Задержка в секундах
    [SerializeField] private float floatHeight = 0.2f; // Высота подъема
    [SerializeField] private float floatSpeed = 1f; // Скорость анимации
    private bool canBePickedUp = false;
    private Collider pickupCollider;
    private Vector3 startPosition;
    private void Start()
    {
        pickupCollider = GetComponent<Collider>();
        pickupCollider.enabled = false; // Отключаем коллайдер на старте
        startPosition = transform.position;
        StartCoroutine(EnablePickup());
    }


    void Update()
    {
        // Синусоидальное движение вверх-вниз
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.up, 50f * Time.deltaTime);
    }

    private IEnumerator EnablePickup()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color originalColor = renderer.material.color;

        // Мигание в течение задержки
        float elapsedTime = 0;
        while (elapsedTime < pickupDelay)
        {
            // Чередуем цвета
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            renderer.material.color = originalColor;
            yield return new WaitForSeconds(0.2f);

            elapsedTime += 0.4f;
        }

        // Финал: включаем коллайдер и устанавливаем цвет готовности
        canBePickedUp = true;
        pickupCollider.enabled = true;
        renderer.material.color = Color.green;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!canBePickedUp) return;

        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerManager3 player))
            {

                PlayerManager3.playerHealth = Mathf.Min(PlayerManager3.playerHealth + healAmount, 100f);


                player.Bar.fillAmount = PlayerManager3.playerHealth / 100f;

                // Если нужно обновить текстовое отображение
                if (player.playerHealthText != null)
                    player.playerHealthText.text = $"HP: {Mathf.CeilToInt(PlayerManager3.playerHealth)}/100";

                Destroy(gameObject);
            }
        }
    }
}