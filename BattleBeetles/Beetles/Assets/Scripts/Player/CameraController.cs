using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float axisX = 0;
    public float axisY = 0;
    public float cameraX = 0;
    public float cameraY = 0;

    GameObject player;
    void Start()
    {

    }


    void Update()
    {
        //Update controller input depending on the player
        UpdateAxes();
    }

    void UpdateAxes()
    {
        if (player.GetComponent<BasePlayer>().isPlayer1)
        {
            axisX = Input.GetAxis("HorizontalLeft");
            axisY = Input.GetAxis("VerticalLeft");

            cameraX = Input.GetAxis("HorizontalRight");
            cameraY = Input.GetAxis("VerticalRight");
        }
        else
        {
            axisX = Input.GetAxis("HorizontalLeft2");
            axisY = Input.GetAxis("VerticalLeft2");

            cameraX = Input.GetAxis("HorizontalRight2");
            cameraY = Input.GetAxis("VerticalRight2");
        }
    }
}
