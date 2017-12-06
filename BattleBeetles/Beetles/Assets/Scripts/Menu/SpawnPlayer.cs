using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

	[Header("Character prefabs for player 1")]
	public GameObject p1Stag;
	public GameObject p1Cicada;
	public GameObject p1Longhorn;

	[Header("Character prefabs for player 2")]
	public GameObject p2Stag;
	public GameObject p2Cicada;
	public GameObject p2Longhorn;

	[Header("Currently locked in number (default 0)")]
	public int p1LockedInNum = 0;
	public int p2LockedInNum = 0;

	private GameObject tmp1, tmp2, info;

	void Awake ()
	{
	 	// make this object persist to the arena scene so it can spawn the characters
		GameObject.DontDestroyOnLoad(transform.gameObject);
	}

	void Update ()
	{
		info = GameObject.Find("ArenaInfo");
	}

	public void lockInP1(int value)
	{
		p1LockedInNum = value;
	}

	public void lockInP2(int value)
	{
		p2LockedInNum = value;
	}

	public void spawnPlayers(Vector3 p1SpawnLoc, Vector3 p1Rotation, Vector3 p2SpawnLoc, Vector3 p2Rotation)
	{
		Debug.Log(p1LockedInNum);
		Debug.Log(p2LockedInNum);

		switch(p1LockedInNum)
		{
			case 0 :
				tmp1 = Instantiate(p1Stag, p1SpawnLoc, Quaternion.Euler(p1Rotation));
				break;

			case 1 :
				tmp1 = Instantiate(p1Cicada, p1SpawnLoc, Quaternion.Euler(p1Rotation));
				break;

			case 2 :
				tmp1 = Instantiate(p1Longhorn, p1SpawnLoc, Quaternion.Euler(p1Rotation));
				break;

			default :
				Debug.Log("INVALID CHARACTER VALUE FOR PLAYER 1: " + p1LockedInNum);
				break;
		}

		switch(p2LockedInNum)
		{
			case 0 :
				tmp2 = Instantiate(p2Stag, p2SpawnLoc, Quaternion.Euler(p2Rotation));
				break;

			case 1 :
				tmp2 = Instantiate(p2Cicada, p2SpawnLoc, Quaternion.Euler(p2Rotation));
				break;

			case 2 :
				tmp2 = Instantiate(p2Longhorn, p2SpawnLoc, Quaternion.Euler(p2Rotation));
				break;

			default :
				Debug.Log("INVALID CHARACTER VALUE FOR PLAYER 2: " + p2LockedInNum);
				break;
		}

		// set the lock ons for each character
		tmp1.GetComponent<LockOn>().EnemyPlayer = tmp2;
		tmp2.GetComponent<LockOn>().EnemyPlayer = tmp1;

		// reset the names or the other scripts will shame you >:(
		tmp1.name = "Player1";
		tmp2.name = "Player2";
	}
}
