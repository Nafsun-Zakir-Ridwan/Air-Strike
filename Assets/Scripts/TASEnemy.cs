using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TASEnemy : MonoBehaviour
{
	public Transform Trns;

	public float MaxHealth;

	public float CurHealth;

	public bool IsEnemyFly;

	public Tweener Tweener;

	public IName Iname;

	public int Wave;

	public int StandAngle = -90;

	public float Rad;

	public LayerMask LayerBullet;

	public PoolManager Pool;

	public Transform BullPos;

	public int NumStar;

	public int DamageBullet;

	[Header("BossInfo")]
	public bool IsBoss;

	public List<GunBossCs> LstGunBoss;

	public int MaxGunBossLive;

	public int GunBossLive;

	public bool IsLive;

	public int Damage;

	public GameObject EffItem;

	public int Score;

	public virtual void Awake()
	{
		this.Trns = base.transform;
	}

	public virtual void OnEnable()
	{
		this.IsLive = true;
		this.CurHealth = this.MaxHealth;
		this.GunBossLive = this.MaxGunBossLive;
	}

	public virtual void Start()
	{
		this.Pool = PoolManager.Instance;
	}

	public virtual void Update()
	{
		if (this.IsLive)
		{
			if (this.IsBoss)
			{
				if (this.GunBossLive <= 0)
				{
					this.CheckBulletPlayer();
				}
			}
			else
			{
				this.CheckBulletPlayer();
			}
		}
	}

	public void CheckBulletPlayer()
	{
		RaycastHit2D raycastHit2D = Physics2D.CircleCast(this.Trns.position, this.Rad, Vector2.one, 0.1f, this.LayerBullet);
		if (raycastHit2D.collider != null)
		{
			raycastHit2D.collider.gameObject.SetActive(false);
			this.Pool.GetEffect(this.Pool.EffPBullPrefab, this.Pool.NEffPBull, this.Pool.GEffPbull, raycastHit2D.collider.transform.position);
			this.CurHealth -= raycastHit2D.collider.GetComponent<PBulletCs>().Damage;
			if (this.CurHealth <= 0f)
			{
				this.EnemyDie();
			}
		}
	}

	public void LaserHit(float damage)
	{
		if (this.IsLive)
		{
			if (this.IsBoss)
			{
				if (this.GunBossLive <= 0)
				{
					this.CurHealth -= damage;
				}
			}
			else
			{
				this.CurHealth -= damage;
			}
			if (this.CurHealth <= 0f)
			{
				this.EnemyDie();
			}
		}
	}

	public virtual void EnemyDie()
	{
		this.IsLive = false;
		for (int i = 0; i < this.NumStar; i++)
		{
			this.Pool.GetItem(this.Pool.Star, this.Pool.NItemStar, this.Pool.GItemStar, this.Trns.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.3f), UnityEngine.Random.Range(-0.2f, 0.3f)));
		}
		if (this.Iname != IName.None)
		{
			this.CreateItem();
		}
		this.Iname = IName.None;
		TASMapManager.Instance.CheckCountEnemy();
		this.Pool.GetEffect(this.Pool.EnemyDie, this.Pool.EffEDie, this.Pool.GEffEDie, base.transform.position);
		GPSManager.Instance.Score += this.Score;
		GPSManager.Instance.ChangeTxtScore();
		base.gameObject.SetActive(false);
		SoundController.Instance.PlaySound(SoundController.Instance.EnemyDie, 0.5f);
	}

	public void CreateItem()
	{
		switch (this.Iname)
		{
		case IName.Bomb:
			this.Pool.GetItem(this.Pool.BombItem, this.Pool.NItemBomb, this.Pool.GItemBomb, this.Trns.position);
			break;
		case IName.Shield:
			this.Pool.GetItem(this.Pool.ShieldItem, this.Pool.NItemShield, this.Pool.GItemShield, this.Trns.position);
			break;
		case IName.Upgrade:
			this.Pool.GetItem(this.Pool.Upgrade, this.Pool.NItemUpgrade, this.Pool.GItemUpgrade, this.Trns.position);
			break;
		case IName.Health:
			this.Pool.GetItem(this.Pool.HealthItem, this.Pool.NItemHealth, this.Pool.GItemHealth, this.Trns.position);
			break;
		case IName.Laser:
			this.Pool.GetItem(this.Pool.LaserItem, this.Pool.NItemLaser, this.Pool.GItemLaser, this.Trns.position);
			break;
		}
	}

	public void FlyOff(Vector3[] path, float duration, bool join)
	{
		if (join)
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLookAt(0.001f, null, null).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
				MissionManager.Instance.AllEnemy = false;
				TASMapManager.Instance.CheckCountEnemy();
			});
		}
		else
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
				MissionManager.Instance.AllEnemy = false;
				TASMapManager.Instance.CheckCountEnemy();
			});
		}
	}

	public void FlyUnmoved(Vector3[] path, float duration, bool join)
	{
		if (join)
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLookAt(0.001f, null, null).OnComplete(delegate
			{
				this.Trns.eulerAngles = new Vector3(0f, 0f, (float)this.StandAngle);
				base.Invoke("MoveAfterStand", 4f);
			});
		}
		else
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).OnComplete(delegate
			{
				base.Invoke("MoveAfterStand", 4f);
			});
		}
	}

	public void FlyRepeat(Vector3[] path, float duration, bool join)
	{
		if (join)
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLoops(-1).SetLookAt(0.001f, null, null);
		}
		else
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLoops(-1);
		}
	}

	public void FlyPingpong(Vector3[] path, float duration, bool join)
	{
		if (join)
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetLookAt(0.001f, null, null).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		}
		else
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		}
	}

	public void FlyAround(Vector3[] path, float duration, bool join)
	{
		if (join)
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLoops(-1).SetLookAt(0.001f, null, null).SetOptions(true, AxisConstraint.None, AxisConstraint.None);
		}
		else
		{
			this.Tweener = this.Trns.DOPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D, 10, null).SetEase(Ease.Linear).SetLoops(-1).SetOptions(true, AxisConstraint.None, AxisConstraint.None);
		}
	}

	public virtual void MoveAfterStand()
	{
		if (!this.IsBoss)
		{
			Vector3[] array = (from s in PathController.Instance.Paths[49].Points
			select s + new Vector3(base.transform.position.x, -(PathController.Instance.Paths[49].Points[0].y - base.transform.position.y), 0f)).ToArray<Vector3>();
			base.transform.position = array[0];
			this.FlyOff(array, 5f, false);
		}
		else
		{
			Vector3[] array2 = (from s in PathController.Instance.Paths[59].Points
			select s + new Vector3(base.transform.position.x, -(PathController.Instance.Paths[59].Points[0].y - base.transform.position.y), 0f)).ToArray<Vector3>();
			base.transform.position = array2[0];
			this.FlyPingpong(array2, 8f, false);
		}
	}

	public virtual void GunBossDie(int id, bool changeGun)
	{
		this.GunBossLive--;
	}

	public virtual void BombHit(int damage)
	{
		if (this.IsBoss)
		{
			if (this.GunBossLive > 0)
			{
				for (int i = 0; i < this.LstGunBoss.Count; i++)
				{
					if (this.LstGunBoss[i].gameObject.activeInHierarchy)
					{
						this.LstGunBoss[i].BombHit(damage);
					}
				}
			}
			else
			{
				this.CurHealth -= (float)damage;
				if (this.CurHealth <= 0f)
				{
					this.EnemyDie();
				}
			}
		}
		else
		{
			this.CurHealth -= (float)damage;
			if (this.CurHealth <= 0f)
			{
				this.EnemyDie();
			}
		}
	}

	public virtual void OnDisable()
	{
		this.Trns.DOKill(false);
		base.CancelInvoke();
		base.StopAllCoroutines();
	}
}
