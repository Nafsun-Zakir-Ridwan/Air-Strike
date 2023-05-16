using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PSManager : MonoBehaviour
{
	public static PSManager Instance;

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

	public List<Text> TxtMissions;

	public List<Image> StarMissions;

	public Sprite Cer;

	public Sprite NotCer;

	public void Awake()
	{
		PSManager.Instance = this;
	}

	public void OnEnable()
	{
		Time.timeScale = 0f;
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
		this.CheckMission();
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
			SoundController.Instance.Backgound.volume = 0.5f;
			this.LoadSoundAndMusic();
		}
		else
		{
			PlayerPrefs.SetInt("Music", 0);
			SoundController.Instance.Backgound.volume = 0f;
			this.LoadSoundAndMusic();
		}
	}

	public void BtnContinueOnClick()
	{
		base.gameObject.SetActive(false);
		GPSManager.Instance.IsPause = false;
		Time.timeScale = 1f;
	}

	public void BtnMapLvOnClick()
	{
		PoolManager.Instance.Reset();
		TASManagerUI.Instance.ChangeScenes(1);
		SoundController.Instance.PlayMusic(SoundController.Instance.UI, 1f, true);
		base.gameObject.SetActive(false);
	}

	public void BtnRestartOnClick()
	{
		Time.timeScale = 1f;
		GPSManager.Instance.ResetGameplay();
		base.gameObject.SetActive(false);
	}

	public void CheckMission()
	{
		if (!TASData.Instance.IsHard)
		{
			for (int i = 0; i < 3; i++)
			{
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate)
				{
					this.TxtMissions[i].color = Color.yellow;
					this.StarMissions[i].sprite = this.Cer;
				}
				else
				{
					this.TxtMissions[i].color = Color.white;
					this.StarMissions[i].sprite = this.NotCer;
				}
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 1)
				{
					this.TxtMissions[i].text = "clear 100% enemies";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 2)
				{
					this.TxtMissions[i].text = "stay untouched";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 3)
				{
					this.TxtMissions[i].text = "collect 100% coin";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 4)
				{
					this.TxtMissions[i].text = "Unlock and use " + TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].MisionCount + " Item";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 5)
				{
					this.TxtMissions[i].text = "Die by bullet of enemy";
				}
			}
		}
		else
		{
			for (int j = 3; j < 6; j++)
			{
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate)
				{
					this.TxtMissions[j - 3].color = Color.yellow;
					this.StarMissions[j - 3].sprite = this.Cer;
				}
				else
				{
					this.TxtMissions[j - 3].color = Color.white;
					this.StarMissions[j - 3].sprite = this.NotCer;
				}
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 1)
				{
					this.TxtMissions[j - 3].text = "clear 100% enemies";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 2)
				{
					this.TxtMissions[j - 3].text = "stay untouched";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 3)
				{
					this.TxtMissions[j - 3].text = "collect 100% coin";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 4)
				{
					this.TxtMissions[j - 3].text = "Unlock and use " + TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].MisionCount + " Item";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 5)
				{
					this.TxtMissions[j - 3].text = "Die by bullet of enemy";
				}
			}
		}
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
	}
}
