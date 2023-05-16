using System;
using UnityEngine;

public class EBulletCs : MonoBehaviour
{
	public Vector3 Direction;

	public float Speed;

	public GameObject Sprite;

	public int Damage;

	public Transform Trns;

	private void Awake()
	{
		this.Trns = base.transform;
	}

	public virtual void OnEnable()
	{
		base.InvokeRepeating("CheckOff", 0f, 0.5f);
	}

	public void CheckOff()
	{
		if (this.Trns.position.x > 4f || this.Trns.position.x < -4f || this.Trns.position.y > 7f || this.Trns.position.y < -7f)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void DirectionMath(Vector3 tar, bool isTarget)
	{
		if (isTarget)
		{
			this.Direction = tar - base.transform.position;
		}
		else
		{
			this.Direction = tar;
		}
		float angle = Mathf.Atan2(this.Direction.y, this.Direction.x) * 57.29578f + 90f;
		this.Sprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		this.Direction.Normalize();
	}

	public virtual void Update()
	{
		base.transform.Translate(this.Direction * this.Speed * Time.deltaTime);
	}

	private void OnDisable()
	{
	}
}
