using System.Collections;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamge = 20;
    public int attackCrossDamage = 30;
    public float attackCrossRate = 3f;
    public float attackRate = 2f;
    public float attackAnimationDuration = 0.8f; // Добавлено: длительность анимации
    public float crossAttackAnimationDuration = 1f; // Добавлено: длительность кросс-атаки

    float nextAttackTime = 0f;
    public static bool isAttacking { get; private set; }  // Добавлено: флаг атаки
    public static bool isCrossAttacking { get; private set; } // Добавлено: флаг кросс-атаки
    public LayerMask enemyLayers;

    [SerializeField] private float speedRecoveryTime = 0.5f; // Время восстановления скорости
    private Coroutine speedRecoveryCoroutine;

    public AudioSource moveSound;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isAttacking && !PlayerController.isBlock)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.R) && !isCrossAttacking && !PlayerController.isBlock)
            {
                StartCoroutine(AttackCross());
                nextAttackTime = Time.time + 1f / attackCrossRate;
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        moveSound.Play();
        PlayerController.speed = 0f;
      
        // Ждем завершения анимации
        yield return new WaitForSeconds(attackAnimationDuration);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy2>().TakeDamage2(attackDamge);
        }

        isAttacking = false;
        if (speedRecoveryCoroutine != null)
        {
            StopCoroutine(speedRecoveryCoroutine);
        }
        speedRecoveryCoroutine = StartCoroutine(RecoverSpeed());
    }

    IEnumerator AttackCross()
    {
        isCrossAttacking = true;
        animator.SetTrigger("Cross_Attack");
        moveSound.Play();
        PlayerController.speed = 0f;
        // Ждем завершения анимации
        yield return new WaitForSeconds(crossAttackAnimationDuration);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy2>().TakeDamage2(attackCrossDamage);
        }

        isCrossAttacking = false;
        if (speedRecoveryCoroutine != null)
        {
            StopCoroutine(speedRecoveryCoroutine);
        }
        speedRecoveryCoroutine = StartCoroutine(RecoverSpeed());
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator RecoverSpeed()
    {
        float startTime = Time.time;
        float startSpeed = 0f;
        float targetSpeed = 5f; // Или возьми переменную [SerializeField]

        while (Time.time < startTime + speedRecoveryTime)
        {
            float t = (Time.time - startTime) / speedRecoveryTime;
            PlayerController.speed = Mathf.Lerp(startSpeed, targetSpeed, t);
            yield return null;
        }

        PlayerController.speed = targetSpeed;
    }
}