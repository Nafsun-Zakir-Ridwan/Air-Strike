using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
	private sealed class _TextTutorialStart_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GPSManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _TextTutorialStart_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.TxtTutorial.text = string.Empty;
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Tutorial.SetActive(true);
				this._this.TxtTutorial.DOText("Now, Tap to Fighter.", 2f, true, ScrambleMode.None, null);
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.Tutorial.SetActive(false);
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _TextTutorialEnd_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GPSManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _TextTutorialEnd_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.TxtTutorial.text = string.Empty;
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Tutorial.SetActive(true);
				this._this.TxtTutorial.DOText("Now, we back to map level.", 2f, true, ScrambleMode.None, null);
				this._current = new WaitForSeconds(4f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._this.Tutorial.SetActive(false);
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				PlayerPrefs.SetInt("Coin", 100);
				TASPlayerControl.Instance.ShowMapLevel();
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _ChangeTxtScore1_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GPSManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _ChangeTxtScore1_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (int.Parse(this._this.TxtScore.text) < this._this.Score)
			{
				this._this.TxtScore.text = (int.Parse(this._this.TxtScore.text) + 1).ToString().PadLeft(6, '0');
				this._current = new WaitForSeconds(0.001f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static GPSManager Instance;

	public GameObject GamePlay;

	public GameObject Player;

	public bool IsPause;

	public int NumShield;

	public int NumLaser;

	public int NumBomb;

	public GameObject SelectItem;

	public Text Health1Player;

	public Text Health2Player;

	public Text TxtScore;

	public int Score;

	public GameObject PanelBlack;

	public GameObject BtnPause;

	public GameObject Tutorial;

	public Text TxtTutorial;

	public bool Tut;

	public GameObject MissionComplete;

	public GameObject MissionFaled;

	public SkeletonAnimation MissionAni;

	public bool PlayerDie;

	public void Awake()
	{
		GPSManager.Instance = this;
	}

	private void OnEnable()
	{
		this.ResetGameplay();
		if (TASData.Instance.Intro == 0)
		{
			this.ShowTutorial(true);
			this.Tut = true;
		}
		else
		{
			this.Tutorial.SetActive(false);
			this.Tut = false;
		}
		if (Advertisements.Instance && Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.HideBanner();
		}
	}

	public void ShowTutorial(bool start)
	{
		if (start)
		{
			base.StartCoroutine(this.TextTutorialStart());
		}
		else
		{
			base.StartCoroutine(this.TextTutorialEnd());
		}
	}

	private IEnumerator TextTutorialStart()
	{
		GPSManager._TextTutorialStart_c__Iterator0 _TextTutorialStart_c__Iterator = new GPSManager._TextTutorialStart_c__Iterator0();
		_TextTutorialStart_c__Iterator._this = this;
		return _TextTutorialStart_c__Iterator;
	}

	private IEnumerator TextTutorialEnd()
	{
		GPSManager._TextTutorialEnd_c__Iterator1 _TextTutorialEnd_c__Iterator = new GPSManager._TextTutorialEnd_c__Iterator1();
		_TextTutorialEnd_c__Iterator._this = this;
		return _TextTutorialEnd_c__Iterator;
	}

	public void LevelComplete()
	{
		if (!this.PlayerDie)
		{
			this.MissionAni.gameObject.SetActive(true);
			this.MissionAni.initialSkinName = "win";
			this.MissionAni.Initialize(true);
			this.MissionAni.AnimationName = "animation";
			this.MissionAni.loop = false;
			this.HideScoreAndBtnPause();
			base.Invoke("FighterFly", 2f);
		}
	}

	public void FighterFly()
	{
		if (!this.PlayerDie)
		{
			TASPlayerMove.Instance.FlyWin();
		}
	}

	public void LevelFailed()
	{
		this.MissionAni.gameObject.SetActive(true);
		this.MissionAni.initialSkinName = "lose";
		this.MissionAni.Initialize(true);
		this.MissionAni.AnimationName = "animation";
		this.MissionAni.loop = false;
		base.Invoke("ShowEnd", 2f);
	}

	public void ShowEnd()
	{
		Time.timeScale = 1f;
		if (Advertisements.Instance.IsInterstitialAvailable())
		{
			Advertisements.Instance.ShowInterstitial(new UnityAction(this.ShowEnd2));
		}
		else
		{
			this.ShowEnd2();
		}
	}

	public void ShowEnd2()
	{
		PopupManager.Instance.ShowEnd(false);
	}

	private void Update()
	{
		this.SelectItem.transform.position = TASPlayerControl.Instance.transform.position;
		if (this.SelectItem.transform.position.x > Camera.main.transform.position.x + 1f)
		{
			this.SelectItem.transform.position = new Vector3(Camera.main.transform.position.x + 1.5f, TASPlayerControl.Instance.transform.position.y);
		}
		else if (this.SelectItem.transform.position.x < Camera.main.transform.position.x - 1f)
		{
			this.SelectItem.transform.position = new Vector3(Camera.main.transform.position.x - 1.5f, TASPlayerControl.Instance.transform.position.y);
		}
		this.Health1Player.transform.position = this.SelectItem.transform.position + new Vector3(0f, 1f, 0f);
	}

	public void ResetGameplay()
	{
		this.TxtScore.text = "000000";
		int @int = PlayerPrefs.GetInt("Stage", 1);
		int int2 = PlayerPrefs.GetInt("Lv", 1);
		this.PlayerDie = false;
		this.MissionComplete.transform.localScale = new Vector3(0f, 0f);
		this.MissionFaled.transform.localScale = new Vector3(0f, 0f);
		this.MissionAni.gameObject.SetActive(false);
		if (TASData.Instance.Intro == 0)
		{
			this.GamePlay.SetActive(false);
			Time.timeScale = 1f;
			this.IsPause = false;
			this.GamePlay.SetActive(true);
			this.Player.SetActive(true);
			this.NumShield = 5;
			this.NumLaser = 5;
			this.NumBomb = 5;
			this.Score = 0;
			this.ChangeTxtScore();
			int index = UnityEngine.Random.Range(0, 6);
			SoundController.Instance.PlayMusic(SoundController.Instance.Bg[index], 0.5f, true);
			this.HideScoreAndBtnPause();
		}
		else
		{
			this.GamePlay.SetActive(false);
			Time.timeScale = 1f;
			this.IsPause = false;
			this.GamePlay.SetActive(true);
			this.Player.SetActive(true);
			this.NumShield = 0;
			this.NumLaser = 0;
			this.NumBomb = 0;
			this.Score = 0;
			this.ChangeTxtScore();
			int index2 = UnityEngine.Random.Range(0, 6);
			SoundController.Instance.PlayMusic(SoundController.Instance.Bg[index2], 0.5f, true);
			this.OpenScoreAndBtnPause();
		}
		this.SelectItem.GetComponent<BuyItemCs>().LoadItem();
	}

	public void HideScoreAndBtnPause()
	{
		this.BtnPause.SetActive(false);
		this.TxtScore.gameObject.SetActive(false);
	}

	public void OpenScoreAndBtnPause()
	{
		this.BtnPause.SetActive(true);
		this.TxtScore.gameObject.SetActive(true);
	}

	public void ChangeTxtScore()
	{
		base.StopCoroutine(this.ChangeTxtScore1());
		base.StartCoroutine(this.ChangeTxtScore1());
	}

	private IEnumerator ChangeTxtScore1()
	{
		GPSManager._ChangeTxtScore1_c__Iterator2 _ChangeTxtScore1_c__Iterator = new GPSManager._ChangeTxtScore1_c__Iterator2();
		_ChangeTxtScore1_c__Iterator._this = this;
		return _ChangeTxtScore1_c__Iterator;
	}

	public void ShowHealth1()
	{
		base.CancelInvoke("OffHealth1");
		this.Health1Player.gameObject.SetActive(true);
		this.Health1Player.text = (int)(TASPlayerControl.Instance.CurHealth * 100f / TASPlayerControl.Instance.MaxHealth) + "%";
		base.Invoke("OffHealth1", 3f);
	}

	public void OffHealth1()
	{
		this.Health1Player.gameObject.SetActive(false);
	}

	public void BtnPauseOnClick()
	{
		if (Advertisements.Instance && Advertisements.Instance.IsInterstitialAvailable())
		{
			Advertisements.Instance.ShowInterstitial(new UnityAction(this.Pause2));
		}
		else
		{
			this.Pause2();
		}
	}

	private void Pause2()
	{
		PopupManager.Instance.PauseScene.SetActive(true);
		this.IsPause = true;
	}

	public void ShowSelectItem(bool start, bool off = false)
	{
		this.SelectItem.SetActive(true);
		if (!off)
		{
			if (start)
			{
				this.SelectItem.GetComponent<BuyItemCs>().ShowStartGame();
			}
			else
			{
				this.SelectItem.GetComponent<BuyItemCs>().ShowOnGame();
			}
		}
		else
		{
			this.SelectItem.GetComponent<BuyItemCs>().Off();
		}
	}

	private void OnDisable()
	{
		if (this.GamePlay != null)
		{
			this.GamePlay.SetActive(false);
		}
		this.NumShield = 0;
		this.NumLaser = 0;
		this.NumBomb = 0;
	}
}
