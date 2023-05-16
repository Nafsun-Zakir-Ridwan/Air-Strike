using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-forward-focus/")]
	public class ProCamera2DForwardFocus : BasePC2D, IPreMover
	{
		private sealed class _Enable_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal ProCamera2DForwardFocus _this;

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

			public _Enable_c__Iterator0()
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
					this._this.__enabled = true;
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

		public static string ExtensionName = "Forward Focus";

		private const float EPSILON = 0.001f;

		public bool Progressive = true;

		public float SpeedMultiplier = 1f;

		public float TransitionSmoothness = 0.5f;

		public bool MaintainInfluenceOnStop = true;

		public Vector2 MovementThreshold = Vector2.zero;

		[Range(0.001f, 0.5f)]
		public float LeftFocus = 0.25f;

		[Range(0.001f, 0.5f)]
		public float RightFocus = 0.25f;

		[Range(0.001f, 0.5f)]
		public float TopFocus = 0.25f;

		[Range(0.001f, 0.5f)]
		public float BottomFocus = 0.25f;

		private float _hVel;

		private float _hVelSmooth;

		private float _vVel;

		private float _vVelSmooth;

		private float _targetHVel;

		private float _targetVVel;

		private bool _isFirstHorizontalCameraMovement;

		private bool _isFirstVerticalCameraMovement;

		private bool __enabled;

		private int _prmOrder = 2000;

		public int PrMOrder
		{
			get
			{
				return this._prmOrder;
			}
			set
			{
				this._prmOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			base.StartCoroutine(this.Enable());
			ProCamera2D.Instance.AddPreMover(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePreMover(this);
		}

		public void PreMove(float deltaTime)
		{
			if (this.__enabled && base.enabled)
			{
				this.ApplyInfluence(deltaTime);
			}
		}

		public override void OnReset()
		{
			this._hVel = 0f;
			this._hVelSmooth = 0f;
			this._vVel = 0f;
			this._vVelSmooth = 0f;
			this._targetHVel = 0f;
			this._targetVVel = 0f;
			this._isFirstHorizontalCameraMovement = false;
			this._isFirstVerticalCameraMovement = false;
			this.__enabled = false;
			base.StartCoroutine(this.Enable());
		}

		private IEnumerator Enable()
		{
			ProCamera2DForwardFocus._Enable_c__Iterator0 _Enable_c__Iterator = new ProCamera2DForwardFocus._Enable_c__Iterator0();
			_Enable_c__Iterator._this = this;
			return _Enable_c__Iterator;
		}

		private void ApplyInfluence(float deltaTime)
		{
			float num = this.Vector3H(this.ProCamera2D.TargetsMidPoint) - this.Vector3H(this.ProCamera2D.PreviousTargetsMidPoint);
			if (Mathf.Abs(num) < this.MovementThreshold.x)
			{
				num = 0f;
			}
			else
			{
				num /= deltaTime;
			}
			float num2 = this.Vector3V(this.ProCamera2D.TargetsMidPoint) - this.Vector3V(this.ProCamera2D.PreviousTargetsMidPoint);
			if (Mathf.Abs(num2) < this.MovementThreshold.y)
			{
				num2 = 0f;
			}
			else
			{
				num2 /= deltaTime;
			}
			if (this.Progressive)
			{
				num = Mathf.Clamp(num * this.SpeedMultiplier, -this.LeftFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.x, this.RightFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.x);
				num2 = Mathf.Clamp(num2 * this.SpeedMultiplier, -this.BottomFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.y, this.TopFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.y);
				if (this.MaintainInfluenceOnStop)
				{
					if ((Mathf.Sign(num) == 1f && num < this._hVel) || (Mathf.Sign(num) == -1f && num > this._hVel) || Mathf.Abs(num) < 0.001f)
					{
						num = this._hVel;
					}
					if ((Mathf.Sign(num2) == 1f && num2 < this._vVel) || (Mathf.Sign(num2) == -1f && num2 > this._vVel) || Mathf.Abs(num2) < 0.001f)
					{
						num2 = this._vVel;
					}
				}
			}
			else if (this.MaintainInfluenceOnStop)
			{
				bool flag;
				if (!this._isFirstHorizontalCameraMovement && Mathf.Abs(num) >= 0.001f)
				{
					this._isFirstHorizontalCameraMovement = true;
					flag = true;
				}
				else
				{
					flag = (Mathf.Sign(num) != Mathf.Sign(this._targetHVel));
				}
				if (Mathf.Abs(num) >= 0.001f && flag)
				{
					this._targetHVel = ((num >= 0f) ? this.RightFocus : (-this.LeftFocus)) * this.ProCamera2D.ScreenSizeInWorldCoordinates.x;
				}
				num = this._targetHVel;
				bool flag2;
				if (!this._isFirstVerticalCameraMovement && Mathf.Abs(num2) >= 0.001f)
				{
					this._isFirstVerticalCameraMovement = true;
					flag2 = true;
				}
				else
				{
					flag2 = (Mathf.Sign(num2) != Mathf.Sign(this._targetVVel));
				}
				if (Mathf.Abs(num2) >= 0.001f && flag2)
				{
					this._targetVVel = ((num2 >= 0f) ? this.TopFocus : (-this.BottomFocus)) * this.ProCamera2D.ScreenSizeInWorldCoordinates.y;
				}
				num2 = this._targetVVel;
			}
			else
			{
				if (Mathf.Abs(num) >= 0.001f)
				{
					num = ((num >= 0f) ? this.RightFocus : (-this.LeftFocus)) * this.ProCamera2D.ScreenSizeInWorldCoordinates.x;
				}
				else
				{
					num = 0f;
				}
				if (Mathf.Abs(num2) >= 0.001f)
				{
					num2 = ((num2 >= 0f) ? this.TopFocus : (-this.BottomFocus)) * this.ProCamera2D.ScreenSizeInWorldCoordinates.y;
				}
				else
				{
					num2 = 0f;
				}
			}
			num = Mathf.Clamp(num, -this.LeftFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.x, this.RightFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.x);
			num2 = Mathf.Clamp(num2, -this.BottomFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.y, this.TopFocus * this.ProCamera2D.ScreenSizeInWorldCoordinates.y);
			this._hVel = Mathf.SmoothDamp(this._hVel, num, ref this._hVelSmooth, this.TransitionSmoothness);
			this._vVel = Mathf.SmoothDamp(this._vVel, num2, ref this._vVelSmooth, this.TransitionSmoothness);
			this.ProCamera2D.ApplyInfluence(new Vector2(this._hVel, this._vVel));
		}
	}
}
