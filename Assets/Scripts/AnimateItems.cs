using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateItems : MonoBehaviour {

    private Vector3 rotationAngle = new Vector3(0, 0, 90);
    private float rotationSpeed = 1;

	void Update () {
        
        /* Performs a continuous 90° rotation of the Z axis of the GameObject. */
        transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
    }
}
