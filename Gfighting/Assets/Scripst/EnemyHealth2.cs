using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy2 enemyCount;


    // ���� ��������� �����
    public bool IsTakingDamage { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        enemyCount = FindObjectOfType<CountEnemy2>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

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
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        enemyCount.EnemyDefeated();
    }
}