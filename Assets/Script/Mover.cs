using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    private Transform targetSprite;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void HandleTargetPointSprite(Vector3 destination)
    {
        targetSprite.position = new Vector3(destination.x, destination.y + 0.003f, destination.z);
        targetSprite.gameObject.SetActive(new Vector3(transform.position.x, 0, transform.position.z) != destination);
    }

}
