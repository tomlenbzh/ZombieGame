using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))] /* Adds a required component : AudioSource as a dependency of GameObject. */
public class PlayerController : MonoBehaviour {

    private Rigidbody   playerRigidBody;
    private Camera      mainCamera;
    public Weapon       curWeapon;

    private Vector3     moveInput;
    private Vector3     moveVelocity;

    public AudioSource  hurtPlayerAudio;
    public AudioSource  healPlayerAudio;
    public AudioSource  killPlayerAudio;
    public AudioSource  gruntPlayerAudio;

    public float        moveSpeed;
    
    // Use this for initialization
    void Start () {

        /* Applying a RigidBody for maintaining the Player on the ground. */
        playerRigidBody = GetComponent<Rigidbody>();

        /*  */
        mainCamera = FindObjectOfType<Camera>();
	}

    // Update is called once per frame
    void Update () {
        /* Checking continuously if the victory conditions are fullfilled. */
        CheckVictory();

        /* Allowing the Player to move, forward, backward, right and left. */
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        /* Creates a ray from camera to mouse position. */
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength)) {

            /* If the ray intersects with the plane, this point becomes the point where the player should look at. */
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            /* Rotates the Player's transform to look at the point created by the intersection of the ray and the plane. */
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetButtonDown("Shoot")) {
            
            /* If Left Mouse is clicked, call the Shoot method. */
            curWeapon.Shoot();
        }

        else if (Input.GetButton("Shoot")) {

            /* If Left Mouse is clicked and maintained, call the ShootContinuous method. */
            curWeapon.ShootContinuous();
        }
    }

    private void FixedUpdate() {

        /* Using FixedUpdtae because the time between calls is always the same. So the velocity never changes. */
        playerRigidBody.velocity = moveVelocity;
    }

    public void PlayHurtPlayer() {

        /* Playing AudioSources when the player is hurt. */
        hurtPlayerAudio.Play();
        gruntPlayerAudio.Play();
    }

    public void PlayHealPlayer() {

        /* Playing AudioSources when the player is healed. */
        healPlayerAudio.Play();
    }

    public void Die() {

        /* When the player diees, the Death Menu is displayed. */
        Debug.Log("Le joueur est mort.");
        FindObjectOfType<DeathMenu>().Death();
    }

    public void CheckVictory() {

        /* Checks if all the conditions for victory are fullfilled. */
        if ((FindObjectOfType<GameManager>().RetCurrentResource() == 60) && (FindObjectOfType<Weapon>().RetRemainEnemies() == 0)) {

            FindObjectOfType<VictoryMenu>().Victory();
        }
    }
}
