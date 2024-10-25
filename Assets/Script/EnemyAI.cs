using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
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
    [SerializeField]
    CharacterCombat characterCombat;
    [Header("Patrol")]
    public Transform centrePoint;

    Vector3  walkPoint;
    public float walkPointRange;
    bool isWalkPointSet;
    Vector3 distanceToWalkPoint;

    public Transform[] waypoints;
    int wayPointIndex;

    public float sightRange;
    public float attackRange;


    bool playerIsSightRange, playerInAttackRange;

    private void Update()
    {
        playerIsSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerIsSightRange && !playerInAttackRange) Patrolling3();
        if (playerIsSightRange && !playerInAttackRange) Chasing();
        if (playerIsSightRange && playerInAttackRange) Attacking();
    }

    private void Attacking()
    {
        Debug.Log("Attck");
        agent.SetDestination(target.position);
        FaceTarget();
        characterCombat.Attack();
    }

    private void Chasing()
    {
        Debug.Log("Chas");
        agent.SetDestination(target.position);
        FaceTarget();
    }
    #region Patrol RANDOM
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
    #endregion
    #region Patrol RandomAroundCenterPoint

    void Patrolling2()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if(RandomPoint(centrePoint.position, walkPointRange,out point))
            {
                agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas) )
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    #endregion
    #region RandomAroundCenterPoint
    void Patrolling3()
    {
        Debug.Log("Patrol3");
        if (agent.remainingDistance <= agent.stoppingDistance) 
        {
            IterateWaypoint();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        Vector3 newPosition = waypoints[wayPointIndex].position;
        var targetRotation = Quaternion.LookRotation(newPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .2f * Time.deltaTime);
        agent.SetDestination(newPosition);
    }

    void IterateWaypoint()
    {
        wayPointIndex++;
        if(wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }

    #endregion

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .2f * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
