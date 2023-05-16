using System;
using UnityEngine;

public class Enemy17 : TASEnemy
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
			this.Pool.GetEBullet(this.Pool.EBullT1, this.Pool.NEBullT1, this.Pool.GEBullT1, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
		else if (this.Id == 1)
		{
			this.Pool.GetEBullet(this.Pool.EBullT2, this.Pool.NEBullT2, this.Pool.GEBullT2, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
		else if (this.Id == 2)
		{
			this.Pool.GetEBullet(this.Pool.EBullT3, this.Pool.NEBullT3, this.Pool.GEBullT3, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
		}
		else if (this.Id == 3)
		{
			this.Pool.GetEBullet(this.Pool.EBullT4, this.Pool.NEBullT4, this.Pool.GEBullT4, this.Gunbarrel.transform.position, position, true, this.DamageBullet);
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
