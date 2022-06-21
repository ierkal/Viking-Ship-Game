using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform enemyParent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Ship").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyParent = GameObject.Find("Enemies").transform;
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange&& !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange) 
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        // SETTING RANDOM V3 
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude<1f) // if ai path completed
        {
            walkPointSet = false;
        }
        LayerMask LM = LayerMask.GetMask("Obstacle"); // if ai sees the obstacle layer in front of

        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),15f,LM))
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint() // PATROL 
    {
        float RandomZ = Random.Range(-walkPointRange, walkPointRange);
        float RandomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);
        
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer()
    {
        // THERE IS NO ATTACKING TO PLAYER ATM.

        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
