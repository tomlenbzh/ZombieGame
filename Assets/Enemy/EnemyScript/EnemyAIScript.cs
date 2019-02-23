using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))] /* Adds a required component : AudioSource as a dependency of GameObject. */

public class EnemyAIScript : MonoBehaviour
{
    public List<GameObject> navPoints = new List<GameObject>();
    public GameObject       player;
    public GameObject       eyes;

    public Transform        deathMenu;
    public Transform        pauseMenu;
    public Transform        enemy;

    public int              currentNavPoint;
    public float            senseDistance;

    public AudioSource      zombieIdleAudio;
    NavMeshAgent            agent;
    Animator                animator;

    public enum State {

        IDLE,
        PATROL,
        SEARCHING,
        CHASE
    }

    public State state;

    // Use this for initialization
    void Start() {

        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.SetDestination(navPoints[currentNavPoint].transform.position);
        
    }

    // Update is called once per frame
    void Update() {

        if (deathMenu.gameObject.activeInHierarchy == true || pauseMenu.gameObject.activeInHierarchy == true)
            zombieIdleAudio.Pause();

            if (state == State.PATROL) {

            eyes.GetComponent<MeshRenderer>().material.color = Color.green;

            if (agent.remainingDistance < 1) {

                if (currentNavPoint < navPoints.Count - 1)
                    currentNavPoint++;
                else
                    currentNavPoint = 0;
                agent.SetDestination(navPoints[currentNavPoint].transform.position);
            }
            Look();
        }
        else if (state == State.CHASE) {

            zombieIdleAudio = GetComponent<AudioSource>();
            zombieIdleAudio.Play();

            eyes.GetComponent<MeshRenderer>().material.color = Color.red;
            if (Vector3.Distance(player.transform.position, transform.position) > 1)
                agent.SetDestination(player.transform.position);
            else
                agent.SetDestination(transform.position);
            Look();
        }
        else if (state == State.SEARCHING) {

            eyes.GetComponent<MeshRenderer>().material.color = Color.yellow;

            if (Vector3.Distance(player.transform.position, transform.position) > 1)
                agent.SetDestination(player.transform.position);
            else
                agent.SetDestination(transform.position);
        }
    }

    void Look()
    {
        if (Vector3.Distance(player.transform.position, eyes.transform.position) < senseDistance) {

            RaycastHit hit;
            Ray ray = new Ray(eyes.transform.position, player.transform.position - eyes.transform.position);
            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit)) {

                if (hit.collider.gameObject.name == "Player") {

                    Vector3 playerDirection = player.transform.position - eyes.transform.position;
                    float angle = Vector3.Angle(eyes.transform.forward, playerDirection);

                    if (angle >= 0 && angle < 56.775f) {

                        if (state != State.CHASE)
                            animator.SetTrigger("Chase");

                        state = State.CHASE;
                        Debug.DrawLine(eyes.transform.position, player.transform.position, Color.red);
                    }
                    else {

                        if (state != State.SEARCHING) {

                            Invoke("StopSearching", 2);
                            animator.SetTrigger("Search");
                        }
                        state = State.SEARCHING;
                    }
                }
                else {

                    if (state != State.SEARCHING) {

                        Invoke("StopSearching", 2);
                    }
                    state = State.SEARCHING;
                }
            }
        }
        else
        {
            if (state == State.PATROL)
                animator.SetTrigger("Patrol");
            state = State.PATROL;
        }

    }

    void StopSearching() {

        animator.SetTrigger("Patrol");
        state = State.PATROL;
    }

}
