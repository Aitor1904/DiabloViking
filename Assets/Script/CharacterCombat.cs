using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField]
    float attackRate;

    float attackCountdown = 0f;

    public event Action OnAttack;

    private void Update()
    {
        attackCountdown -= Time.deltaTime;
    }

    public void Attack()
    {
        if(attackCountdown <= 0f)
        {
            attackCountdown = 1 / attackRate;

            OnAttack?.Invoke();
        }
    }
}
