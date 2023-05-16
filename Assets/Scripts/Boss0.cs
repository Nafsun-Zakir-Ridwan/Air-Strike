using System;
using UnityEngine;

public class Boss0 : TASEnemy
{
	public GameObject Gunbarrel0;

	public GameObject Gunbarrel1;

	public GameObject Gunbarrel2;

	public GameObject Gunbarrel3;

	public GameObject Gunbarrel4;

	public Transform PosGun0;

	public Transform PosGun1;

	public Transform PosGun2;

	public Transform PosGun3;

	public Transform PosGun4;

	public GameObject Lightning1;

	public GameObject Lightning2;

	public override void OnEnable()
	{
		base.OnEnable();
		for (int i = 0; i < this.LstGunBoss.Count; i++)
		{
			this.LstGunBoss[i].gameObject.SetActive(true);
			this.LstGunBoss[i].IsNew = true;
			this.LstGunBoss[i].NewGun();
		}
		this.ShootPlayer();
	}

	public override void Update()
	{
		base.Update();
		if (this.Gunbarrel2.gameObject.activeInHierarchy)
		{
			Vector3 vector = TASPlayerControl.Instance.transform.position - this.Gunbarrel2.transform.position;
			float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
			Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
			this.Gunbarrel2.transform.rotation = Quaternion.Slerp(this.Gunbarrel2.transform.rotation, b, Time.deltaTime * 10f);
		}
		if (this.Gunbarrel3.transform.gameObject.activeInHierarchy)
		{
			Vector3 vector2 = TASPlayerControl.Instance.transform.position - this.Gunbarrel3.transform.position;
			float angle2 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f + 90f;
			Quaternion b2 = Quaternion.AngleAxis(angle2, Vector3.forward);
			this.Gunbarrel3.transform.rotation = Quaternion.Slerp(this.Gunbarrel3.transform.rotation, b2, Time.deltaTime * 10f);
		}
	}

	public void ShootPlayer()
	{
		base.InvokeRepeating("ShootGun0", 5f, 0.5f);
		base.InvokeRepeating("ShootGun1", 5f, 0.5f);
		base.InvokeRepeating("ShootGun2", 6f, 5f);
		base.InvokeRepeating("ShootGun3", 6f, 5f);
		base.InvokeRepeating("ShootGun4", 9f, 5f);
	}

	public void ShootGun0()
	{
		if (this.LstGunBoss[0].CurHealth > 0f)
		{
			Vector3 tar = this.Gunbarrel0.transform.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.EBullCombo1, this.Pool.NEBullCombo1, this.Pool.GEBullCombo1, this.PosGun0.position, tar, false, this.DamageBullet);
		}
	}

	public void ShootGun1()
	{
		if (this.LstGunBoss[1].CurHealth > 0f)
		{
			Vector3 tar = this.Gunbarrel1.transform.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.EBullCombo1, this.Pool.NEBullCombo1, this.Pool.GEBullCombo1, this.PosGun1.position, tar, false, this.DamageBullet);
		}
	}

	public void ShootGun2()
	{
		if (this.LstGunBoss[2].CurHealth > 0f)
		{
			Vector3 position = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullN1, this.Pool.NEBullN1, this.Pool.GEBullN1, this.PosGun2.position, position, true, this.DamageBullet);
		}
	}

	public void ShootGun3()
	{
		if (this.LstGunBoss[3].CurHealth > 0f)
		{
			Vector3 position = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullN1, this.Pool.NEBullN1, this.Pool.GEBullN1, this.PosGun3.position, position, true, this.DamageBullet);
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

	public void ShowLaser()
	{
		this.Lightning1.SetActive(true);
		this.Lightning2.SetActive(true);
	}

	public override void GunBossDie(int id, bool changeGun)
	{
		base.GunBossDie(id, changeGun);
		if (this.GunBossLive < 3)
		{
			this.ShowLaser();
		}
	}
}
