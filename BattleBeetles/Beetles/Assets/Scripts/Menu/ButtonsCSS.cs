using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsCSS : MonoBehaviour {

	[Header("Return here if the player presses B")]
	public string returnName;

	private GameObject spawner;

	[Header("Locking in your character")]
	public bool p1LockedIn;
	public bool p2LockedIn;

	void Start ()
	{
		spawner = GameObject.Find("Player Spawner");
	}

	void Update ()
	{
		checkSelect();
		checkCancel();
		checkLock();
	}

	void checkLock ()
	{
		if (p1LockedIn && p2LockedIn)
		{
			SceneManager.LoadScene("Tree Stump");
		}
	}

	void checkCancel ()
	{
		if (Input.GetButtonDown("B"))
		{
			if (p1LockedIn)
			{
				p1LockedIn = false;
			}
			else
			{
				GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive(returnName);
			}
		}

		if (Input.GetButtonDown("B2"))
		{
			if (p2LockedIn)
			{
				p2LockedIn = false;
			}
			else
			{
				GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive(returnName);
			}
		}
	}

	void checkSelect ()
	{
		if (GetComponent<MenuNavigation2>().isActive)
		{
			if (Input.GetButtonDown("A"))
			{
				switch (GetComponent<MenuNavigation2>().currentSelection)
				{
					case 0 :
						Debug.Log("player 1 selected stag");
						spawner.GetComponent<SpawnPlayer>().lockInP1(0);
						p1LockedIn = true;
						break;

					case 1 :
						Debug.Log("player 1 selected cicada");
						spawner.GetComponent<SpawnPlayer>().lockInP1(1);
						p1LockedIn = true;
						break;

					case 2 :
						Debug.Log("player 1 selected longhorn");
						spawner.GetComponent<SpawnPlayer>().lockInP1(2);
						p1LockedIn = true;
						break;

					default :
						Debug.Log("INVALID SELECTION for player 1");
						break;
				}
			}

			if (Input.GetButtonDown("A2"))
			{
				switch (GetComponent<MenuNavigation2>().currentSelection2)
				{
					case 0 :
						Debug.Log("player 2 selected stag");
						spawner.GetComponent<SpawnPlayer>().lockInP2(0);
						p2LockedIn = true;
						break;

					case 1 :
						Debug.Log("player 2 selected cicada");
						spawner.GetComponent<SpawnPlayer>().lockInP2(1);
						p2LockedIn = true;
						break;

					case 2 :
						Debug.Log("player 2 selected longhorn");
						spawner.GetComponent<SpawnPlayer>().lockInP2(2);
						p2LockedIn = true;
						break;

					default :
						Debug.Log("INVALID SELECTION for player 2");
						break;
				}
			}
		}
	}
}
