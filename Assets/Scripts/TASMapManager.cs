using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TASMapManager : MonoBehaviour
{
	private sealed class _CreatWave_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal TurnInfor wave;

		internal TASMapManager _this;

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

		public _CreatWave_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(this.wave.TimeCreatTurn);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
			{
				TurnType turnType = this.wave.TurnType;
				if (turnType == TurnType.Enemy)
				{
					this._this.StartCoroutine(this._this.CreatEnemies(this.wave));
				}
				if (this._this.wave < this._this.mapinfor.Turns.Count - 1)
				{
					this._this.wave++;
					this._this.StartCoroutine(this._this.CreatWave(this._this.mapinfor.Turns[this._this.wave]));
				}
				this._PC = -1;
				break;
			}
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

	private sealed class _CreatEnemies_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _enermyItemIndex___0;

		internal TurnInfor turn;

		internal WaitForSeconds _wait___1;

		internal int _count___1;

		internal List<EnemyProperties>.Enumerator _locvar0;

		internal EnemyProperties _enemy___2;

		internal float _timeSpeed___3;

		internal TASEnemy _enemyObj___3;

		internal TASMapManager _this;

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

		public _CreatEnemies_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			bool flag = false;
			switch (num)
			{
			case 0u:
				this._enermyItemIndex___0 = -1;
				if (this.turn.ItemName != IName.None)
				{
					this._enermyItemIndex___0 = UnityEngine.Random.Range(0, this.turn.EProperties.Count);
				}
				if (this.turn.FlyStyle != FlyStyle.Herd)
				{
					int num2 = 0;
					float timeScale = 1f;
					foreach (EnemyProperties current in this.turn.EProperties)
					{
						TASEnemy tASEnemy = PoolManager.Instance.CreateEnemySingle(current.Name, this.turn.IdPath, this.turn.EndFlyStyle, this.turn.TurnDuration, current.StartPos, this.turn.Overturned, this.turn.Join);
						tASEnemy.Wave = this.turn.TurnId;
						if (num2 == this._enermyItemIndex___0)
						{
							tASEnemy.Iname = this.turn.ItemName;
							if (this.turn.ItemName == IName.Upgrade)
							{
								tASEnemy.EffItem.SetActive(true);
							}
							else
							{
								tASEnemy.EffItem.SetActive(false);
							}
						}
						else
						{
							tASEnemy.Iname = IName.None;
							tASEnemy.EffItem.SetActive(false);
						}
						tASEnemy.Tweener.timeScale = timeScale;
						if (TASData.Instance.IsHard)
						{
							tASEnemy.MaxHealth = current.HpHard;
							tASEnemy.CurHealth = current.HpHard;
						}
						else
						{
							tASEnemy.MaxHealth = current.Hp;
							tASEnemy.CurHealth = current.Hp;
						}
						tASEnemy.DamageBullet = current.DamageBullet;
						tASEnemy.NumStar = current.NumStar;
						this._this.CountStar += current.NumStar;
						num2++;
					}
					goto IL_486;
				}
				this._wait___1 = new WaitForSeconds(this.turn.Frequency);
				this._count___1 = 0;
				this._locvar0 = this.turn.EProperties.GetEnumerator();
				num = 4294967293u;
				break;
			case 1u:
				break;
			default:
				return false;
			}
			try
			{
				switch (num)
				{
				}
				if (this._locvar0.MoveNext())
				{
					this._enemy___2 = this._locvar0.Current;
					this._timeSpeed___3 = 1f;
					this._enemyObj___3 = PoolManager.Instance.CreateEnemyHerd(this._enemy___2.Name, this.turn.IdPath, this.turn.EndFlyStyle, this.turn.TurnDuration, this.turn.PathPosVector3, this.turn.Overturned, this.turn.Join);
					this._enemyObj___3.Wave = this.turn.TurnId;
					if (this._count___1 == this._enermyItemIndex___0)
					{
						this._enemyObj___3.Iname = this.turn.ItemName;
						if (this.turn.ItemName == IName.Upgrade)
						{
							this._enemyObj___3.EffItem.SetActive(true);
						}
						else
						{
							this._enemyObj___3.EffItem.SetActive(false);
						}
					}
					else
					{
						this._enemyObj___3.Iname = IName.None;
						this._enemyObj___3.EffItem.SetActive(false);
					}
					this._enemyObj___3.Tweener.timeScale = this._timeSpeed___3;
					if (TASData.Instance.IsHard)
					{
						this._enemyObj___3.MaxHealth = this._enemy___2.HpHard;
						this._enemyObj___3.CurHealth = this._enemy___2.HpHard;
					}
					else
					{
						this._enemyObj___3.MaxHealth = this._enemy___2.Hp;
						this._enemyObj___3.CurHealth = this._enemy___2.Hp;
					}
					this._enemyObj___3.DamageBullet = this._enemy___2.DamageBullet;
					this._enemyObj___3.NumStar = this._enemy___2.NumStar;
					this._this.CountStar += this._enemy___2.NumStar;
					this._count___1++;
					this._current = this._wait___1;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					flag = true;
					return true;
				}
			}
			finally
			{
				if (!flag)
				{
					((IDisposable)this._locvar0).Dispose();
				}
			}
			IL_486:
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			uint num = (uint)this._PC;
			this._disposing = true;
			this._PC = -1;
			switch (num)
			{
			case 1u:
				try
				{
				}
				finally
				{
					((IDisposable)this._locvar0).Dispose();
				}
				break;
			}
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static TASMapManager Instance;

	[SerializeField]
	private LevelInfor mapinfor;

	public int Level = 1;

	public int NumE;

	private int wave;

	public int CountStar;

	public int StarEat;

	private void Awake()
	{
		TASMapManager.Instance = this;
	}

	private void OnEnable()
	{
	}

	public void StartGame(int level, bool IsNew)
	{
		this.CountStar = 0;
		this.StarEat = 0;
		base.StopAllCoroutines();
		if (TASData.Instance.Intro == 0)
		{
			this.LoadMapLevel(0);
		}
		else
		{
			this.LoadMapLevel(level);
		}
		MissionManager.Instance.LoadMission();
	}

	private void LoadMapLevel(int level)
	{
		TextAsset textAsset = Resources.Load<TextAsset>("Levels/Level" + level);
		if (textAsset)
		{
			this.mapinfor = JsonUtility.FromJson<LevelInfor>(textAsset.text);
			int num = this.mapinfor.Turns.Count - 1;
			for (int i = num; i > 0; i--)
			{
				this.mapinfor.Turns[i].TimeCreatTurn -= this.mapinfor.Turns[i - 1].TimeCreatTurn;
			}
			this.NumE = 0;
			int num2 = 1;
			foreach (TurnInfor current in this.mapinfor.Turns)
			{
				if (current.TurnType == TurnType.Enemy)
				{
					this.NumE += current.EProperties.Count;
				}
				current.TurnId = num2;
				num2++;
			}
			this.wave = 0;
			if (this.mapinfor.Turns.Count > 0)
			{
				base.StartCoroutine(this.CreatWave(this.mapinfor.Turns[this.wave]));
			}
		}
		else
		{
			UnityEngine.Debug.Log("Không tồn tại Level" + this.Level);
		}
	}

	private IEnumerator CreatWave(TurnInfor wave)
	{
		TASMapManager._CreatWave_c__Iterator0 _CreatWave_c__Iterator = new TASMapManager._CreatWave_c__Iterator0();
		_CreatWave_c__Iterator.wave = wave;
		_CreatWave_c__Iterator._this = this;
		return _CreatWave_c__Iterator;
	}

	private IEnumerator CreatEnemies(TurnInfor turn)
	{
		TASMapManager._CreatEnemies_c__Iterator1 _CreatEnemies_c__Iterator = new TASMapManager._CreatEnemies_c__Iterator1();
		_CreatEnemies_c__Iterator.turn = turn;
		_CreatEnemies_c__Iterator._this = this;
		return _CreatEnemies_c__Iterator;
	}

	public void CheckCountEnemy()
	{
		this.NumE--;
		if (this.NumE <= 0)
		{
			Time.timeScale = 1f;
			base.Invoke("ShowEndGame", 3.5f);
		}
	}

	public void ShowEndGame()
	{
		if (!GPSManager.Instance.PlayerDie)
		{
			TASPlayerMove.Instance.IsWin = true;
			GPSManager.Instance.PanelBlack.SetActive(false);
			Time.timeScale = 1f;
			GPSManager.Instance.LevelComplete();
		}
	}

	private void OnDisable()
	{
		base.CancelInvoke();
	}
}
