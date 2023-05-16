using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TASManagerUI : MonoBehaviour
{
	public static TASManagerUI Instance;

	public List<GameObject> LstScenes;

	public GameObject Bg;

	public Transform CameraContainer;

	public float SDuration = 1f;

	public float SLeng = 0.3f;

	public int SVibrato = 10;

	public Transform Grid;

	public Vector3 GridOldpos;

	public GameObject MapLvScene;

	public GameObject UpgradeLvScene;

	public GameObject GamePlayScene;

	private void Awake()
	{
		TASManagerUI.Instance = this;
        Application.targetFrameRate = 60;
	}

	private void OnEnable()
	{
		this.ChangeScenes(0);
	}

	private void Start()
	{
		base.GetComponent<Canvas>().worldCamera = Camera.main;
		this.CameraContainer = new GameObject("CameraContainer").transform;
		Camera.main.transform.SetParent(this.CameraContainer);
		this.GridOldpos = this.Grid.position;
	}

	private void Update()
	{
		this.Grid.position = Vector3.MoveTowards(this.Grid.position, this.GridOldpos + new Vector3(Input.acceleration.x, Input.acceleration.y), Time.deltaTime);
	}

	public void ShakeCamera()
	{
		this.CameraContainer.DOKill(false);
		this.CameraContainer.DOShakePosition(this.SDuration, this.SLeng, this.SVibrato, 90f, false, true).OnComplete(delegate
		{
			this.CameraContainer.position = new Vector3(0f, 0f, 0f);
		});
	}

	public void ChangeScenes(int id)
	{
		Camera.main.transform.position = new Vector3(0f, 0f, -10f);
		for (int i = 0; i < this.LstScenes.Count; i++)
		{
			if (i == id)
			{
				this.LstScenes[i].SetActive(true);
			}
			else
			{
				this.LstScenes[i].SetActive(false);
			}
		}
	}
}
