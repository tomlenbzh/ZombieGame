using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSprite : MonoBehaviour {

    public Animator animator;

    public void Start() {

        /* Upates the animator variable with the Animator component attached to the GameObject. */
        animator = GetComponent<Animator>();
    }

    public void Animate(bool state, int curWeapon) {

        /* Send information to the animator to allow it to switch between animations. */
        animator.SetBool("Reloading", state);
        animator.SetInteger("Weapon", curWeapon);
    }
}
