using System;
using UnityEngine;

public class Enemy20 : TASEnemy
{
	public override void OnEnable()
	{
		base.OnEnable();
		base.InvokeRepeating("Shoot", UnityEngine.Random.Range(1f, 2f), 2f);
	}

	public void Shoot()
	{
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(0f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(0f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(1f, 0f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-1f, 0f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(1f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(1f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-1f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-1f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(1f, 2f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(2f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(2f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(1f, -2f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-1f, -2f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-2f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-2f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, base.transform.position, new Vector3(-1f, 2f), false, this.DamageBullet);
	}
}
