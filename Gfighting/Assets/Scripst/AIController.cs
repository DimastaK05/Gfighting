using UnityEngine.AI;
using UnityEngine;
public class AIController : MonoBehaviour
{
    public Animator animator;
    public Transform player; // ���� ��� �����
    public DamageDealer damageDealer; // ������ ��� ��������� �����
    public float attackRange = 2f; // ������ �����
    public float attackCooldown = 1f; // ����� ����������� �����
    private float lastAttackTime; // ����� ��������� �����

    void Update()
    {
       
        // �������� ���������� �� ������
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetBool("isAttacking", true);
            // ������� ���� ������
            damageDealer.DealDamage(player.gameObject);
            lastAttackTime = Time.time;
        }
    }
}