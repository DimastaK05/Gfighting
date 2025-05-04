using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCntroller : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public DamageDealer3 damageDealer3;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private NavMeshAgent agent;

    // ������ �� ������ Enemy
    private Enemy3 enemy;


    void Start()
    {
        // �������� ��������� Enemy
        enemy = GetComponent<Enemy3>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        agent = GetComponent<NavMeshAgent>();
        // ��������� ��� �������� ��������
        agent.angularSpeed = 120f; // ���������, ���� �������� ������� ������
        agent.acceleration = 8f;
        agent.updateRotation = true; // ��������� ������ ��������� ���������
    }

    void Update()
    {
        // ���� ���� �������� ���� - ����� �����������
        if (enemy.IsTakingDamage) return;



        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange
            && Time.time - lastAttackTime >= attackCooldown
            && PlayerManager3.playerHealth >= 0)
        {
            animator.SetBool("isAttacking", true);
            damageDealer3.DealDamage3(player.gameObject);

            lastAttackTime = Time.time;
        }

        if (agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }
}
