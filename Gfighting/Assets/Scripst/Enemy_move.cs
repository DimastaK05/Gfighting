using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_move : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);
    }
}