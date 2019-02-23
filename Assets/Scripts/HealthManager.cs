using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))] /* Adds a required component : AudioSource as a dependency of GameObject. */
public class HealthManager : MonoBehaviour {

    public Transform handGunImg;
    public Transform rifleImg;
    public Transform shotGunImg;

    public Sprite[] HeartSprites;
    public Image    HeartsUI;
    public Text     healthText;

    private int     maxHealth = 5;
    public int      currentHealth;

    // Use this for initialization
    void Start () {

        /* At the beginning, we set the Player's health to the maximum. */
        currentHealth = maxHealth;
    }

    public void Update() {
        
        /* Updating the Health's sprite between 5 and 0. */
        HeartsUI.sprite = HeartSprites[currentHealth];

        if (FindObjectOfType<HealthManager>().GetCurrentHealth() == 0) {

            /* If the Player's health reaches 0, then we call the Die method of the PlayerController. */
            FindObjectOfType<PlayerController>().Die();
        }
    }

    public int GetCurrentHealth() {

        /* Method to return the current health of the Player. */
        return currentHealth;
    }

    public int GetMaxHealth() {

        /* Method to return the maximum health the Player can have. */
        return maxHealth;
    }

    public void HurtPlayer(int damage) {

        /* Executes the HurtPlayer of the PlayerController. */
        FindObjectOfType<PlayerController>().PlayHurtPlayer();

        /* Decrease the Player's health. */
        currentHealth -= damage;

        if (currentHealth <= 0) {

            /* If the Player's health equals 0, it can't go any lower. */
            currentHealth = 0;
        } 
    }
    public void HealPlayer(int healAmount) {

        /* Executes the HealPlayer of the PlayerController. */
        FindObjectOfType<PlayerController>().PlayHealPlayer();

        /* Increase the Player's health. */
        currentHealth += healAmount;

        if (currentHealth > maxHealth) {

            /* If the Player's health is equal to his maximum health, it can't go any higher. */
            currentHealth = maxHealth;
        }
    }

    public void CurrentWeapon(int weaponID)
    {
        if (weaponID == 1) {

            /* If WeapoID equals 1, the handGun GUI is active and the others are not. */
            handGunImg.gameObject.SetActive(true);
            rifleImg.gameObject.SetActive(false);
            shotGunImg.gameObject.SetActive(false);
        }

        else if (weaponID == 2) {

            /* If WeapoID equals , the rifle GUI is active and the others are not. */
            handGunImg.gameObject.SetActive(false);
            rifleImg.gameObject.SetActive(true);
            shotGunImg.gameObject.SetActive(false);
        }

        else if (weaponID == 3) {

            /* If WeapoID equals 3, the shotGun GUI is active and the others are not. */
            handGunImg.gameObject.SetActive(false);
            rifleImg.gameObject.SetActive(false);
            shotGunImg.gameObject.SetActive(true);
        }
    }
}
