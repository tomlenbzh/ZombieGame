using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public void PlayGame() {

        /* Loading scene n°1. */
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {

        /* Quits the Application. */
        Debug.Log("Quit");
        Application.Quit();
    }

}
