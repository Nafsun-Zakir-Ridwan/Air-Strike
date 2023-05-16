using DG.Tweening;
using System;
using UnityEngine;

public class ItemCs : MonoBehaviour
{
	public int IdItem;

	public float MaxSpeedRandom;

	public float SpeedRandom;

	public Vector3 Direction;

	public bool IsMagnet;

	public bool IsScale;

	public Transform Trns;

	public Rigidbody2D Rigd;

	private void Awake()
	{
		this.Trns = base.transform;
		this.Rigd = base.GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		this.Rigd.AddForce(new Vector2(UnityEngine.Random.Range(-50f, 51f), UnityEngine.Random.Range(100f, 200f)));
		this.Rigd.gravityScale = 1f;
		this.SpeedRandom = this.MaxSpeedRandom;
		this.IsMagnet = false;
		base.transform.localScale = new Vector3(1f, 1f);
		this.IsScale = false;
		base.InvokeRepeating("CheckOff", 0f, 0.5f);
	}

	public void CheckOff()
	{
		if (this.Trns.position.x > 6f || this.Trns.position.x < -6f || this.Trns.position.y < -8f)
		{
			base.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (this.SpeedRandom > 1.5f)
		{
			this.SpeedRandom -= 0.005f;
		}
		if (this.IsMagnet)
		{
			this.Rigd.gravityScale = 0f;
			base.transform.position = Vector3.MoveTowards(base.transform.position, TASPlayerControl.Instance.transform.position, 3f * Time.deltaTime);
		}
	}

	public void ScaleItem()
	{
		if (!this.IsScale)
		{
			this.IsScale = true;
			base.transform.DOScale(1.5f, 0.2f).SetEase(Ease.InQuad).OnComplete(delegate
			{
				base.transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuad).OnComplete(delegate
				{
					base.gameObject.SetActive(false);
				});
			});
		}
	}
}
