using System;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
	public static PopupManager Instance;

	public GameObject SettingScene;

	public GameObject PauseScene;

	public GameObject EndGameScene;

	public int CurSceneId;

	public void Awake()
	{
		PopupManager.Instance = this;
	}

	public void ShowEnd(bool isWin)
	{
		this.EndGameScene.SetActive(true);
		this.EndGameScene.GetComponent<ESManager>().Show(isWin);
	}
}
