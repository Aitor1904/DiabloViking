using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

            int damage = gameObject.GetComponent<CharacterStats>().damage.GetValue();
            Player.instance.GetComponent<CharacterStats>().ModifyHealth(-damage);

            OnAttack?.Invoke();
        }
    }

    public void AttackPlayerToEnemy(CharacterStats enemyHealthManager)
    {
        if (attackCountdown <= 0f)
        {
            attackCountdown = 1 / attackRate;
            int damage = Player.instance.GetComponent<CharacterStats>().damage.GetValue();

            enemyHealthManager.ModifyHealth(-damage);
            OnAttack?.Invoke();
        }
    }
}
