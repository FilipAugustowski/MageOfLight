using UnityEngine.Audio;
using System;
using UnityEngine;

/* From https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys */
/* only added high priority option */

public class AudioManager : MonoBehaviour
{

	public static AudioManager Instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	//[Range(0f, 1f)]
	//public float gameVolume = 0.1f;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			//DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;

			///* TAKE THIS OUT BEFORE A BUILD!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! */
			//s.volume = gameVolume;
		}
	}

	public void Play(string sound, bool _highPriority = false)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if(_highPriority)
        {
			s.source.priority = 0;
		}
		s.source.Play();
	}



}
