using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Transform    deathMenu;
    public Transform    Player;

    // Use this for initialization
    public void Death() {

        if (deathMenu.gameObject.activeInHierarchy == false) {

            /* If the Death Menu is not active, we enable it. */
            deathMenu.gameObject.SetActive(true);

            /* Then we freeze time. */
            Time.timeScale = 0;

            /* And we disable the player. */
            Player.GetComponent<PlayerController>().enabled = false;
        }
    }

    public void ContinueGame() {

        /* If the Continue Button is triggered, we load the scene 0 (Start Menu). */
        SceneManager.LoadScene(0);

        /* And we don't forget to reactive Time. */
        Time.timeScale = 1;
    }
}
