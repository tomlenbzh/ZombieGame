using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))] /* Adds a required component : AudioSource as a dependency of GameObject. */

public class Weapon : MonoBehaviour {
    
    public AudioSource      reloadGunSound;
    public AudioSource      handGunSound;
    public AudioSource      rifleSound;
    public AudioSource      shotGunSound;
    public AudioSource      dryFire;

    public Text             EnemiesCount;
    public Text             BulletCount;

    private LineRenderer    tracer;
    public Transform        spawn;
    

    private float roundsPerMinute;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    private int currenWeapon;

    private int maxEnemies = 65;
    private int remainingEnemies;

    public int handGunMagazine = 20;
    public int rifleMagazine = 60;
    public int shotGunMagazine = 24;

    public int maxHandGunAmmo = 20;
    public int maxRifleAmmo = 60;
    public int maxShotGunAmmo = 24;

    private int CurHandGunAmmo;
    private int CurRifleAmmo;
    private int CurShotGunAmmo;

    public float reloadTime = 1f;
    private bool isReloading = false;

    public enum GunType {HandGun, Rifle, ShotGun};
    public GunType gunType;

    public void SupplyMagazine(int AmmoValue, int WeaponID) {

        /* Regarding the weapon, the Magazine is incremented by the AmmoValue. */
        /* We check if the new amount of munition is not higher than the maximum allowed. */
        /* And then we re-display the current amount of munition. */

        if (WeaponID == 1) {

            handGunMagazine += AmmoValue;
            if (handGunMagazine > maxHandGunAmmo)
                handGunMagazine = maxHandGunAmmo;
            BulletCount.text = CurHandGunAmmo + "/" + handGunMagazine;
        }

        if (WeaponID == 2) {

            rifleMagazine += AmmoValue;
            if (rifleMagazine > maxRifleAmmo)
                rifleMagazine = maxRifleAmmo;
            BulletCount.text = CurRifleAmmo + "/" + rifleMagazine;
        }

        else if (WeaponID == 3) {

            shotGunMagazine += AmmoValue;
            if (shotGunMagazine > maxShotGunAmmo)
                shotGunMagazine = maxShotGunAmmo;
            BulletCount.text = CurShotGunAmmo + "/" + shotGunMagazine;
        }
    }

    void Update() {

        /* We continuousely check if the current weapon has bean changed. */
        /* Then we display what is the current weapon by reaching the CurrentWeapon() method in the HealthManager. */
        /* If the Player is not reloading, we set his animation with the animation that suits his current weapon. */
        /* If the player is reloading, we don't play any animation because it would loop during de reloading period. */
        /* Regarding the weapon, if the current ammunition value reaches 0, the player reloads automatically.*/
        /* The SwapWeapon() method resets the current weapon. */

        currenWeapon = RetCurrentWeapon();
        FindObjectOfType<HealthManager>().CurrentWeapon(currenWeapon);

        if (isReloading == false)
            FindObjectOfType<AnimateSprite>().Animate(false, currenWeapon);

        if (isReloading == true)
            return;

        if ((gunType == GunType.HandGun && CurHandGunAmmo <= 0 && handGunMagazine >= 1) ||
            (gunType == GunType.Rifle && CurRifleAmmo <= 0 && rifleMagazine >= 1) ||
            (gunType == GunType.ShotGun && CurShotGunAmmo <= 0 && shotGunMagazine >= 1)) {

            StartCoroutine(Reload());
            return;
        }
        SwapWeapons();
    }

    public int RetCurrentWeapon() {

        /* Regarding the current weapon, we set its ID and it's rounds per minute. */
        /* Then we return the ID of the current weapon. */

        if (gunType == GunType.HandGun) {
            currenWeapon = 1;
            roundsPerMinute = 300;
        }
        else if (gunType == GunType.Rifle) {
            currenWeapon = 2;
            roundsPerMinute = 600;
        }
        else if (gunType == GunType.ShotGun) {
            currenWeapon = 3;
            roundsPerMinute = 60;
        }
        secondsBetweenShots = 60 / roundsPerMinute;
        return currenWeapon;
    }

    IEnumerator Reload() {

        /* Setting the reload variable to true and playing the reloading AudioSource. */
        isReloading = true;
        reloadGunSound.Play();
        Debug.Log("Reloading...");

        /* Find the suitable reloading animation for the current weapon. */
        /* Wait for a second where the player can't do anythung but to reload. */
        /* The we go back to the IDLE animation. */
        FindObjectOfType<AnimateSprite>().Animate(true, currenWeapon);
        yield return new WaitForSeconds(reloadTime);
        FindObjectOfType<AnimateSprite>().Animate(false, currenWeapon);

        /* Regarding the Weapon, we reload it.*/
        /* We check if the new ammunition value doesn't exceed the max value of a magazine. */
        /* Then we re-display the ammunition value for the current weapon on the GUI. */
        /* At the very end, we re-set isReloading to false. */
        if (gunType == GunType.HandGun) {

            if (handGunMagazine >= 10) {

                handGunMagazine -= 10;
                CurHandGunAmmo += 10;
            }
            else {

                CurHandGunAmmo += handGunMagazine;
                handGunMagazine = 0;
            }
            BulletCount.text = CurHandGunAmmo + "/" + handGunMagazine;
        }

        else if (gunType == GunType.Rifle) {

            if (rifleMagazine >= 20) {

                rifleMagazine -= 20;
                CurRifleAmmo += 20;
            }

            else {

                CurRifleAmmo += rifleMagazine;
                rifleMagazine = 0;
            }
            BulletCount.text = CurRifleAmmo + "/" + rifleMagazine;
        }
        else if (gunType == GunType.ShotGun) {

            if (shotGunMagazine >= 6) {

                shotGunMagazine -= 6;
                CurShotGunAmmo += 6;
            }
            else {

                CurShotGunAmmo += shotGunMagazine;
                shotGunMagazine = 0;
            }
            BulletCount.text = CurShotGunAmmo + "/" + shotGunMagazine;
        }
        isReloading = false;
    }

    void SwapWeapons() {

        /* Regarding the pressed key, we set the gun type of the current weapon. */
        /* Then we re-display the weapon in the GUI. */
        if (Input.GetKeyDown("1")) {

            gunType = GunType.HandGun;
            BulletCount.text = CurHandGunAmmo + "/" + handGunMagazine;
        }

        if (Input.GetKeyDown("2")) {

            gunType = GunType.Rifle;
            BulletCount.text = CurRifleAmmo + "/" + rifleMagazine;
        }

        if (Input.GetKeyDown("3")) {

            gunType = GunType.ShotGun;
            BulletCount.text = CurShotGunAmmo + "/" + shotGunMagazine;
        }
    }

    void PlaySound() {

        /* Plays the shooting sound regarding the weapon that is currently being used. */
        if (gunType == GunType.HandGun)
            handGunSound.Play();
        if (gunType == GunType.Rifle)
            rifleSound.Play();
        if (gunType == GunType.ShotGun)
            shotGunSound.Play();
    }

    public int RetRemainEnemies() {

        /* Return the number of remaining foes on the map. */
        return remainingEnemies;
    }

    public void Shoot() {

        if (CanShoot() && isReloading == false) {

            Debug.Log("Shooting...");

            /* If the Player is allowed to shoot, regarding to his current weapon, we decrease the ammo value. */
            /* Then we re-display the ammo value in the GUI. */
            if (gunType == GunType.HandGun) {

                CurHandGunAmmo--;
                BulletCount.text = CurHandGunAmmo + "/" + handGunMagazine;
                Debug.Log(CurHandGunAmmo + "/" + handGunMagazine);
            }

            else if (gunType == GunType.Rifle) {

                CurRifleAmmo--;
                BulletCount.text = CurRifleAmmo + "/" + rifleMagazine;
                Debug.Log(CurRifleAmmo + "/" + rifleMagazine);
            }

            else if (gunType == GunType.ShotGun) {
                CurShotGunAmmo--;
                BulletCount.text = CurShotGunAmmo + "/" + shotGunMagazine;
                Debug.Log(CurShotGunAmmo + "/" + shotGunMagazine);
            }

            
            /* Creating a ray going 20 units forward from the Spawn position; */
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;
            float shotDistance = 20;
            
            if (Physics.Raycast(ray, out hit, shotDistance)) {

                shotDistance = hit.distance;

                /* If the ray hits a collider and if this collider is part of a GameObject with the tag Enemy : */
                /* We destroy this GameObject */
                /* We decrease the amount of remaining enemies on the map. */
                /* And we re-display this value in the GUI. */
                if (hit.collider.gameObject.name == "Enemy") {
                    Destroy(hit.collider.gameObject);
                    remainingEnemies--;
                    EnemiesCount.text = remainingEnemies + "/" + maxEnemies;
                }
            }

            /* Setting the time between the now and the nex possible shoot. */
            /* Then we call PlaySound to play the AudioSource of the appropriate weapon's shot. */
            nextPossibleShootTime = Time.time + secondsBetweenShots;
            PlaySound();

            if (tracer) {

                /* Activate the LineRenderer to show the shot. */
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
        }
    }

    public void ShootContinuous() {

        /* If left click is kept down and we are using a rifle, then let's keep shooting. */
        if (gunType == GunType.Rifle)
            Shoot();
    }


    // Use this for initialization
    void Start () {

        /* Setting the remaining enemies and ammo variables */
        /* Display them in the GUI for the first time. */
        remainingEnemies = maxEnemies;
        EnemiesCount.text = remainingEnemies + "/" + maxEnemies;

        CurHandGunAmmo = 10;
        CurRifleAmmo = 20;
        CurShotGunAmmo = 6;

        BulletCount.text = CurHandGunAmmo + "/" + handGunMagazine;

        /* Returning the existence of a LineRendererComponent attached to the Weapon GameObject. */
        /* If it exists, we set it in the tracer variable. */
        if (GetComponent<LineRenderer>()) {
            tracer = GetComponent<LineRenderer>();
        }
	}

    private bool CanShoot() {
        bool canShoot = true;

        /* If there has not been enough time between the previous shot and the next one, we set canShoot to false. */
        if (Time.time < nextPossibleShootTime) {
            canShoot = false;
        }

        /* Same if the current weapon is renning out of ammunition; */
        if ((gunType == GunType.HandGun && CurHandGunAmmo == 0 && handGunMagazine == 0) ||
            (gunType == GunType.Rifle && CurRifleAmmo == 0 && rifleMagazine == 0) ||
            (gunType == GunType.ShotGun && CurShotGunAmmo == 0 && shotGunMagazine == 0)) {

            canShoot = false;
            dryFire.Play();
            /* In that case we play a Dry Magazine sound effect via AudioSource. */
        }
        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint) {

        /*Activate the LineRenderer. */
        /* Set its origin and end position. */
        /* Wait. */
        /* Disactivate the LineRenderer. */

        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);
        yield return null;
        tracer.enabled = false;
    }
}