using System;
using UnityEngine;

public class ShieldPlayerCS : MonoBehaviour
{
	public LayerMask LayerEBullet;

	public float Rad;

	private void Update()
	{
		this.CheckBulletEnemy();
	}

	public void CheckBulletEnemy()
	{
		RaycastHit2D raycastHit2D = Physics2D.CircleCast(base.transform.position, this.Rad, Vector2.one, 0.1f, this.LayerEBullet);
		if (raycastHit2D.collider != null)
		{
			raycastHit2D.collider.gameObject.SetActive(false);
		}
	}
}
