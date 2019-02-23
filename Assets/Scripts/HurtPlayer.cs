using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    private int damageToGive = 1;

    private void OnTriggerEnter(Collider other) {

        /* When the GameObject collides with the Player : */
        if (other.gameObject.tag == "Player") {

            Debug.Log("Le joueur perd des HP.");
            /* Call the HurtPlayer method of the HealthManager. */
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive);
        }
    }
}
