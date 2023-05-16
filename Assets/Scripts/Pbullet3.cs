using System;
using UnityEngine;

public class Pbullet3 : PBulletCs
{
	public Transform Target;

	public float max_velocity = 1f;

	public float mass = 1f;

	public float max_speed = 1f;

	public float max_force = 1f;

	public Vector3 velocity;

	public Vector3 desired_velocity;

	public Vector3 steering;

	private Vector3 oldPos;

	public float TimeFollow = 3f;

	public float Rad;

	public LayerMask LayerEnemy;

	public override void OnEnable()
	{
		base.OnEnable();
		this.Damage = TASData.Instance.LstWingMissleDamage[TASData.Instance.LstItemPP[4].Lv];
		base.transform.rotation = Quaternion.identity;
		this.Target = null;
	}

	public void Update()
	{
		if (CheckEnemyCam.Instance.HaveEnemy)
		{
			RaycastHit2D raycastHit2D = Physics2D.CircleCast(base.transform.position, this.Rad, Vector2.one, 0.1f, this.LayerEnemy);
			if (raycastHit2D.collider != null)
			{
				if (raycastHit2D.collider.tag == "Enemy")
				{
					if (!this.Target || !this.Target.gameObject.activeInHierarchy || (this.Target.gameObject.GetComponent<TASEnemy>() && !this.Target.gameObject.GetComponent<TASEnemy>().IsLive))
					{
						this.Target = raycastHit2D.collider.transform;
					}
				}
				else if (raycastHit2D.collider.tag == "GunBoss" && (!this.Target || !this.Target.gameObject.activeInHierarchy || (this.Target.gameObject.GetComponent<GunBossCs>() && this.Target.gameObject.GetComponent<GunBossCs>().CurHealth < 0f)))
				{
					this.Target = raycastHit2D.collider.transform;
				}
			}
		}
		base.transform.Translate(Vector3.up * Time.deltaTime * 5f);
		if (this.Target && this.Target.gameObject.activeInHierarchy && ((this.Target.gameObject.GetComponent<TASEnemy>() && this.Target.gameObject.GetComponent<TASEnemy>().IsLive && this.Target.gameObject.GetComponent<TASEnemy>().GunBossLive <= 0) || (this.Target.gameObject.GetComponent<GunBossCs>() && this.Target.gameObject.GetComponent<GunBossCs>().CurHealth > 0f)))
		{
			Vector2 vector = this.Target.position - base.transform.position;
			float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f - 90f;
			Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * 3f);
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		this.Target = null;
	}
}
