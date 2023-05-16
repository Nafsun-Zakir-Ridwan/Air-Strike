using System;
using UnityEngine;

public class Boss4 : TASEnemy
{
	public GameObject Gunbarrel0;

	public GameObject Gunbarrel1;

	public GameObject Gunbarrel2;

	public GameObject Gunbarrel3;

	public GameObject Gunbarrel4;

	public GameObject Gunbarrel5;

	public GameObject Gunbarrel6;

	public GameObject Gunbarrel7;

	public GameObject Gunbarrel8;

	public Transform PosGun0;

	public Transform PosGun1;

	public Transform PosGun2;

	public Transform PosGun3;

	public Transform PosGun4;

	public Transform PosGun5;

	public Transform PosGun6;

	public Transform PosGun7;

	public Transform PosGun8;

	public GameObject HpGun6;

	public GameObject HpGun7;

	public GameObject HpGun8;

	public GameObject HpGun9;

	public override void OnEnable()
	{
		base.OnEnable();
		for (int i = 0; i < this.LstGunBoss.Count; i++)
		{
			this.LstGunBoss[i].gameObject.SetActive(true);
			this.LstGunBoss[i].IsNew = true;
			this.LstGunBoss[i].NewGun();
		}
		this.LstGunBoss[5].gameObject.SetActive(false);
		this.LstGunBoss[6].gameObject.SetActive(false);
		this.LstGunBoss[7].gameObject.SetActive(false);
		this.LstGunBoss[8].gameObject.SetActive(false);
		this.HpGun6.SetActive(false);
		this.HpGun7.SetActive(false);
		this.HpGun8.SetActive(false);
		this.HpGun9.SetActive(false);
		this.ShootPlayer();
	}

	private new void Update()
	{
	}

	public void ShootPlayer()
	{
	}

	public void ShootGun0()
	{
		if (this.LstGunBoss[0].CurHealth > 0f)
		{
			Vector3 position = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullN1, this.Pool.NEBullN1, this.Pool.GEBullN1, this.PosGun0.position, position, true, this.DamageBullet);
		}
	}

	public void ShootGun1()
	{
		if (this.LstGunBoss[1].CurHealth > 0f)
		{
			Vector3 position = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullN1, this.Pool.NEBullN1, this.Pool.GEBullN1, this.PosGun1.position, position, true, this.DamageBullet);
		}
	}

	public void ShootGun2()
	{
		if (this.LstGunBoss[2].CurHealth > 0f)
		{
			Vector3 tar = this.Gunbarrel2.transform.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.EBullCombo1, this.Pool.NEBullCombo1, this.Pool.GEBullCombo1, this.PosGun2.position, tar, false, this.DamageBullet);
		}
	}

	public void ShootGun3()
	{
		if (this.LstGunBoss[3].CurHealth > 0f)
		{
			Vector3 tar = this.Gunbarrel3.transform.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.EBullCombo1, this.Pool.NEBullCombo1, this.Pool.GEBullCombo1, this.PosGun3.position, tar, false, this.DamageBullet);
		}
	}

	public void ShootGun4()
	{
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(0f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(0f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(1f, 0f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(-1f, 0f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(1f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(1f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(-1f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.PosGun4.position, new Vector3(-1f, -1f), false, this.DamageBullet);
	}

	public void ShootGun5()
	{
	}

	public void ShootGun6()
	{
	}

	public void ShootGun7()
	{
	}

	public void ShootGun8()
	{
	}
}
