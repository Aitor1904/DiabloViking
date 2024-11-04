using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAltar : MonoBehaviour
{
    [SerializeField]
    private float healingAmount = 5f;
    [SerializeField]
    private float healingInterval = 1f;

    private HealthManager playerHealthManager; 
    private bool isHealingActive = false;       

    [SerializeField]
    private ParticleSystem healingParticles;

    [SerializeField]
    private AudioSource healingSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter");
            playerHealthManager = other.GetComponent<HealthManager>();
            /*if (playerHealthManager != null && healingParticles != null)
            {
                healingParticles.Play(); 
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit");
            playerHealthManager = null; 
            /*if (healingParticles != null && healingParticles.isPlaying)
            {
                healingParticles.Stop(); 
            }*/
            StopHealing(); 
            Debug.Log("Stop Healing");
        }
    }

    private void Update()
    {
        if (playerHealthManager != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isHealingActive = !isHealingActive; 
                if (isHealingActive)
                {
                    Debug.Log("Start Healing");
                    StartHealing();
                }
                else
                {
                    StopHealing();
                    Debug.Log("Stop particles");
                }
            }
        }

        if (isHealingActive)
        {
            HealPlayer();
        }
    }

    private void StartHealing()
    {
        if (healingParticles != null)
        {
            healingParticles.Play(); 
        }

        if (healingParticles != null)
        {
            healingParticles.Play();
        }

        if (healingSound != null)
        {
            healingSound.Play();
        }
    }

    private void StopHealing()
    {
        if (healingParticles != null && healingParticles.isPlaying)
        {
            healingParticles.Stop();
        }

        if (healingSound != null && healingSound.isPlaying)
        {
            healingSound.Stop(); 
        }
    }

    private void HealPlayer()
    {
        if (playerHealthManager != null)
        {
            float newHealth = Mathf.Clamp(playerHealthManager.health + healingAmount * Time.deltaTime / healingInterval, 0, playerHealthManager.maxHealth);
            playerHealthManager.ModifyHealth(newHealth);
        }
    }
}
