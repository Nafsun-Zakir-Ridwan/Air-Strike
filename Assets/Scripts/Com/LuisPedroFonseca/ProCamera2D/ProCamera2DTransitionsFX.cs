using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-transitions-fx/")]
	public class ProCamera2DTransitionsFX : BasePC2D
	{
		private sealed class _TransitionRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float startValue;

			internal Material material;

			internal float endValue;

			internal float delay;

			internal float _t___0;

			internal float duration;

			internal EaseType easeType;

			internal ProCamera2DTransitionsFX _this;

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

			public _TransitionRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._this._blit.enabled = true;
					this._this._step = this.startValue;
					this._this._blit.CurrentMaterial = this.material;
					this._this._blit.CurrentMaterial.SetFloat(this._this._material_StepID, this._this._step);
					if (this.endValue == 0f)
					{
						if (this._this.OnTransitionEnterStarted != null)
						{
							this._this.OnTransitionEnterStarted();
						}
					}
					else if (this.endValue == 1f && this._this.OnTransitionExitStarted != null)
					{
						this._this.OnTransitionExitStarted();
					}
					if (this._this.OnTransitionStarted != null)
					{
						this._this.OnTransitionStarted();
					}
					if (this.delay > 0f)
					{
						this._current = new WaitForSeconds(this.delay);
						if (!this._disposing)
						{
							this._PC = 1;
						}
						return true;
					}
					break;
				case 1u:
					break;
				case 2u:
					goto IL_1DA;
				default:
					return false;
				}
				this._t___0 = 0f;
				IL_1DA:
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.duration;
					this._this._step = Utils.EaseFromTo(this.startValue, this.endValue, this._t___0, this.easeType);
					this.material.SetFloat(this._this._material_StepID, this._this._step);
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				}
				this._this._step = this.endValue;
				this.material.SetFloat(this._this._material_StepID, this._this._step);
				if (this.endValue == 0f)
				{
					if (this._this.OnTransitionEnterEnded != null)
					{
						this._this.OnTransitionEnterEnded();
					}
				}
				else if (this.endValue == 1f && this._this.OnTransitionExitEnded != null)
				{
					this._this.OnTransitionExitEnded();
				}
				if (this._this.OnTransitionEnded != null)
				{
					this._this.OnTransitionEnded();
				}
				if (this.endValue == 0f)
				{
					this._this._blit.enabled = false;
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

		public static string ExtensionName = "TransitionsFX";

		public Action OnTransitionEnterStarted;

		public Action OnTransitionEnterEnded;

		public Action OnTransitionExitStarted;

		public Action OnTransitionExitEnded;

		public Action OnTransitionStarted;

		public Action OnTransitionEnded;

		private static ProCamera2DTransitionsFX _instance;

		public TransitionsFXShaders TransitionShaderEnter;

		public float DurationEnter = 0.5f;

		public float DelayEnter;

		public EaseType EaseTypeEnter = EaseType.EaseOut;

		public Color BackgroundColorEnter = Color.black;

		public TransitionFXSide SideEnter;

		public TransitionFXDirection DirectionEnter;

		[Range(2f, 128f)]
		public int BlindsEnter = 16;

		public Texture TextureEnter;

		[Range(0f, 1f)]
		public float TextureSmoothingEnter = 0.2f;

		public TransitionsFXShaders TransitionShaderExit;

		public float DurationExit = 0.5f;

		public float DelayExit;

		public EaseType EaseTypeExit = EaseType.EaseOut;

		public Color BackgroundColorExit = Color.black;

		public TransitionFXSide SideExit;

		public TransitionFXDirection DirectionExit;

		[Range(2f, 128f)]
		public int BlindsExit = 16;

		public Texture TextureExit;

		[Range(0f, 1f)]
		public float TextureSmoothingExit = 0.2f;

		public bool StartSceneOnEnterState = true;

		private Coroutine TrnsitionCoroutine;

		private float _step;

		private Material TrnsitionEnterMaterial;

		private Material TrnsitionExitMaterial;

		private BasicBlit _blit;

		private int _material_StepID;

		private int _material_BackgroundColorID;

		private string _previousEnterShader = string.Empty;

		private string _previousExitShader = string.Empty;

		public static ProCamera2DTransitionsFX Instance
		{
			get
			{
				if (object.Equals(ProCamera2DTransitionsFX._instance, null))
				{
					ProCamera2DTransitionsFX._instance = ProCamera2D.Instance.GetComponent<ProCamera2DTransitionsFX>();
					if (object.Equals(ProCamera2DTransitionsFX._instance, null))
					{
						throw new UnityException("ProCamera2D does not have a TransitionFX extension.");
					}
				}
				return ProCamera2DTransitionsFX._instance;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			ProCamera2DTransitionsFX._instance = this;
			this._material_StepID = Shader.PropertyToID("_Step");
			this._material_BackgroundColorID = Shader.PropertyToID("_BackgroundColor");
			this._blit = base.gameObject.AddComponent<BasicBlit>();
			this._blit.enabled = false;
			this.UpdateTransitionsShaders();
			this.UpdateTransitionsProperties();
			this.UpdateTransitionsColor();
			if (this.StartSceneOnEnterState)
			{
				this._step = 1f;
				this._blit.CurrentMaterial = this.TrnsitionEnterMaterial;
				this._blit.CurrentMaterial.SetFloat(this._material_StepID, this._step);
				this._blit.enabled = true;
			}
		}

		public void TransitionEnter()
		{
			this.Transition(this.TrnsitionEnterMaterial, this.DurationEnter, this.DelayEnter, 1f, 0f, this.EaseTypeEnter);
		}

		public void TransitionExit()
		{
			this.Transition(this.TrnsitionExitMaterial, this.DurationExit, this.DelayExit, 0f, 1f, this.EaseTypeExit);
		}

		public void UpdateTransitionsShaders()
		{
			string text = this.TransitionShaderEnter.ToString();
			if (!this._previousEnterShader.Equals(text))
			{
				this.TrnsitionEnterMaterial = new Material(Shader.Find("Hidden/ProCamera2D/TransitionsFX/" + text));
				this._previousEnterShader = text;
			}
			string text2 = this.TransitionShaderExit.ToString();
			if (!this._previousExitShader.Equals(text2))
			{
				this.TrnsitionExitMaterial = new Material(Shader.Find("Hidden/ProCamera2D/TransitionsFX/" + text2));
				this._previousExitShader = text2;
			}
		}

		public void UpdateTransitionsProperties()
		{
			if (this.TransitionShaderEnter == TransitionsFXShaders.Wipe || this.TransitionShaderEnter == TransitionsFXShaders.Blinds)
			{
				this.TrnsitionEnterMaterial.SetInt("_Direction", (int)this.SideEnter);
				this.TrnsitionEnterMaterial.SetInt("_Blinds", this.BlindsEnter);
			}
			else if (this.TransitionShaderEnter == TransitionsFXShaders.Shutters)
			{
				this.TrnsitionEnterMaterial.SetInt("_Direction", (int)this.DirectionEnter);
			}
			else if (this.TransitionShaderEnter == TransitionsFXShaders.Texture)
			{
				this.TrnsitionEnterMaterial.SetTexture("TrnsitionTex", this.TextureEnter);
				this.TrnsitionEnterMaterial.SetFloat("_Smoothing", this.TextureSmoothingEnter);
			}
			if (this.TransitionShaderExit == TransitionsFXShaders.Wipe || this.TransitionShaderExit == TransitionsFXShaders.Blinds)
			{
				this.TrnsitionExitMaterial.SetInt("_Direction", (int)this.SideExit);
				this.TrnsitionExitMaterial.SetInt("_Blinds", this.BlindsExit);
			}
			else if (this.TransitionShaderExit == TransitionsFXShaders.Shutters)
			{
				this.TrnsitionExitMaterial.SetInt("_Direction", (int)this.DirectionExit);
			}
			else if (this.TransitionShaderExit == TransitionsFXShaders.Texture)
			{
				this.TrnsitionExitMaterial.SetTexture("TrnsitionTex", this.TextureExit);
				this.TrnsitionExitMaterial.SetFloat("_Smoothing", this.TextureSmoothingExit);
			}
		}

		public void UpdateTransitionsColor()
		{
			this.TrnsitionEnterMaterial.SetColor(this._material_BackgroundColorID, this.BackgroundColorEnter);
			this.TrnsitionExitMaterial.SetColor(this._material_BackgroundColorID, this.BackgroundColorExit);
		}

		public void Clear()
		{
			this._blit.enabled = false;
		}

		private void Transition(Material material, float duration, float delay, float startValue, float endValue, EaseType easeType)
		{
			if (this.TrnsitionEnterMaterial == null)
			{
				UnityEngine.Debug.LogWarning("TransitionsFX not initialized yet. You're probably calling TransitionEnter/Exit from an Awake method. Please call it from a Start method instead.");
				return;
			}
			if (this.TrnsitionCoroutine != null)
			{
				base.StopCoroutine(this.TrnsitionCoroutine);
			}
			this.TrnsitionCoroutine = base.StartCoroutine(this.TransitionRoutine(material, duration, delay, startValue, endValue, easeType));
		}

		private IEnumerator TransitionRoutine(Material material, float duration, float delay, float startValue, float endValue, EaseType easeType)
		{
			ProCamera2DTransitionsFX._TransitionRoutine_c__Iterator0 _TransitionRoutine_c__Iterator = new ProCamera2DTransitionsFX._TransitionRoutine_c__Iterator0();
			_TransitionRoutine_c__Iterator.startValue = startValue;
			_TransitionRoutine_c__Iterator.material = material;
			_TransitionRoutine_c__Iterator.endValue = endValue;
			_TransitionRoutine_c__Iterator.delay = delay;
			_TransitionRoutine_c__Iterator.duration = duration;
			_TransitionRoutine_c__Iterator.easeType = easeType;
			_TransitionRoutine_c__Iterator._this = this;
			return _TransitionRoutine_c__Iterator;
		}
	}
}
