using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;


    Transform target;
    NavMeshAgent agent;

    float timePassed;

    bool alreadyAttacked;

    float attackCD = 3f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        alreadyAttacked = false;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //Attack the target
                faceTarget();
                //Face the target
                alreadyAttacked = false;
                attackPlayer();
            }
        }
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,Time.deltaTime * 5f);
    }

    private void attackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (alreadyAttacked == false && timePassed >= attackCD)
        {
            Debug.Log("Enemy Attacking!");
            animator.SetTrigger("Attack");
            timePassed = 0;
            alreadyAttacked = true;

            //Invoke(nameof(resetAttack), attackCooldown);
        }
        timePassed += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
