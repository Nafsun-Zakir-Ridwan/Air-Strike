using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class PlayerHealth : MonoBehaviour
	{
		private sealed class _HitAnim_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal PlayerHealth _this;

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
					ProCamera2DShake.Instance.ShakeUsingPreset("PlayerHit");
					for (int i = 0; i < this._this._renderers.Length; i++)
					{
						this._this._renderers[i].material.color = Color.white;
					}
					this._current = new WaitForSeconds(0.05f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					for (int j = 0; j < this._this._renderers.Length; j++)
					{
						this._this._renderers[j].material.color = this._this._originalColor;
					}
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

		private Renderer[] _renderers;

		private Color _originalColor;

		private void Awake()
		{
			this._renderers = base.GetComponentsInChildren<Renderer>();
			this._originalColor = this._renderers[0].material.color;
		}

		private void Hit(int damage)
		{
			this.Health -= damage;
			base.StartCoroutine(this.HitAnim());
			if (this.Health <= 0)
			{
			}
		}

		private IEnumerator HitAnim()
		{
			PlayerHealth._HitAnim_c__Iterator0 _HitAnim_c__Iterator = new PlayerHealth._HitAnim_c__Iterator0();
			_HitAnim_c__Iterator._this = this;
			return _HitAnim_c__Iterator;
		}
	}
}
