using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float verticalOffset = 20f;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy enemyCount;
    [SerializeField] private GameObject healthKitPrefab;
    [SerializeField][Range(0, 1)] private float dropChance = 0.5f; // 30% ����
    private NavMeshAgent navAgent;
    // ���� ��������� �����
    public bool IsTakingDamage { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        enemyCount = FindObjectOfType<CountEnemy>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

       
        animator.ResetTrigger("Hurt"); // ����� ���������
        animator.ResetTrigger("back");

        StartCoroutine(HandleDamage());

        if (currentHealth >= 0)
        {
            // ��������� ������� ��������� �����
            StartCoroutine(HandleDamage());
        }
        else
        {
            Die();
        }
    }

    // �������� ��� ��������� �����
    private IEnumerator HandleDamage()
    {
        IsTakingDamage = true;
        animator.SetTrigger("Hurt");
        animator.SetTrigger("back");

        // ���� ������������ ��������
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        IsTakingDamage = false;
    }

    void Die()
    {
        IsTakingDamage = true; // ��������� ����� ��� ������
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