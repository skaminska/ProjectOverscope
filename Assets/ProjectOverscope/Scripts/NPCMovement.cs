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
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {

        transform.LookAt(destination.position);
        //transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, destination.position) < 1.0f)
        {
            //agent.enabled = false;
            BasicNPCController.Instance.AddToList(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public void SetDestination(Transform newDestination)
    {
        destination = newDestination;
        agent.destination = destination.position;
    }
}
