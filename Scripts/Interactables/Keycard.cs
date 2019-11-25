using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour {
    /******************************************************************************************************
    * Called when the player interacts with the keycard
    *******************************************************************************************************/
    public void interact() {
        // Destroy keycard when clicked on 
        Destroy(gameObject);
    }
}
