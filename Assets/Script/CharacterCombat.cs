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

    public void AttackEnemyToPlayer()
    {
        if(attackCountdown <= 0f)
        {
            attackCountdown = 1 / attackRate;

            Player.instance.GetComponent<HealthManager>().ModifyHealth(-7);

            OnAttack?.Invoke();
        }
    }

    public void AttackPlayerToEnemy(HealthManager enemyHealthManager)
    {
        if (attackCountdown <= 0f)
        {
            attackCountdown = 1 / attackRate;
            enemyHealthManager.ModifyHealth(-35);
            OnAttack?.Invoke();
        }
    }
}
