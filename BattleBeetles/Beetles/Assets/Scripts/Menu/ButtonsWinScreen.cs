using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsWinScreen : MonoBehaviour {

	private GameObject spawner;

	void Start ()
	{
		spawner = GameObject.Find("Player Spawner");
	}

	void Update ()
	{
		if (GetComponent<MenuNavigation>().isActive)
		{
			if (Input.GetButtonDown("A") || Input.GetButtonDown("A2"))
			{
				switch (GetComponent<MenuNavigation>().currentSelection)
				{
				case 0 :
					Debug.Log("playing again");
					SceneManager.LoadScene("Tree Stump");
					break;

				case 1 :
					Debug.Log("returing to menu");
					SceneManager.LoadScene("Main Menu");

					// make sure to destroy the player spawner so there aren't two of them
					if (spawner != null)
					{
						Destroy(spawner);
					}

					break;
				}
			}
		}
	}
}
