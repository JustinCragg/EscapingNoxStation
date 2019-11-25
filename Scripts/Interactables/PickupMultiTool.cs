using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMultiTool : MonoBehaviour {
    /******************************************************************************************************
    * Called when the player interacts with the multitool
    *******************************************************************************************************/
    public void interact() {
        // swap contollers
        foreach (VRInput controller in GetComponentsInChildren<VRInput>(true)) {
            if (controller.name == "OculusGoController") {
                controller.gameObject.SetActive(false);
            }
            else if (controller.name == "geo_multiTool") {
                controller.gameObject.SetActive(true);
            }
        } 

        // Destroy MultiTool when clicked on 
        Destroy(gameObject);
    }
}
