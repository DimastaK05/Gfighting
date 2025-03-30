using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
 
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public int attackDamge = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public LayerMask enemyLayers;
    void Update()
    {
      
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Attack();
                nextAttackTime = Time.time+1f/attackRate;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
      
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamge);
          
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}