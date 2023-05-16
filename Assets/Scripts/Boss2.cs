using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss2 : TASEnemy
{
	private sealed class _LaserBeam_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Boss2 _this;

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

		public _LaserBeam_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.LstGunBoss[2].CurHealth > 0f)
				{
					this._this.Laser1.SetActive(true);
				}
				if (this._this.LstGunBoss[3].CurHealth > 0f)
				{
					this._this.Laser2.SetActive(true);
				}
				this._current = new WaitForSeconds(3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Laser1.SetActive(false);
				this._this.Laser2.SetActive(false);
				this._this.Invoke("ShootLaser", 3f);
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

	private sealed class _SpawnBulletCombo_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal Vector3 _tar1___2;

		internal Vector3 _tar2___2;

		internal Boss2 _this;

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

		public _SpawnBulletCombo_c__Iterator1()
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
			if (this._i___1 < 4)
			{
				this._tar1___2 = TASPlayerControl.Instance.transform.position;
				this._this.Pool.GetEBullet(this._this.Pool.EBullN4, this._this.Pool.NEBullN4, this._this.Pool.GEBullN4, this._this.PosGun3.position, Vector3.down, false, this._this.DamageBullet);
				this._tar2___2 = TASPlayerControl.Instance.transform.position;
				this._this.Pool.GetEBullet(this._this.Pool.EBullN4, this._this.Pool.NEBullN4, this._this.Pool.GEBullN4, this._this.PosGun4.position, Vector3.down, false, this._this.DamageBullet);
				this._current = new WaitForSeconds(0.1f);
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

	private sealed class _EffBossDie_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal Boss2 _this;

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

		public _EffBossDie_c__Iterator2()
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
				this._this.Pool.GetEffect(this._this.Pool.EnemyDie, this._this.Pool.EffEDie, this._this.Pool.GEffEDie, this._this.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
				SoundController.Instance.PlaySound(SoundController.Instance.EnemyDie, 1f);
				this._current = new WaitForSeconds(0.5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			SoundController.Instance.PlaySound(SoundController.Instance.BossDie, 1f);
			this._this.Pool.GetEffect(this._this.Pool.BossDie, this._this.Pool.EffBDie, this._this.Pool.GEffBDie, this._this.transform.position);
			TASMapManager.Instance.CheckCountEnemy();
			this._this.gameObject.SetActive(false);
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

	public Transform Gun1;

	public Transform Gun2;

	public Transform PosGun1;

	public Transform PosGun2;

	public Transform PosGun3;

	public Transform PosGun4;

	public GameObject Laser1;

	public GameObject Laser2;

	public SkeletonAnimation Ske;

	public bool CheckLaze;

	public override void OnEnable()
	{
		base.OnEnable();
		this.ShootPlayer();
		this.CheckLaze = false;
		this.Ske.initialSkinName = "100%";
		this.Ske.Initialize(true);
		this.Ske.AnimationName = "thunder1";
		this.Ske.loop = true;
		for (int i = 0; i < this.LstGunBoss.Count; i++)
		{
			this.LstGunBoss[i].gameObject.SetActive(true);
			this.LstGunBoss[i].IsNew = true;
			this.LstGunBoss[i].NewGun();
		}
	}

	public void ShootPlayer()
	{
		base.InvokeRepeating("ShootGun23", 5f, 0.3f);
		base.Invoke("ShootLaser", 5f);
	}

	public void ShootLaser()
	{
		base.StartCoroutine(this.LaserBeam());
	}

	private IEnumerator LaserBeam()
	{
		Boss2._LaserBeam_c__Iterator0 _LaserBeam_c__Iterator = new Boss2._LaserBeam_c__Iterator0();
		_LaserBeam_c__Iterator._this = this;
		return _LaserBeam_c__Iterator;
	}

	public void ShootGun1()
	{
		base.StartCoroutine(this.SpawnBulletCombo());
	}

	private IEnumerator SpawnBulletCombo()
	{
		Boss2._SpawnBulletCombo_c__Iterator1 _SpawnBulletCombo_c__Iterator = new Boss2._SpawnBulletCombo_c__Iterator1();
		_SpawnBulletCombo_c__Iterator._this = this;
		return _SpawnBulletCombo_c__Iterator;
	}

	public void ShootGun23()
	{
		if (this.LstGunBoss[0].CurHealth > 0f)
		{
			Vector3 tar = this.Gun1.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.EBullN4, this.Pool.NEBullN4, this.Pool.GEBullN4, this.PosGun1.position, tar, false, this.DamageBullet);
		}
		if (this.LstGunBoss[1].CurHealth > 0f)
		{
			Vector3 tar2 = this.Gun2.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.EBullN4, this.Pool.NEBullN4, this.Pool.GEBullN4, this.PosGun2.position, tar2, false, this.DamageBullet);
		}
	}

	public override void GunBossDie(int id, bool changeGun)
	{
		base.GunBossDie(id, changeGun);
		if (!this.CheckLaze)
		{
			if (id == 2)
			{
				this.Ske.initialSkinName = "70%-2";
				this.Ske.Initialize(true);
				this.Ske.AnimationName = "thunder4";
				this.Ske.loop = true;
			}
			else if (id == 3)
			{
				this.Ske.initialSkinName = "70%-1";
				this.Ske.Initialize(true);
				this.Ske.AnimationName = "thunder2";
				this.Ske.loop = true;
			}
			this.CheckLaze = true;
		}
		else if (this.GunBossLive > 0)
		{
			if (id == 2 || id == 3)
			{
				this.Ske.initialSkinName = "30%";
				this.Ske.Initialize(true);
				this.Ske.AnimationName = "thunder3";
				this.Ske.loop = true;
			}
		}
		else
		{
			this.Ske.initialSkinName = "10%";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "thunder3";
			this.Ske.loop = true;
		}
	}

	public override void EnemyDie()
	{
		this.IsLive = false;
		for (int i = 0; i < this.NumStar; i++)
		{
			this.Pool.GetItem(this.Pool.Star, this.Pool.NItemStar, this.Pool.GItemStar, this.Trns.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.3f), UnityEngine.Random.Range(-0.2f, 0.3f)));
		}
		if (this.Iname != IName.None)
		{
			base.CreateItem();
		}
		this.Iname = IName.None;
		base.StartCoroutine(this.EffBossDie());
	}

	private IEnumerator EffBossDie()
	{
		Boss2._EffBossDie_c__Iterator2 _EffBossDie_c__Iterator = new Boss2._EffBossDie_c__Iterator2();
		_EffBossDie_c__Iterator._this = this;
		return _EffBossDie_c__Iterator;
	}
}
