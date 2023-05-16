using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemCs : MonoBehaviour
{
	public GameObject BtnBuy1;

	public GameObject BtnBuy2;

	public GameObject BtnBuy3;

	public Button Buy1;

	public Button Buy2;

	public Button Buy3;

	public List<GameObject> Shield;

	public List<GameObject> Laser;

	public List<GameObject> Bomb;

	public Text Cost1;

	public Text Cost2;

	public Text Cost3;

	private void OnEnable()
	{
		this.LoadItem();
	}

	public void Off()
	{
		this.Cost1.gameObject.SetActive(false);
		this.BtnBuy1.SetActive(false);
		this.Buy1.gameObject.SetActive(false);
		this.Cost2.gameObject.SetActive(false);
		this.BtnBuy2.SetActive(false);
		this.Buy2.gameObject.SetActive(false);
		this.Cost3.gameObject.SetActive(false);
		this.BtnBuy3.SetActive(false);
		this.Buy3.gameObject.SetActive(false);
		GPSManager.Instance.PanelBlack.SetActive(false);
	}

	public void ShowStartGame()
	{
		if (TASData.Instance.Intro == 0)
		{
			GPSManager.Instance.PanelBlack.SetActive(true);
			this.Cost1.gameObject.SetActive(false);
			this.BtnBuy1.SetActive(false);
			this.Buy1.enabled = true;
			this.Buy1.gameObject.SetActive(true);
			this.Cost2.gameObject.SetActive(false);
			this.BtnBuy2.SetActive(false);
			this.Buy2.enabled = true;
			this.Buy2.gameObject.SetActive(true);
			this.Cost3.gameObject.SetActive(false);
			this.BtnBuy3.SetActive(false);
			this.Buy3.enabled = true;
			this.Buy3.gameObject.SetActive(true);
			GPSManager.Instance.Health1Player.gameObject.SetActive(false);
		}
		else
		{
			GPSManager.Instance.PanelBlack.SetActive(true);
			if (TASData.Instance.LstItemPP[5].Unlock)
			{
				this.Cost1.gameObject.SetActive(true);
				this.BtnBuy1.SetActive(true);
				this.Buy1.enabled = false;
				this.Buy1.gameObject.SetActive(true);
			}
			else
			{
				this.Cost1.gameObject.SetActive(false);
				this.BtnBuy1.SetActive(false);
				this.Buy1.gameObject.SetActive(false);
			}
			if (TASData.Instance.LstItemPP[6].Unlock)
			{
				this.Cost2.gameObject.SetActive(true);
				this.BtnBuy2.SetActive(true);
				this.Buy2.enabled = false;
				this.Buy2.gameObject.SetActive(true);
			}
			else
			{
				this.Cost2.gameObject.SetActive(false);
				this.BtnBuy2.SetActive(false);
				this.Buy2.gameObject.SetActive(false);
			}
			if (TASData.Instance.LstItemPP[7].Unlock)
			{
				this.Cost3.gameObject.SetActive(true);
				this.BtnBuy3.SetActive(true);
				this.Buy3.enabled = false;
				this.Buy3.gameObject.SetActive(true);
			}
			else
			{
				this.Cost3.gameObject.SetActive(false);
				this.BtnBuy3.SetActive(false);
				this.Buy3.gameObject.SetActive(false);
			}
			GPSManager.Instance.Health1Player.gameObject.SetActive(false);
		}
	}

	public void ShowOnGame()
	{
		this.Cost1.gameObject.SetActive(false);
		this.Cost2.gameObject.SetActive(false);
		this.Cost3.gameObject.SetActive(false);
		if (TASData.Instance.Intro == 0)
		{
			GPSManager.Instance.PanelBlack.SetActive(true);
			this.BtnBuy1.SetActive(false);
			this.Buy1.enabled = true;
			this.Buy1.gameObject.SetActive(true);
			this.BtnBuy2.SetActive(false);
			this.Buy2.enabled = true;
			this.Buy2.gameObject.SetActive(true);
			this.BtnBuy3.SetActive(false);
			this.Buy3.enabled = true;
			this.Buy3.gameObject.SetActive(true);
			GPSManager.Instance.Health1Player.gameObject.SetActive(true);
		}
		else
		{
			GPSManager.Instance.PanelBlack.SetActive(true);
			if (TASData.Instance.LstItemPP[5].Unlock)
			{
				this.BtnBuy1.SetActive(false);
				this.Buy1.enabled = true;
				this.Buy1.gameObject.SetActive(true);
			}
			else
			{
				this.BtnBuy1.SetActive(false);
				this.Buy1.gameObject.SetActive(false);
			}
			if (TASData.Instance.LstItemPP[6].Unlock)
			{
				this.BtnBuy2.SetActive(false);
				this.Buy2.enabled = true;
				this.Buy2.gameObject.SetActive(true);
			}
			else
			{
				this.BtnBuy2.SetActive(false);
				this.Buy2.gameObject.SetActive(false);
			}
			if (TASData.Instance.LstItemPP[7].Unlock)
			{
				this.BtnBuy3.SetActive(false);
				this.Buy3.enabled = true;
				this.Buy3.gameObject.SetActive(true);
			}
			else
			{
				this.BtnBuy3.SetActive(false);
				this.Buy3.gameObject.SetActive(false);
			}
			GPSManager.Instance.Health1Player.gameObject.SetActive(true);
		}
	}

	public void LoadItem()
	{
		if (GPSManager.Instance.NumBomb < 5)
		{
			this.Cost1.text = TASData.Instance.LstCostBuy1[GPSManager.Instance.NumBomb].ToString();
		}
		else
		{
			this.Cost1.gameObject.SetActive(false);
		}
		if (GPSManager.Instance.NumLaser < 5)
		{
			this.Cost2.text = TASData.Instance.LstCostBuy2[GPSManager.Instance.NumLaser].ToString();
		}
		else
		{
			this.Cost2.gameObject.SetActive(false);
		}
		if (GPSManager.Instance.NumShield < 5)
		{
			this.Cost3.text = TASData.Instance.LstCostBuy3[GPSManager.Instance.NumShield].ToString();
		}
		else
		{
			this.Cost3.gameObject.SetActive(false);
		}
		for (int i = 0; i < this.Shield.Count; i++)
		{
			if (i < GPSManager.Instance.NumShield)
			{
				this.Shield[i].SetActive(true);
			}
			else
			{
				this.Shield[i].SetActive(false);
			}
		}
		for (int j = 0; j < this.Laser.Count; j++)
		{
			if (j < GPSManager.Instance.NumLaser)
			{
				this.Laser[j].SetActive(true);
			}
			else
			{
				this.Laser[j].SetActive(false);
			}
		}
		for (int k = 0; k < this.Bomb.Count; k++)
		{
			if (k < GPSManager.Instance.NumBomb)
			{
				this.Bomb[k].SetActive(true);
			}
			else
			{
				this.Bomb[k].SetActive(false);
			}
		}
	}

	public void BtnBuyShieldOnClick()
	{
		if (GPSManager.Instance.NumShield < 5 && PlayerPrefs.GetInt("Coin", 0) > TASData.Instance.LstCostBuy3[GPSManager.Instance.NumShield])
		{
			PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - TASData.Instance.LstCostBuy3[GPSManager.Instance.NumShield]);
			GPSManager.Instance.NumShield++;
			this.LoadItem();
		}
	}

	public void BtnBuyLaserOnClick()
	{
		if (GPSManager.Instance.NumLaser < 5 && PlayerPrefs.GetInt("Coin", 0) > TASData.Instance.LstCostBuy2[GPSManager.Instance.NumLaser])
		{
			PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - TASData.Instance.LstCostBuy2[GPSManager.Instance.NumLaser]);
			GPSManager.Instance.NumLaser++;
			this.LoadItem();
		}
	}

	public void BtnBuyBombOnClick()
	{
		if (GPSManager.Instance.NumBomb < 5 && PlayerPrefs.GetInt("Coin", 0) > TASData.Instance.LstCostBuy1[GPSManager.Instance.NumBomb])
		{
			PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - TASData.Instance.LstCostBuy1[GPSManager.Instance.NumBomb]);
			GPSManager.Instance.NumBomb++;
			this.LoadItem();
		}
	}

	public void BtnShieldOnClick()
	{
		if (GPSManager.Instance.NumShield > 0)
		{
			GPSManager.Instance.NumShield--;
			MissionManager.Instance.NumItem--;
			if (MissionManager.Instance.NumItem == 0)
			{
				MissionManager.Instance.UseItem = true;
				MissionManager.Instance.CheckUseItem();
			}
			this.LoadItem();
			TASPlayerControl.Instance.ShowShield();
		}
	}

	public void BtnLaserOnClick()
	{
		if (GPSManager.Instance.NumLaser > 0)
		{
			GPSManager.Instance.NumLaser--;
			MissionManager.Instance.NumItem--;
			if (MissionManager.Instance.NumItem == 0)
			{
				MissionManager.Instance.UseItem = true;
				MissionManager.Instance.CheckUseItem();
			}
			this.LoadItem();
			GunPlayerCs.Instance.ShootLaser();
		}
	}

	public void BtnBombOnClick()
	{
		if (GPSManager.Instance.NumBomb > 0)
		{
			GPSManager.Instance.NumBomb--;
			MissionManager.Instance.NumItem--;
			if (MissionManager.Instance.NumItem == 0)
			{
				MissionManager.Instance.UseItem = true;
				MissionManager.Instance.CheckUseItem();
			}
			this.LoadItem();
			PoolManager.Instance.EnemyHitBomb();
			PoolManager.Instance.BulletEnemyHitBomb();
			PoolManager.Instance.GetEffect(PoolManager.Instance.EffBombPrefab, PoolManager.Instance.NEffBomb, PoolManager.Instance.GEffBomb, new Vector2(0f, 0f));
		}
	}

	private void OnDisable()
	{
		GPSManager.Instance.PanelBlack.SetActive(false);
	}
}
