using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MLSManager : MonoBehaviour
{
	public static MLSManager Instance;

	public List<Text> TxtMissions;

	public List<Image> StarMissions;

	public Text TxtDifficulty;

	public RawImage scroll;

	public Image ModeBar;

	public Sprite EasySprite;

	public Sprite NormalSprite;

	public Sprite HardSprite;

	public int NumStarCollect;

	public Text TxtStarCollected;

	public Sprite Cer;

	public Sprite NotCer;

	public float a;

	public GameObject Warnning;

	public GameObject WarnningLevelLock;

	public GameObject WarnningHard;

	public GameObject LvUnlockInfo;

	public List<Sprite> LstBgEarth;

	public List<Sprite> LstBgMoon;

	public List<Sprite> LstBgMars;

	public List<Sprite> LstBgSun;

	public Image BtnBackLight;

	public Image BtnUpgradeLight;

	public Image BtnSettingLight;

	public Image BtnStartLight;

	public Image Light;

	public Text TxtCoin;

	public int Coin;

	public GameObject BtnUpgrade;

	public GameObject Hand;

	public GameObject BtnNavi;

	public GameObject TxtLock;

	public GameObject SelectLv;

	private static TweenCallback __f__am_cache0;

	private static TweenCallback __f__am_cache1;

	private void Awake()
	{
		MLSManager.Instance = this;
	}

	private void OnEnable()
	{
		this.BtnBackLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnUpgradeLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnSettingLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnStartLight.color = new Color(1f, 1f, 1f, 0f);
		this.Light.color = new Color(0f, 0f, 0f, 1f);
		this.Light.DOColor(new Color(0f, 0f, 0f, 0f), 1f).SetEase(Ease.Linear);
		if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[0].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[1].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[2].Certificate)
		{
			this.CheckMission(true);
		}
		else
		{
			this.CheckMission(false);
		}
		this.NumStarCollect = 0;
		this.CalculateStar();
		this.ChangeBG();
		this.LoadCoin();
		this.CheckUpgrade();
		if (Advertisements.Instance && Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.HideBanner();
		}
		this.SelectLv.SetActive(true);
	}

	public void CheckUpgrade()
	{
		this.BtnUpgrade.transform.DOKill(false);
		this.BtnUpgrade.transform.localScale = new Vector3(1f, 1f);
		this.Hand.SetActive(false);
		for (int i = 0; i < TASData.Instance.LstItemPP.Count; i++)
		{
			if (this.Coin >= TASData.Instance.LstItemsCost[i][TASData.Instance.LstItemPP[i].Lv])
			{
				if (PlayerPrefs.GetInt("Tutorial", 2) == 0)
				{
					this.Hand.SetActive(true);
					PlayerPrefs.SetInt("Tutorial", 1);
				}
				else
				{
					this.Hand.SetActive(false);
				}
				this.BtnUpgrade.transform.DOKill(false);
				this.BtnUpgrade.transform.DOScale(new Vector3(1.2f, 1.2f), 0.5f).SetLoops(-1, LoopType.Yoyo);
				break;
			}
		}
	}

	public void LoadCoin()
	{
		this.Coin = PlayerPrefs.GetInt("Coin", 0);
		this.TxtCoin.text = this.Coin.ToString();
	}

	public void ChangeBG()
	{
		int @int = PlayerPrefs.GetInt("Stage", 1);
		int int2 = PlayerPrefs.GetInt("Lv", 1);
		if (@int == 1)
		{
			this.scroll.texture = this.LstBgEarth[int2 - 1].texture;
		}
		else if (@int == 2)
		{
			this.scroll.texture = this.LstBgMoon[int2 - 1].texture;
		}
		else if (@int == 3)
		{
			this.scroll.texture = this.LstBgMars[int2 - 1].texture;
		}
		else if (@int == 4)
		{
			this.scroll.texture = this.LstBgSun[int2 - 1].texture;
		}
	}

	public void CalculateStar()
	{
		for (int i = 0; i < TASData.Instance.LstLvPP.Count; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (TASData.Instance.LstLvPP[i].ModeLevel[j].Certificate)
				{
					this.NumStarCollect++;
				}
			}
		}
		this.TxtStarCollected.text = this.NumStarCollect.ToString();
	}

	private void Update()
	{
		this.a += 0.001f;
		this.scroll.uvRect = new Rect(0f, this.a, 1f, 1f);
	}

	public void BtnBackOnClick()
	{
		SoundController.Instance.PlaySound(SoundController.Instance.BtnClick, 1f);
		this.BtnBackLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnBackLight.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.BtnBackLight.DOColor(new Color(1f, 1f, 1f, 0f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
			{
				TASManagerUI.Instance.ChangeScenes(0);
			});
		});
	}

	public void BtnUpgradeOnClick()
	{
		SoundController.Instance.PlaySound(SoundController.Instance.BtnClick, 1f);
		this.BtnUpgradeLight.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.BtnUpgradeLight.DOColor(new Color(1f, 1f, 1f, 0f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
			{
				TASManagerUI.Instance.ChangeScenes(2);
				TASManagerUI.Instance.Bg.transform.DOKill(false);
				TASManagerUI.Instance.Bg.transform.DOMove(base.transform.position + new Vector3(-3f, 0f), 0.2f, false);
			});
		});
	}

	public void BtnSettingOnClick()
	{
		SoundController.Instance.PlaySound(SoundController.Instance.BtnClick, 1f);
		this.BtnSettingLight.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.BtnSettingLight.DOColor(new Color(1f, 1f, 1f, 0f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
			{
				PopupManager.Instance.SettingScene.SetActive(true);
				base.gameObject.SetActive(false);
				TASManagerUI.Instance.Bg.transform.DOKill(false);
				TASManagerUI.Instance.Bg.transform.DOMove(base.transform.position + new Vector3(3f, 0f), 0.2f, false);
			});
		});
	}

	public void BtnStartOnClick()
	{
		if (SelectLevelPanel.Instance.LevelUnlock)
		{
			if (SelectLevelPanel.Instance.EnoughStar)
			{
				SoundController.Instance.PlaySound(SoundController.Instance.BtnClick, 1f);
				this.BtnStartLight.DOColor(new Color(1f, 1f, 1f, 1f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
				{
					this.BtnStartLight.DOColor(new Color(1f, 1f, 1f, 0f), 0.3f).SetEase(Ease.Linear).OnComplete(delegate
					{
						TASManagerUI.Instance.ChangeScenes(3);
					});
				});
			}
			else
			{
				this.BtnNavi.transform.localScale = new Vector3(1f, 1f);
				this.BtnNavi.transform.DOScale(new Vector3(1.2f, 1.2f), 0.2f).SetEase(Ease.Linear).OnComplete(delegate
				{
					this.BtnNavi.transform.DOScale(new Vector3(1f, 1f), 0.2f);
				});
			}
		}
		else
		{
			this.TxtLock.transform.localScale = new Vector3(1f, 1f);
			this.TxtLock.transform.DOScale(new Vector3(1.2f, 1.2f), 0.2f).SetEase(Ease.Linear).OnComplete(delegate
			{
				this.TxtLock.transform.DOScale(new Vector3(1f, 1f), 0.2f);
			});
		}
	}

	public void BtnEasyOnClick()
	{
		SoundController.Instance.PlaySound(SoundController.Instance.BtnClick, 1f);
		this.CheckMission(false);
		this.TxtDifficulty.text = "Easy";
		this.LvUnlockInfo.SetActive(true);
		this.Warnning.SetActive(false);
	}

	public void BtnNormalOnClick()
	{
		this.CheckMission(false);
		this.TxtDifficulty.text = "Normal";
		this.ModeBar.sprite = this.NormalSprite;
		if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect].ModeLevel[1].Unlock)
		{
		}
	}

	public void BtnHardOnClick()
	{
		SoundController.Instance.PlaySound(SoundController.Instance.BtnClick, 1f);
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate)
			{
				num++;
			}
		}
		if (num == 3)
		{
			this.WarnningHard.SetActive(false);
			this.LvUnlockInfo.SetActive(true);
			this.CheckMission(true);
			this.TxtDifficulty.text = "Hard";
		}
		else
		{
			this.WarnningHard.SetActive(true);
			this.LvUnlockInfo.SetActive(false);
			this.ModeBar.sprite = this.HardSprite;
		}
		this.Warnning.SetActive(false);
	}

	public void CheckMission(bool isHard)
	{
		this.ModeBar.gameObject.SetActive(true);
		this.WarnningHard.SetActive(false);
		if (!isHard)
		{
			TASData.Instance.IsHard = false;
			this.ModeBar.sprite = this.EasySprite;
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
			TASData.Instance.IsHard = true;
			this.ModeBar.sprite = this.HardSprite;
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
		this.BtnBackLight.DOKill(false);
		this.BtnUpgradeLight.DOKill(false);
		this.BtnSettingLight.DOKill(false);
		this.BtnStartLight.DOKill(false);
		this.SelectLv.SetActive(false);
	}
}
