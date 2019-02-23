using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMenu : MonoBehaviour
{
    public GameObject   storyMenu;
    public GameObject   playButton;
    public GameObject   quitButton;
    public GameObject   storyButton;

    public void Story() {

        if (storyMenu.gameObject.activeInHierarchy == false) {

            /* Disactivate the StartMeny GameObject components to see the StoryMenu. */
            storyMenu.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            storyButton.gameObject.SetActive(false);
        }
    }

    public void GetBack() {

        /* /* Disactivate the SoryMenu to get back to the StartMenu. */

        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        storyButton.gameObject.SetActive(true);
        storyMenu.gameObject.SetActive(false);
        
    }
}