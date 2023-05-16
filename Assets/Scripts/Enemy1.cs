using System;
using UnityEngine;

public class Enemy1 : TASEnemy
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
		this.Pool.GetEBullet(this.Pool.EBullN4, this.Pool.NEBullN4, this.Pool.GEBullN4, this.Trns.position, position, true, this.DamageBullet);
	}
}
