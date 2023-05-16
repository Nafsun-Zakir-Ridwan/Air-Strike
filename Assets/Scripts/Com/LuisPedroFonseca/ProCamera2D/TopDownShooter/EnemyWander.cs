using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class EnemyWander : MonoBehaviour
	{
		private sealed class _CheckAgentPosition_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal EnemyWander _this;

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

			public _CheckAgentPosition_c__Iterator0()
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
				if (this._this._navMeshAgent.remainingDistance <= this._this.WaypointOffset && !this._this._hasReachedDestination)
				{
					this._this._hasReachedDestination = true;
					if (Time.time - this._this._startingTime >= this._this.WanderDuration && this._this.WanderDuration > 0f)
					{
						UnityEngine.Debug.Log("Stopped wandering");
					}
					else
					{
						this._this.GoToWaypoint();
					}
				}
				this._current = null;
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

		public float WanderDuration = 10f;

		public float WaypointOffset = 0.1f;

		public float WanderRadius = 10f;

		private NavMeshAgent _navMeshAgent;

		private bool _hasReachedDestination;

		private Vector3 _startingPos;

		private float _startingTime;

		private void Awake()
		{
			this._navMeshAgent = base.GetComponentInChildren<NavMeshAgent>();
			this._startingPos = this._navMeshAgent.transform.position;
		}

		public void StartWandering()
		{
			this._startingTime = Time.time;
			this.GoToWaypoint();
			base.StartCoroutine(this.CheckAgentPosition());
		}

		public void StopWandering()
		{
			base.StopAllCoroutines();
		}

		private IEnumerator CheckAgentPosition()
		{
			EnemyWander._CheckAgentPosition_c__Iterator0 _CheckAgentPosition_c__Iterator = new EnemyWander._CheckAgentPosition_c__Iterator0();
			_CheckAgentPosition_c__Iterator._this = this;
			return _CheckAgentPosition_c__Iterator;
		}

		private void GoToWaypoint()
		{
			NavMeshPath navMeshPath = new NavMeshPath();
			Vector3 vector = Vector3.zero;
			while (navMeshPath.status == NavMeshPathStatus.PathPartial || navMeshPath.status == NavMeshPathStatus.PathInvalid)
			{
				Vector3 b = UnityEngine.Random.insideUnitSphere * this.WanderRadius;
				b.y = this._startingPos.y;
				vector = this._startingPos + b;
				this._navMeshAgent.CalculatePath(vector, navMeshPath);
			}
			this._navMeshAgent.SetDestination(vector);
			this._hasReachedDestination = false;
		}
	}
}
