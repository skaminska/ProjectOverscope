using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class NPCAgressive : NPCStats
{
    [SerializeField] Transform destination;
    //just for testing
    [SerializeField] Renderer infoColor;
    [SerializeField] Slider healthBar;
    [SerializeField] Material def, alert, fight;
    bool taskNotSet;
    [SerializeField] GameObject weapon;

    NavMeshAgent agent;
    [SerializeField] Vector3 startPosition;
    Location location;
    PatrolPoint patrolPoint;

    Animator animator;

    [SerializeField] bool playerInRange;
    [SerializeField] GameObject player;

    //TODO create location controller which will collect places that npc can stay
    //those places should have information if they already occupied
    //patrol points should be collected autommaticaly on Awake from location controller
    //[SerializeField] List<Transform> patrolPositions;

    bool shoot;

    private void Awake()
    {
        weapon.SetActive(false);
        location = GetComponentInParent<Location>();
        healthBar.maxValue = healthPoint;
        healthBar.value = healthPoint;
        healthBar.gameObject.SetActive(false);
        playerInRange = false;
        startPosition = transform.position;
        state = NPCState.PATROL;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shoot = false;
        taskNotSet = true;
        FindNewDestination();
        //agent.SetDestination(patrolPositions[Random.Range(0, patrolPositions.Count)].position);
    }

    private void LateUpdate()
    {
        SetAnmations();

        if (playerInRange)
        {
            transform.LookAt(player.transform.position);
        }

        if (taskNotSet)
        {
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
                        taskNotSet = false;
                    }
                    break;
                case NPCState.SEE_PLAYER:
                    agent.isStopped = true;
                    animator.SetBool("lookForPlayer", false);
                    if (player != null)
                        transform.LookAt(player.transform.position);
                    break;
                case NPCState.FIGHT:
                    if (!weapon.activeInHierarchy)
                    {
                        weapon.SetActive(true);
                        
                    }
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
        }

    }

    void SetAnmations()
    {
        if (agent.velocity != Vector3.zero)
            animator.SetBool("WALK", true);
        else
            animator.SetBool("WALK", false);

        if (state == NPCState.FIGHT)
            animator.SetLayerWeight(1, 1);
        else
            animator.SetLayerWeight(1, 0);
    }

    void FindNewDestination()
    {
        if (patrolPoint != null)
        {
            Debug.Log("free position: " + patrolPoint.name);
            patrolPoint.SetOccupiedState(false);
        }

        Debug.Log("looking for new point");
        patrolPoint = location.GetUnoccupiedPatrolPosition();

        Debug.Log("i get position: " + patrolPoint.name);
        patrolPoint.SetOccupiedState(true);
        agent.SetDestination(patrolPoint.GetPatrolPointPosition());
        agent.isStopped = false;
    }

    IEnumerator LookForPlayer()
    {
        animator.SetBool("lookForPlayer", true);
        yield return new WaitForSeconds(15);
        if (state == NPCState.STAND)
        {
            infoColor.material = def;
            FindNewDestination();

            state = NPCState.PATROL;
        }
        taskNotSet = true;
        animator.SetBool("lookForPlayer", false);
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
            animator.SetBool("lookForPlayer",false);
            state = NPCState.SEARCH_FOR_PLAYER;
            agent.isStopped = false;
            agent.SetDestination(destination.position);
            transform.LookAt(destination.position);
        }

        taskNotSet = true;
    }

    public void GivePlayer(GameObject player)
    {
        agent.isStopped = true;
        this.player = player;
        state = NPCState.FIGHT;

        infoColor.material = fight;
        animator.SetBool("lookForPlayer", false);
        taskNotSet = true;
    }

    public override void GetHit(int hit)
    {
        if (!healthBar.gameObject.activeInHierarchy)
            healthBar.gameObject.SetActive(true);
        base.GetHit(hit);
        healthBar.value = healthPoint;
        
    }

    public override void GetHitAddictionalBehaviour()
    {
        base.GetHitAddictionalBehaviour();
        GetComponentInParent<Location>().CheckIfLocationComplete();
        gameObject.SetActive(false);
    }

    //maybe there should be interface for things like this 
    public void ShowHealthBar()
    {
        if (!healthBar.gameObject.activeInHierarchy)
        {
            healthBar.gameObject.SetActive(true);
        }
    }
}
