using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;

    private void Update()
    {
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }
}
