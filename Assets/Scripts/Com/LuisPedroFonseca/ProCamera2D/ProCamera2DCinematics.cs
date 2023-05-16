using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-cinematics/")]
	public class ProCamera2DCinematics : BasePC2D, IPositionOverrider, ISizeOverrider
	{
		private sealed class _StartCinematicRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _count___0;

			internal ProCamera2DCinematics _this;

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

			public _StartCinematicRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (this._this.OnCinematicStarted != null)
					{
						this._this.OnCinematicStarted.Invoke();
					}
					this._this._startPos = this._this.ProCamera2D.LocalPosition;
					this._this._newPos = this._this.ProCamera2D.LocalPosition;
					this._this._newSize = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
					if (this._this.UseLetterbox)
					{
						if (this._this._letterbox == null)
						{
							this._this.SetupLetterbox();
						}
						this._this._letterbox.Color = this._this.LetterboxColor;
						this._this._letterbox.TweenTo(this._this.LetterboxAmount, this._this.LetterboxAnimDuration);
					}
					this._count___0 = -1;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._count___0 < this._this.CinematicTargets.Count - 1)
				{
					this._count___0++;
					this._this._skipTarget = false;
					this._this._goToCinematicRoutine = this._this.StartCoroutine(this._this.GoToCinematicTargetRoutine(this._this.CinematicTargets[this._count___0], this._count___0));
					this._current = this._this._goToCinematicRoutine;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.Stop();
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

		private sealed class _GoToCinematicTargetRoutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal CinematicTarget cinematicTarget;

			internal float _initialPosH___0;

			internal float _initialPosV___0;

			internal float _currentCameraSize___0;

			internal float _t___0;

			internal int targetIndex;

			internal ProCamera2DCinematics _this;

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

			public _GoToCinematicTargetRoutine_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (this.cinematicTarget.TargetTransform == null)
					{
						return false;
					}
					this._initialPosH___0 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition);
					this._initialPosV___0 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition);
					this._currentCameraSize___0 = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
					this._t___0 = 0f;
					if (this.cinematicTarget.EaseInDuration <= 0f)
					{
						float arg = this._this.Vector3H(this.cinematicTarget.TargetTransform.position) - this._this.Vector3H(this._this.ProCamera2D.ParentPosition);
						float arg2 = this._this.Vector3V(this.cinematicTarget.TargetTransform.position) - this._this.Vector3V(this._this.ProCamera2D.ParentPosition);
						this._this._newPos = this._this.VectorHVD(arg, arg2, 0f);
						this._this._newSize = this._this._initialCameraSize / this.cinematicTarget.Zoom;
						goto IL_369;
					}
					break;
				case 1u:
					break;
				case 2u:
					IL_510:
					if (this.cinematicTarget.HoldDuration >= 0f && this._t___0 > this.cinematicTarget.HoldDuration)
					{
						this._PC = -1;
						return false;
					}
					if (!this._this._paused)
					{
						this._t___0 += this._this.ProCamera2D.DeltaTime;
						float arg3 = this._this.Vector3H(this.cinematicTarget.TargetTransform.position) - this._this.Vector3H(this._this.ProCamera2D.ParentPosition);
						float arg4 = this._this.Vector3V(this.cinematicTarget.TargetTransform.position) - this._this.Vector3V(this._this.ProCamera2D.ParentPosition);
						if (this._this.UseNumericBoundaries)
						{
							this._this.LimitToNumericBoundaries(ref arg3, ref arg4);
						}
						this._this._newPos = this._this.VectorHVD(arg3, arg4, 0f);
						if (this._this._skipTarget)
						{
							return false;
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					if (!this._this._paused)
					{
						this._t___0 += this._this.ProCamera2D.DeltaTime / this.cinematicTarget.EaseInDuration;
						float arg5 = Utils.EaseFromTo(this._initialPosH___0, this._this.Vector3H(this.cinematicTarget.TargetTransform.position) - this._this.Vector3H(this._this.ProCamera2D.ParentPosition), this._t___0, this.cinematicTarget.EaseType);
						float arg6 = Utils.EaseFromTo(this._initialPosV___0, this._this.Vector3V(this.cinematicTarget.TargetTransform.position) - this._this.Vector3V(this._this.ProCamera2D.ParentPosition), this._t___0, this.cinematicTarget.EaseType);
						if (this._this.UseNumericBoundaries)
						{
							this._this.LimitToNumericBoundaries(ref arg5, ref arg6);
						}
						this._this._newPos = this._this.VectorHVD(arg5, arg6, 0f);
						this._this._newSize = Utils.EaseFromTo(this._currentCameraSize___0, this._this._initialCameraSize / this.cinematicTarget.Zoom, this._t___0, this.cinematicTarget.EaseType);
						if (this._this._skipTarget)
						{
							return false;
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				IL_369:
				if (this._this.OnCinematicTargetReached != null)
				{
					this._this.OnCinematicTargetReached.Invoke(this.targetIndex);
				}
				if (!string.IsNullOrEmpty(this.cinematicTarget.SendMessageName))
				{
					this.cinematicTarget.TargetTransform.SendMessage(this.cinematicTarget.SendMessageName, this.cinematicTarget.SendMessageParam, SendMessageOptions.DontRequireReceiver);
				}
				this._t___0 = 0f;
                //goto IL_510;
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

		private sealed class _EndCinematicRoutine_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialPosH___0;

			internal float _initialPosV___0;

			internal float _currentCameraSize___0;

			internal float _t___0;

			internal ProCamera2DCinematics _this;

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

			public _EndCinematicRoutine_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (this._this._letterbox != null && this._this.LetterboxAmount > 0f)
					{
						this._this._letterbox.TweenTo(0f, this._this.LetterboxAnimDuration);
					}
					this._initialPosH___0 = this._this.Vector3H(this._this._newPos);
					this._initialPosV___0 = this._this.Vector3V(this._this._newPos);
					this._currentCameraSize___0 = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					if (!this._this._paused)
					{
						this._t___0 += this._this.ProCamera2D.DeltaTime / this._this.EndDuration;
						float arg = 0f;
						float arg2 = 0f;
						if (this._this.ProCamera2D.CameraTargets.Count > 0)
						{
							arg = Utils.EaseFromTo(this._initialPosH___0, this._this.Vector3H(this._this._originalPos), this._t___0, this._this.EndEaseType);
							arg2 = Utils.EaseFromTo(this._initialPosV___0, this._this.Vector3V(this._this._originalPos), this._t___0, this._this.EndEaseType);
						}
						else
						{
							arg = Utils.EaseFromTo(this._initialPosH___0, this._this.Vector3H(this._this._startPos), this._t___0, this._this.EndEaseType);
							arg2 = Utils.EaseFromTo(this._initialPosV___0, this._this.Vector3V(this._this._startPos), this._t___0, this._this.EndEaseType);
						}
						if (this._this.UseNumericBoundaries)
						{
							this._this.LimitToNumericBoundaries(ref arg, ref arg2);
						}
						this._this._newPos = this._this.VectorHVD(arg, arg2, 0f);
						this._this._newSize = Utils.EaseFromTo(this._currentCameraSize___0, this._this._initialCameraSize, this._t___0, this._this.EndEaseType);
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				if (this._this.OnCinematicFinished != null)
				{
					this._this.OnCinematicFinished.Invoke();
				}
				this._this._isPlaying = false;
				if (this._this.ProCamera2D.CameraTargets.Count == 0)
				{
					this._this.ProCamera2D.Reset(true, true, true);
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

		public static string ExtensionName = "Cinematics";

		public UnityEvent OnCinematicStarted;

		public CinematicEvent OnCinematicTargetReached;

		public UnityEvent OnCinematicFinished;

		private bool _isPlaying;

		public List<CinematicTarget> CinematicTargets = new List<CinematicTarget>();

		public float EndDuration = 1f;

		public EaseType EndEaseType = EaseType.EaseOut;

		public bool UseNumericBoundaries;

		public bool UseLetterbox = true;

		[Range(0f, 0.5f)]
		public float LetterboxAmount = 0.1f;

		public float LetterboxAnimDuration = 1f;

		public Color LetterboxColor = Color.black;

		private float _initialCameraSize;

		private ProCamera2DNumericBoundaries _numericBoundaries;

		private ProCamera2DLetterbox _letterbox;

		private Coroutine _startCinematicRoutine;

		private Coroutine _goToCinematicRoutine;

		private Coroutine _endCinematicRoutine;

		private bool _skipTarget;

		private Vector3 _newPos;

		private Vector3 _originalPos;

		private Vector3 _startPos;

		private float _newSize;

		private bool _paused;

		private int _poOrder;

		private int _soOrder = 3000;

		private static Func<Camera, float> __f__am_cache0;

		public bool IsPlaying
		{
			get
			{
				return this._isPlaying;
			}
		}

		public int POOrder
		{
			get
			{
				return this._poOrder;
			}
			set
			{
				this._poOrder = value;
			}
		}

		public int SOOrder
		{
			get
			{
				return this._soOrder;
			}
			set
			{
				this._soOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (this.UseLetterbox)
			{
				this.SetupLetterbox();
			}
			this.ProCamera2D.AddPositionOverrider(this);
			this.ProCamera2D.AddSizeOverrider(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePositionOverrider(this);
			this.ProCamera2D.RemoveSizeOverrider(this);
		}

		public Vector3 OverridePosition(float deltaTime, Vector3 originalPosition)
		{
			if (!base.enabled)
			{
				return originalPosition;
			}
			this._originalPos = originalPosition;
			if (this._isPlaying)
			{
				return this._newPos;
			}
			return originalPosition;
		}

		public float OverrideSize(float deltaTime, float originalSize)
		{
			if (!base.enabled)
			{
				return originalSize;
			}
			if (this._isPlaying)
			{
				return this._newSize;
			}
			return originalSize;
		}

		public void Play()
		{
			if (this._isPlaying)
			{
				return;
			}
			this._paused = false;
			if (this.CinematicTargets.Count == 0)
			{
				UnityEngine.Debug.LogWarning("No cinematic targets added to the list");
				return;
			}
			this._initialCameraSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
			if (this.UseNumericBoundaries && this._numericBoundaries == null)
			{
				this._numericBoundaries = this.ProCamera2D.GetComponentInChildren<ProCamera2DNumericBoundaries>();
			}
			if (this._numericBoundaries == null)
			{
				this.UseNumericBoundaries = false;
			}
			this._isPlaying = true;
			if (this._endCinematicRoutine != null)
			{
				base.StopCoroutine(this._endCinematicRoutine);
				this._endCinematicRoutine = null;
			}
			if (this._startCinematicRoutine == null)
			{
				this._startCinematicRoutine = base.StartCoroutine(this.StartCinematicRoutine());
			}
		}

		public void Stop()
		{
			if (!this._isPlaying)
			{
				return;
			}
			if (this._startCinematicRoutine != null)
			{
				base.StopCoroutine(this._startCinematicRoutine);
				this._startCinematicRoutine = null;
			}
			if (this._goToCinematicRoutine != null)
			{
				base.StopCoroutine(this._goToCinematicRoutine);
				this._goToCinematicRoutine = null;
			}
			if (this._endCinematicRoutine == null)
			{
				this._endCinematicRoutine = base.StartCoroutine(this.EndCinematicRoutine());
			}
		}

		public void Toggle()
		{
			if (this._isPlaying)
			{
				this.Stop();
			}
			else
			{
				this.Play();
			}
		}

		public void GoToNextTarget()
		{
			this._skipTarget = true;
		}

		public void Pause()
		{
			this._paused = true;
		}

		public void Unpause()
		{
			this._paused = false;
		}

		public void AddCinematicTarget(Transform targetTransform, float easeInDuration = 1f, float holdDuration = 1f, float zoom = 1f, EaseType easeType = EaseType.EaseOut, string sendMessageName = "", string sendMessageParam = "", int index = -1)
		{
			CinematicTarget item = new CinematicTarget
			{
				TargetTransform = targetTransform,
				EaseInDuration = easeInDuration,
				HoldDuration = holdDuration,
				Zoom = zoom,
				EaseType = easeType,
				SendMessageName = sendMessageName,
				SendMessageParam = sendMessageParam
			};
			if (index == -1 || index > this.CinematicTargets.Count)
			{
				this.CinematicTargets.Add(item);
			}
			else
			{
				this.CinematicTargets.Insert(index, item);
			}
		}

		public void RemoveCinematicTarget(Transform targetTransform)
		{
			for (int i = 0; i < this.CinematicTargets.Count; i++)
			{
				if (this.CinematicTargets[i].TargetTransform.GetInstanceID() == targetTransform.GetInstanceID())
				{
					this.CinematicTargets.Remove(this.CinematicTargets[i]);
				}
			}
		}

		private IEnumerator StartCinematicRoutine()
		{
			ProCamera2DCinematics._StartCinematicRoutine_c__Iterator0 _StartCinematicRoutine_c__Iterator = new ProCamera2DCinematics._StartCinematicRoutine_c__Iterator0();
			_StartCinematicRoutine_c__Iterator._this = this;
			return _StartCinematicRoutine_c__Iterator;
		}

		private IEnumerator GoToCinematicTargetRoutine(CinematicTarget cinematicTarget, int targetIndex)
		{
			ProCamera2DCinematics._GoToCinematicTargetRoutine_c__Iterator1 _GoToCinematicTargetRoutine_c__Iterator = new ProCamera2DCinematics._GoToCinematicTargetRoutine_c__Iterator1();
			_GoToCinematicTargetRoutine_c__Iterator.cinematicTarget = cinematicTarget;
			_GoToCinematicTargetRoutine_c__Iterator.targetIndex = targetIndex;
			_GoToCinematicTargetRoutine_c__Iterator._this = this;
			return _GoToCinematicTargetRoutine_c__Iterator;
		}

		private IEnumerator EndCinematicRoutine()
		{
			ProCamera2DCinematics._EndCinematicRoutine_c__Iterator2 _EndCinematicRoutine_c__Iterator = new ProCamera2DCinematics._EndCinematicRoutine_c__Iterator2();
			_EndCinematicRoutine_c__Iterator._this = this;
			return _EndCinematicRoutine_c__Iterator;
		}

		private void SetupLetterbox()
		{
			ProCamera2DLetterbox componentInChildren = this.ProCamera2D.gameObject.GetComponentInChildren<ProCamera2DLetterbox>();
			if (componentInChildren == null)
			{
				Camera[] array = this.ProCamera2D.gameObject.GetComponentsInChildren<Camera>();
				array = (from c in array
				orderby c.depth descending
				select c).ToArray<Camera>();
				array[0].gameObject.AddComponent<ProCamera2DLetterbox>();
			}
			this._letterbox = componentInChildren;
		}

		private void LimitToNumericBoundaries(ref float horizontalPos, ref float verticalPos)
		{
			if (this._numericBoundaries.UseLeftBoundary && horizontalPos - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f < this._numericBoundaries.LeftBoundary)
			{
				horizontalPos = this._numericBoundaries.LeftBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
			}
			else if (this._numericBoundaries.UseRightBoundary && horizontalPos + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f > this._numericBoundaries.RightBoundary)
			{
				horizontalPos = this._numericBoundaries.RightBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
			}
			if (this._numericBoundaries.UseBottomBoundary && verticalPos - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f < this._numericBoundaries.BottomBoundary)
			{
				verticalPos = this._numericBoundaries.BottomBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
			}
			else if (this._numericBoundaries.UseTopBoundary && verticalPos + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f > this._numericBoundaries.TopBoundary)
			{
				verticalPos = this._numericBoundaries.TopBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
			}
		}
	}
}
