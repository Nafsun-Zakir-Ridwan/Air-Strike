using System;
using UnityEngine;

public class Enemy4 : TASEnemy
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
		this.Pool.GetEBullet(this.Pool.EBullN1, this.Pool.NEBullN1, this.Pool.GEBullN1, this.Trns.position, position, true, this.DamageBullet);
	}
}
