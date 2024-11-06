using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public Image healthBar;

    public event Action OnDie;

    [SerializeField]
    private EnemyAI enemyAI;

    private void Start()
    {
        healthBar.fillAmount = health / maxHealth;
    }
    public void ModifyHealth(float healthModified)
    {
        health += healthModified;
        healthBar.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            enemyAI.IsDead = true;
            OnDie?.Invoke();
        }
    }
}
