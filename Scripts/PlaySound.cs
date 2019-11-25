using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    public bool reactivatable = true;
    bool activated = false;

    /******************************************************************************************************
    * Plays a sound when the player enters the trigger box
    *******************************************************************************************************/
    void OnTriggerEnter(Collider other) {
        if (activated == false || reactivatable == true) {
            if (other.tag == "Player") {
                GetComponent<AudioSource>().Play();
                activated = true;
            }
        }
    }
}
