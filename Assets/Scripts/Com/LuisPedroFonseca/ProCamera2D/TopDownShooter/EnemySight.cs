using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class EnemySight : MonoBehaviour
	{
		private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector3 _direction___1;

			internal float _angle___1;

			internal EnemySight _this;

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

			public _Start_c__Iterator0()
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
				this._direction___1 = this._this.player.position - this._this.transform.position;
				this._angle___1 = Vector3.Angle(this._direction___1, this._this.transform.forward);
				if (this._angle___1 < this._this.fieldOfViewAngle * 0.5f && Physics.Raycast(this._this.transform.position + this._this.transform.up, this._direction___1.normalized, out this._this._hit, this._this.ViewDistance, this._this.LayerMask) && this._this._hit.collider.transform.GetInstanceID() == this._this.player.GetInstanceID())
				{
					if (!this._this.playerInSight)
					{
						this._this.playerInSight = true;
						if (this._this.OnPlayerInSight != null)
						{
							this._this.OnPlayerInSight(this._this._hit.collider.transform);
						}
					}
				}
				else if (this._this.playerInSight)
				{
					this._this.playerInSight = false;
					if (this._this.OnPlayerOutOfSight != null)
					{
						this._this.OnPlayerOutOfSight();
					}
				}
				this._current = new WaitForSeconds(this._this.RefreshRate);
				if (!this._disposing)
				{
					this._PC = 1;
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

		public Action<Transform> OnPlayerInSight;

		public Action OnPlayerOutOfSight;

		public float RefreshRate = 1f;

		public float fieldOfViewAngle = 110f;

		public float ViewDistance = 30f;

		public bool playerInSight;

		public Transform player;

		public LayerMask LayerMask;

		private RaycastHit _hit;

		private void Awake()
		{
			this.RefreshRate += UnityEngine.Random.Range(-this.RefreshRate * 0.2f, this.RefreshRate * 0.2f);
		}

		private IEnumerator Start()
		{
			EnemySight._Start_c__Iterator0 _Start_c__Iterator = new EnemySight._Start_c__Iterator0();
			_Start_c__Iterator._this = this;
			return _Start_c__Iterator;
		}
	}
}
