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
    [SerializeField]
    CharacterCombat characterCombat;
    private void Start()
    {
        characterCombat.OnAttack += OnAttack;
    }
    private void Update()
    {
        animator.SetFloat("Velocity", agent.velocity.magnitude);
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }
}
