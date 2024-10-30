using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health = 100;
    public float maxHealth;
    public Image healthBar;

    private void Start()
    {
        healthBar.fillAmount = health / maxHealth;
    }
    public void ModifyHealth(float healthModificed)
    {

        health = healthModificed;
        healthBar.fillAmount = health / maxHealth;
    }
}
