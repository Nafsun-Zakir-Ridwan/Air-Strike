using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy8 : TASEnemy
{
	private sealed class _ComboBullet_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Vector3 _tar___0;

		internal int _i___1;

		internal Enemy8 _this;

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

		public _ComboBullet_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._tar___0 = TASPlayerControl.Instance.transform.position;
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
				this._this.Pool.GetEBullet(this._this.Pool.EBullN4, this._this.Pool.NEBullN4, this._this.Pool.GEBullN4, this._this.BullPos.position, this._tar___0, true, this._this.DamageBullet);
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

	public GameObject Gunbarrel;

	public GameObject body;

	public override void OnEnable()
	{
		base.OnEnable();
		base.InvokeRepeating("Shoot", UnityEngine.Random.Range(1f, 3f), 2f);
	}

	public void Shoot()
	{
		base.StartCoroutine(this.ComboBullet());
	}

	private IEnumerator ComboBullet()
	{
		Enemy8._ComboBullet_c__Iterator0 _ComboBullet_c__Iterator = new Enemy8._ComboBullet_c__Iterator0();
		_ComboBullet_c__Iterator._this = this;
		return _ComboBullet_c__Iterator;
	}

	public override void Update()
	{
		base.Update();
		Vector3 vector = TASPlayerControl.Instance.transform.position - base.transform.position;
		float angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
		Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
		this.Gunbarrel.transform.rotation = Quaternion.Slerp(this.Gunbarrel.transform.rotation, b, Time.deltaTime * 10f);
	}

	public override void MoveAfterStand()
	{
		if (!this.IsBoss)
		{
			Vector3[] array = (from s in PathController.Instance.Paths[UnityEngine.Random.Range(56, 58)].Points
			select s + new Vector3(base.transform.position.x, -(PathController.Instance.Paths[UnityEngine.Random.Range(56, 58)].Points[0].y - base.transform.position.y), 0f)).ToArray<Vector3>();
			base.transform.position = array[0];
			base.FlyOff(array, 5f, false);
		}
	}
}
