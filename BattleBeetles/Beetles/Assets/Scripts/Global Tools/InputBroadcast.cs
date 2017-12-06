using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBroadcast : MonoBehaviour {
	
	void Update () {
        // A
        if (Input.GetButtonDown("A")) { Debug.Log("A"); }
        if (Input.GetButtonDown("A2")) { Debug.Log("A2"); }

        // B
        if (Input.GetButtonDown("B")) { Debug.Log("B"); }
        if (Input.GetButtonDown("B2")) { Debug.Log("B2"); }

        // X
        if (Input.GetButtonDown("X")) { Debug.Log("X"); }
        if (Input.GetButtonDown("X2")) { Debug.Log("X2"); }

        // Y
        if (Input.GetButtonDown("Y")) { Debug.Log("Y"); }
        if (Input.GetButtonDown("Y2")) { Debug.Log("Y2"); }

        // Left Bumper
        if (Input.GetButtonDown("Left Bumper")) { Debug.Log("Left Bumper"); }
        if (Input.GetButtonDown("Left Bumper2")) { Debug.Log("Left Bumper2"); }

        // Right Bumper
        if (Input.GetButtonDown("Right Bumper")) { Debug.Log("Right Bumper"); }
        if (Input.GetButtonDown("Right Bumper2")) { Debug.Log("Right Bumper2"); }

        // Start
        if (Input.GetButtonDown("Start")) { Debug.Log("Start"); }
        if (Input.GetButtonDown("Start2")) { Debug.Log("Start2"); }

        // Select
        if (Input.GetButtonDown("Select")) { Debug.Log("Select"); }
        if (Input.GetButtonDown("Select2")) { Debug.Log("Select2"); }
    }
}
