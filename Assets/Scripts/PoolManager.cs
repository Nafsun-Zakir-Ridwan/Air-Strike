using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	private sealed class _CreateEnemyHerd_c__AnonStorey0
	{
		internal Vector3 offset;

		internal Vector3 __m__0(Vector3 s)
		{
			return s + this.offset;
		}
	}

	private sealed class _CreateEnemySingle_c__AnonStorey1
	{
		internal Vector3 offset;

		internal Vector3 __m__0(Vector3 s)
		{
			return s + this.offset;
		}
	}

	public static PoolManager Instance;

	public List<TASEnemy> EnemiesPrefab;

	public List<Transform> GEnemy;

	[Header("EName")]
	public string NE1 = "Enemy1";

	public string NE2 = "Enemy2";

	public string NE3 = "Enemy3";

	public string NE4 = "Enemy4";

	public string NE5 = "Enemy5";

	public string NE6 = "Enemy6";

	public string NE7 = "Enemy7";

	public string NE8 = "Enemy8";

	public string NE9 = "Enemy9";

	public string NE10 = "Enemy10";

	public string NE11 = "Enemy11";

	public string NE12 = "Enemy12";

	public string NE13 = "Enemy13";

	public string NE14 = "Enemy14";

	public string NE15 = "Enemy15";

	public string NE16 = "Enemy16";

	public string NE17 = "Enemy17";

	public string NE18 = "Enemy18";

	public string NE19 = "Enemy19";

	public string NE20 = "Enemy20";

	public string NE21 = "Enemy21";

	public string NE11_1 = "Enemy11_1";

	[Header("PBulletName")]
	public string NPBull1 = "PBullet1";

	public string NPBull2 = "PBullet2";

	public string NPBull3 = "PBullet3";

	[Header("EBulletName")]
	public string NEBull1 = "EBullet1";

	public string NEBull2 = "EBullet2";

	public string NEBull9 = "EBullet9";

	public string NEBull12 = "EBullet12";

	public string NEBull14 = "EBullet14";

	public string NEBull15 = "EBullet15";

	public string NEBull16 = "EBullet16";

	public string NEBull17 = "EBullet17";

	public string NEBull18 = "EBullet18";

	public string NEBull19 = "EBullet19";

	public string NEBull20 = "EBullet20";

	public string NEBull21 = "EBullet21";

	public string NEBullN1 = "EBullN1";

	public string NEBullN2 = "EBullN2";

	public string NEBullN3 = "EBullN3";

	public string NEBullN4 = "EBullN4";

	public string NEBullT1 = "EBullT1";

	public string NEBullT2 = "EBullT2";

	public string NEBullT3 = "EBullT3";

	public string NEBullT4 = "EBullT4";

	public string NEBullCombo1 = "EBullCombo1";

	public string NEBullCombo2 = "EBullCombo2";

	[Header("BBulletName")]
	public string NB1Bull1 = "B1Bullet1";

	[Header("ItemName")]
	public string NItemShield = "ShieldItem";

	public string NItemUpgrade = "UpgradeItem";

	public string NItemHealth = "HealthItem";

	public string NItemLaser = "LaserItem";

	public string NItemBomb = "BombItem";

	public string NItemStar = "StarItem";

	[Header("EffectName")]
	public string EffEDie = "EffEDie";

	public string EffBDie = "EffBDie";

	public string EffEBull = "EffEBull";

	public string NEffPBull = "NEffPBull";

	public string NEffPDie = "NEffPDie";

	public string NEffWin = "NEffWin";

	public string NEffBomb = "NEffBomb";

	[Header("GroupPBullet")]
	public Transform GPBullet1;

	public Transform GPBullet2;

	public Transform GPBullet3;

	[Header("GroupEBullet")]
	public Transform GEBullet1;

	public Transform GEBullet2;

	public Transform GEBullet12;

	public Transform GEBullet14;

	public Transform GEBullet15;

	public Transform GEBullet16;

	public Transform GEBullet17;

	public Transform GEBullet18;

	public Transform GEBullet19;

	public Transform GEBullet20;

	public Transform GEBullet21;

	public Transform GEBullN1;

	public Transform GEBullN2;

	public Transform GEBullN3;

	public Transform GEBullN4;

	public Transform GEBullT1;

	public Transform GEBullT2;

	public Transform GEBullT3;

	public Transform GEBullT4;

	public Transform GEBullCombo1;

	public Transform GEBullCombo2;

	[Header("GroupBossBullet")]
	public Transform GB1Bullet1;

	[Header("GroupItem")]
	public Transform GItemShield;

	public Transform GItemBomb;

	public Transform GItemHealth;

	public Transform GItemUpgrade;

	public Transform GItemLaser;

	public Transform GItemStar;

	[Header("GroupEff")]
	public Transform GEffEDie;

	public Transform GEffBDie;

	public Transform GEffEbull;

	public Transform GEffPbull;

	public Transform GEffPDie;

	public Transform GEffWin;

	public Transform GEffBomb;

	[Header("PBullet")]
	public GameObject PBullet1;

	public GameObject PBullet2;

	public GameObject PBullet3;

	[Header("EBullet")]
	public GameObject EBullet1;

	public GameObject EBullet2;

	public GameObject EBullet9;

	public GameObject EBullet12;

	public GameObject EBullet14;

	public GameObject EBullet15;

	public GameObject EBullet16;

	public GameObject EBullet17;

	public GameObject EBullet18;

	public GameObject EBullet19;

	public GameObject EBullet20;

	public GameObject EBullet21;

	public GameObject EBullN1;

	public GameObject EBullN2;

	public GameObject EBullN3;

	public GameObject EBullN4;

	public GameObject EBullT1;

	public GameObject EBullT2;

	public GameObject EBullT3;

	public GameObject EBullT4;

	public GameObject EBullCombo1;

	public GameObject EBullCombo2;

	[Header("BBullet")]
	public GameObject B1Bullet1;

	[Header("ItemPrefabs")]
	public GameObject LaserItem;

	public GameObject ShieldItem;

	public GameObject HealthItem;

	public GameObject BombItem;

	public GameObject Upgrade;

	public GameObject Star;

	[Header("EffectPrefabs")]
	public GameObject EnemyDie;

	public GameObject BossDie;

	public GameObject EBullet;

	public GameObject EffPBullPrefab;

	public GameObject EffPDiePrefab;

	public GameObject EffWinPrefab;

	public GameObject EffBombPrefab;

	private void Awake()
	{
		PoolManager.Instance = this;
	}

	private void OnEnable()
	{
		this.Reset();
	}

	public void Reset()
	{
		this.ResetEnemy();
		this.ResetBulletEnemy();
		this.ResetItem();
		this.ResetEffect();
	}

	public void ResetEnemy()
	{
		for (int i = 0; i < this.GEnemy.Count; i++)
		{
			for (int j = 0; j < this.GEnemy[i].childCount; j++)
			{
				this.GEnemy[i].GetChild(j).gameObject.SetActive(false);
			}
		}
	}

	public void ResetBulletEnemy()
	{
		for (int i = 0; i < this.GEBullet1.childCount; i++)
		{
			this.GEBullet1.GetChild(i).gameObject.SetActive(false);
		}
		for (int j = 0; j < this.GEBullet2.childCount; j++)
		{
			this.GEBullet2.GetChild(j).gameObject.SetActive(false);
		}
		for (int k = 0; k < this.GEBullet12.childCount; k++)
		{
			this.GEBullet12.GetChild(k).gameObject.SetActive(false);
		}
		for (int l = 0; l < this.GEBullet14.childCount; l++)
		{
			this.GEBullet14.GetChild(l).gameObject.SetActive(false);
		}
		for (int m = 0; m < this.GB1Bullet1.childCount; m++)
		{
			this.GB1Bullet1.GetChild(m).gameObject.SetActive(false);
		}
		for (int n = 0; n < this.GEBullN1.childCount; n++)
		{
			this.GEBullN1.GetChild(n).gameObject.SetActive(false);
		}
		for (int num = 0; num < this.GEBullN2.childCount; num++)
		{
			this.GEBullN2.GetChild(num).gameObject.SetActive(false);
		}
		for (int num2 = 0; num2 < this.GEBullN3.childCount; num2++)
		{
			this.GEBullN3.GetChild(num2).gameObject.SetActive(false);
		}
		for (int num3 = 0; num3 < this.GEBullN4.childCount; num3++)
		{
			this.GEBullN4.GetChild(num3).gameObject.SetActive(false);
		}
		for (int num4 = 0; num4 < this.GEBullT1.childCount; num4++)
		{
			this.GEBullT1.GetChild(num4).gameObject.SetActive(false);
		}
		for (int num5 = 0; num5 < this.GEBullT2.childCount; num5++)
		{
			this.GEBullT2.GetChild(num5).gameObject.SetActive(false);
		}
		for (int num6 = 0; num6 < this.GEBullT3.childCount; num6++)
		{
			this.GEBullT3.GetChild(num6).gameObject.SetActive(false);
		}
		for (int num7 = 0; num7 < this.GEBullT4.childCount; num7++)
		{
			this.GEBullT4.GetChild(num7).gameObject.SetActive(false);
		}
		for (int num8 = 0; num8 < this.GEBullCombo1.childCount; num8++)
		{
			this.GEBullCombo1.GetChild(num8).gameObject.SetActive(false);
		}
		for (int num9 = 0; num9 < this.GEBullCombo2.childCount; num9++)
		{
			this.GEBullCombo2.GetChild(num9).gameObject.SetActive(false);
		}
	}

	public void ResetItem()
	{
		for (int i = 0; i < this.GItemShield.childCount; i++)
		{
			this.GItemShield.GetChild(i).gameObject.SetActive(false);
		}
		for (int j = 0; j < this.GItemBomb.childCount; j++)
		{
			this.GItemBomb.GetChild(j).gameObject.SetActive(false);
		}
		for (int k = 0; k < this.GItemHealth.childCount; k++)
		{
			this.GItemHealth.GetChild(k).gameObject.SetActive(false);
		}
		for (int l = 0; l < this.GItemUpgrade.childCount; l++)
		{
			this.GItemUpgrade.GetChild(l).gameObject.SetActive(false);
		}
		for (int m = 0; m < this.GItemLaser.childCount; m++)
		{
			this.GItemLaser.GetChild(m).gameObject.SetActive(false);
		}
		for (int n = 0; n < this.GItemStar.childCount; n++)
		{
			this.GItemStar.GetChild(n).gameObject.SetActive(false);
		}
	}

	public void ResetEffect()
	{
		for (int i = 0; i < this.GEffEDie.childCount; i++)
		{
			this.GEffEDie.GetChild(i).gameObject.SetActive(false);
		}
		for (int j = 0; j < this.GEffBDie.childCount; j++)
		{
			this.GEffBDie.GetChild(j).gameObject.SetActive(false);
		}
		for (int k = 0; k < this.GEffEbull.childCount; k++)
		{
			this.GEffEbull.GetChild(k).gameObject.SetActive(false);
		}
		for (int l = 0; l < this.GEffPbull.childCount; l++)
		{
			this.GEffPbull.GetChild(l).gameObject.SetActive(false);
		}
		for (int m = 0; m < this.GEffPDie.childCount; m++)
		{
			this.GEffPDie.GetChild(m).gameObject.SetActive(false);
		}
		for (int n = 0; n < this.GEffWin.childCount; n++)
		{
			this.GEffWin.GetChild(n).gameObject.SetActive(false);
		}
		for (int num = 0; num < this.GEffBomb.childCount; num++)
		{
			this.GEffBomb.GetChild(num).gameObject.SetActive(false);
		}
	}

	public TASEnemy CreateEnemyHerd(EName enemiesType, int pathIndex, EndFlyStyle moveType, float duration, Vector3 offset, bool flipX = false, bool join = false)
	{
		TASEnemy tASEnemy = null;
		for (int i = 0; i < this.GEnemy[(int)enemiesType].childCount; i++)
		{
			Transform child = this.GEnemy[(int)enemiesType].GetChild(i);
			if (!child.gameObject.activeSelf)
			{
				tASEnemy = child.gameObject.GetComponent<TASEnemy>();
				tASEnemy.gameObject.SetActive(true);
				break;
			}
		}
		if (tASEnemy == null)
		{
			tASEnemy = UnityEngine.Object.Instantiate<TASEnemy>(this.EnemiesPrefab[(int)enemiesType]);
			tASEnemy.transform.SetParent(this.GEnemy[(int)enemiesType]);
			tASEnemy.name = enemiesType.ToString();
		}
		Vector3[] array = (from s in PathController.Instance.Paths[pathIndex].Points
		select s + offset).ToArray<Vector3>();
		if (flipX)
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j].x = -array[j].x;
			}
		}
		tASEnemy.transform.position = array[0];
		switch (moveType)
		{
		case EndFlyStyle.Off:
			tASEnemy.FlyOff(array, duration, join);
			break;
		case EndFlyStyle.Repeat:
			tASEnemy.FlyRepeat(array, duration, join);
			break;
		case EndFlyStyle.Unmoved:
			tASEnemy.FlyUnmoved(array, duration, join);
			break;
		case EndFlyStyle.Around:
			tASEnemy.FlyAround(array, duration, join);
			break;
		case EndFlyStyle.Pingpong:
			tASEnemy.FlyPingpong(array, duration, join);
			break;
		}
		return tASEnemy;
	}

	public TASEnemy CreateEnemySingle(EName enemiesType, int pathIndex, EndFlyStyle moveType, float duration, Vector3 offset, bool flipX = false, bool join = false)
	{
		TASEnemy tASEnemy = null;
		for (int i = 0; i < this.GEnemy[(int)enemiesType].childCount; i++)
		{
			Transform child = this.GEnemy[(int)enemiesType].GetChild(i);
			if (!child.gameObject.activeSelf)
			{
				tASEnemy = child.gameObject.GetComponent<TASEnemy>();
				tASEnemy.gameObject.SetActive(true);
				break;
			}
		}
		if (tASEnemy == null)
		{
			tASEnemy = UnityEngine.Object.Instantiate<TASEnemy>(this.EnemiesPrefab[(int)enemiesType]);
			tASEnemy.transform.SetParent(this.GEnemy[(int)enemiesType]);
			tASEnemy.name = enemiesType.ToString();
		}
		Vector3[] array = (from s in PathController.Instance.Paths[pathIndex].Points
		select s + offset).ToArray<Vector3>();
		if (flipX)
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j].x = -array[j].x;
			}
		}
		tASEnemy.transform.position = array[0];
		switch (moveType)
		{
		case EndFlyStyle.Off:
			tASEnemy.FlyOff(array, duration, join);
			break;
		case EndFlyStyle.Repeat:
			tASEnemy.FlyRepeat(array, duration, join);
			break;
		case EndFlyStyle.Unmoved:
			tASEnemy.FlyUnmoved(array, duration, join);
			break;
		case EndFlyStyle.Around:
			tASEnemy.FlyAround(array, duration, join);
			break;
		case EndFlyStyle.Pingpong:
			tASEnemy.FlyPingpong(array, duration, join);
			break;
		}
		return tASEnemy;
	}

	public void GetBullet(GameObject prefab, string name, Transform group, Vector2 pos, Vector3 rot = default(Vector3))
	{
		GameObject gameObject = null;
		for (int i = 0; i < group.childCount; i++)
		{
			Transform child = group.GetChild(i);
			if (!child.gameObject.activeSelf)
			{
				gameObject = child.gameObject;
				child.gameObject.SetActive(true);
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab).gameObject;
			gameObject.transform.SetParent(group);
			gameObject.name = name;
		}
		gameObject.transform.position = pos;
		gameObject.transform.rotation = Quaternion.Euler(rot);
	}

	public void GetEBullet(GameObject prefab, string name, Transform group, Vector2 pos, Vector3 tar, bool isTarget, int damage)
	{
		GameObject gameObject = null;
		for (int i = 0; i < group.childCount; i++)
		{
			Transform child = group.GetChild(i);
			if (!child.gameObject.activeSelf)
			{
				gameObject = child.gameObject;
				child.gameObject.SetActive(true);
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab).gameObject;
			gameObject.transform.SetParent(group);
			gameObject.name = name;
		}
		gameObject.transform.position = pos;
		gameObject.GetComponent<EBulletCs>().DirectionMath(tar, isTarget);
		gameObject.GetComponent<EBulletCs>().Damage = damage;
		if ((pos.x < 4f & pos.x > -4f & pos.y < 7f & pos.y > -7f) && !GPSManager.Instance.PlayerDie)
		{
			SoundController.Instance.PlaySound(SoundController.Instance.WeaponEnemy, 0.1f);
		}
	}

	public void GetItem(GameObject prefab, string name, Transform group, Vector2 pos)
	{
		GameObject gameObject = null;
		for (int i = 0; i < group.childCount; i++)
		{
			Transform child = group.GetChild(i);
			if (!child.gameObject.activeSelf)
			{
				gameObject = child.gameObject;
				gameObject.gameObject.SetActive(true);
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab).gameObject;
			gameObject.transform.SetParent(group);
			gameObject.name = name;
		}
		gameObject.transform.position = pos;
		if (pos.x >= 0f)
		{
			gameObject.GetComponent<ItemCs>().Direction = Vector3.down + new Vector3(UnityEngine.Random.Range(-0.2f, 0f), 0f);
		}
		else
		{
			gameObject.GetComponent<ItemCs>().Direction = Vector3.down + new Vector3(UnityEngine.Random.Range(0f, 0.2f), 0f);
		}
	}

	public void GetEffect(GameObject prefab, string name, Transform group, Vector2 pos)
	{
		GameObject gameObject = null;
		for (int i = 0; i < group.childCount; i++)
		{
			Transform child = group.GetChild(i);
			if (!child.gameObject.activeSelf)
			{
				gameObject = child.gameObject;
				gameObject.gameObject.SetActive(true);
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab).gameObject;
			gameObject.transform.SetParent(group);
			gameObject.name = name;
		}
		gameObject.transform.position = pos;
	}

	public void EnemyHitBomb()
	{
		for (int i = 0; i < this.GEnemy.Count; i++)
		{
			for (int j = 0; j < this.GEnemy[i].childCount; j++)
			{
				if (this.GEnemy[i].GetChild(j).gameObject.activeInHierarchy)
				{
					this.GEnemy[i].GetChild(j).GetComponent<TASEnemy>().BombHit(TASData.Instance.LstNuclearBomb[TASData.Instance.LstItemPP[5].Lv]);
				}
			}
		}
	}

	public void BulletEnemyHitBomb()
	{
		for (int i = 0; i < this.GEBullet1.childCount; i++)
		{
			if (this.GEBullet1.GetChild(i).gameObject.activeInHierarchy)
			{
				this.GEBullet1.GetChild(i).gameObject.SetActive(false);
			}
		}
		for (int j = 0; j < this.GEBullet2.childCount; j++)
		{
			if (this.GEBullet2.GetChild(j).gameObject.activeInHierarchy)
			{
				this.GEBullet2.GetChild(j).gameObject.SetActive(false);
			}
		}
		for (int k = 0; k < this.GEBullet12.childCount; k++)
		{
			if (this.GEBullet12.GetChild(k).gameObject.activeInHierarchy)
			{
				this.GEBullet12.GetChild(k).gameObject.SetActive(false);
			}
		}
		for (int l = 0; l < this.GEBullet14.childCount; l++)
		{
			if (this.GEBullet14.GetChild(l).gameObject.activeInHierarchy)
			{
				this.GEBullet14.GetChild(l).gameObject.SetActive(false);
			}
		}
		for (int m = 0; m < this.GB1Bullet1.childCount; m++)
		{
			if (this.GB1Bullet1.GetChild(m).gameObject.activeInHierarchy)
			{
				this.GB1Bullet1.GetChild(m).gameObject.SetActive(false);
			}
		}
	}

	private void OnDisable()
	{
	}
}
