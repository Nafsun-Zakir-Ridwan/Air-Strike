using System;
using UnityEngine;

public class StartGameCs : MonoBehaviour
{
	public GameObject eff1;

	public GameObject eff2;

	public GameObject Hand;

	private void OnEnable()
	{
		if (TASData.Instance.Intro == 0)
		{
			this.Hand.SetActive(true);
		}
		else
		{
			this.Hand.SetActive(false);
		}
		this.eff1.SetActive(true);
		this.eff2.SetActive(false);
	}

	private void OnMouseDown()
	{
		if (!TASPlayerMove.Instance.IsStart)
		{
			this.eff1.SetActive(false);
			this.eff2.SetActive(true);
			this.Hand.SetActive(false);
			GPSManager.Instance.ShowSelectItem(true, true);
			base.Invoke("StartGame", 0.1f);
		}
	}

	public void StartGame()
	{
		TASPlayerMove.Instance.IsStart = true;
		TASMapManager.Instance.StartGame(TASData.Instance.LvSelect, false);
	}
}
