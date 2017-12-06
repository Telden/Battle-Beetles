using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortSound : MonoBehaviour
{

    private bool isDistorted;
    private float t;
    private bool inProgress = false;
    private float volume;

    void Start()
    {
        isDistorted = false;
        t = 1.0f;
        inProgress = false;
        volume = GetComponent<AudioSource>().volume;
    }

    public void Distort(bool val)
    {
        if (val == true)
        {
            isDistorted = true;
        }
        else
        {
            isDistorted = false;
        }
    }

    void Update()
    {
        if (t > 1.0f)
        {
            t = 1.0f;
            inProgress = false;
        }
        if (t < -1.0f)
        {
            isDistorted = false;
            //t = -1.0f;
            //inProgress = false;
        }

        if (inProgress)
        {
            GetComponent<AudioSource>().volume = volume + volume;
        }
        else
        {
            GetComponent<AudioSource>().volume = volume;
        }

        GetComponent<AudioSource>().pitch = t;

        if (Input.GetKeyDown("w"))
        {
            /*if (isDistorted)
			{
				Debug.Log("NORMAL");
				isDistorted = false;
			}
			else*/
            {
                Debug.Log("DISTORTION");
                isDistorted = true;
            }

            inProgress = true;
        }

        if (isDistorted)
        {
            t -= 0.1f;
        }
        else
        {
            t += 0.1f;
        }
    }
}