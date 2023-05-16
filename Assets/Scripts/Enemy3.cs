using System;
using UnityEngine;

public class Enemy3 : TASEnemy
{
	public override void OnEnable()
	{
		base.OnEnable();
		int num = UnityEngine.Random.Range(0, 100);
		if (num >= 1 && num < 80)
		{
			base.Invoke("Shoot", UnityEngine.Random.Range(0f, 2f));
		}
	}

	public void Shoot()
	{
		Vector3 position = TASPlayerControl.Instance.transform.position;
		this.Pool.GetEBullet(this.Pool.EBullN2, this.Pool.NEBullN2, this.Pool.GEBullN2, this.Trns.position, position, true, this.DamageBullet);
	}
}
