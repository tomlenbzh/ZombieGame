using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform    cameraTarget; /* Variable designed to get the transform of the GameObject we want our camera to follow. */
    public Vector3      minCameraPos;
    public Vector3      maxCameraPos;
    public bool         edges;

    void Update() {

        /* The position of the camera is assigned to the position of the cameraTarget variable (the Player). */
        transform.position = new Vector3(cameraTarget.position.x, transform.position.y, cameraTarget.position.z);

        if (edges) {

            /* If the map has edges, using clamp will forbid the camera to go any further than the edges.*/
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
}
