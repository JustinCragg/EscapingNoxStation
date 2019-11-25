using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    Animator anim;
    public bool doorOpen;
    public bool isLocked;

    /******************************************************************************************************
    * Called once at scene start
    *******************************************************************************************************/
    private void Start() {
        doorOpen = false;
        anim = GetComponent<Animator>();
    }

    /******************************************************************************************************
    * Called when the player interacts with a door
    *******************************************************************************************************/
    public void interact() {
        if (isLocked) {
            VRInput player = GameObject.FindGameObjectWithTag("Player").GetComponent<VRInput>();

            if (player.hasConsole) {
                isLocked = false;
            }
        }

        // play opening animation
        if (!isLocked) {
            if (doorOpen == false) {
                doorOpen = true;
                anim.SetBool("DoorOpen", true);
            }
            // play closing animation
            else if (doorOpen == true) {
                doorOpen = false;
                anim.SetBool("DoorOpen", false);
            }
        }

    }
}