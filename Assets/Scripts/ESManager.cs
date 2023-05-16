using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ESManager : MonoBehaviour
{
	private sealed class _FireWork_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal ESManager _this;

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

		public _FireWork_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 5)
			{
				PoolManager.Instance.GetEffect(PoolManager.Instance.EffWinPrefab, PoolManager.Instance.NEffWin, PoolManager.Instance.GEffWin, this._this.transform.position + new Vector3((float)UnityEngine.Random.Range(-3, 3), (float)UnityEngine.Random.Range(-4, 4)));
				this._current = new WaitForSeconds(0.5f);
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

	private sealed class _WinCoroutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _score___0;

		internal int _highscore___0;

		internal int _coin___0;

		internal int _i___1;

		internal ESManager _this;

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

		public _WinCoroutine_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._score___0 = 0;
				break;
			case 1u:
				if (this._score___0 <= 10)
				{
					this._score___0 += 7;
				}
				else if (this._score___0 > 10 && this._score___0 <= 100)
				{
					this._score___0 += 17;
				}
				else if (this._score___0 > 100 && this._score___0 <= 1000)
				{
					this._score___0 += 167;
				}
				else if (this._score___0 > 1000)
				{
					this._score___0 += 1674;
				}
				if (this._score___0 >= GPSManager.Instance.Score)
				{
					this._current = new WaitForSeconds(1E-17f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				break;
			case 2u:
				this._score___0 = GPSManager.Instance.Score;
				this._this.TxtScore.text = this._score___0.ToString();
				goto IL_1B6;
			case 3u:
				if (this._highscore___0 <= 10)
				{
					this._highscore___0 += 7;
				}
				else if (this._highscore___0 > 10 && this._highscore___0 <= 100)
				{
					this._highscore___0 += 17;
				}
				else if (this._highscore___0 > 100 && this._highscore___0 <= 1000)
				{
					this._highscore___0 += 167;
				}
				else if (this._highscore___0 > 1000)
				{
					this._highscore___0 += 1674;
				}
				if (this._highscore___0 >= TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score)
				{
					this._current = new WaitForSeconds(Time.deltaTime);
					if (!this._disposing)
					{
						this._PC = 4;
					}
					return true;
				}
				goto IL_346;
			case 4u:
				this._highscore___0 = TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score;
				this._this.TxtHighScore.text = this._highscore___0.ToString();
				goto IL_371;
			case 5u:
				if (this._coin___0 <= 10)
				{
					this._coin___0 += 7;
				}
				else if (this._coin___0 > 10 && this._coin___0 <= 100)
				{
					this._coin___0 += 17;
				}
				else if (this._coin___0 > 100 && this._coin___0 <= 1000)
				{
					this._coin___0 += 167;
				}
				else if (this._coin___0 > 1000)
				{
					this._coin___0 += 1674;
				}
				if (this._coin___0 >= TASMapManager.Instance.StarEat)
				{
					this._current = new WaitForSeconds(Time.deltaTime);
					if (!this._disposing)
					{
						this._PC = 6;
					}
					return true;
				}
				goto IL_4D5;
			case 6u:
				this._coin___0 = TASMapManager.Instance.StarEat;
				this._this.TxtCoin.text = this._coin___0.ToString();
				goto IL_4EA;
			case 7u:
				this._i___1++;
				goto IL_5C5;
			case 8u:
				this._this.GBtnWin.SetActive(true);
				this._this.GBtnLose.SetActive(false);
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._score___0 <= GPSManager.Instance.Score)
			{
				this._this.TxtScore.text = this._score___0.ToString();
				this._current = new WaitForSeconds(1E-17f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			IL_1B6:
			this._highscore___0 = 0;
			IL_346:
			if (this._highscore___0 <= TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score)
			{
				this._this.TxtHighScore.text = this._highscore___0.ToString();
				this._current = new WaitForSeconds(Time.deltaTime);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			}
			IL_371:
			this._coin___0 = 0;
			IL_4D5:
			if (this._coin___0 <= TASMapManager.Instance.StarEat)
			{
				this._this.TxtCoin.text = this._coin___0.ToString();
				this._current = new WaitForSeconds(Time.deltaTime);
				if (!this._disposing)
				{
					this._PC = 5;
				}
				return true;
			}
			IL_4EA:
			this._i___1 = 0;
			IL_5C5:
			if (this._i___1 >= this._this.StarMissions.Count)
			{
				this._this.Done.transform.localScale = new Vector3(3f, 3f);
				this._this.Done.gameObject.SetActive(true);
				this._this.Done.transform.DOKill(false);
				this._this.Done.transform.DOScale(1f, 0.1f);
				this._current = new WaitForSeconds(0.3f);
				if (!this._disposing)
				{
					this._PC = 8;
				}
				return true;
			}
			this._this.StarMissions[this._i___1].transform.localScale = new Vector3(3f, 3f);
			this._this.StarMissions[this._i___1].gameObject.SetActive(true);
			this._this.StarMissions[this._i___1].transform.DOKill(false);
			this._this.StarMissions[this._i___1].transform.DOScale(1f, 0.1f);
			this._current = new WaitForSeconds(0.3f);
			if (!this._disposing)
			{
				this._PC = 7;
			}
			return true;
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

	private sealed class _LoseCoroutine_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _score___0;

		internal int _highscore___0;

		internal int _coin___0;

		internal int _i___1;

		internal ESManager _this;

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

		public _LoseCoroutine_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._score___0 = 0;
				break;
			case 1u:
				if (this._score___0 <= 10)
				{
					this._score___0 += 7;
				}
				else if (this._score___0 > 10 && this._score___0 <= 100)
				{
					this._score___0 += 17;
				}
				else if (this._score___0 > 100 && this._score___0 <= 1000)
				{
					this._score___0 += 167;
				}
				else if (this._score___0 > 1000)
				{
					this._score___0 += 1674;
				}
				if (this._score___0 >= GPSManager.Instance.Score)
				{
					this._current = new WaitForSeconds(1E-17f);
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				break;
			case 2u:
				this._score___0 = GPSManager.Instance.Score;
				this._this.TxtScore.text = this._score___0.ToString();
				goto IL_1B6;
			case 3u:
				if (this._highscore___0 <= 10)
				{
					this._highscore___0 += 7;
				}
				else if (this._highscore___0 > 10 && this._highscore___0 <= 100)
				{
					this._highscore___0 += 17;
				}
				else if (this._highscore___0 > 100 && this._highscore___0 <= 1000)
				{
					this._highscore___0 += 167;
				}
				else if (this._highscore___0 > 1000)
				{
					this._highscore___0 += 1674;
				}
				if (this._highscore___0 >= TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score)
				{
					this._current = new WaitForSeconds(Time.deltaTime);
					if (!this._disposing)
					{
						this._PC = 4;
					}
					return true;
				}
				goto IL_346;
			case 4u:
				this._highscore___0 = TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score;
				this._this.TxtHighScore.text = this._highscore___0.ToString();
				goto IL_371;
			case 5u:
				if (this._coin___0 <= 10)
				{
					this._coin___0 += 7;
				}
				else if (this._coin___0 > 10 && this._coin___0 <= 100)
				{
					this._coin___0 += 17;
				}
				else if (this._coin___0 > 100 && this._coin___0 <= 1000)
				{
					this._coin___0 += 167;
				}
				else if (this._coin___0 > 1000)
				{
					this._coin___0 += 1674;
				}
				if (this._coin___0 >= TASMapManager.Instance.StarEat)
				{
					this._current = new WaitForSeconds(Time.deltaTime);
					if (!this._disposing)
					{
						this._PC = 6;
					}
					return true;
				}
				goto IL_4D5;
			case 6u:
				this._coin___0 = TASMapManager.Instance.StarEat;
				this._this.TxtCoin.text = this._coin___0.ToString();
				goto IL_4EA;
			case 7u:
				this._i___1++;
				goto IL_5C5;
			case 8u:
				this._this.GBtnWin.SetActive(false);
				this._this.GBtnLose.SetActive(true);
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._score___0 <= GPSManager.Instance.Score)
			{
				this._this.TxtScore.text = this._score___0.ToString();
				this._current = new WaitForSeconds(1E-17f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			IL_1B6:
			this._highscore___0 = 0;
			IL_346:
			if (this._highscore___0 <= TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score)
			{
				this._this.TxtHighScore.text = this._highscore___0.ToString();
				this._current = new WaitForSeconds(Time.deltaTime);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			}
			IL_371:
			this._coin___0 = 0;
			IL_4D5:
			if (this._coin___0 <= TASMapManager.Instance.StarEat)
			{
				this._this.TxtCoin.text = this._coin___0.ToString();
				this._current = new WaitForSeconds(Time.deltaTime);
				if (!this._disposing)
				{
					this._PC = 5;
				}
				return true;
			}
			IL_4EA:
			this._i___1 = 0;
			IL_5C5:
			if (this._i___1 >= this._this.StarMissions.Count)
			{
				this._this.Failed.transform.localScale = new Vector3(3f, 3f);
				this._this.Failed.gameObject.SetActive(true);
				this._this.Failed.transform.DOKill(false);
				this._this.Failed.transform.DOScale(1f, 0.1f);
				this._current = new WaitForSeconds(0.3f);
				if (!this._disposing)
				{
					this._PC = 8;
				}
				return true;
			}
			this._this.StarMissions[this._i___1].transform.localScale = new Vector3(3f, 3f);
			this._this.StarMissions[this._i___1].gameObject.SetActive(true);
			this._this.StarMissions[this._i___1].transform.DOKill(false);
			this._this.StarMissions[this._i___1].transform.DOScale(1f, 0.1f);
			this._current = new WaitForSeconds(0.3f);
			if (!this._disposing)
			{
				this._PC = 7;
			}
			return true;
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

	public GameObject GBtnWin;

	public GameObject GBtnLose;

	public GameObject Done;

	public GameObject Failed;

	public List<Text> TxtMissions;

	public List<Image> StarMissions;

	public Sprite Cer;

	public Sprite NotCer;

	public Text TxtScore;

	public Text TxtHighScore;

	public Text TxtCoin;

	public Text TxtStageLv;

	public Text TxtNameStage;

	private void OnEnable()
	{
		this.TxtCoin.text = "0";
		this.TxtHighScore.text = "0";
		this.TxtScore.text = "0";
		for (int i = 0; i < this.StarMissions.Count; i++)
		{
			this.StarMissions[i].gameObject.SetActive(false);
		}
		this.Done.SetActive(false);
		this.Failed.SetActive(false);
		this.ShowDesLv();
		if (PlayerPrefs.GetInt("Tutorial", 2) == 2)
		{
			PlayerPrefs.SetInt("Tutorial", 0);
		}
	}

	public void ShowDesLv()
	{
		int @int = PlayerPrefs.GetInt("Stage", 1);
		int int2 = PlayerPrefs.GetInt("Lv", 1);
		this.TxtStageLv.text = string.Concat(new object[]
		{
			"Stage ",
			@int,
			" - Level ",
			int2,
			":"
		});
		if (@int == 1)
		{
			this.TxtNameStage.text = "EARTH";
		}
		else if (@int == 2)
		{
			this.TxtNameStage.text = "DARK OF THE MOON";
		}
		else if (@int == 3)
		{
			this.TxtNameStage.text = "SUN LIGHT";
		}
		else if (@int == 4)
		{
			this.TxtNameStage.text = "SIRIUS";
		}
	}

	public void Show(bool isWin)
	{
		this.GBtnWin.SetActive(false);
		this.GBtnLose.SetActive(false);
		if (isWin)
		{
			if (GPSManager.Instance.Score > TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score)
			{
				TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].Score = GPSManager.Instance.Score;
				TASData.Instance.SaveDataLevel();
			}
			MissionManager.Instance.CheckMissionWin();
			this.CheckMission();
			base.StartCoroutine(this.WinCoroutine());
			base.StartCoroutine(this.FireWork());
		}
		else
		{
			MissionManager.Instance.CheckMissionDie();
			this.CheckMission();
			base.StartCoroutine(this.LoseCoroutine());
		}
	}

	private IEnumerator FireWork()
	{
		ESManager._FireWork_c__Iterator0 _FireWork_c__Iterator = new ESManager._FireWork_c__Iterator0();
		_FireWork_c__Iterator._this = this;
		return _FireWork_c__Iterator;
	}

	private IEnumerator WinCoroutine()
	{
		ESManager._WinCoroutine_c__Iterator1 _WinCoroutine_c__Iterator = new ESManager._WinCoroutine_c__Iterator1();
		_WinCoroutine_c__Iterator._this = this;
		return _WinCoroutine_c__Iterator;
	}

	private IEnumerator LoseCoroutine()
	{
		ESManager._LoseCoroutine_c__Iterator2 _LoseCoroutine_c__Iterator = new ESManager._LoseCoroutine_c__Iterator2();
		_LoseCoroutine_c__Iterator._this = this;
		return _LoseCoroutine_c__Iterator;
	}

	public void BtnRestartOnClick(bool isWin)
	{
		if (isWin)
		{
			Time.timeScale = 1f;
			GPSManager.Instance.ResetGameplay();
			base.gameObject.SetActive(false);
		}
		else
		{
			Time.timeScale = 1f;
			GPSManager.Instance.ResetGameplay();
			base.gameObject.SetActive(false);
		}
	}

	public void BtnMapLvOnClick()
	{
		TASManagerUI.Instance.ChangeScenes(1);
		SoundController.Instance.PlayMusic(SoundController.Instance.UI, 1f, true);
		base.gameObject.SetActive(false);
	}

	public void BtnContinueOnClick()
	{
		if (PlayerPrefs.GetInt("Lv", 1) < 5)
		{
			PlayerPrefs.SetInt("Lv", PlayerPrefs.GetInt("Lv", 1) + 1);
		}
		else if (TASData.Instance.StageSelect < 2)
		{
			PlayerPrefs.SetInt("Lv", 1);
			TASData.Instance.StageSelect++;
		}
		if (TASData.Instance.LvSelect < 10)
		{
			TASData.Instance.LvSelect++;
		}
		PlayerPrefs.SetInt("LvSelect", TASData.Instance.LvSelect);
		PlayerPrefs.SetInt("Stage", TASData.Instance.StageSelect);
		TASManagerUI.Instance.ChangeScenes(1);
		SoundController.Instance.PlayMusic(SoundController.Instance.UI, 1f, true);
		base.gameObject.SetActive(false);
	}

	public void BtnUpgradeOnClick()
	{
		TASManagerUI.Instance.ChangeScenes(2);
		SoundController.Instance.PlayMusic(SoundController.Instance.UI, 1f, true);
		base.gameObject.SetActive(false);
	}

	public void CheckMission()
	{
		if (!TASData.Instance.IsHard)
		{
			for (int i = 0; i < 3; i++)
			{
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate)
				{
					this.TxtMissions[i].color = Color.yellow;
					this.StarMissions[i].sprite = this.Cer;
				}
				else
				{
					this.TxtMissions[i].color = Color.white;
					this.StarMissions[i].sprite = this.NotCer;
				}
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 1)
				{
					this.TxtMissions[i].text = "clear 100% enemies";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 2)
				{
					this.TxtMissions[i].text = "stay untouched";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 3)
				{
					this.TxtMissions[i].text = "collect 100% coin";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 4)
				{
					this.TxtMissions[i].text = "Unlock and use " + TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].MisionCount + " Item";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 5)
				{
					this.TxtMissions[i].text = "Die by bullet of enemy";
				}
			}
		}
		else
		{
			for (int j = 3; j < 6; j++)
			{
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate)
				{
					this.TxtMissions[j - 3].color = Color.yellow;
					this.StarMissions[j - 3].sprite = this.Cer;
				}
				else
				{
					this.TxtMissions[j - 3].color = Color.white;
					this.StarMissions[j - 3].sprite = this.NotCer;
				}
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 1)
				{
					this.TxtMissions[j - 3].text = "clear 100% enemies";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 2)
				{
					this.TxtMissions[j - 3].text = "stay untouched";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 3)
				{
					this.TxtMissions[j - 3].text = "collect 100% coin";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 4)
				{
					this.TxtMissions[j - 3].text = "Unlock and use " + TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].MisionCount + " Item";
				}
				else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 5)
				{
					this.TxtMissions[j - 3].text = "Die by bullet of enemy";
				}
			}
		}
	}
}
