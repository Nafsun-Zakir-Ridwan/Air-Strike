using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunPlayerCs : MonoBehaviour
{
	private sealed class _BigGunUp_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GunPlayerCs _this;

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

		public _BigGunUp_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (this._this.LvUp == 0)
				{
					this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos1.position, new Vector3(0f, 0f, 0f));
				}
				else
				{
					if (this._this.LvUp == 1)
					{
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, default(Vector3));
						this._current = new WaitForSeconds(this._this.Firerate1 / 2f);
						if (!this._disposing)
						{
							this._PC = 1;
						}
						return true;
					}
					if (this._this.LvUp == 2)
					{
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos1.position, default(Vector3));
						this._current = new WaitForSeconds(this._this.Firerate1 / 2f);
						if (!this._disposing)
						{
							this._PC = 2;
						}
						return true;
					}
					if (this._this.LvUp == 3)
					{
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos1.position, default(Vector3));
						this._current = new WaitForSeconds(this._this.Firerate1 / 2f);
						if (!this._disposing)
						{
							this._PC = 3;
						}
						return true;
					}
					if (this._this.LvUp == 4)
					{
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, default(Vector3));
						this._current = new WaitForSeconds(this._this.Firerate1 / 4f);
						if (!this._disposing)
						{
							this._PC = 4;
						}
						return true;
					}
					if (this._this.LvUp == 5)
					{
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, default(Vector3));
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, default(Vector3));
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos4.position, default(Vector3));
						this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos5.position, default(Vector3));
					}
					else
					{
						if (this._this.LvUp == 6)
						{
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, default(Vector3));
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, default(Vector3));
							this._current = new WaitForSeconds(this._this.Firerate1 / 2f);
							if (!this._disposing)
							{
								this._PC = 7;
							}
							return true;
						}
						if (this._this.LvUp == 7)
						{
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, default(Vector3));
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, default(Vector3));
							this._current = new WaitForSeconds(this._this.Firerate1 / 2f);
							if (!this._disposing)
							{
								this._PC = 8;
							}
							return true;
						}
						if (this._this.LvUp == 8)
						{
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos1.position, default(Vector3));
							this._current = new WaitForSeconds(this._this.Firerate1 / 3f);
							if (!this._disposing)
							{
								this._PC = 9;
							}
							return true;
						}
						if (this._this.LvUp >= 9)
						{
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos1.position, default(Vector3));
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, new Vector3(0f, 0f, 5f));
							this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, new Vector3(0f, 0f, -5f));
							this._current = new WaitForSeconds(this._this.Firerate1 / 2f);
							if (!this._disposing)
							{
								this._PC = 11;
							}
							return true;
						}
					}
				}
				break;
			case 1u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, default(Vector3));
				break;
			case 2u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, default(Vector3));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, default(Vector3));
				break;
			case 3u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, new Vector3(0f, 0f, 5f));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, new Vector3(0f, 0f, -5f));
				break;
			case 4u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, default(Vector3));
				this._current = new WaitForSeconds(this._this.Firerate1 / 4f);
				if (!this._disposing)
				{
					this._PC = 5;
				}
				return true;
			case 5u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos4.position, default(Vector3));
				this._current = new WaitForSeconds(this._this.Firerate1 / 4f);
				if (!this._disposing)
				{
					this._PC = 6;
				}
				return true;
			case 6u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos5.position, default(Vector3));
				break;
			case 7u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos4.position, default(Vector3));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos5.position, default(Vector3));
				break;
			case 8u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos4.position, new Vector3(0f, 0f, 5f));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos5.position, new Vector3(0f, 0f, -5f));
				break;
			case 9u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos2.position, new Vector3(0f, 0f, 5f));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos3.position, new Vector3(0f, 0f, -5f));
				this._current = new WaitForSeconds(this._this.Firerate1 / 3f);
				if (!this._disposing)
				{
					this._PC = 10;
				}
				return true;
			case 10u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos4.position, new Vector3(0f, 0f, 10f));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos5.position, new Vector3(0f, 0f, -10f));
				break;
			case 11u:
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos4.position, new Vector3(0f, 0f, 10f));
				this._this.Pool.GetBullet(this._this.Pool.PBullet1, this._this.Pool.NPBull1, this._this.Pool.GPBullet1, this._this.Pos5.position, new Vector3(0f, 0f, -10f));
				break;
			default:
				return false;
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

	private sealed class _LaserShow_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GunPlayerCs _this;

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

		public _LaserShow_c__Iterator1()
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
				this._current = new WaitForSeconds(this._this.TimeLaser);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.Laser.SetActive(false);
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

	public static GunPlayerCs Instance;

	private float nextFire1;

	public float Firerate1;

	private float nextFire2;

	public float Firerate2;

	private float nextFire3;

	public float Firerate3;

	public GameObject dan;

	public Transform Pos1;

	public Transform Pos2;

	public Transform Pos3;

	public Transform Pos4;

	public Transform Pos5;

	public PoolManager Pool;

	public GameObject Laser;

	public float TimeLaser;

	public int LvUp;

	public GameObject Eff1;

	public GameObject Eff2;

	public GameObject Eff3;

	public GameObject Eff4;

	public GameObject Eff5;

	private void Awake()
	{
		GunPlayerCs.Instance = this;
	}

	private void OnEnable()
	{
		this.LvUp = 0;
		this.Firerate1 = 0.2f;
		this.Firerate2 = 0.38f;
		this.Laser.SetActive(false);
		this.Firerate3 = TASData.Instance.LstWingMissleFirerate[TASData.Instance.LstItemPP[4].Lv];
		this.CheckEff();
	}

	private void Start()
	{
		this.Pool = PoolManager.Instance;
	}

	private void Update()
	{
		if (TASPlayerMove.Instance.IsWin)
		{
			return;
		}
		this.nextFire1 -= Time.deltaTime;
		if (this.nextFire1 < 0f)
		{
			base.StartCoroutine(this.BigGunUp());
			this.nextFire1 = this.Firerate1;
			SoundController.Instance.PlaySound(SoundController.Instance.WeaponPlayer, 0.8f);
		}
		if (TASData.Instance.Intro == 0)
		{
			this.nextFire2 -= Time.deltaTime;
			if (this.nextFire2 < 0f)
			{
				this.Pool.GetBullet(this.Pool.PBullet2, this.Pool.NPBull2, this.Pool.GPBullet2, this.Pos2.position, default(Vector3));
				this.Pool.GetBullet(this.Pool.PBullet2, this.Pool.NPBull2, this.Pool.GPBullet2, this.Pos3.position, default(Vector3));
				this.nextFire2 = this.Firerate2;
				SoundController.Instance.PlaySound(SoundController.Instance.WeaponPlayer, 0.8f);
			}
			this.nextFire3 -= Time.deltaTime;
			if (this.nextFire3 < 0f)
			{
				this.Pool.GetBullet(this.Pool.PBullet3, this.Pool.NPBull3, this.Pool.GPBullet3, this.Pos1.position, default(Vector3));
				this.nextFire3 = this.Firerate3;
			}
		}
		else
		{
			if (TASData.Instance.LstItemPP[2].Unlock)
			{
				this.nextFire2 -= Time.deltaTime;
				if (this.nextFire2 < 0f)
				{
					this.Pool.GetBullet(this.Pool.PBullet2, this.Pool.NPBull2, this.Pool.GPBullet2, this.Pos2.position, default(Vector3));
					this.Pool.GetBullet(this.Pool.PBullet2, this.Pool.NPBull2, this.Pool.GPBullet2, this.Pos3.position, default(Vector3));
					this.nextFire2 = this.Firerate2;
					SoundController.Instance.PlaySound(SoundController.Instance.WeaponPlayer, 0.8f);
				}
			}
			if (TASData.Instance.LstItemPP[4].Unlock)
			{
				this.nextFire3 -= Time.deltaTime;
				if (this.nextFire3 < 0f)
				{
					this.Pool.GetBullet(this.Pool.PBullet3, this.Pool.NPBull3, this.Pool.GPBullet3, this.Pos1.position, default(Vector3));
					this.nextFire3 = this.Firerate3;
				}
			}
		}
	}

	public IEnumerator BigGunUp()
	{
		GunPlayerCs._BigGunUp_c__Iterator0 _BigGunUp_c__Iterator = new GunPlayerCs._BigGunUp_c__Iterator0();
		_BigGunUp_c__Iterator._this = this;
		return _BigGunUp_c__Iterator;
	}

	public void CheckEff()
	{
		if (this.LvUp == 0)
		{
			this.Eff1.SetActive(true);
			this.Eff2.SetActive(false);
			this.Eff3.SetActive(false);
			this.Eff4.SetActive(false);
			this.Eff5.SetActive(false);
		}
		else if (this.LvUp == 1)
		{
			this.Eff1.SetActive(false);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(false);
			this.Eff5.SetActive(false);
		}
		else if (this.LvUp == 2)
		{
			this.Eff1.SetActive(true);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(false);
			this.Eff5.SetActive(false);
		}
		else if (this.LvUp == 3)
		{
			this.Eff1.SetActive(true);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(false);
			this.Eff5.SetActive(false);
		}
		else if (this.LvUp == 4)
		{
			this.Eff1.SetActive(false);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(true);
			this.Eff5.SetActive(true);
		}
		else if (this.LvUp == 5)
		{
			this.Eff1.SetActive(false);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(true);
			this.Eff5.SetActive(true);
		}
		else if (this.LvUp == 6)
		{
			this.Eff1.SetActive(false);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(true);
			this.Eff5.SetActive(true);
		}
		else if (this.LvUp == 7)
		{
			this.Eff1.SetActive(false);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(true);
			this.Eff5.SetActive(true);
		}
		else if (this.LvUp == 8)
		{
			this.Eff1.SetActive(true);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(true);
			this.Eff5.SetActive(true);
		}
		else if (this.LvUp >= 9)
		{
			this.Eff1.SetActive(true);
			this.Eff2.SetActive(true);
			this.Eff3.SetActive(true);
			this.Eff4.SetActive(true);
			this.Eff5.SetActive(true);
		}
	}

	public void UpGrade()
	{
		if (this.LvUp < 10)
		{
			this.LvUp++;
			if (this.Firerate1 > 0.12f)
			{
				this.Firerate1 -= 0.01f;
			}
			if (this.Firerate1 > 0.2f)
			{
				this.Firerate2 -= 0.01f;
			}
		}
		this.CheckEff();
	}

	public void ShootLaser()
	{
		base.StartCoroutine(this.LaserShow());
	}

	private IEnumerator LaserShow()
	{
		GunPlayerCs._LaserShow_c__Iterator1 _LaserShow_c__Iterator = new GunPlayerCs._LaserShow_c__Iterator1();
		_LaserShow_c__Iterator._this = this;
		return _LaserShow_c__Iterator;
	}

	private void OnDisable()
	{
		base.StopAllCoroutines();
	}
}
