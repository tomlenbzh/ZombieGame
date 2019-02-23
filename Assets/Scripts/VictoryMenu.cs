using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {

    public Transform    victoryMenu;
    public Transform    Player;

    // Use this for initialization
    public void Victory() {

        if (victoryMenu.gameObject.activeInHierarchy == false) {

            /* If the player has won, activate the VictoryMenu GameObject */
            victoryMenu.gameObject.SetActive(true);

            /* Set the time to 0 to freeze the game. */
            Time.timeScale = 0;

            /* Disactivate the Player to forbit him to rotate. */
            Player.GetComponent<PlayerController>().enabled = false;

            /* Stop the Zombies AudioSourceToPlay */
            FindObjectOfType<EnemyAIScript>().zombieIdleAudio.Pause();
        }
    }

    public void ContinueGame() {

        /* Load scene(0), the StartMenu to continue. */
        SceneManager.LoadScene(0);

        /* Re-activate the time */
        Time.timeScale = 1;
    }
}
