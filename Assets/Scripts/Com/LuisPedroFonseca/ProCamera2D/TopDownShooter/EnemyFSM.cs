using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	[RequireComponent(typeof(EnemySight)), RequireComponent(typeof(EnemyAttack)), RequireComponent(typeof(EnemyWander))]
	public class EnemyFSM : MonoBehaviour
	{
		private sealed class _HitAnim_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal EnemyFSM _this;

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

			public _HitAnim_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._this.Colorize(Color.white);
					this._current = new WaitForSeconds(0.05f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.Colorize(this._this._currentColor);
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

		public int Health = 100;

		public Color AttackColor = Color.red;

		public DoorKey Key;

		private EnemySight _sight;

		private EnemyAttack _attack;

		private EnemyWander _wander;

		private Renderer[] _renderers;

		private Color _originalColor;

		private Color _currentColor;

		private void Awake()
		{
			this._sight = base.GetComponent<EnemySight>();
			this._attack = base.GetComponent<EnemyAttack>();
			this._wander = base.GetComponent<EnemyWander>();
			this._renderers = base.GetComponentsInChildren<Renderer>();
			this._originalColor = this._renderers[0].material.color;
			this._currentColor = this._originalColor;
			EnemySight expr_5A = this._sight;
			expr_5A.OnPlayerInSight = (Action<Transform>)Delegate.Combine(expr_5A.OnPlayerInSight, new Action<Transform>(this.OnPlayerInSight));
			EnemySight expr_81 = this._sight;
			expr_81.OnPlayerOutOfSight = (Action)Delegate.Combine(expr_81.OnPlayerOutOfSight, new Action(this.OnPlayerOutOfSight));
			if (this.Key != null)
			{
				this.Key.gameObject.SetActive(false);
			}
		}

		private void Start()
		{
			this._wander.StartWandering();
		}

		private void OnDestroy()
		{
			EnemySight expr_06 = this._sight;
			expr_06.OnPlayerInSight = (Action<Transform>)Delegate.Remove(expr_06.OnPlayerInSight, new Action<Transform>(this.OnPlayerInSight));
			EnemySight expr_2D = this._sight;
			expr_2D.OnPlayerOutOfSight = (Action)Delegate.Remove(expr_2D.OnPlayerOutOfSight, new Action(this.OnPlayerOutOfSight));
		}

		private void Hit(int damage)
		{
			if (this.Health <= 0)
			{
				return;
			}
			this.Health -= damage;
			base.StartCoroutine(this.HitAnim());
			if (this.Health <= 0)
			{
				this.Die();
			}
		}

		private IEnumerator HitAnim()
		{
			EnemyFSM._HitAnim_c__Iterator0 _HitAnim_c__Iterator = new EnemyFSM._HitAnim_c__Iterator0();
			_HitAnim_c__Iterator._this = this;
			return _HitAnim_c__Iterator;
		}

		private void OnPlayerInSight(Transform obj)
		{
			this._wander.StopWandering();
			this._attack.Attack(obj);
			ProCamera2D.Instance.AddCameraTarget(base.transform, 1f, 1f, 0f, default(Vector2));
			this._currentColor = this.AttackColor;
			this.Colorize(this._currentColor);
		}

		private void OnPlayerOutOfSight()
		{
			this._wander.StartWandering();
			this._attack.StopAttack();
			ProCamera2D.Instance.RemoveCameraTarget(base.transform, 2f);
			this._currentColor = this._originalColor;
			this.Colorize(this._currentColor);
		}

		private void Colorize(Color color)
		{
			for (int i = 0; i < this._renderers.Length; i++)
			{
				this._renderers[i].material.color = color;
			}
		}

		private void DropLoot()
		{
			if (this.Key != null)
			{
				this.Key.gameObject.SetActive(true);
				this.Key.transform.position = base.transform.position + new Vector3(0f, 3f, 0f);
			}
		}

		private void Die()
		{
			ProCamera2DShake.Instance.ShakeUsingPreset("EnemyDeath");
			this.OnPlayerOutOfSight();
			this.DropLoot();
			UnityEngine.Object.Destroy(base.gameObject, 0.2f);
		}
	}
}
