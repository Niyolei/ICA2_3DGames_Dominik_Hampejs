using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoevement : MonoBehaviour
{
    private Vector3 targetPosition;
    private NavMeshAgent agent;
    public LayerMask groundLayer;
    private Animator animator;
    public EmptyGameEvent playerMoved;
    
    private bool waitForArival = false;
    
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
        
        if (waitForArival && agent.remainingDistance < 0.001f)
        {
            waitForArival = false;
            playerMoved.Raise(new Empty());
        }
    }
    
    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        agent.SetDestination(targetPosition);
    }
    
    public void SetForFight(Vector3 position)
    {
        targetPosition = position;
        
        agent.SetDestination(targetPosition);
        waitForArival = true;
    }
}
