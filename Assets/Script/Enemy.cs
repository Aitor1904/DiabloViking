using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    [SerializeField]
    private EnemyHealthManager enemyHealthManager;
    public override void Interact()
    {
        base.Interact();
        CharacterCombat combatManager = Player.instance.characterCombat;
        combatManager.AttackPlayerToEnemy(enemyHealthManager);
    }
}
