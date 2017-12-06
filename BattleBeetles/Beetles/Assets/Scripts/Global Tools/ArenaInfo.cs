using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaInfo : MonoBehaviour {

	[Header("Spawn points")]
	public Vector3 p1Point;
	public Vector3 p1Rotation;
	public Vector3 p2Point;
	public Vector3 p2Rotation;

	private GameObject spawner;

	void Start ()
	{
		spawner = GameObject.Find("Player Spawner");

		spawner.GetComponent<SpawnPlayer>().spawnPlayers(p1Point, p1Rotation, p2Point, p2Rotation);

		//Destroy(spawner.gameObject);
	}
}