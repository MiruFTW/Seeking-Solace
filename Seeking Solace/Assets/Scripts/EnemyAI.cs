using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    Animator animator;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    float timePassed;
    float attackCD = 1f;
    public float walkPointRange;

    //Attacking
    public float attackCooldown;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackingRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackingRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackingRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackingRange)
        {
            chasePlayer();
        }
        if (playerInSightRange && playerInAttackingRange)
        {
            attackPlayer();
        }
    }

    private void Patroling()
    {
        if (walkPointSet == false)
        {
            searchWalkPoint();
        }
        else if (walkPointSet == true)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void chasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void attackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (alreadyAttacked == false && timePassed >= attackCD)
        {
            Debug.Log("Enemy Attacking!");
            animator.SetTrigger("Attack");
            timePassed = 0;
            alreadyAttacked = true;

            Invoke(nameof(resetAttack), attackCooldown);
        }
        timePassed += Time.deltaTime;
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    private void searchWalkPoint()
    {
        // Calculate random point in range
        float z = Random.Range(-walkPointRange, walkPointRange);
        float x = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
