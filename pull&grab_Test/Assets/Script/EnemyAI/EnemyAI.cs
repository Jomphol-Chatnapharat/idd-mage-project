using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform playerLoc;

    public LayerMask whatIsGround, whatIsPlayer;

    public SimpleEnemy enemy;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttack;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool canSeePlayer, canAttackPlayer;

    public float attackDmg;

    public float fleeRange;

    public bool melee, range, suicide;
    public GameObject shooter;

    public float bombCount = 3f;
    public bool countStart = false;

    float distance;

    private void Awake()
    {
        playerLoc = GameObject.Find("Player1").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        canAttackPlayer = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        distance = Vector3.Distance(transform.position, playerLoc.position);

        if (suicide && alreadyAttacked)
        {
            countStart = true;
        }

        if(countStart == true)
        {
            bombCount -= 1 * Time.deltaTime;

            agent.enabled = false;
        }

        if (enemy.CurrentHp > 0 && bombCount <= 0)
        {
                Debug.Log("Boom");

                Collider[] characters = Physics.OverlapSphere(transform.position, attackRange);

                foreach (Collider character in characters)
                {
                    if (character.GetComponent<SimpleEnemy>() != null)
                    {
                        character.GetComponent<SimpleEnemy>().OnDamaged(50);
                    }

                    if (character.GetComponent<PlayerBehavior>() != null)
                    {
                        character.GetComponent<PlayerBehavior>().currentHP -= attackDmg;
                    }

                    Destroy(this.gameObject);
                }
        }

        if (!canSeePlayer && !canAttackPlayer) Patroling();
        if (canSeePlayer && !canAttackPlayer) Chasing();
        if (canSeePlayer && canAttackPlayer && distance > fleeRange) Attacking();
        if (canSeePlayer && canAttackPlayer && distance < fleeRange) Flee();
        if (canSeePlayer && enemy.CurrentHp <= enemy.MaxHp / 2) Flee();
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
            agent.speed = 0;
            Debug.Log("attacking");
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
        float distance = Vector3.Distance(transform.position, playerLoc.position);
        Debug.Log("shooting");

        if (distance > fleeRange)
        {
            shooter.GetComponent<Projectile>().Shoot();
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        agent.speed = 4;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void Flee()
    {
            Vector3 dirToPlayer = transform.position - playerLoc.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
    }
}
