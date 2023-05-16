using System;
using System.Collections.Generic;
using UnityEngine;

public class testcloud : MonoBehaviour
{
	private MeshRenderer _render;

	public float ScrollSpeed = 1f;

	public List<Material> Cloud;

	public Vector3 CamOldPos;

	public Vector3 CamNewPos;

	private void Awake()
	{
		this._render = base.GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		this._render.material.mainTextureOffset = Vector2.zero;
		int index = UnityEngine.Random.Range(0, this.Cloud.Count);
		this._render.material = this.Cloud[index];
	}

	private void Update()
	{
		this._render.material.mainTextureOffset += Vector2.up * this.ScrollSpeed * Time.deltaTime;
		this.CamNewPos = Camera.main.transform.position;
		if (this.CamNewPos.x > this.CamOldPos.x)
		{
			base.transform.position -= new Vector3((this.CamNewPos.x - this.CamOldPos.x) / 3f, 0f);
			this.CamOldPos = this.CamNewPos;
		}
		else if (this.CamNewPos.x < this.CamOldPos.x)
		{
			base.transform.position += new Vector3((this.CamOldPos.x - this.CamNewPos.x) / 3f, 0f);
			this.CamOldPos = this.CamNewPos;
		}
		if (this.CamNewPos.x == 0f)
		{
			base.transform.position = Vector3.zero;
		}
	}

	private void OnDisable()
	{
		base.transform.position = Vector3.zero;
	}
}
