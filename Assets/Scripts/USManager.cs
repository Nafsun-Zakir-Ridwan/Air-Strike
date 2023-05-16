using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class USManager : MonoBehaviour
{
	public static USManager Instance;

	public List<ItemUpgrade> LstItem;

	public Sprite Lock;

	public Sprite Unlock;

	public Sprite LockLight;

	public Sprite UnlockLight;

	public Text TxtCoin;

	public int Coin;

	public Image Light;

	private void Awake()
	{
		USManager.Instance = this;
	}

	private void OnEnable()
	{
		this.Light.color = new Color(0f, 0f, 0f, 1f);
		this.Light.DOColor(new Color(0f, 0f, 0f, 0f), 1f).SetEase(Ease.Linear);
		this.LoadItems();
		this.LoadCoin();
		if (Advertisements.Instance && Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
		}
	}

	public void LoadCoin()
	{
		this.Coin = PlayerPrefs.GetInt("Coin", 0);
		this.TxtCoin.text = this.Coin.ToString();
	}

	public void LoadItems()
	{
		for (int i = 0; i < this.LstItem.Count; i++)
		{
			this.LstItem[i].LoadDataItem(i);
		}
	}

	public void BtnBackOnClick()
	{
		TASManagerUI.Instance.ChangeScenes(1);
		TASManagerUI.Instance.Bg.transform.DOKill(false);
		TASManagerUI.Instance.Bg.transform.DOMove(base.transform.position, 0.2f, false);
	}
}
