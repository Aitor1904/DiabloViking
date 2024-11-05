using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAltar : MonoBehaviour
{
    [SerializeField]
    private float healingAmount = 5f;
    [SerializeField]
    private float healingInterval = 1f;
    [SerializeField]
    private float healingDuration = 10f;

    private HealthManager playerHealthManager; 
    private bool isHealingActive = false;
    private float healingTimer;
    private bool playerInRange = false;
    private Inventory playerInventory;

    [SerializeField]
    private ParticleSystem healingParticles;
    [SerializeField]
    private AudioSource healingSound;

    [SerializeField]
    private Item healthPotion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealthManager = other.GetComponent<HealthManager>();
            playerInventory = other.GetComponent<Inventory>(); // Aquí se asigna el inventario

            if (playerHealthManager != null && playerInventory != null)
            {
                playerInRange = true;
                Debug.Log("Player in range");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            StopHealing();
            Debug.Log("Player left healing range");
        }
    }

    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            if (playerInventory != null && HasHealthPotion())
            {
                isHealingActive = !isHealingActive;

                if (isHealingActive)
                {
                    StartHealing();
                    playerInventory.Remove(healthPotion); // Consumir la poción
                }
                else
                {
                    StopHealing();
                }
            }
            else
            {
                Debug.Log("No healthPtion in Iventory");
            }
        }

        if (isHealingActive)
        {
            healingTimer -= Time.deltaTime;
            HealPlayer();

            if (healingTimer <= 0)
            {
                StopHealing();
            }
        }
    }

    private void StartHealing()
    {
        if (healingParticles != null)
        {
            healingParticles.Play();
        }

        if (healingSound != null)
        {
            healingSound.Play();
        }

        healingTimer = healingDuration;
        Debug.Log("Stat healing " + healingDuration + " seconds");
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

        isHealingActive = false;
        Debug.Log("Healing stoped");
    }

    private void HealPlayer()
    {
        if (playerHealthManager != null)
        {
            float newHealth = Mathf.Clamp(playerHealthManager.health + healingAmount * Time.deltaTime / healingInterval, 0, playerHealthManager.maxHealth);
            playerHealthManager.ModifyHealth(newHealth);
        }
    }
    private bool HasHealthPotion()
    {
        if (playerInventory == null)
        {
            return false;
        }

        foreach (Item item in playerInventory.items)
        {
            if (item == healthPotion)
            {
                return true;
            }
        }
        return false;
    }
}
