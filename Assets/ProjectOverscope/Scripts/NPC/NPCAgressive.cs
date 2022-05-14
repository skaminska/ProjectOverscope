using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAgressive : NPCStats
{
    [SerializeField] Transform destination;
    //just for testing
    [SerializeField] Renderer infoColor;
    [SerializeField] Material def, alert, fight; 

    NavMeshAgent agent;
    [SerializeField] Vector3 startPosition;

    Animator animator;

    [SerializeField] bool playerInRange;

    [SerializeField] GameObject player;

    //TODO create location controller which will collect places that npc can stay
    //those places should have information if they already occupied
    //patrol points should be collected autommaticaly on Awake from location controller
    [SerializeField] List<Transform> patrolPositions;

    bool shoot;

    private void Awake()
    {
        playerInRange = false;
        startPosition = transform.position;
        state = NPCState.PATROL;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shoot = false;
        agent.SetDestination(patrolPositions[Random.Range(0, patrolPositions.Count)].position);
    }

    private void LateUpdate()
    {
        if (agent.velocity != Vector3.zero)
            animator.SetBool("WALK", true);
        else
            animator.SetBool("WALK", false);



        switch (state)
        {
            case NPCState.STAND:
                break;
            case NPCState.PATROL:
                if(agent.destination != null && Vector3.Distance(agent.destination, transform.position) < 1.0f)
                {
                    agent.isStopped = true;
                    state = NPCState.STAND;
                    StartCoroutine(LookForPlayer());
                }
                break;
            case NPCState.SEE_PLAYER:
                agent.isStopped = true;
                if (player != null)
                    transform.LookAt(player.transform.position);
                break;
            case NPCState.FIGHT:
                if (!shoot && Vector3.Distance(transform.position, player.transform.position) <= 10.0f)
                {
                    agent.isStopped = true;
                    shoot = true;
                    transform.LookAt(player.transform.position);
                    StartCoroutine(Shoot());
                }
                else if(Vector3.Distance(transform.position, player.transform.position) > 10.0f)
                {
                    agent.isStopped = false;
                    agent.SetDestination(player.transform.position);
                }
                break;
            case NPCState.SEARCH_FOR_PLAYER:
                if (Vector3.Distance(agent.destination, transform.position) < 1.0f)
                {
                    StartCoroutine(LookForPlayer());
                    state = NPCState.STAND;
                }
                break;
            case NPCState.GO_BACK_TO_START_POSITION:
                break;

        }


        //if (playerInRange && state == NPCState.FIGHT)
        //{
        //    transform.LookAt(player.transform);
        //    agent.SetDestination(player.transform.position);
        //}

        //if(state == NPCState.FIGHT && Vector3.Distance(agent.destination, transform.position) < 1.0f && !playerInRange)
        //{
        //    state = NPCState.LOSTPLAYER;
        //    StartCoroutine(LookForPlayer());
        //}
        //if(state == NPCState.BACKTOPOSITION && Vector3.Distance(agent.destination, transform.position) < 1.0f)
        //{
        //    agent.isStopped = true;
        //    state = NPCState.STAND;
        //}

        //if(player!= null)
        //{
        //    Debug.Log(player.transform.position);
        //}
    }

    IEnumerator LookForPlayer()
    {
        yield return new WaitForSeconds(5);
        if (state == NPCState.STAND)
        {
            infoColor.material = def;
            agent.isStopped = false;
            agent.SetDestination(patrolPositions[Random.Range(0, patrolPositions.Count)].position);
            state = NPCState.PATROL;
        }
    }

    IEnumerator Shoot()
    {
        player.GetComponent<PlayerStats>().ChangeCurrentStatValue(StatType.HEALTH, -5);
        yield return new WaitForSeconds(1);
        shoot = false;
    }

    public void PlayerSeen(GameObject player)
    {
        if (state != NPCState.FIGHT)
        {
            this.player = player;
            StartCoroutine(PlayerInRangeTimer());
        }
        playerInRange = true;
    }

    public void PlayerLost()
    {
        destination = player.transform;
        playerInRange = false;
    }


    IEnumerator PlayerInRangeTimer()
    {
        infoColor.material = alert;
        state = NPCState.SEE_PLAYER;
        yield return new WaitForSeconds(3);
        if (playerInRange)
        {
            infoColor.material = fight;
            this.state = NPCState.FIGHT;
            Collider[] objects = Physics.OverlapSphere(transform.position, 10f);
            foreach(var obj in objects)
            {
                if (obj.GetComponent<NPCAgressive>() != null && obj.GetComponent<NPCAgressive>().state != NPCState.FIGHT)
                {
                    obj.GetComponent<NPCAgressive>().GivePlayer(player);
                }
            }
        }
        else
        {
            player = null;
            state = NPCState.SEARCH_FOR_PLAYER;
            agent.isStopped = false;
            agent.SetDestination(destination.position);
            transform.LookAt(destination.position);
        }
    }

    public void GivePlayer(GameObject player)
    {
        agent.isStopped = true;
        this.player = player;
        state = NPCState.FIGHT;

        infoColor.material = fight;
    }
}
