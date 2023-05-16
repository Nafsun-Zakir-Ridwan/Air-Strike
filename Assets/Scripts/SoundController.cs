using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	public static SoundController Instance;

	public List<AudioSource> Sources;

	public AudioSource Backgound;

	public AudioClip UI;

	public AudioClip Earth;

	public AudioClip Moon;

	public AudioClip Mars;

	public AudioClip Sun;

	public AudioClip EnemyDie;

	public AudioClip BtnClick;

	public AudioClip BuyItem;

	public AudioClip BossDie;

	public AudioClip WeaponPlayer;

	public AudioClip WeaponEnemy;

	public AudioClip Coin;

	public List<AudioClip> Bg;

	private void Awake()
	{
		SoundController.Instance = this;
	}

	public void PlaySound(AudioClip clip, float volume = 0f)
	{
		foreach (AudioSource current in this.Sources)
		{
			if (PlayerPrefs.GetInt("Sound", 1) == 1)
			{
				current.volume = volume;
			}
			else
			{
				current.volume = 0f;
			}
			if (!current.isPlaying)
			{
				current.clip = clip;
				current.Play();
				return;
			}
		}
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip;
		if (PlayerPrefs.GetInt("Sound", 1) == 1)
		{
			audioSource.volume = volume;
		}
		else
		{
			audioSource.volume = 0f;
		}
		audioSource.Play();
		this.Sources.Add(audioSource);
	}

	public void PlayMusic(AudioClip clip, float volume = 0f, bool loop = true)
	{
		if (PlayerPrefs.GetInt("Music", 1) == 1)
		{
			this.Backgound.volume = volume;
		}
		else
		{
			this.Backgound.volume = 0f;
		}
		this.Backgound.clip = clip;
		this.Backgound.loop = loop;
		this.Backgound.Play();
	}

	public void StopBgMusic()
	{
		this.Backgound.Pause();
	}
}
