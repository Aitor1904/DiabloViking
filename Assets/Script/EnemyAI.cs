using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Transform target;
    [SerializeField]
    LayerMask whatIsPlayer;

    Vector3  walkPoint;
    public float walkPointRange;
    bool isWalkPointSet;
    Vector3 distanceToWalkPoint;

    public float sightRange;
    public float attackRange;


    bool playerIsSightRange, playerInAttackRange;

    private void Update()
    {
        playerIsSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerIsSightRange && !playerInAttackRange) Patrolling();
        if (!playerIsSightRange && !playerInAttackRange) Chasing();
        if (!playerIsSightRange && !playerInAttackRange) Attacking();
    }

    private void Attacking()
    {
    }

    private void Chasing()
    {

    }

    private void Patrolling()
    {
        if (!isWalkPointSet) SearchWalkPoint();
        if (isWalkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 1.5f)
        {
            isWalkPointSet = false;
        }
    }

    bool SetCorrectorDestination(Vector3 targetDestination)
    {
        if(NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, .5f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            return true;
        }
        return false;
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (SetCorrectorDestination(walkPoint))
        {
            isWalkPointSet = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
