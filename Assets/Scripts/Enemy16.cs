using System;
using UnityEngine;

public class Enemy16 : TASEnemy
{
	public int Id;

	public GameObject Gunbarrel;

	public override void OnEnable()
	{
		base.OnEnable();
		base.InvokeRepeating("Shoot", UnityEngine.Random.Range(1f, 2f), 2f);
	}

	public void Shoot()
	{
		Vector3 position = TASPlayerControl.Instance.transform.position;
		if (this.Id == 0)
		{
			this.Pool.GetEBullet(this.Pool.EBullN1, this.Pool.NEBullN1, this.Pool.GEBullN1, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
		else if (this.Id == 1)
		{
			this.Pool.GetEBullet(this.Pool.EBullN2, this.Pool.NEBullN2, this.Pool.GEBullN2, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
		else if (this.Id == 2)
		{
			this.Pool.GetEBullet(this.Pool.EBullN3, this.Pool.NEBullN3, this.Pool.GEBullN3, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
		else if (this.Id == 3)
		{
			this.Pool.GetEBullet(this.Pool.EBullN4, this.Pool.NEBullN4, this.Pool.GEBullN4, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
	}

	public override void Update()
	{
		base.Update();
		Vector3 vector = TASPlayerControl.Instance.transform.position - base.transform.position;
		float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
		Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
		this.Gunbarrel.transform.rotation = Quaternion.Slerp(this.Gunbarrel.transform.rotation, b, Time.deltaTime * 10f);
	}
}
