using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy enemyCount;
    [SerializeField] private GameObject healthKitPrefab;
    [SerializeField][Range(0, 1)] private float dropChance = 0.5f; // 30% шанс

    // Флаг получения урона
    public bool IsTakingDamage { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        enemyCount = FindObjectOfType<CountEnemy>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

       
        animator.ResetTrigger("Hurt"); // Сброс триггеров
        animator.ResetTrigger("back");

        StartCoroutine(HandleDamage());

        if (currentHealth >= 0)
        {
            // Запускаем процесс получения урона
            StartCoroutine(HandleDamage());
        }
        else
        {
            Die();
        }
    }

    // Корутина для обработки урона
    private IEnumerator HandleDamage()
    {
        IsTakingDamage = true;
        animator.SetTrigger("Hurt");
        animator.SetTrigger("back");

        // Ждем длительность анимации
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        IsTakingDamage = false;
    }

    void Die()
    {
        IsTakingDamage = true; // Блокируем атаку при смерти
        animator.SetTrigger("Die");
        if (healthKitPrefab != null && Random.value <= dropChance)
        {
            Instantiate(healthKitPrefab, transform.position, Quaternion.identity);
        }


        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        enemyCount.EnemyDefeated();
    }
}