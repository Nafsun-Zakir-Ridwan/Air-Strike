using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[ExecuteInEditMode]
	public class ProCamera2DLetterbox : MonoBehaviour
	{
		private sealed class _TweenToRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialAmount___0;

			internal float _t___0;

			internal float duration;

			internal float targetAmount;

			internal ProCamera2DLetterbox _this;

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

			public _TweenToRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._initialAmount___0 = this._this.Amount;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				case 2u:
					this._PC = -1;
					return false;
				default:
					return false;
				}
				if (this._t___0 > 1f)
				{
					this._this.Amount = this.targetAmount;
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
				}
				else
				{
					this._t___0 += Time.deltaTime / this.duration;
					this._this.Amount = Utils.EaseFromTo(this._initialAmount___0, this.targetAmount, this._t___0, EaseType.EaseOut);
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
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

		[Range(0f, 0.5f)]
		public float Amount;

		public Color Color;

		private Material _material;

		private Material material
		{
			get
			{
				if (this._material == null)
				{
					this._material = new Material(Shader.Find("Hidden/ProCamera2D/Letterbox"));
					this._material.hideFlags = HideFlags.HideAndDontSave;
				}
				return this._material;
			}
		}

		private void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
		}

		private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
		{
			if (Mathf.Approximately(this.Amount, 0f) || this.material == null)
			{
				Graphics.Blit(sourceTexture, destTexture);
				return;
			}
			this.Amount = Mathf.Clamp01(this.Amount);
			this.material.SetFloat("_Top", 1f - this.Amount);
			this.material.SetFloat("_Bottom", this.Amount);
			this.material.SetColor("_Color", this.Color);
			Graphics.Blit(sourceTexture, destTexture, this.material);
		}

		private void OnDisable()
		{
			if (this._material)
			{
				UnityEngine.Object.DestroyImmediate(this._material);
			}
		}

		public void TweenTo(float targetAmount, float duration)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.TweenToRoutine(targetAmount, duration));
		}

		private IEnumerator TweenToRoutine(float targetAmount, float duration)
		{
			ProCamera2DLetterbox._TweenToRoutine_c__Iterator0 _TweenToRoutine_c__Iterator = new ProCamera2DLetterbox._TweenToRoutine_c__Iterator0();
			_TweenToRoutine_c__Iterator.duration = duration;
			_TweenToRoutine_c__Iterator.targetAmount = targetAmount;
			_TweenToRoutine_c__Iterator._this = this;
			return _TweenToRoutine_c__Iterator;
		}
	}
}
