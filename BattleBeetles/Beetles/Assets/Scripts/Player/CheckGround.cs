using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {

	public LayerMask layer;
	public bool grounded;

	private Vector3 pos;

	void Start()
	{
		pos = transform.position;
	}

	void Update()
	{
		transform.position = new Vector3(0, 0, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			grounded = true;
		}

		else
		{
			grounded = false;
		}
	}
}
