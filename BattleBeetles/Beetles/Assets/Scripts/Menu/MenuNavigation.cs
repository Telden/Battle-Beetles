using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour {

	[Header("Is this menu active")]
	public bool isActive;

	[Header("Buttons")]
	public int currentSelection = 0;
	private int maxSelection;

	public bool madeInput = false;

	public GameObject[] buttons;

	void Start ()
	{
		maxSelection = buttons.Length - 1;
	}

	void Update ()
	{
		checkInput();
		reenableInput();
		setColor();
	}

	void checkInput ()
	{
		if (isActive)
		{
			if (!madeInput && (Input.GetAxis("VerticalLeft") > 0.1f || Input.GetAxis("VerticalLeft2") > 0.1f))
			{
				if (currentSelection - 1 < 0)
				{
					currentSelection = maxSelection;
				}
				else
				{
					currentSelection--;
				}
				madeInput = true;
			}

			if (!madeInput && (Input.GetAxis("VerticalLeft") < -0.1f || Input.GetAxis("VerticalLeft2") < -0.1f))
			{
				if (currentSelection + 1 > maxSelection)
				{
					currentSelection = 0;
				}
				else
				{
					currentSelection++;
				}
				madeInput = true;
			}
		}
	}

	void reenableInput()
	{
		if (Input.GetAxis("VerticalLeft") == 0 && Input.GetAxis("VerticalLeft2") == 0)
		{
			madeInput = false;
		}
	}

	void setColor()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if (i == currentSelection)
			{
				buttons[i].GetComponent<SpriteRenderer>().color = Color.green;
			}
			else
			{
				buttons[i].GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}
}
