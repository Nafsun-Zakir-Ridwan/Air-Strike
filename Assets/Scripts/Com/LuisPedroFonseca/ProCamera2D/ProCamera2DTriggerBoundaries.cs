using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/trigger-boundaries/")]
	public class ProCamera2DTriggerBoundaries : BaseTrigger, IPositionOverrider
	{
		private sealed class _TurnOffPreviousTrigger_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal ProCamera2DTriggerBoundaries trigger;

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

			public _TurnOffPreviousTrigger_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForEndOfFrame();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this.trigger.Trnsitioning = false;
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

		private sealed class _Transition_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal bool _skip___0;

			internal ProCamera2DTriggerBoundaries _this;

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

			public _Transition_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (!this._this.UseTopBoundary && !this._this.UseBottomBoundary && !this._this.UseLeftBoundary && !this._this.UseRightBoundary)
					{
						this._this.NumericBoundaries.UseNumericBoundaries = false;
					}
					else
					{
						this._skip___0 = true;
						if (this._this.UseTopBoundary && (this._this.NumericBoundaries.TopBoundary != this._this.TopBoundary || !this._this.NumericBoundaries.UseTopBoundary))
						{
							this._skip___0 = false;
						}
						if (this._this.UseBottomBoundary && (this._this.NumericBoundaries.BottomBoundary != this._this.BottomBoundary || !this._this.NumericBoundaries.UseBottomBoundary))
						{
							this._skip___0 = false;
						}
						if (this._this.UseLeftBoundary && (this._this.NumericBoundaries.LeftBoundary != this._this.LeftBoundary || !this._this.NumericBoundaries.UseLeftBoundary))
						{
							this._skip___0 = false;
						}
						if (this._this.UseRightBoundary && (this._this.NumericBoundaries.RightBoundary != this._this.RightBoundary || !this._this.NumericBoundaries.UseRightBoundary))
						{
							this._skip___0 = false;
						}
						if (!this._skip___0)
						{
							this._this.NumericBoundaries.UseNumericBoundaries = true;
							this._this.GetTargetBoundaries();
							this._this._boundsAnim.UseTopBoundary = this._this.UseTopBoundary;
							this._this._boundsAnim.TopBoundary = this._this._targetTopBoundary;
							this._this._boundsAnim.UseBottomBoundary = this._this.UseBottomBoundary;
							this._this._boundsAnim.BottomBoundary = this._this._targetBottomBoundary;
							this._this._boundsAnim.UseLeftBoundary = this._this.UseLeftBoundary;
							this._this._boundsAnim.LeftBoundary = this._this._targetLeftBoundary;
							this._this._boundsAnim.UseRightBoundary = this._this.UseRightBoundary;
							this._this._boundsAnim.RightBoundary = this._this._targetRightBoundary;
							this._this._boundsAnim.TransitionDuration = this._this.TransitionDuration;
							this._this._boundsAnim.TransitionEaseType = this._this.TransitionEaseType;
							if (this._this.ChangeZoom && this._this._initialCamSize / this._this.TargetZoom != this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f)
							{
								this._this.ProCamera2D.UpdateScreenSize(this._this._initialCamSize / this._this.TargetZoom, this._this.ZoomSmoothness, this._this.TransitionEaseType);
							}
							if (this._this._boundsAnim.GetAnimsCount() > 1)
							{
								if (this._this.NumericBoundaries.MoveCameraToTargetRoutine != null)
								{
									this._this.NumericBoundaries.StopCoroutine(this._this.NumericBoundaries.MoveCameraToTargetRoutine);
								}
								this._this.NumericBoundaries.MoveCameraToTargetRoutine = this._this.NumericBoundaries.StartCoroutine(this._this.MoveCameraToTarget());
							}
							this._current = new WaitForEndOfFrame();
							if (!this._disposing)
							{
								this._PC = 1;
							}
							return true;
						}
					}
					break;
				case 1u:
					this._this._boundsAnim.Transition();
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

		private sealed class _MoveCameraToTarget_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialCamPosH___0;

			internal float _initialCamPosV___0;

			internal float _t___0;

			internal float _newPosH___1;

			internal float _newPosV___1;

			internal ProCamera2DTriggerBoundaries _this;

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

			public _MoveCameraToTarget_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._initialCamPosH___0 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition);
					this._initialCamPosV___0 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition);
					this._this._newPos = this._this.VectorHVD(this._initialCamPosH___0, this._initialCamPosV___0, 0f);
					this._this.Trnsitioning = true;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this._this.TransitionDuration;
					this._newPosH___1 = Utils.EaseFromTo(this._initialCamPosH___0, this._this.ProCamera2D.CameraTargetPositionSmoothed.x, this._t___0, this._this.TransitionEaseType);
					this._newPosV___1 = Utils.EaseFromTo(this._initialCamPosV___0, this._this.ProCamera2D.CameraTargetPositionSmoothed.y, this._t___0, this._this.TransitionEaseType);
					this._this.LimitToNumericBoundaries(ref this._newPosH___1, ref this._newPosV___1);
					this._this._newPos = this._this.VectorHVD(this._newPosH___1, this._newPosV___1, 0f);
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.NumericBoundaries.MoveCameraToTargetRoutine = null;
				this._this.Trnsitioning = false;
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

		public static string TriggerName = "Boundaries Trigger";

		public ProCamera2DNumericBoundaries NumericBoundaries;

		public bool AreBoundariesRelative = true;

		public bool UseTopBoundary = true;

		public float TopBoundary = 10f;

		public bool UseBottomBoundary = true;

		public float BottomBoundary = -10f;

		public bool UseLeftBoundary = true;

		public float LeftBoundary = -10f;

		public bool UseRightBoundary = true;

		public float RightBoundary = 10f;

		public float TransitionDuration = 1f;

		public EaseType TransitionEaseType;

		public bool ChangeZoom;

		public float TargetZoom = 1.5f;

		public float ZoomSmoothness = 1f;

		public bool _setAsStartingBoundaries;

		private float _initialCamSize;

		private BoundariesAnimator _boundsAnim;

		private float _targetTopBoundary;

		private float _targetBottomBoundary;

		private float _targetLeftBoundary;

		private float _targetRightBoundary;

		private bool Trnsitioning;

		private Vector3 _newPos;

		private int _poOrder = 1000;

		public bool IsCurrentTrigger
		{
			get
			{
				return this.NumericBoundaries.CurrentBoundariesTrigger._instanceID == this._instanceID;
			}
		}

		public bool SetAsStartingBoundaries
		{
			get
			{
				return this._setAsStartingBoundaries;
			}
			set
			{
				if (value && !this._setAsStartingBoundaries)
				{
					UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(ProCamera2DTriggerBoundaries));
					UnityEngine.Object[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						ProCamera2DTriggerBoundaries proCamera2DTriggerBoundaries = (ProCamera2DTriggerBoundaries)array2[i];
						proCamera2DTriggerBoundaries.SetAsStartingBoundaries = false;
					}
				}
				this._setAsStartingBoundaries = value;
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

		protected override void Awake()
		{
			base.Awake();
			ProCamera2D.Instance.AddPositionOverrider(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePositionOverrider(this);
		}

		private void Start()
		{
			if (this.ProCamera2D == null)
			{
				return;
			}
			if (this.NumericBoundaries == null)
			{
				ProCamera2DNumericBoundaries proCamera2DNumericBoundaries = UnityEngine.Object.FindObjectOfType<ProCamera2DNumericBoundaries>();
				this.NumericBoundaries = ((!(proCamera2DNumericBoundaries == null)) ? proCamera2DNumericBoundaries : this.ProCamera2D.gameObject.AddComponent<ProCamera2DNumericBoundaries>());
			}
			this._boundsAnim = new BoundariesAnimator(this.ProCamera2D, this.NumericBoundaries);
			BoundariesAnimator expr_6E = this._boundsAnim;
			expr_6E.OnTransitionStarted = (Action)Delegate.Combine(expr_6E.OnTransitionStarted, new Action(delegate
			{
				if (this.NumericBoundaries.OnBoundariesTransitionStarted != null)
				{
					this.NumericBoundaries.OnBoundariesTransitionStarted();
				}
			}));
			BoundariesAnimator expr_95 = this._boundsAnim;
			expr_95.OnTransitionFinished = (Action)Delegate.Combine(expr_95.OnTransitionFinished, new Action(delegate
			{
				if (this.NumericBoundaries.OnBoundariesTransitionFinished != null)
				{
					this.NumericBoundaries.OnBoundariesTransitionFinished();
				}
			}));
			this.GetTargetBoundaries();
			if (this.SetAsStartingBoundaries)
			{
				this.SetBoundaries();
			}
			this._initialCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
		}

		public Vector3 OverridePosition(float deltaTime, Vector3 originalPosition)
		{
			if (!base.enabled)
			{
				return originalPosition;
			}
			if (this.Trnsitioning)
			{
				return this._newPos;
			}
			return originalPosition;
		}

		protected override void EnteredTrigger()
		{
			base.EnteredTrigger();
			if (this.NumericBoundaries.CurrentBoundariesTrigger != null)
			{
				base.StartCoroutine(this.TurnOffPreviousTrigger(this.NumericBoundaries.CurrentBoundariesTrigger));
			}
			if ((this.NumericBoundaries.CurrentBoundariesTrigger != null && this.NumericBoundaries.CurrentBoundariesTrigger._instanceID != this._instanceID) || this.NumericBoundaries.CurrentBoundariesTrigger == null)
			{
				this.NumericBoundaries.CurrentBoundariesTrigger = this;
				base.StartCoroutine(this.Transition());
			}
		}

		private IEnumerator TurnOffPreviousTrigger(ProCamera2DTriggerBoundaries trigger)
		{
			ProCamera2DTriggerBoundaries._TurnOffPreviousTrigger_c__Iterator0 _TurnOffPreviousTrigger_c__Iterator = new ProCamera2DTriggerBoundaries._TurnOffPreviousTrigger_c__Iterator0();
			_TurnOffPreviousTrigger_c__Iterator.trigger = trigger;
			return _TurnOffPreviousTrigger_c__Iterator;
		}

		public void SetBoundaries()
		{
			if (this.NumericBoundaries != null)
			{
				this.NumericBoundaries.CurrentBoundariesTrigger = this;
				this.NumericBoundaries.UseLeftBoundary = this.UseLeftBoundary;
				if (this.UseLeftBoundary)
				{
					this.NumericBoundaries.LeftBoundary = (this.NumericBoundaries.TargetLeftBoundary = this._targetLeftBoundary);
				}
				this.NumericBoundaries.UseRightBoundary = this.UseRightBoundary;
				if (this.UseRightBoundary)
				{
					this.NumericBoundaries.RightBoundary = (this.NumericBoundaries.TargetRightBoundary = this._targetRightBoundary);
				}
				this.NumericBoundaries.UseTopBoundary = this.UseTopBoundary;
				if (this.UseTopBoundary)
				{
					this.NumericBoundaries.TopBoundary = (this.NumericBoundaries.TargetTopBoundary = this._targetTopBoundary);
				}
				this.NumericBoundaries.UseBottomBoundary = this.UseBottomBoundary;
				if (this.UseBottomBoundary)
				{
					this.NumericBoundaries.BottomBoundary = (this.NumericBoundaries.TargetBottomBoundary = this._targetBottomBoundary);
				}
				if (!this.UseTopBoundary && !this.UseBottomBoundary && !this.UseLeftBoundary && !this.UseRightBoundary)
				{
					this.NumericBoundaries.UseNumericBoundaries = false;
				}
				else
				{
					this.NumericBoundaries.UseNumericBoundaries = true;
				}
			}
		}

		private void GetTargetBoundaries()
		{
			if (this.AreBoundariesRelative)
			{
				this._targetTopBoundary = this.Vector3V(base.transform.position) + this.TopBoundary;
				this._targetBottomBoundary = this.Vector3V(base.transform.position) + this.BottomBoundary;
				this._targetLeftBoundary = this.Vector3H(base.transform.position) + this.LeftBoundary;
				this._targetRightBoundary = this.Vector3H(base.transform.position) + this.RightBoundary;
			}
			else
			{
				this._targetTopBoundary = this.TopBoundary;
				this._targetBottomBoundary = this.BottomBoundary;
				this._targetLeftBoundary = this.LeftBoundary;
				this._targetRightBoundary = this.RightBoundary;
			}
		}

		private IEnumerator Transition()
		{
			ProCamera2DTriggerBoundaries._Transition_c__Iterator1 _Transition_c__Iterator = new ProCamera2DTriggerBoundaries._Transition_c__Iterator1();
			_Transition_c__Iterator._this = this;
			return _Transition_c__Iterator;
		}

		private IEnumerator MoveCameraToTarget()
		{
			ProCamera2DTriggerBoundaries._MoveCameraToTarget_c__Iterator2 _MoveCameraToTarget_c__Iterator = new ProCamera2DTriggerBoundaries._MoveCameraToTarget_c__Iterator2();
			_MoveCameraToTarget_c__Iterator._this = this;
			return _MoveCameraToTarget_c__Iterator;
		}

		private void LimitToNumericBoundaries(ref float horizontalPos, ref float verticalPos)
		{
			if (this.NumericBoundaries.UseLeftBoundary && horizontalPos - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f < this.NumericBoundaries.LeftBoundary)
			{
				horizontalPos = this.NumericBoundaries.LeftBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
			}
			else if (this.NumericBoundaries.UseRightBoundary && horizontalPos + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f > this.NumericBoundaries.RightBoundary)
			{
				horizontalPos = this.NumericBoundaries.RightBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
			}
			if (this.NumericBoundaries.UseBottomBoundary && verticalPos - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f < this.NumericBoundaries.BottomBoundary)
			{
				verticalPos = this.NumericBoundaries.BottomBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
			}
			else if (this.NumericBoundaries.UseTopBoundary && verticalPos + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f > this.NumericBoundaries.TopBoundary)
			{
				verticalPos = this.NumericBoundaries.TopBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
			}
		}
	}
}
