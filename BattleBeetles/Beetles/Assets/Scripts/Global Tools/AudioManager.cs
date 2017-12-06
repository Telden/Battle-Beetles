using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource[] audioSources;

	void Start()
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			gameObject.AddComponent<AudioSource>();
		}

		// populating array found here:
		// http://answers.unity3d.com/questions/795797/gather-audiosources-in-an-array.html
		audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	}

	public void PlaySound (AudioClip newAudio, float volume)
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			if (audioSources[i].isPlaying == false)
			{
				audioSources[i].clip = newAudio;
				audioSources[i].volume = volume;
				audioSources[i].Play();
				return;
			}
		}
	}
}
