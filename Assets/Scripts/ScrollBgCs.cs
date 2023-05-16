using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBgCs : MonoBehaviour
{
	private MeshRenderer _render;

	public float ScrollSpeed = 1f;

	public List<Material> Earth;

	public List<Material> Moon;

	public List<Material> Mars;

	public List<Material> Sun;

	public GameObject CloudScroll;

	public GameObject Rain;

	public GameObject Snow;

	public GameObject Cloud;

	private void Awake()
	{
		this._render = base.GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		this.CloudScroll.transform.position = Vector3.zero;
		this.ChangeBg();
		this.ChangeWeather();
		this._render.material.mainTextureOffset = Vector2.zero;
	}

	public void ChangeWeather()
	{
		if (UnityEngine.Random.Range(0, 3) == 0)
		{
			this.CloudScroll.SetActive(true);
		}
		else
		{
			this.CloudScroll.SetActive(false);
		}
	}

	public void ChangeBg()
	{
		if (TASData.Instance.Intro == 0)
		{
			this._render.material = this.Moon[0];
			this.Snow.SetActive(false);
			this.Cloud.SetActive(false);
			this.Rain.SetActive(true);
		}
		else
		{
			int @int = PlayerPrefs.GetInt("Stage", 1);
			int int2 = PlayerPrefs.GetInt("Lv", 1);
			if (@int == 1)
			{
				this._render.material = this.Earth[int2 - 1];
				this.Snow.SetActive(false);
				this.Rain.SetActive(false);
				if (UnityEngine.Random.Range(0, 3) == 0)
				{
					this.Cloud.SetActive(true);
				}
				else
				{
					this.Cloud.SetActive(false);
				}
			}
			else if (@int == 2)
			{
				this._render.material = this.Moon[int2 - 1];
				this.Snow.SetActive(false);
				this.Cloud.SetActive(false);
				if (UnityEngine.Random.Range(0, 2) == 0)
				{
					this.Rain.SetActive(true);
				}
				else
				{
					this.Rain.SetActive(false);
				}
			}
			else if (@int == 3)
			{
				this._render.material = this.Mars[int2 - 1];
			}
			else if (@int == 4)
			{
				this._render.material = this.Sun[int2 - 1];
			}
		}
	}

	private void Update()
	{
		this._render.material.mainTextureOffset += Vector2.up * this.ScrollSpeed * Time.deltaTime;
	}
}
