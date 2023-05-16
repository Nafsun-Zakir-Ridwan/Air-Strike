using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SSManager : MonoBehaviour
{
	public Sprite Mode1On;

	public Sprite Mode1Off;

	public Sprite Mode2On;

	public Sprite Mode2Off;

	public Sprite SoundOn;

	public Sprite SoundOff;

	public Sprite MusicOn;

	public Sprite MusicOff;

	public Image Mode1;

	public Image Mode2;

	public Image Sound;

	public Image Music;

	public Image Light;

	private void OnEnable()
	{
		if (PlayerPrefs.GetInt("ModeMove", 1) == 1)
		{
			this.Mode1.sprite = this.Mode1On;
			this.Mode2.sprite = this.Mode2Off;
		}
		else
		{
			this.Mode1.sprite = this.Mode1Off;
			this.Mode2.sprite = this.Mode2On;
		}
		this.LoadSoundAndMusic();
		this.Light.color = new Color(0f, 0f, 0f, 1f);
		this.Light.DOColor(new Color(0f, 0f, 0f, 0f), 1f).SetEase(Ease.Linear);
		if (Advertisements.Instance && Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
		}
	}

	public void BtnBackOnClick()
	{
		TASManagerUI.Instance.ChangeScenes(1);
		base.gameObject.SetActive(false);
		TASManagerUI.Instance.Bg.transform.DOKill(false);
		TASManagerUI.Instance.Bg.transform.DOMove(base.transform.position, 0.2f, false);
	}

	public void BtnMode1OnClick()
	{
		PlayerPrefs.SetInt("ModeMove", 1);
		this.Mode1.sprite = this.Mode1On;
		this.Mode2.sprite = this.Mode2Off;
	}

	public void BtnMode2OnClick()
	{
		PlayerPrefs.SetInt("ModeMove", 2);
		this.Mode1.sprite = this.Mode1Off;
		this.Mode2.sprite = this.Mode2On;
	}

	public void LoadSoundAndMusic()
	{
		if (PlayerPrefs.GetInt("Sound", 1) == 0)
		{
			this.Sound.sprite = this.SoundOff;
		}
		else
		{
			this.Sound.sprite = this.SoundOn;
		}
		if (PlayerPrefs.GetInt("Music", 1) == 0)
		{
			this.Music.sprite = this.MusicOff;
		}
		else
		{
			this.Music.sprite = this.MusicOn;
		}
	}

	public void BtnSoundOnClick()
	{
		if (PlayerPrefs.GetInt("Sound", 1) == 0)
		{
			PlayerPrefs.SetInt("Sound", 1);
			this.LoadSoundAndMusic();
		}
		else
		{
			PlayerPrefs.SetInt("Sound", 0);
			this.LoadSoundAndMusic();
		}
	}

	public void BtnMusicOnClick()
	{
		if (PlayerPrefs.GetInt("Music", 1) == 0)
		{
			PlayerPrefs.SetInt("Music", 1);
			SoundController.Instance.Backgound.volume = 1f;
			this.LoadSoundAndMusic();
		}
		else
		{
			PlayerPrefs.SetInt("Music", 0);
			SoundController.Instance.Backgound.volume = 0f;
			this.LoadSoundAndMusic();
		}
	}
}
