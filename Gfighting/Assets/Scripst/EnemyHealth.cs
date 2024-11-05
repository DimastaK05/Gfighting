using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем текущее здоровье на полученный урон

        // Проверяем, если здоровье меньше или равно нулю
        if (currentHealth > 0)
        {
            // Запускаем анимацию получения урона
            animator.SetTrigger("hurt"); // Предполагается, что в Animator есть триггер "Hit"
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
    }

}
