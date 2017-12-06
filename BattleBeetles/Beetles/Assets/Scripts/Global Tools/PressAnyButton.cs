using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour {

	private bool delay = true;

	[Header("Pick one")]
	public string sceneName = "";
	public int sceneID = -1;

	void Start() {
		Invoke ("StopDelay", 1);
	}

	void Update () {
		if (delay == false)
		{
			if (Input.anyKeyDown && sceneName != "")
			{
				SceneManager.LoadScene (sceneName);
			}
			if (Input.anyKeyDown && sceneID != -1)
			{
				SceneManager.LoadScene (sceneName);
			}
		}
	}

	void StopDelay()
	{
		delay = false;
	}
}