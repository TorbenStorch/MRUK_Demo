using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	public Sound[] musicSounds, sfxSounds;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		SetUpAudioSources(musicSounds);
		SetUpAudioSources(sfxSounds);
	}
	void SetUpAudioSources(Sound[] sounds)
	{
		foreach (var s in sounds)
		{
			s.audioSource = gameObject.AddComponent<AudioSource>();
			s.audioSource.clip = s.clip;
			s.audioSource.volume = s.volume;
			s.audioSource.pitch = s.pitch;
			s.audioSource.loop = s.loop;
		}
	}
	public void PlayMusic(string name)
	{
		Sound s = Array.Find(musicSounds, x => x.name == name);

		if (s == null)
		{
			Debug.LogWarning("Sound with name " + name + " not found!");
			return;
		}
		else
		{
			s.audioSource.Play();
		}
	}
	public void PlaySFX(string name)
	{
		Sound s = Array.Find(sfxSounds, x => x.name == name);

		if (s == null)
		{
			Debug.LogWarning("Sound with name " + name + " not found!");
			return;
		}
		else
		{
			s.audioSource.PlayOneShot(s.clip);
		}
	}

	public void ToggleMusic()
	{
		foreach (var s in musicSounds)
		{
			s.audioSource.mute = !s.audioSource.mute;
		}
	}
	public void ToggleSFX()
	{
		foreach (var s in sfxSounds)
		{
			s.audioSource.mute = !s.audioSource.mute;
		}
	}
}