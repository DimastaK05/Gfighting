using UnityEngine.AI;
using UnityEngine;
public class AIController : MonoBehaviour
{
    public Animator animator;
    public Transform player; // Цель для атаки
    public DamageDealer damageDealer; // Скрипт для нанесения урона
    public float attackRange = 2f; // Радиус атаки
    public float attackCooldown = 1f; // Время перезарядки атаки
    private float lastAttackTime; // Время последней атаки

    void Update()
    {
       
        // Проверка расстояния до игрока
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetBool("isAttacking", true);
            // Нанести урон игроку
            damageDealer.DealDamage(player.gameObject);
            lastAttackTime = Time.time;
        }
    }
}