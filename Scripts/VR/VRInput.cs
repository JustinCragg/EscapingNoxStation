using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VRInput : MonoBehaviour {
    NavMeshAgent agent;
    LineRenderer line;

    float lineCool = 0;

    bool triggerPress = false;
    bool triggerDown = false;

    public bool hasMultitool;
    public bool hasKeycard;
    public bool hasConsole;

    /******************************************************************************************************
    * Called once when the scene begins
    *******************************************************************************************************/
    private void Start() {
        agent = transform.root.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        line = GetComponent<LineRenderer>();
    }

    /******************************************************************************************************
    * Called once very frame
    *******************************************************************************************************/
    void Update() {
        // Resets the colour of the controller line
        if (lineCool > 0) {
            lineCool -= Time.deltaTime;
        }

        // Stopping movement jitter
        if (agent.remainingDistance < 0.25f) {
            agent.isStopped = true;
        }

        // Toggle the flashlight with the touch pad
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad)) {
            GetComponentInChildren<Flashlight>().toggleLight();
        }

        // This is neccesary to get the TriggerDown
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f) {
            triggerDown = false;
            if (triggerPress == false) {
                triggerDown = true;
            }
            triggerPress = true;
        }
        else {
            triggerDown = false;
            triggerPress = false;
        }

        GameObject hitObject = null;
        VRRaycaster raycast = GetComponent<VRRaycaster>();
        if (raycast.getValidHit() == true) {
            hitObject = raycast.getHitObject();
            if (hitObject != null) {
                // Changing the colour of the beam
                if (lineCool <= 0) {
                    if (hitObject.tag == "Floor") {
                        line.material.color = Color.green;
                    }
                    else if (hitObject.tag == "Locker" && Vector3.Distance(transform.position, raycast.getHitPos()) <= 3) {
                        line.material.color = Color.cyan;
                    }
                    else if (hitObject.tag == "Door" || hitObject.tag == "Locker") {
                        line.material.color = Color.blue;
                    }
                    else if (hitObject.tag == "Elevator" || hitObject.tag == "Console" || hitObject.tag == "MultiTool" || hitObject.tag == "Keycard") {
                        line.material.color = Color.magenta;
                    }
                    else if (hitObject.tag == "UI") {
                        line.material.color = Color.gray;
                    }
                    else if (hitObject.tag == "Monster") {
                        line.material.color = new Color(255, 128, 0);
                    }
                    else {
                        line.material.color = Color.red;
                    }
                }

                // On trigger
                if (triggerDown == true) {
                    // On hit object
                    // If object is a floor
                    if (hitObject.tag == "Floor") {
                        // Change line colour
                        line.material.color = Color.yellow;
                        lineCool = 0.1f;

                        // Begin moving using the navmesh
                        Vector3 temp = raycast.getHitPos();
                        temp.y = transform.position.y;
                        agent.SetDestination(temp);

                        agent.isStopped = false;
                    }
                    // if player interacts with console
                    else if (hitObject.tag == "Console") {
                        if (Vector3.Distance(transform.position, raycast.getHitPos()) < 5) {
                            line.material.color = Color.yellow;
                            lineCool = 0.1f;

                            hasKeycard = true;
                            hitObject.GetComponent<Keycard>().interact();
                        }
                    }
                    // if player picks up multiTool
                    else if (hitObject.tag == "MultiTool") {
                        if (Vector3.Distance(transform.position, raycast.getHitPos()) < 5) {
                            line.material.color = Color.yellow;
                            lineCool = 0.1f;

                            hasMultitool = true;
                            hitObject.GetComponent<PickupMultiTool>().interact();
                        }
                    }
                    // if player picks up the keycard
                    else if (hitObject.tag == "Keycard") {
                        if (Vector3.Distance(transform.position, raycast.getHitPos()) < 5) {
                            line.material.color = Color.yellow;
                            lineCool = 0.1f;

                            hasKeycard = true;
                            hitObject.GetComponent<Keycard>().interact();
                        }
                    }
                    // Click on Elevator Button 
                    else if (hitObject.tag == "Elevator") {
                        // Change line colour to yellow
                        line.material.color = Color.yellow;
                        lineCool = 0.1f;

                        // Load Elevator Scene if player has all items required
                        if (hasMultitool && hasKeycard && hasConsole) {
                            hitObject.GetComponent<Elevator>().workingElevatorClip();
                            hitObject.GetComponent<Elevator>().interact();
                        }
                        else {
                            hitObject.GetComponent<Elevator>().brokenElevatorClip();
                        }
                    }
                    // Click on door
                    else if (hitObject.tag == "Door") {
                        // Change line colour to yellow
                        line.material.color = Color.yellow;
                        lineCool = 0.1f;

                        // animate door
                        hitObject.transform.parent.GetComponent<Door>().interact();
                    }
                    else if (hitObject.tag == "Locker") {
                        if (Vector3.Distance(transform.position, raycast.getHitPos()) < 3) {
                            // Change line colour to yellow
                            line.material.color = Color.yellow;
                            lineCool = 0.1f;

                            // Use Locker
                            hitObject.GetComponent<HidingLockerScript>().interact();
                        }
                        else {
                            // Change line colour to yellow
                            line.material.color = Color.yellow;
                            lineCool = 0.1f;
                        }
                    }
                    else if (hitObject.tag == "Monster") {
                        // Change the line to yellow
                        line.material.color = Color.yellow;
                        lineCool = 0.1f;

                        hitObject.transform.GetComponent<Monster>().interact();
                    }
                    else if (hitObject.tag == "UI") {
                        // Change the line to yellow
                        line.material.color = Color.yellow;
                        lineCool = 0.1f;

                        hitObject.GetComponentInParent<Button>().onClick.Invoke();
                    }
                }
            }
        }
    }
}
