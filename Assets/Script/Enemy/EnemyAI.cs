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
    Animator animator;

    [SerializeField]
    LayerMask whatIsPlayer;
    [SerializeField]
    CharacterCombat characterCombat;


    [Header("Patrol")]
    public Transform centrePoint;
    Vector3 walkPoint;
    public float walkPointRange;
    bool isWalkPointSet;
    Vector3 distanceToWalkPoint;
    public Transform[] waypoints;
    int wayPointIndex;
    public float sightRange;
    public float attackRange;

    [Header("Drop")]
    public GameObject itemToDrop;
    public Transform whereToDrop;



    bool playerInSightRange, playerInAttackRange;
    bool isDead = false;
    public bool IsDead { get => isDead; set => isDead = value; }

    private void Update()
    {
        if (!IsDead)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling3();
            if (playerInSightRange && !playerInAttackRange) Chasing();
            if (playerInSightRange && playerInAttackRange) Attacking();
        }
    }

    #region Patrol FullRandom
    void Patrolling()
    {
        Debug.Log("Patrolling");

        if (!isWalkPointSet) SearchWalkPoint();

        if (isWalkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            isWalkPointSet = false;
        }
    }
    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);

        if (SetCorrectDestination(walkPoint))
        {
            isWalkPointSet = true;
        }
    }
    bool SetCorrectDestination(Vector3 targetDestination)
    {
        if (NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, .5f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            return true;
        }
        return false;
    }
    #endregion

    #region Patrol RandomAroundCenterPoint
    void Patrolling2()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, walkPointRange, out point))
            {
                agent.SetDestination(point);
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    #endregion

    #region FollowPoints
    void Patrolling3()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            IterateWaypoints();
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

    void IterateWaypoints()
    {
        wayPointIndex++;
        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }
    #endregion

    void Chasing()
    {
        agent.SetDestination(target.position);
        FaceTarget();
    }

    void Attacking()
    {
        agent.SetDestination(target.position);
        FaceTarget();
        characterCombat.AttackEnemyToPlayer();
        //CombatLogic
    }

    public void Dying()
    {
        isDead = true;
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        Instantiate(itemToDrop, whereToDrop.transform.position, Quaternion.identity);
    }
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
