using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss1 : TASEnemy
{
	private sealed class _SpawnBulletCombo_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal Vector3 _tar___2;

		internal Boss1 _this;

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
				if (this._this.LstGunBoss[0].CurHealth <= 0f)
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
			if (this._i___1 < 3)
			{
				this._tar___2 = TASPlayerControl.Instance.transform.position;
				this._this.Pool.GetEBullet(this._this.Pool.EBullN3, this._this.Pool.NEBullN3, this._this.Pool.GEBullN3, this._this.PosGun1.position, this._tar___2, true, this._this.DamageBullet);
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

	private sealed class _LaserBeam_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Boss1 _this;

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

		public _LaserBeam_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.Laser.SetActive(true);
				this._current = new WaitForSeconds(3f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Laser.SetActive(false);
				this._this.Invoke("ShootGun3", 3f);
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

	public Transform Gun0;

	public Transform Gun1;

	public Transform Gun2;

	public Transform Gun3;

	public Transform PosGun1;

	public Transform PosGun2;

	public Transform PosGun3;

	public Transform PosGun4;

	public GameObject Laser;

	public List<GameObject> LstBreaks;

	public override void OnEnable()
	{
		base.OnEnable();
		for (int i = 0; i < this.LstGunBoss.Count; i++)
		{
			this.LstGunBoss[i].gameObject.SetActive(true);
			this.LstGunBoss[i].IsNew = true;
			this.LstGunBoss[i].NewGun();
		}
		for (int j = 0; j < this.LstBreaks.Count; j++)
		{
			this.LstBreaks[j].SetActive(false);
		}
		this.ShootPlayer();
	}

	public override void Update()
	{
		base.Update();
		if (this.Gun0.gameObject.activeInHierarchy)
		{
			Vector3 vector = TASPlayerControl.Instance.transform.position - this.Gun0.position;
			float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
			Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
			this.Gun0.rotation = Quaternion.Slerp(this.Gun0.rotation, b, Time.deltaTime * 10f);
		}
		if (this.Gun1.gameObject.activeInHierarchy)
		{
			Vector3 vector2 = TASPlayerControl.Instance.transform.position - this.Gun1.position;
			float angle2 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f + 90f;
			Quaternion b2 = Quaternion.AngleAxis(angle2, Vector3.forward);
			this.Gun1.rotation = Quaternion.Slerp(this.Gun1.rotation, b2, Time.deltaTime * 10f);
		}
		if (this.Gun2.gameObject.activeInHierarchy)
		{
			Vector3 vector3 = TASPlayerControl.Instance.transform.position - this.Gun2.position;
			float angle3 = Mathf.Atan2(vector3.y, vector3.x) * 57.29578f + 90f;
			Quaternion b3 = Quaternion.AngleAxis(angle3, Vector3.forward);
			this.Gun2.rotation = Quaternion.Slerp(this.Gun2.rotation, b3, Time.deltaTime * 10f);
		}
	}

	public void ShootPlayer()
	{
		base.InvokeRepeating("ShootGun0", 5f, 2f);
		base.InvokeRepeating("ShootGun12", 5f, 0.5f);
		base.Invoke("ShootGun3", 3f);
	}

	public void ShootGun0()
	{
		base.StartCoroutine(this.SpawnBulletCombo());
	}

	private IEnumerator SpawnBulletCombo()
	{
		Boss1._SpawnBulletCombo_c__Iterator0 _SpawnBulletCombo_c__Iterator = new Boss1._SpawnBulletCombo_c__Iterator0();
		_SpawnBulletCombo_c__Iterator._this = this;
		return _SpawnBulletCombo_c__Iterator;
	}

	public void ShootGun12()
	{
		if (this.LstGunBoss[1].CurHealth > 0f)
		{
			Vector3 position = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullN3, this.Pool.NEBullN3, this.Pool.GEBullN3, this.PosGun2.position, position, true, this.DamageBullet);
		}
		if (this.LstGunBoss[2].CurHealth > 0f)
		{
			Vector3 position2 = TASPlayerControl.Instance.transform.position;
			this.Pool.GetEBullet(this.Pool.EBullN3, this.Pool.NEBullN3, this.Pool.GEBullN3, this.PosGun3.position, position2, true, this.DamageBullet);
		}
	}

	public void ShootGun3()
	{
		base.StartCoroutine(this.LaserBeam());
	}

	private IEnumerator LaserBeam()
	{
		Boss1._LaserBeam_c__Iterator1 _LaserBeam_c__Iterator = new Boss1._LaserBeam_c__Iterator1();
		_LaserBeam_c__Iterator._this = this;
		return _LaserBeam_c__Iterator;
	}

	public override void GunBossDie(int id, bool changeGun)
	{
		base.GunBossDie(id, changeGun);
		this.LstBreaks[id].SetActive(true);
	}
}
