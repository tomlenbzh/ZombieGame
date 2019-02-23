using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour {

    private int hpToRestore = 1;
    private int checkHealth;

    private void OnTriggerEnter(Collider other) {

        /* When the GameObject collides with the Player : */
        if (other.gameObject.tag == "Player") {

            /* If the player doesn't have all his health : */
            if (FindObjectOfType<HealthManager>().GetCurrentHealth() < FindObjectOfType<HealthManager>().GetMaxHealth()) {

                Debug.Log("Le joueur se soigne.");

                /* We destroy the GameObject */
                Destroy(gameObject);

                /* And we indicate to the HealthManager to restore a certain amount of HP to the Player. */
                FindObjectOfType<HealthManager>().HealPlayer(hpToRestore);
            }
        }
    }
}
