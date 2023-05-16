using System;
using UnityEngine;

public class PBulletCs : MonoBehaviour
{
	public float Damage;

	public float Speed;

	public Vector3 Direct;

	public Transform Trns;

	private void Awake()
	{
		this.Trns = base.transform;
	}

	public virtual void OnEnable()
	{
		base.InvokeRepeating("CheckOff", 0f, 0.1f);
	}

	public void CheckOff()
	{
		if (this.Trns.position.x > 4f || this.Trns.position.x < -4f || this.Trns.position.y > 6f || this.Trns.position.y < -6f)
		{
			base.gameObject.SetActive(false);
		}
	}

	public virtual void OnDisable()
	{
		base.CancelInvoke();
	}
}
