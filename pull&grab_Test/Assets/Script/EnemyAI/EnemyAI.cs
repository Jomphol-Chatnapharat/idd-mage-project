using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform playerLoc;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttack;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool canSeePlayer, canAttackPlayer;

    public float attackDmg;

    public bool melee, range, suicide;
    public GameObject shooter;

    private void Awake()
    {
        playerLoc = GameObject.Find("Player1").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        canAttackPlayer = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!canSeePlayer && !canAttackPlayer) Patroling();
        if (canSeePlayer && !canAttackPlayer) Chasing();
        if (canSeePlayer && canAttackPlayer) Attacking();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distantToWalkPoint = transform.position - walkPoint;

        if (distantToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }


    private void Chasing()
    {
        agent.SetDestination(playerLoc.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(playerLoc);

        if (!alreadyAttacked)
        {
            if (melee) MeleeAttack();
            if (range) RangeAttack();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    void MeleeAttack()
    {
        GameObject player = GameObject.Find("Player1");

        player.GetComponent<PlayerBehavior>().currentHP -= attackDmg;
    }

    void RangeAttack()
    {
        shooter.GetComponent<Projectile>().Shoot();
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
