using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour {

    public int  resource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            /*When the crystal collides with the player : */
            Debug.Log("Le joueur ramasse des ressources.");

            /* Tell the GameManager to increase the number of collected crystals. */
            FindObjectOfType<GameManager>().AddResources(resource);
            Destroy(gameObject);
        }
    }
}
