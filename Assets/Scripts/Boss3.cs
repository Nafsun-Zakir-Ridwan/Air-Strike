using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss3 : TASEnemy
{
	private sealed class _SpawnBulletCombo_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal Vector3 _tar___2;

		internal Boss3 _this;

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

		public _SpawnBulletCombo_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.LstGunBoss[3].CurHealth <= 0f)
				{
					goto IL_107;
				}
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
				this._tar___2 = TASPlayerControl.Instance.transform.position;
				this._this.Pool.GetEBullet(this._this.Pool.B1Bullet1, this._this.Pool.NB1Bull1, this._this.Pool.GB1Bullet1, this._this.PosGun3.position, this._tar___2, true, this._this.DamageBullet);
				this._current = new WaitForSeconds(0.1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			IL_107:
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

	public GameObject Gunbarrel0;

	public GameObject Gunbarrel1;

	public GameObject Gunbarrel2;

	public GameObject Gunbarrel3;

	public Transform PosGun0;

	public Transform PosGun1;

	public Transform PosGun2;

	public Transform PosGun3;

	public GameObject HpGun4;

	public SkeletonAnimation Ske;

	public override void OnEnable()
	{
		base.OnEnable();
		this.Ske.initialSkinName = "100%";
		this.Ske.Initialize(true);
		this.Ske.AnimationName = "animation";
		this.Ske.loop = true;
		for (int i = 0; i < this.LstGunBoss.Count; i++)
		{
			this.LstGunBoss[i].gameObject.SetActive(true);
			this.LstGunBoss[i].IsNew = true;
			this.LstGunBoss[i].NewGun();
		}
		this.LstGunBoss[3].gameObject.SetActive(false);
		this.HpGun4.SetActive(false);
		this.ShootPlayer();
	}

	public void ShootPlayer()
	{
		base.InvokeRepeating("ShootGun01", 5f, 0.5f);
		base.InvokeRepeating("ShootGun2", (float)UnityEngine.Random.Range(1, 4), 3f);
	}

	public void ShootGun01()
	{
		if (this.LstGunBoss[0].CurHealth > 0f)
		{
			Vector3 tar = this.Gunbarrel0.transform.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.B1Bullet1, this.Pool.NB1Bull1, this.Pool.GB1Bullet1, this.PosGun0.position, tar, false, this.DamageBullet);
		}
		if (this.LstGunBoss[1].CurHealth > 0f)
		{
			Vector3 tar2 = this.Gunbarrel1.transform.rotation * Vector3.down;
			this.Pool.GetEBullet(this.Pool.B1Bullet1, this.Pool.NB1Bull1, this.Pool.GB1Bullet1, this.PosGun1.position, tar2, false, this.DamageBullet);
		}
	}

	public void ShootGun2()
	{
		if (this.LstGunBoss[2].CurHealth > 0f)
		{
			Vector3 position = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullet12, this.Pool.NEBull12, this.Pool.GEBullet12, this.PosGun2.position, position, true, this.DamageBullet);
		}
	}

	public override void Update()
	{
		base.Update();
		Vector3 vector = TASPlayerControl.Instance.transform.position - base.transform.position;
		float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
		Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
		if (this.Gunbarrel2.activeInHierarchy)
		{
			this.Gunbarrel2.transform.rotation = Quaternion.Slerp(this.Gunbarrel2.transform.rotation, b, Time.deltaTime * 10f);
		}
		if (this.Gunbarrel3.activeInHierarchy)
		{
			this.Gunbarrel3.transform.rotation = Quaternion.Slerp(this.Gunbarrel3.transform.rotation, b, Time.deltaTime * 10f);
		}
	}

	public override void GunBossDie(int id, bool changeGun)
	{
		base.GunBossDie(id, changeGun);
		if (changeGun)
		{
			this.Gunbarrel3.SetActive(true);
			this.HpGun4.SetActive(true);
			base.InvokeRepeating("ShootGun3", 1f, 2f);
		}
		if (this.LstGunBoss[0].CurHealth <= 0f && this.LstGunBoss[1].CurHealth <= 0f && this.LstGunBoss[3].CurHealth > 0f)
		{
			this.Ske.initialSkinName = "2-wing";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
		else if (this.LstGunBoss[0].CurHealth <= 0f && this.LstGunBoss[1].CurHealth <= 0f && this.LstGunBoss[3].CurHealth <= 0f)
		{
			this.Ske.initialSkinName = "break-all";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
		else if (this.LstGunBoss[0].CurHealth > 0f && this.LstGunBoss[1].CurHealth > 0f && this.LstGunBoss[3].CurHealth <= 0f)
		{
			this.Ske.initialSkinName = "head-first";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
		else if (this.LstGunBoss[0].CurHealth <= 0f && this.LstGunBoss[1].CurHealth > 0f && this.LstGunBoss[3].CurHealth <= 0f)
		{
			this.Ske.initialSkinName = "left+head";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
		else if (this.LstGunBoss[0].CurHealth > 0f && this.LstGunBoss[1].CurHealth <= 0f && this.LstGunBoss[3].CurHealth <= 0f)
		{
			this.Ske.initialSkinName = "right+head";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
		else if (this.LstGunBoss[0].CurHealth <= 0f && this.LstGunBoss[1].CurHealth > 0f && this.LstGunBoss[3].CurHealth > 0f)
		{
			this.Ske.initialSkinName = "left-first";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
		else if (this.LstGunBoss[0].CurHealth > 0f && this.LstGunBoss[1].CurHealth <= 0f && this.LstGunBoss[3].CurHealth > 0f)
		{
			this.Ske.initialSkinName = "right-first";
			this.Ske.Initialize(true);
			this.Ske.AnimationName = "animation";
			this.Ske.loop = true;
		}
	}

	public void ShootGun3()
	{
		base.StartCoroutine(this.SpawnBulletCombo());
	}

	private IEnumerator SpawnBulletCombo()
	{
		Boss3._SpawnBulletCombo_c__Iterator0 _SpawnBulletCombo_c__Iterator = new Boss3._SpawnBulletCombo_c__Iterator0();
		_SpawnBulletCombo_c__Iterator._this = this;
		return _SpawnBulletCombo_c__Iterator;
	}
}
