using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePowerUp : MonoBehaviour
{
    public float powerUpAmount = 10f; // Amount to increase player's power-up stat
    public GameObject pickupEffect; // Particle effect to play when picked up

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply power-up effect to the player (e.g., increase health, speed, etc.)
            //PlayerMovement player = other.GetComponent<PlayerMovement>();
            // if (player != null)
            // {
            //     player.IncreasePowerUpStat(powerUpAmount);
            // }

            // // Play the pickup effect
            // Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // // Disable or destroy the power-up GameObject
            // gameObject.SetActive(false);
            // // or Destroy(gameObject);
        }
    }
}
