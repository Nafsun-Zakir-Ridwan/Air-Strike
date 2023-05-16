using System;
using UnityEngine;

public class GunBossCs : MonoBehaviour
{
	public int IdGun;

	public float MaxHealth;

	public float CurHealth;

	public TASEnemy Parent;

	public Transform Trns;

	public float Rad;

	public LayerMask LayerBullet;

	public bool ChangeGun;

	public bool IsNew;

	public GameObject HpGun;

	public GameObject Hp;

	public TextMesh TxtHp;

	public void Awake()
	{
		this.Trns = base.transform;
	}

	private void OnEnable()
	{
	}

	public void NewGun()
	{
		if (this.IsNew)
		{
			this.CurHealth = this.MaxHealth;
			this.IsNew = false;
			this.HpGun.SetActive(true);
			this.CheckHpGun();
		}
	}

	public void Update()
	{
		this.CheckBulletPlayer();
	}

	public void CheckHpGun()
	{
		this.Hp.transform.localScale = new Vector3(this.CurHealth / this.MaxHealth, 1f, 1f);
		this.TxtHp.text = (int)(this.CurHealth * 100f / this.MaxHealth) + "%";
	}

	public void CheckBulletPlayer()
	{
		RaycastHit2D raycastHit2D = Physics2D.CircleCast(this.Trns.position, this.Rad, Vector2.one, 0.1f, this.LayerBullet);
		if (raycastHit2D.collider != null)
		{
			PoolManager.Instance.GetEffect(PoolManager.Instance.EffPBullPrefab, PoolManager.Instance.NEffPBull, PoolManager.Instance.GEffPbull, raycastHit2D.collider.transform.position);
			raycastHit2D.collider.gameObject.SetActive(false);
			this.CurHealth -= 1f;
			this.CheckHpGun();
			if (this.CurHealth <= 0f)
			{
				this.GunDie();
			}
		}
	}

	public void LaserHit(float damage)
	{
		this.CurHealth -= damage;
		this.CheckHpGun();
		if (this.CurHealth <= 0f)
		{
			this.GunDie();
		}
	}

	public void BombHit(int damage)
	{
		this.CurHealth -= (float)damage;
		if (this.CurHealth <= 0f)
		{
			this.GunDie();
		}
	}

	public void GunDie()
	{
		PoolManager.Instance.GetEffect(PoolManager.Instance.EnemyDie, PoolManager.Instance.EffEDie, PoolManager.Instance.GEffEDie, base.transform.position);
		this.Parent.GunBossDie(this.IdGun, this.ChangeGun);
		this.HpGun.SetActive(false);
		base.gameObject.SetActive(false);
	}
}
