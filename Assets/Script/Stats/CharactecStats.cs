using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactecStats : MonoBehaviour
{
    public Stat damage;
    public Stat armor;

    public int maxHelath = 100;

    public int currentHealth {  get; private set; }

    public Image healthBar;

    private void Awake()
    {
        currentHealth = maxHelath;
        healthBar.fillAmount = currentHealth / maxHelath;
    }

    public void Takedamage()
    {

    }
}
