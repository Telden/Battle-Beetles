using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMain : MonoBehaviour {

	void Update ()
	{
		if (GetComponent<MenuNavigation>().isActive)
		{
			if (Input.GetButtonDown("A") || Input.GetButtonDown("A2"))
			{
				switch (GetComponent<MenuNavigation>().currentSelection)
				{
				case 0 :
					Debug.Log("going to character select");
					GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Character Select");
					break;

				case 1 :
					Debug.Log("going to classic mode");
					break;

				case 2 :
					Debug.Log("going to extras menu");
					break;

				case 3 :
					Debug.Log("going to credits");
					break;

				case 4 :
					Debug.Log("quitting game");
					Application.Quit();
					break;
				}
			}
		}
	}
}