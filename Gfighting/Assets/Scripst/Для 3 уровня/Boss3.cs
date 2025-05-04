using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss3 : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 500;
    int currentHealth;
    [SerializeField] private GameObject healthKitPrefab;
    [SerializeField][Range(0, 1)] private float dropChance = 0.5f;
    private NavMeshAgent navAgent;
    private CountEnemy3 enemyCount;

    public bool IsTakingDamage { get; private set; }

    void Start()
    {
        maxHealth = 500;
        currentHealth = maxHealth;
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void SetEnemyCountReference(CountEnemy3 counter)
    {
        enemyCount = counter;
    }

    public void TakeDamage3(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            StartCoroutine(HandleDamage());
        }
        else
        {
            Die();
        }
    }

    private IEnumerator HandleDamage()
    {
        IsTakingDamage = true;
        animator.SetTrigger("Hurt");
        animator.SetTrigger("back");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        IsTakingDamage = false;
    }

    void Die()
    {
        IsTakingDamage = true;
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
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);

        if (enemyCount != null)
        {
            enemyCount.EnemyDefeatedBoss();
        }
    }
}