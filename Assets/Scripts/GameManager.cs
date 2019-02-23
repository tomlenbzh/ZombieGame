using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))] /* Adds a required component : AudioSource as a dependency of GameObject. */
public class GameManager : MonoBehaviour {

    public AudioSource  goldPickUpAudio;
    public Transform    player;
    public Text         resourcesText;
    public int          currentResources;
    public int          maxResources;

    public void Start() {

        /* As soon as the game starts, we enable the player. */
        player.GetComponent<PlayerController>().enabled = true;

        /* And display a first time the number of crystals collected. */
        resourcesText.text = currentResources + "/" + maxResources;
    }

    public int RetCurrentResource() {

        /* Method returning the current number of crystals. */
        return currentResources;
    }

    public void AddResources(int resourcesToAdd) {

        /* Inrement the current number of crystals. */
        currentResources += resourcesToAdd;

        /* Then we re-display the number of crystals. */
        resourcesText.text = currentResources + "/" + maxResources;

        /* And play the crystals pick up sound. */
        goldPickUpAudio.Play();
    }
}