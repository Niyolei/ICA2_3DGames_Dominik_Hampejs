using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoevement : MonoBehaviour
{
    private Vector3 targetPosition;
    private NavMeshAgent agent;
    public LayerMask groundLayer;
    private Animator animator;
    
    private int velocityHash = Animator.StringToHash("Velocity");

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat(velocityHash, agent.velocity.magnitude/agent.speed);
        agent.SetDestination(targetPosition);
    }
    
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }
}
