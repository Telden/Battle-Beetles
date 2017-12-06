using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour {

	public string mName;
	public AudioSource mSource;

	public AudioObject (string name, AudioSource source)
	{
		mName = name;
		mSource = source;
	}
}
