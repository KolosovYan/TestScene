using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Policeman_Controller : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints;
    private Animator animator;
    private NavMeshAgent agent;
    [SerializeField] private int counter = 0;
    private Transform cashedTransform;

    private void Start()
    {
        cashedTransform = transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        MoveToWaypoint();
        StartCoroutine(CheckMovementStatus());
    }

    private Vector3 currentWaypointPosition;

    IEnumerator CheckMovementStatus()
    {
        while (counter != waypoints.Count)
        {
            yield return new WaitUntil(() => Vector3.Distance(cashedTransform.position, currentWaypointPosition) <= 1);
            counter += 1;
            if (counter < waypoints.Count)
                MoveToWaypoint();
            else
                StopMovenemt();
        }
    }

    private void MoveToWaypoint()
    {
        currentWaypointPosition = waypoints[counter].transform.position;
        agent.SetDestination(currentWaypointPosition);
        SetMovementType();
    }

    private void StopMovenemt()
    {
        animator.Play("Idle");
    }

    [SerializeField] private float runningSpeed = 5;
    [SerializeField] private float walkingSpeed = 3.5f;

    private void SetMovementType()
    {
        animator.Play(waypoints[counter].CurrentWaypointType.ToString());
        if (waypoints[counter].CurrentWaypointType.ToString() == "Running")
            agent.speed = runningSpeed;
        else
            agent.speed = walkingSpeed;
    }
}
