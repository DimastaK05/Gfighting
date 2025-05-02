using System.Collections;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;
    [SerializeField] private float pickupDelay = 3f; // Задержка в секундах

    private bool canBePickedUp = false;
    private Collider pickupCollider;

    private void Start()
    {
        pickupCollider = GetComponent<Collider>();
        pickupCollider.enabled = false; // Отключаем коллайдер на старте

        StartCoroutine(EnablePickup());
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
            if (other.TryGetComponent(out PlayerManager player))
            {
                PlayerManager.playerHealth += healAmount;
                Destroy(gameObject);
            }
        }
    }
}