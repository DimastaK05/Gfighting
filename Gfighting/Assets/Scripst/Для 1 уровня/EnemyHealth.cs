using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy enemyCount;
    [SerializeField] private GameObject healthKitPrefab;
    [SerializeField][Range(0, 1)] private float dropChance = 0.5f; // 30% ����

    // ���� ��������� �����
    public bool IsTakingDamage { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        enemyCount = FindObjectOfType<CountEnemy>();
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