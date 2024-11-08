using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public Image healthBar;

    public event Action OnDie;
    private void Start()
    {
        healthBar.fillAmount = health / maxHealth;
    }
    public void ModifyHealth(float healthModified)
    {
        health += healthModified;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            OnDie?.Invoke();

            if(gameObject.GetComponent<Player>())
            {

            }

            if(gameObject.GetComponent<Enemy>())
            {

            }
            SceneManager.LoadScene("SampleScene");
        }
    }
}
