using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Patrol = 0,
        Seek = 1
    }

    public NavMeshAgent agent;
    public State currentState = State.Patrol;
    public Transform target;
    public float seekRadius = 5f;
    public Transform waypointParent;
    
    // Creates a collection of Transforms
    private Transform currentPoint;
    private Transform[] waypoints;
    private int currentIndex = 1;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seekRadius);

        switch (currentState)
        {
            case State.Patrol:
                if (currentPoint)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(currentPoint.position, agent.stoppingDistance);
                }
                break;
            case State.Seek:
                if(target)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(target.position, agent.stoppingDistance);
                }
                break;
            default:
                break;
        }
    }

    void Patrol()
    {
        currentPoint = waypoints[currentIndex];
        
        float distance = Vector3.Distance(transform.position, currentPoint.position);
        if(distance < agent.stoppingDistance)
        {
            // currentIndex = currentIndex + 1
            currentIndex++;
            if(currentIndex >= waypoints.Length)
            {
                currentIndex = 1;
            }
        }
        
        agent.SetDestination(currentPoint.position);
        
        float distToTarget = Vector3.Distance(transform.position, target.position);
        if(distToTarget < seekRadius)
        {
            currentState = State.Seek;
        }
    }
    
    void Seek()
    {
        agent.SetDestination(target.position);

        float distToTarget = Vector3.Distance(transform.position, target.position);
        if (distToTarget > seekRadius)
        {
            currentState = State.Patrol;
        }
    }

    // Use this for initialization
    void Start()
    {        
        // Getting children of waypointParent
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Switch current state
        switch (currentState)
        {
            case State.Patrol:
                // Patrol state
                Patrol();
                break;
            case State.Seek:
                // Seek state
                Seek();
                break;
            default:
                break;
        }
    }
}
