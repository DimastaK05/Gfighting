using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    private float timer;
    private NavMeshAgent agent;
    private Transform player;
    private Transform[] points;

    [SerializeField] private float chaseRange = 3;
    [SerializeField] private float patrolTime = 5;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ������������� �����������
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ��������� ����� ������� (������ ���� ��������� � ����������)
        var parent = GameObject.Find("Points");
        points = parent.GetComponentsInChildren<Transform>();

        // ��������� ����� �������
        agent.SetDestination(points[Random.Range(1, points.Length)].position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �������� ��������� �� ������
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
            return;
        }

        // ���������� ���� ��� ���������� �����
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(points[Random.Range(1, points.Length)].position);
        }

        // ������ ��� ����� ���������
        timer += Time.deltaTime;
        if (timer > patrolTime)
        {
            animator.SetBool("isPatrolling", false);
            timer = 0;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.ResetPath();
    }
}