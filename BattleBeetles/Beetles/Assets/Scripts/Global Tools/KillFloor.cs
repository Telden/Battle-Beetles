using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        if (transform.position.y < -20)
            transform.position = new Vector3(0, 5, 0);
	}
}
