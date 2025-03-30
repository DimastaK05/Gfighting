using System.Collections;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy2 enemyCount;


    // Флаг получения урона
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
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        enemyCount.EnemyDefeated();
    }
}