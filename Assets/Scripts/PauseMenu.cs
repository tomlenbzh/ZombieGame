using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Transform    pauseMenu;
    public Transform    Player;

    public void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            /* When ESCAPE is pressed */
            if (pauseMenu.gameObject.activeInHierarchy == false) {

                /* If the PauseMenu GameObject is not enables, we activate it. */
                pauseMenu.gameObject.SetActive(true);

                /* We desactivate the time. */
                Time.timeScale = 0;

                /* We disable the Player to forbid it to move during the pause. */
                Player.GetComponent<PlayerController>().enabled = false;
            }
            else {
                /* If the PauseMenu GameObject is activated, we disable it. */
                pauseMenu.gameObject.SetActive(false);

                /* We reactivate the time. */
                Time.timeScale = 1;

                /* And we enable the player; */
                Player.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    public void GetBack() {

        /* Button to get baack to the game : Exact same behavior as the upper else. */
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void QuitGame() {
    
        Debug.Log("Quit");

        /* Quits the Application; */
        Application.Quit();
    }

}
