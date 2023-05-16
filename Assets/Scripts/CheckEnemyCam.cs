using System;
using UnityEngine;

public class CheckEnemyCam : MonoBehaviour
{
	public static CheckEnemyCam Instance;

	public LayerMask LayerEnemy;

	public bool HaveEnemy;

	private void Awake()
	{
		CheckEnemyCam.Instance = this;
	}

	private void Update()
	{
		this.CheckEnemy();
	}

	public void CheckEnemy()
	{
		if (Physics2D.BoxCast(base.transform.position, new Vector2(6f, 11f), 0f, Vector2.zero, 1f, this.LayerEnemy).collider != null)
		{
			this.HaveEnemy = true;
		}
		else
		{
			this.HaveEnemy = false;
		}
	}
}
