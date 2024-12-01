using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private CountEnemy enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyCount = FindObjectOfType<CountEnemy>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем текущее здоровье на полученный урон

        // Проверяем, если здоровье меньше или равно нулю
        if (currentHealth > 0)
        {
            // Запускаем анимацию получения урона
            animator.SetTrigger("Hurt");
            animator.SetTrigger("back");
        }
        else
        {
            Die(); // Вызываем метод смерти
        }
    }

    void Die()
    {
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
