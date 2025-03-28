using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float attackRange = 1;
    public DamageDealer damageDealer;
    private float lastAttackTime;
    public float attackCooldown = 1f;
    //   float chaseRange = 10;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 4;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var pos = player.position;
        pos.y = agent.transform.position.y;
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);


        if (distance > 5)
            animator.SetBool("isChasing", false);

        if (distance < attackRange)
            animator.SetBool("isAttacking", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 2;
    }

}
