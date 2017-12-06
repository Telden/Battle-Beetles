using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

	[Header("Hover Ability")]
	public bool isHovering;
	public bool justHovered;
	public ParticleSystem parts;

	[Header("Hover Parameters")]
	public float hoverTime;

	void Start () {
		parts.Stop();
	}

	void Update () {
		AbilityHover();

		if (isHovering)
		{
			Movement();
			CheckHover();
		}
	}

	void AbilityHover ()
	{
		if (Input.GetButtonDown(GetComponent<InputStorage>().getFromInputStorage("Jump", GetComponent<BasePlayer>().isPlayer1)) && !GetComponent<BasePlayer>().grounded && !isHovering && !justHovered)
		{
            parts.Play();
            isHovering = true;
            justHovered = true;
            Invoke("StopHovering", hoverTime);
		}
	}

	void Movement()
	{
		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
	}

	void CheckHover()
	{
		if (GetComponent<BasePlayer>().grounded)
		{
			CancelInvoke();
			StopHovering();
		}
	}

	public void StopHovering()
    {
    	justHovered = false;
        isHovering = false;
        parts.Stop();
    }
}
