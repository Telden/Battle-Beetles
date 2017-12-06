using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour {

    public GameObject EnemyPlayer;
    public bool lockOnEnabled = true;
    Vector3 playerPos;

    [Header("Hitbox Used")]
    public float RotationSpeed;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        EnemyPosision();
        checkInput();
	}

    //Update the enemy posision for lock-on
    void EnemyPosision()
    {
        playerPos = new Vector3(EnemyPlayer.GetComponent<Rigidbody>().position.x, EnemyPlayer.GetComponent<Rigidbody>().position.y, EnemyPlayer.GetComponent<Rigidbody>().position.z);
    }

    //Check Controller Input
    void checkInput()
    {
        if(Input.GetButton(GetComponent<InputStorage>().getFromInputStorage("LockOn", GetComponent<BasePlayer>().isPlayer1)) && lockOnEnabled)
        {
            //calulate the vector between the posision of the camera and the posision of the enemy player
            //Vector3 EnemyVector = new Vector3(transform.position.x - EnemyPlayer.GetComponent<Rigidbody>().position.x,
            //                                  0,
            //                                  transform.position.z - EnemyPlayer.GetComponent<Rigidbody>().position.z);
            //transform.forward = EnemyVector * -1;
            Vector3 directionVector = (playerPos - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(directionVector);

            rotation.x = transform.rotation.x;
            rotation.z = transform.rotation.z;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);

        }
    }
}
