using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
	public int IdItem;

	public List<GameObject> LvUpgrade;

	public Image BtnUpgradeSprite;

	public Text TxtCost;

	public Image BtnLight;

	public Image BtnLight2;

	public int cost;

	private void OnEnable()
	{
		this.BtnLight.color = new Color(1f, 1f, 1f, 0f);
	}

	private void Start()
	{
		this.BtnUpgradeSprite.GetComponent<Button>().onClick.AddListener(new UnityAction(this.BtnUpgradeOnClick));
	}

	public void LoadLight2()
	{
		this.BtnLight2.color = new Color(1f, 1f, 1f, 0f);
		if (TASData.Instance.LstItemPP[this.IdItem].Unlock)
		{
			this.BtnLight2.sprite = USManager.Instance.UnlockLight;
		}
		else
		{
			this.BtnLight2.sprite = USManager.Instance.LockLight;
		}
	}

	public void LoadDataItem(int id)
	{
		this.IdItem = id;
		if (TASData.Instance.LstItemPP[this.IdItem].Unlock)
		{
			this.BtnUpgradeSprite.sprite = USManager.Instance.UnlockLight;
			this.BtnUpgradeSprite.transform.GetChild(0).GetComponentInChildren<Image>().sprite = USManager.Instance.Unlock;
			for (int i = 0; i < this.LvUpgrade.Count; i++)
			{
				if (i <= TASData.Instance.LstItemPP[this.IdItem].Lv)
				{
					this.LvUpgrade[i].SetActive(true);
				}
				else
				{
					this.LvUpgrade[i].SetActive(false);
				}
			}
			if (TASData.Instance.LstItemPP[this.IdItem].Lv < 19)
			{
				this.TxtCost.text = TASData.Instance.LstItemsCost[this.IdItem][TASData.Instance.LstItemPP[this.IdItem].Lv + 1].ToString();
			}
			else
			{
				this.TxtCost.gameObject.SetActive(false);
				this.BtnUpgradeSprite.gameObject.SetActive(false);
			}
		}
		else
		{
			this.BtnUpgradeSprite.sprite = USManager.Instance.LockLight;
			this.BtnUpgradeSprite.transform.GetChild(0).GetComponentInChildren<Image>().sprite = USManager.Instance.Lock;
			for (int j = 0; j < this.LvUpgrade.Count; j++)
			{
				this.LvUpgrade[j].SetActive(false);
			}
			if (TASData.Instance.LstItemPP[this.IdItem].Lv < 19)
			{
				this.TxtCost.text = TASData.Instance.LstItemsCost[this.IdItem][TASData.Instance.LstItemPP[this.IdItem].Lv].ToString();
			}
			else
			{
				this.TxtCost.gameObject.SetActive(false);
			}
		}
		this.LoadLight2();
		this.cost = int.Parse(this.TxtCost.text);
		if (PlayerPrefs.GetInt("Coin", 0) >= this.cost)
		{
			this.BtnUpgradeSprite.DOKill(false);
			this.BtnUpgradeSprite.color = new Color(1f, 1f, 1f, 1f);
			this.BtnUpgradeSprite.DOColor(new Color(1f, 1f, 1f, 0f), 1f).SetLoops(-1, LoopType.Yoyo);
			this.BtnUpgradeSprite.transform.GetChild(1).GetComponentInChildren<Image>().DOKill(false);
			this.BtnUpgradeSprite.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
			this.BtnUpgradeSprite.transform.GetChild(1).GetComponentInChildren<Image>().DOColor(new Color(1f, 1f, 1f, 0f), 1f).SetLoops(-1, LoopType.Yoyo);
		}
		else
		{
			this.BtnUpgradeSprite.DOKill(false);
			this.BtnUpgradeSprite.color = new Color(1f, 1f, 1f, 0f);
			this.BtnUpgradeSprite.transform.GetChild(1).GetComponentInChildren<Image>().DOKill(false);
			this.BtnUpgradeSprite.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0f);
		}
	}

	public void BtnUpgradeOnClick()
	{
		int num = int.Parse(this.TxtCost.text);
		if (USManager.Instance.Coin >= num && USManager.Instance.Coin >= num && TASData.Instance.LstItemPP[this.IdItem].Lv < 19)
		{
			PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - num);
			USManager.Instance.LoadCoin();
			if (TASData.Instance.LstItemPP[this.IdItem].Unlock)
			{
				TASData.Instance.LstItemPP[this.IdItem].Lv++;
				TASData.Instance.SaveDataItem();
				this.LoadDataItem(this.IdItem);
				USManager.Instance.LoadItems();
			}
			else
			{
				TASData.Instance.LstItemPP[this.IdItem].Unlock = true;
				TASData.Instance.SaveDataItem();
				this.LoadDataItem(this.IdItem);
				USManager.Instance.LoadItems();
			}
			SoundController.Instance.PlaySound(SoundController.Instance.BuyItem, 1f);
		}
	}
}
