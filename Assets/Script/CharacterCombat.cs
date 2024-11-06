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

            GameManager.Instance.GetComponent<HealthManager>().ModifyHealth(-10);

            OnAttack?.Invoke();
        }
    }

    public void AttackPlayerToEnemy(EnemyHealthManager enemyHealthManager)
    {
        if (attackCountdown <= 0f)
        {
            attackCountdown = 1 / attackRate;
            enemyHealthManager.ModifyHealth(-25);
            OnAttack?.Invoke();
        }
    }
}
