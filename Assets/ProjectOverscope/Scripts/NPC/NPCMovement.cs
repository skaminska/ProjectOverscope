using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : NPCStats
{
    Transform destination;
    NavMeshAgent agent;

    private void Awake()
    {
        //state = NPCState.WALK;
        agent = GetComponent<NavMeshAgent>();
    }
    void LateUpdate()
    {
        transform.LookAt(destination.position);
        if(Vector3.Distance(transform.position, destination.position) < 1.0f)
        {
            BasicNPCController.Instance.AddToList(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public override void GetHitAddictionalBehaviour()
    {
        base.GetHitAddictionalBehaviour();
        BasicNPCController.Instance.AddToList(this.gameObject);
        gameObject.SetActive(false);
    }

    public void SetDestination(Transform newDestination)
    {
        destination = newDestination;
        agent.destination = destination.position;
    }
}
