using System;
using UnityEngine;

public class Enemy19 : TASEnemy
{
	public override void OnEnable()
	{
		base.OnEnable();
		base.InvokeRepeating("Shoot", UnityEngine.Random.Range(1f, 2f), 2f);
	}

	public void Shoot()
	{
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(0f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(0f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(1f, 0f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(-1f, 0f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(1f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(1f, -1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(-1f, 1f), false, this.DamageBullet);
		this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, base.transform.position, new Vector3(-1f, -1f), false, this.DamageBullet);
	}
}
