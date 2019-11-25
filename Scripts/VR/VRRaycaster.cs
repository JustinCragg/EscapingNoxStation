using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRaycaster : MonoBehaviour {
    bool validHit = false;
    RaycastHit lastHitObject;
    LineRenderer line;

    /******************************************************************************************************
    * Called once at the start of the scene
    *******************************************************************************************************/
    void Start() {
        line = GetComponent<LineRenderer>();
    }

    /******************************************************************************************************
    * Called every frame
    *******************************************************************************************************/
    void Update() {
        // Sets the positions for the beam
        LayerMask mask = ~LayerMask.GetMask("TriggerBox");
        if (Physics.Raycast(transform.position, transform.forward, out lastHitObject, Mathf.Infinity, mask)) {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, lastHitObject.point);
            line.enabled = true;
            validHit = true;
        }
        else {
            line.enabled = false;
            validHit = false;
        }
    }

    /******************************************************************************************************
    * Returns whether something was hit this frame
    *******************************************************************************************************/
    public bool getValidHit() {
        return validHit;
    }

    /******************************************************************************************************
    * Returns the point the raycast was hit
    *******************************************************************************************************/
    public Vector3 getHitPos() {
        if (validHit == true) {
            return lastHitObject.point;
        }
        return Vector3.zero;
    }

    /******************************************************************************************************
    * Returns the object the raycast hit
    *******************************************************************************************************/
    public GameObject getHitObject() {
        if (validHit == true) {
            return lastHitObject.transform.gameObject;
        }
        return null;
    }
}
