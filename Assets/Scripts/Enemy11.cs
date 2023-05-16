using System;
using UnityEngine;

public class Enemy11 : TASEnemy
{
	public override void OnEnable()
	{
		base.OnEnable();
		base.Invoke("Shoot", UnityEngine.Random.Range(1f, 3f));
	}

	public void Shoot()
	{
		Vector3 position = TASPlayerControl.Instance.transform.position;
		this.Pool.GetEBullet(this.Pool.EBullet2, this.Pool.NEBull2, this.Pool.GEBullet2, this.Trns.position, position, true, this.DamageBullet);
	}
}
