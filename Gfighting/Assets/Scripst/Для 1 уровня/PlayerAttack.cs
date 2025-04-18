using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamge = 20;
    public int attackCrossDamage = 30;
    public float attackCrossRate = 3f;
    public float attackRate = 2f;
    public float attackAnimationDuration = 1f; // ���������: ������������ ��������
    public float crossAttackAnimationDuration = 1.667f; // ���������: ������������ �����-�����

    float nextAttackTime = 0f;
    public static bool isAttacking { get ; private set; }  // ���������: ���� �����
    public static bool isCrossAttacking { get; private set; } // ���������: ���� �����-�����
    public LayerMask enemyLayers;

    [SerializeField] private float speedRecoveryTime = 0.5f; // ����� �������������� ��������
    private Coroutine speedRecoveryCoroutine;


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
        PlayerController.speed = 0f;
        // ���� ���������� ��������
        yield return new WaitForSeconds(attackAnimationDuration);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamge);
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
        PlayerController.speed = 0f;
        // ���� ���������� ��������
        yield return new WaitForSeconds(crossAttackAnimationDuration);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackCrossDamage);
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
        float targetSpeed = 5f; // ��� ������ ���������� [SerializeField]

        while (Time.time < startTime + speedRecoveryTime)
        {
            float t = (Time.time - startTime) / speedRecoveryTime;
            PlayerController.speed = Mathf.Lerp(startSpeed, targetSpeed, t);
            yield return null;
        }

        PlayerController.speed = targetSpeed;
    }
}