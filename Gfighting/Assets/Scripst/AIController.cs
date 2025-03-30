using UnityEngine.AI;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public DamageDealer damageDealer;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private NavMeshAgent agent;

    // Ссылка на скрипт Enemy
    private Enemy enemy;

    void Start()
    {
        // Получаем компонент Enemy
        enemy = GetComponent<Enemy>();

        agent = GetComponent<NavMeshAgent>();
        // Настройки для плавного поворота
        agent.angularSpeed = 120f; // Уменьшите, если вращение слишком резкое
        agent.acceleration = 8f;
        agent.updateRotation = true; // Разрешить агенту управлять поворотом
    }

    void Update()
    {
        // Если враг получает урон - атака блокируется
        if (enemy.IsTakingDamage) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange
            && Time.time - lastAttackTime >= attackCooldown
            && PlayerManager.playerHealth >= 0)
        {
            animator.SetBool("isAttacking", true);
            damageDealer.DealDamage(player.gameObject);
            lastAttackTime = Time.time;
        }

        if (agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }
}