using System;
using UnityEngine;

public class EBullet2 : EBulletCs
{
	public Transform Target;

	public float max_velocity = 1f;

	public float mass = 1f;

	public float max_speed = 1f;

	public float max_force = 1f;

	public Vector3 velocity;

	public Vector3 desired_velocity;

	public Vector3 steering;

	public float MaxTimeTarget;

	public float TimeTarget;

	private Vector3 oldPos;

	public override void OnEnable()
	{
		base.OnEnable();
		this.TimeTarget = this.MaxTimeTarget;
		this.Target = TASPlayerControl.Instance.transform;
	}

	public override void Update()
	{
		this.TimeTarget -= 0.02f;
		if (this.TimeTarget > 0f)
		{
			this.desired_velocity = (TASPlayerControl.Instance.transform.position - base.transform.position).normalized * this.max_velocity;
		}
		this.steering = this.desired_velocity - this.velocity;
		this.steering.Normalize();
		this.steering *= this.max_force;
		this.steering /= this.mass;
		this.velocity += this.steering;
		this.velocity.Normalize();
		this.velocity *= this.max_speed;
		base.transform.position += this.velocity * Time.deltaTime;
		if (Vector2.Distance(base.transform.position, this.oldPos) > 0.1f)
		{
			base.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(this.oldPos.y - base.transform.position.y, this.oldPos.x - base.transform.position.x) * 57.29578f - 90f);
			this.oldPos = base.transform.position;
		}
	}
}
