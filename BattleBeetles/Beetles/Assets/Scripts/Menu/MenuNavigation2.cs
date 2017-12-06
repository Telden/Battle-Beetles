using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation2 : MonoBehaviour {

	[Header("Is this menu active")]
	public bool isActive;

	[Header("Buttons")]
	public int currentSelection = 0;
	public int currentSelection2 = 0;
	private int maxSelection;

	public bool madeInput = false;
	public bool madeInput2 = false;

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
			if(GetComponent<ButtonsCSS>().p1LockedIn == false)
			{
				// player 1 up
				if (!madeInput && Input.GetAxis("VerticalLeft") > 0.1f)
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

				// player 1 down
				if (!madeInput && Input.GetAxis("VerticalLeft") < -0.1f)
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

			if(GetComponent<ButtonsCSS>().p2LockedIn == false)
			{
				// player 2 up
				if (!madeInput2 && Input.GetAxis("VerticalLeft2") > 0.1f)
				{
					if (currentSelection2 - 1 < 0)
					{
						currentSelection2 = maxSelection;
					}
					else
					{
						currentSelection2--;
					}
					madeInput2 = true;
				}

				// player 2 down
				if (!madeInput2 && Input.GetAxis("VerticalLeft2") < -0.1f)
				{
					if (currentSelection2 + 1 > maxSelection)
					{
						currentSelection2 = 0;
					}
					else
					{
						currentSelection2++;
					}
					madeInput2 = true;
				}
			}
		}
	}

	void reenableInput()
	{
		if (Input.GetAxis("VerticalLeft") == 0)
		{
			madeInput = false;
		}

		if (Input.GetAxis("VerticalLeft2") == 0)
		{
			madeInput2 = false;
		}
	}

	void setColor()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if (i == currentSelection)
			{
				buttons[i].GetComponent<SpriteRenderer>().color = Color.yellow;
			}
			else
			{
				buttons[i].GetComponent<SpriteRenderer>().color = Color.blue;
			}
		}
	}
}
