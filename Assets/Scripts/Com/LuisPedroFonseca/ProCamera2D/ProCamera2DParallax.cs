using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[ExecuteInEditMode, HelpURL("http://www.procamera2d.com/user-guide/extension-parallax/")]
	public class ProCamera2DParallax : BasePC2D, IPostMover
	{
		private sealed class _Animate_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float[] _currentSpeeds___0;

			internal float _t___0;

			internal float duration;

			internal bool value;

			internal EaseType easeType;

			internal ProCamera2DParallax _this;

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

			public _Animate_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._currentSpeeds___0 = new float[this._this.ParallaxLayers.Count];
					for (int i = 0; i < this._currentSpeeds___0.Length; i++)
					{
						this._currentSpeeds___0[i] = this._this.ParallaxLayers[i].Speed;
					}
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.duration;
					for (int j = 0; j < this._this.ParallaxLayers.Count; j++)
					{
						if (this.value)
						{
							this._this.ParallaxLayers[j].Speed = Utils.EaseFromTo(this._currentSpeeds___0[j], this._this._initialSpeeds[j], this._t___0, this.easeType);
						}
						else
						{
							this._this.ParallaxLayers[j].Speed = Utils.EaseFromTo(this._currentSpeeds___0[j], 1f, this._t___0, this.easeType);
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
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

		public static string ExtensionName = "Parallax";

		public List<ProCamera2DParallaxLayer> ParallaxLayers = new List<ProCamera2DParallaxLayer>();

		public bool ParallaxHorizontal = true;

		public bool ParallaxVertical = true;

		public Vector3 RootPosition = Vector3.zero;

		private float _initialOrtographicSize;

		private float[] _initialSpeeds;

		private Coroutine _animateCoroutine;

		private int _pmOrder = 1000;

		public int PMOrder
		{
			get
			{
				return this._pmOrder;
			}
			set
			{
				this._pmOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (this.ProCamera2D == null)
			{
				return;
			}
			if (Application.isPlaying)
			{
				this.CalculateParallaxObjectsOffset();
			}
			foreach (ProCamera2DParallaxLayer current in this.ParallaxLayers)
			{
				if (current.ParallaxCamera != null)
				{
					current.CameraTransform = current.ParallaxCamera.transform;
				}
			}
			this._initialSpeeds = new float[this.ParallaxLayers.Count];
			for (int i = 0; i < this._initialSpeeds.Length; i++)
			{
				this._initialSpeeds[i] = this.ParallaxLayers[i].Speed;
			}
			if (this.ProCamera2D.GameCamera != null)
			{
				this._initialOrtographicSize = this.ProCamera2D.GameCamera.orthographicSize;
				if (!this.ProCamera2D.GameCamera.orthographic)
				{
					base.enabled = false;
				}
			}
			ProCamera2D.Instance.AddPostMover(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePostMover(this);
		}

		public void PostMove(float deltaTime)
		{
			if (base.enabled)
			{
				this.Move();
			}
		}

		public void CalculateParallaxObjectsOffset()
		{
			ProCamera2DParallaxObject[] array = UnityEngine.Object.FindObjectsOfType<ProCamera2DParallaxObject>();
			Dictionary<int, ProCamera2DParallaxLayer> dictionary = new Dictionary<int, ProCamera2DParallaxLayer>();
			for (int i = 0; i <= 31; i++)
			{
				foreach (ProCamera2DParallaxLayer current in this.ParallaxLayers)
				{
					if (current.LayerMask == (current.LayerMask | 1 << i))
					{
						dictionary[i] = current;
					}
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				Vector3 arg = array[j].transform.position - this.RootPosition;
				float arg2 = this.Vector3H(arg) * dictionary[array[j].gameObject.layer].Speed;
				float arg3 = this.Vector3V(arg) * dictionary[array[j].gameObject.layer].Speed;
				array[j].transform.position = this.VectorHVD(arg2, arg3, this.Vector3D(arg)) + this.RootPosition;
			}
		}

		private void Move()
		{
			Vector3 arg = this.Trnsform.position - this.RootPosition;
			for (int i = 0; i < this.ParallaxLayers.Count; i++)
			{
				if (this.ParallaxLayers[i].CameraTransform != null)
				{
					float arg2 = (!this.ParallaxHorizontal) ? this.Vector3H(arg) : (this.Vector3H(arg) * this.ParallaxLayers[i].Speed);
					float arg3 = (!this.ParallaxVertical) ? this.Vector3V(arg) : (this.Vector3V(arg) * this.ParallaxLayers[i].Speed);
					this.ParallaxLayers[i].CameraTransform.position = this.RootPosition + this.VectorHVD(arg2, arg3, this.Vector3D(this.Trnsform.position));
					this.ParallaxLayers[i].ParallaxCamera.orthographicSize = this._initialOrtographicSize + (this.ProCamera2D.GameCamera.orthographicSize - this._initialOrtographicSize) * this.ParallaxLayers[i].Speed;
				}
			}
		}

		public void ToggleParallax(bool value, float duration = 2f, EaseType easeType = EaseType.EaseInOut)
		{
			if (this._initialSpeeds == null)
			{
				return;
			}
			if (this._animateCoroutine != null)
			{
				base.StopCoroutine(this._animateCoroutine);
			}
			this._animateCoroutine = base.StartCoroutine(this.Animate(value, duration, easeType));
		}

		private IEnumerator Animate(bool value, float duration, EaseType easeType)
		{
			ProCamera2DParallax._Animate_c__Iterator0 _Animate_c__Iterator = new ProCamera2DParallax._Animate_c__Iterator0();
			_Animate_c__Iterator.duration = duration;
			_Animate_c__Iterator.value = value;
			_Animate_c__Iterator.easeType = easeType;
			_Animate_c__Iterator._this = this;
			return _Animate_c__Iterator;
		}
	}
}
