using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class EnemyAttack : MonoBehaviour
	{
		private sealed class _LookAtTarget_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector3 _lookAtPos___1;

			internal Vector3 _diff___1;

			internal Quaternion _newRotation___1;

			internal EnemyAttack _this;

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

			public _LookAtTarget_c__Iterator0()
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
				if (this._this._hasTarget)
				{
					this._lookAtPos___1 = new Vector3(this._this._target.position.x, this._this.Trnsform.position.y, this._this._target.position.z);
					this._diff___1 = this._lookAtPos___1 - this._this.Trnsform.position;
					this._newRotation___1 = Quaternion.LookRotation(this._diff___1, Vector3.up);
					this._this.Trnsform.rotation = Quaternion.Slerp(this._this.Trnsform.rotation, this._newRotation___1, this._this.RotationSpeed * Time.deltaTime);
					this._current = null;
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

		private sealed class _FollowTarget_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector2 _rnd___1;

			internal EnemyAttack _this;

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

			public _FollowTarget_c__Iterator1()
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
				if (this._this._hasTarget)
				{
					this._rnd___1 = UnityEngine.Random.insideUnitCircle;
					this._this._navMeshAgent.destination = this._this._target.position - (this._this._target.position - this._this.Trnsform.position).normalized * 5f + new Vector3(this._rnd___1.x, 0f, this._rnd___1.y);
					this._current = null;
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

		private sealed class _Fire_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal GameObject _bullet___1;

			internal EnemyAttack _this;

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

			public _Fire_c__Iterator2()
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
				if (this._this._hasTarget)
				{
					this._bullet___1 = this._this.BulletPool.nextThing;
					this._bullet___1.transform.position = this._this.WeaponTip.position;
					this._bullet___1.transform.rotation = this._this.Trnsform.rotation * Quaternion.Euler(new Vector3(0f, -90f + UnityEngine.Random.Range(-this._this.FireAngleRandomness, this._this.FireAngleRandomness), 0f));
					this._current = new WaitForSeconds(this._this.FireRate);
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

		public float RotationSpeed = 2f;

		public Pool BulletPool;

		public Transform WeaponTip;

		public float FireRate = 0.3f;

		public float FireAngleRandomness = 10f;

		private bool _hasTarget;

		private Transform _target;

		private NavMeshAgent _navMeshAgent;

		private Transform Trnsform;

		private void Awake()
		{
			this.Trnsform = base.transform;
			this._navMeshAgent = base.GetComponentInChildren<NavMeshAgent>();
		}

		public void Attack(Transform target)
		{
			this._navMeshAgent.updateRotation = false;
			this._target = target;
			this._hasTarget = true;
			base.StartCoroutine(this.LookAtTarget());
			base.StartCoroutine(this.FollowTarget());
			base.StartCoroutine(this.Fire());
		}

		public void StopAttack()
		{
			this._navMeshAgent.updateRotation = true;
			this._hasTarget = false;
		}

		private IEnumerator LookAtTarget()
		{
			EnemyAttack._LookAtTarget_c__Iterator0 _LookAtTarget_c__Iterator = new EnemyAttack._LookAtTarget_c__Iterator0();
			_LookAtTarget_c__Iterator._this = this;
			return _LookAtTarget_c__Iterator;
		}

		private IEnumerator FollowTarget()
		{
			EnemyAttack._FollowTarget_c__Iterator1 _FollowTarget_c__Iterator = new EnemyAttack._FollowTarget_c__Iterator1();
			_FollowTarget_c__Iterator._this = this;
			return _FollowTarget_c__Iterator;
		}

		private IEnumerator Fire()
		{
			EnemyAttack._Fire_c__Iterator2 _Fire_c__Iterator = new EnemyAttack._Fire_c__Iterator2();
			_Fire_c__Iterator._this = this;
			return _Fire_c__Iterator;
		}

		public static Vector2 RandomOnUnitCircle2(float radius)
		{
			Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
			insideUnitCircle.Normalize();
			return insideUnitCircle * radius;
		}
	}
}
