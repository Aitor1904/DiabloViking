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
    public event Action OnHit;

    [SerializeField]
    private Transform healthRock;

    private ScoreManager scoreManager;

    private void Start()
    {
        healthBar.fillAmount = health / maxHealth;
        scoreManager = FindObjectOfType<ScoreManager>();

    }
    public void ModifyHealth(float healthModified)
    {
        if (healthModified < 0)
        {
            OnHit?.Invoke();
        }

        health += healthModified;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            OnDie?.Invoke();

            if(gameObject.GetComponent<Player>())
            {
                SceneManager.LoadScene("JuegoFN");
            }

            if(gameObject.GetComponent<Enemy>())
            {
                if (scoreManager != null)
                {
                    scoreManager.AddScore(5);
                }

                if (healthRock != null)
                {
                    Instantiate(healthRock, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }


    }
}
