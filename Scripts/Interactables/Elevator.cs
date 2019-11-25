using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {
    public string loadElevator;

    bool trigger = false;
    public float delay = 0;
    float count = 0;

    public AudioClip hasCard;
    public AudioClip noCard;

    public AudioSource CardSource;

    void Update() {
        if (trigger == true) {
            count += Time.deltaTime;
        }
        if (count >= delay && trigger == true) {
            SceneManager.LoadSceneAsync(loadElevator);
            trigger = false;
        }
    }

    /******************************************************************************************************
    * If the player clicks on elevator and has the required tools then player goes to the next scene
    *******************************************************************************************************/
    public void interact() {
        trigger = true;
    }

    /******************************************************************************************************
    * Plays audio for working elevator if has needed items
    *******************************************************************************************************/
    public void workingElevatorClip() {
        if (hasCard != null) {
            // get audio source
            CardSource.clip = hasCard;
            // play audio source
            CardSource.Play();
        }
    }

    /******************************************************************************************************
    * Plays audio for elevator if missing items
    *******************************************************************************************************/
    public void brokenElevatorClip() {
        if (noCard != null) {
            // get audio source
            CardSource.clip = noCard;
            // play audio source
            CardSource.Play();
        }
    }
}
