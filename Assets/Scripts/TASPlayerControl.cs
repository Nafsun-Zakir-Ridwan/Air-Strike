using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TASPlayerControl : MonoBehaviour
{
	private sealed class _ShieldOn_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TASPlayerControl _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _ShieldOn_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.IsShield = true;
				this._this.Shield.SetActive(true);
				if (TASData.Instance.Intro == 0)
				{
					this._current = new WaitForSeconds(7f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
				}
				else
				{
					this._current = new WaitForSeconds(this._this.TimeShield);
					if (!this._disposing)
					{
						this._PC = 2;
					}
				}
				return true;
			case 1u:
				break;
			case 2u:
				break;
			default:
				return false;
			}
			this._this.Shield.SetActive(false);
			this._this.IsShield = false;
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static TASPlayerControl Instance;

	public float MaxHealth;

	public float CurHealth;

	public GameObject Shield;

	public float TimeShield;

	public float Rad;

	public LayerMask LayerItem;

	public LayerMask LayerEBullet;

	public LayerMask LayerEnemy;

	private HashSet<ItemCs> hsItems = new HashSet<ItemCs>();

	public PoolManager Pool;

	public bool IsShield;

	private void Awake()
	{
		TASPlayerControl.Instance = this;
	}

	private void OnEnable()
	{
		this.ReSetPlayer();
		this.TimeShield = TASData.Instance.LstShield[TASData.Instance.LstItemPP[7].Lv];
	}

	public void ReSetPlayer()
	{
		this.hsItems.Clear();
		this.MaxHealth = (float)TASData.Instance.LstHPPlayer[TASData.Instance.LstItemPP[0].Lv];
		this.CurHealth = this.MaxHealth;
		this.IsShield = false;
	}

	private void Update()
	{
		this.CheckItem();
		this.CheckEnemy();
		this.CheckBulletEnemy();
		if (TASData.Instance.Intro == 0)
		{
			this.CheckMagnet();
		}
		else if (TASData.Instance.LstItemPP[3].Unlock)
		{
			this.CheckMagnet();
		}
	}

	public void CheckMagnet()
	{
		HashSet<ItemCs> hashSet = new HashSet<ItemCs>();
		int index;
		if (TASData.Instance.Intro == 0)
		{
			index = 19;
		}
		else
		{
			index = TASData.Instance.LstItemPP[3].Lv;
		}
		RaycastHit2D[] array = Physics2D.CircleCastAll(base.transform.position, TASData.Instance.LstMagnetRad[index], Vector2.one, 0.1f, this.LayerItem);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].collider != null)
			{
				ItemCs component = array[i].collider.GetComponent<ItemCs>();
				if (component)
				{
					hashSet.Add(component);
				}
			}
		}
		foreach (ItemCs current in this.hsItems)
		{
			if (hashSet.Contains(current))
			{
				current.IsMagnet = true;
			}
			else
			{
				current.IsMagnet = false;
			}
		}
		this.hsItems = hashSet;
	}

	public void CheckEnemy()
	{
		RaycastHit2D raycastHit2D = Physics2D.CircleCast(base.transform.position, this.Rad, Vector2.one, 0.1f, this.LayerEnemy);
		if (raycastHit2D.collider != null && raycastHit2D.collider.tag == "Enemy")
		{
			if (!raycastHit2D.collider.GetComponent<TASEnemy>().IsBoss)
			{
				if (this.IsShield)
				{
					raycastHit2D.collider.GetComponent<TASEnemy>().EnemyDie();
				}
				else
				{
					this.CurHealth -= (float)raycastHit2D.collider.GetComponent<TASEnemy>().Damage;
					TASManagerUI.Instance.ShakeCamera();
					MissionManager.Instance.StillAlive = false;
					raycastHit2D.collider.GetComponent<TASEnemy>().EnemyDie();
					if (this.CurHealth <= 0f)
					{
						this.PlayerDie();
					}
					else
					{
						GPSManager.Instance.ShowHealth1();
					}
				}
			}
			else if (!this.IsShield)
			{
				TASManagerUI.Instance.ShakeCamera();
				this.PlayerDie();
			}
		}
	}

	public void CheckBulletEnemy()
	{
		if (this.CurHealth > 0f)
		{
			RaycastHit2D raycastHit2D = Physics2D.CircleCast(base.transform.position, this.Rad, Vector2.one, 0.1f, this.LayerEBullet);
			if (raycastHit2D.collider != null)
			{
				if (this.IsShield)
				{
					this.Pool.GetEffect(this.Pool.EBullet, this.Pool.EffEBull, this.Pool.GEffEbull, raycastHit2D.collider.transform.position);
				}
				else
				{
					this.CurHealth -= (float)raycastHit2D.collider.GetComponent<EBulletCs>().Damage;
					MissionManager.Instance.StillAlive = false;
					raycastHit2D.collider.gameObject.SetActive(false);
					this.Pool.GetEffect(this.Pool.EBullet, this.Pool.EffEBull, this.Pool.GEffEbull, raycastHit2D.collider.transform.position);
					if (this.CurHealth <= 0f)
					{
						this.PlayerDie();
						MissionManager.Instance.Die = true;
					}
					else
					{
						GPSManager.Instance.ShowHealth1();
					}
				}
			}
		}
	}

	public void CheckItem()
	{
		RaycastHit2D[] array = Physics2D.CircleCastAll(base.transform.position, this.Rad + 0.2f, Vector2.one, 0.1f, this.LayerItem);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].collider != null)
			{
				if (array[i].collider.GetComponent<ItemCs>().IdItem == 0)
				{
					if (!array[i].collider.GetComponent<ItemCs>().IsScale)
					{
						GunPlayerCs.Instance.UpGrade();
					}
				}
				else if (array[i].collider.GetComponent<ItemCs>().IdItem == 1)
				{
					if (!array[i].collider.GetComponent<ItemCs>().IsScale && GPSManager.Instance.NumBomb < 5)
					{
						GPSManager.Instance.NumBomb++;
					}
				}
				else if (array[i].collider.GetComponent<ItemCs>().IdItem == 2)
				{
					if (!array[i].collider.GetComponent<ItemCs>().IsScale)
					{
						this.CurHealth += 100f;
						if (this.CurHealth > this.MaxHealth)
						{
							this.CurHealth = this.MaxHealth;
						}
						GPSManager.Instance.ShowHealth1();
					}
				}
				else if (array[i].collider.GetComponent<ItemCs>().IdItem == 3)
				{
					if (!array[i].collider.GetComponent<ItemCs>().IsScale && GPSManager.Instance.NumLaser < 5)
					{
						GPSManager.Instance.NumLaser++;
					}
				}
				else if (array[i].collider.GetComponent<ItemCs>().IdItem == 4)
				{
					if (!array[i].collider.GetComponent<ItemCs>().IsScale && GPSManager.Instance.NumShield < 5)
					{
						GPSManager.Instance.NumShield++;
					}
				}
				else if (array[i].collider.GetComponent<ItemCs>().IdItem == 5 && !array[i].collider.GetComponent<ItemCs>().IsScale)
				{
					if (TASData.Instance.Intro != 0)
					{
						PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + 1);
					}
					TASMapManager.Instance.StarEat++;
					SoundController.Instance.PlaySound(SoundController.Instance.Coin, 0.6f);
					if (TASMapManager.Instance.StarEat == TASMapManager.Instance.CountStar)
					{
						MissionManager.Instance.AllCoin = true;
					}
					else
					{
						MissionManager.Instance.AllCoin = false;
					}
				}
				array[i].collider.gameObject.SetActive(false);
			}
		}
	}

	public void LaserHited(float damage)
	{
		if (!this.IsShield)
		{
			this.CurHealth -= damage;
			MissionManager.Instance.StillAlive = false;
			if (this.CurHealth <= 0f)
			{
				this.PlayerDie();
			}
			else
			{
				GPSManager.Instance.ShowHealth1();
			}
		}
	}

	public void PlayerDie()
	{
		if (!GPSManager.Instance.PlayerDie)
		{
			this.Pool.GetEffect(this.Pool.EffPDiePrefab, this.Pool.NEffPDie, this.Pool.GEffPDie, base.transform.position);
			base.gameObject.SetActive(false);
			if (TASData.Instance.Intro == 0)
			{
				GPSManager.Instance.ShowTutorial(false);
			}
			else
			{
				GPSManager.Instance.HideScoreAndBtnPause();
				GPSManager.Instance.PlayerDie = true;
				Time.timeScale = 1f;
				base.Invoke("ShowGameOver", 2f);
			}
		}
	}

	public void ShowMapLevel()
	{
		TASData.Instance.Intro = 1;
		PlayerPrefs.SetInt("Intro", 1);
		PoolManager.Instance.Reset();
		TASManagerUI.Instance.ChangeScenes(1);
		SoundController.Instance.PlayMusic(SoundController.Instance.UI, 1f, true);
	}

	public void ShowGameOver()
	{
		GPSManager.Instance.LevelFailed();
	}

	public void ShowShield()
	{
		base.StopCoroutine(this.ShieldOn());
		base.StartCoroutine(this.ShieldOn());
	}

	private IEnumerator ShieldOn()
	{
		TASPlayerControl._ShieldOn_c__Iterator0 _ShieldOn_c__Iterator = new TASPlayerControl._ShieldOn_c__Iterator0();
		_ShieldOn_c__Iterator._this = this;
		return _ShieldOn_c__Iterator;
	}

	private void OnDisable()
	{
		base.StopAllCoroutines();
	}
}
