using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy3 enemyCount;
    [SerializeField] private GameObject healthKitPrefab;
    [SerializeField][Range(0, 1)] private float dropChance = 0.5f; // 30% шанс
    private NavMeshAgent navAgent;
    // Флаг получения урона
    public bool IsTakingDamage { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        enemyCount = FindObjectOfType<CountEnemy3>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage3(int damage)
    {
        currentHealth -= damage;

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
  
        navAgent.enabled = false;
        if (healthKitPrefab != null && Random.value <= dropChance)
        {
            Vector3 spawnPosition = new Vector3(
        transform.position.x,
        transform.position.y + 1,
        transform.position.z
    );

            Instantiate(healthKitPrefab, spawnPosition, Quaternion.identity);
        }
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        enemyCount.EnemyDefeated();
    }
}
