using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] /* Adds a required component : AudioSource as a dependency of GameObject. */
public class AmmoPickUp : MonoBehaviour {

    public AudioSource  AmmoSound;
    public int          AmmoValue;
    public int          WeaponID;

    /* When the player collides with a trigger : */
    private void OnTriggerEnter(Collider other) {

        /* If that trigger is a HandGunBullet : */
        if (other.gameObject.tag == "HandGunBullet") {

            /* If the current ammo  value is lower than the max ammo value : Update the variables. */
            if (FindObjectOfType<Weapon>().handGunMagazine < FindObjectOfType<Weapon>().maxHandGunAmmo) {

                AmmoValue = 10;
                WeaponID = 1;

                /* Call the SupplyMagazine method to reload. */
                FindObjectOfType<Weapon>().SupplyMagazine(AmmoValue, WeaponID);

                /* Plays the AudioSource */
                AmmoSound.Play();

                /* Destroy the GameObject with which we collided. */
                Destroy(other.gameObject);
            }
            else
                return;
        }

        /* If that trigger is a RifleBullet : */
        if (other.gameObject.tag == "RifleBullet") {

            /* If the current ammo  value is lower than the max ammo value : Update the variables. */
            if (FindObjectOfType<Weapon>().rifleMagazine < FindObjectOfType<Weapon>().maxRifleAmmo) {
                
                AmmoValue = 20;
                WeaponID = 2;

                /* Call the SupplyMagazine method to reload. */
                FindObjectOfType<Weapon>().SupplyMagazine(AmmoValue, WeaponID);

                /* Plays the AudioSource */
                AmmoSound.Play();

                /* Destroy the GameObject with which we collided. */
                Destroy(other.gameObject);
            }
            else
                return;
        }

        /* If that trigger is a ShotGunBullet : */
        if (other.gameObject.tag == "ShotGunBullet") {

            /* If the current ammo  value is lower than the max ammo value : Update the variables. */
            if (FindObjectOfType<Weapon>().shotGunMagazine < FindObjectOfType<Weapon>().maxShotGunAmmo) {

                AmmoValue = 6;
                WeaponID = 3;

                /* Call the SupplyMagazine method to reload. */
                FindObjectOfType<Weapon>().SupplyMagazine(AmmoValue, WeaponID);

                /* Plays the AudioSource */
                AmmoSound.Play();

                /* Destroy the GameObject with which we collided. */
                Destroy(other.gameObject);
            }
            else
                return;
        }
    }
}
