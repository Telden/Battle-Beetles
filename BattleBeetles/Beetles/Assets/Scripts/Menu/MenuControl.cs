using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {

	[Header("A list of menus")]
	public GameObject[] menus;
	public int currentlyActive = 0;

	public GameObject mainCam;

	void Start () {
		// populating array found here:
		// http://answers.unity3d.com/questions/795797/gather-audiosources-in-an-array.html
		menus = GameObject.FindGameObjectsWithTag("Menu");

		// a failsafe if more than one (or none at all) menu is active at runtime
		setOneMenuActive();
	}

	void Update () {
		MoveCamera();
	}

	public void setNewAsActive(string name)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].name != name && i == menus.Length)
			{
				return;
			}
		}

		for (int i = 0; i < menus.Length; i++)
		{
			// set the menu of the matching name to active
			if (menus[i].name == name)
			{
				if (menus[i].GetComponent<MenuNavigation>() != null)
				{
					menus[i].GetComponent<MenuNavigation>().isActive = true;
					currentlyActive = i;
				}
				if (menus[i].GetComponent<MenuNavigation2>() != null)
				{
					menus[i].GetComponent<MenuNavigation2>().isActive = true;
					currentlyActive = i;
				}
			}
			else
			{
			// set everything else to inactive
				if (menus[i].GetComponent<MenuNavigation>() != null)
				{
					menus[i].GetComponent<MenuNavigation>().isActive = false;
				}
				if (menus[i].GetComponent<MenuNavigation2>() != null)
				{
					menus[i].GetComponent<MenuNavigation2>().isActive = false;
				}
			}
		}
	}

	void setOneMenuActive()
	{
		bool foundOne = false;

		for (int i = 0; i < menus.Length; i++)
		{
			// check through all of the menus to see if one is set active
			if (!foundOne)
			{
				if (menus[i].GetComponent<MenuNavigation>() != null)
				{
					if (menus[i].GetComponent<MenuNavigation>().isActive)
					{
						currentlyActive = i;
						foundOne = true;
					}
				}

				if (menus[i].GetComponent<MenuNavigation2>() != null)
				{
					if (menus[i].GetComponent<MenuNavigation2>().isActive)
					{
						currentlyActive = i;
						foundOne = true;
					}
				}
			}
			// if one is found active, set the rest to be false just in case
			else
			{
				if (menus[i].GetComponent<MenuNavigation>() != null)
				{
					menus[i].GetComponent<MenuNavigation>().isActive = false;
				}

				if (menus[i].GetComponent<MenuNavigation2>() != null)
				{
					menus[i].GetComponent<MenuNavigation2>().isActive = false;
				}
			}
		}

		// if none were found active, set the first to active
		if (!foundOne)
		{
			if (menus[0].GetComponent<MenuNavigation>() != null)
			{
				menus[0].GetComponent<MenuNavigation>().isActive = true;
			}

			if (menus[0].GetComponent<MenuNavigation2>() != null)
			{
				menus[0].GetComponent<MenuNavigation2>().isActive = true;
			}
		}
	}

	void MoveCamera()
	{
		mainCam.transform.position = new Vector3(menus[currentlyActive].transform.position.x, menus[currentlyActive].transform.position.y, -10);
	}
}
