using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-zoom-to-fit/")]
	public class ProCamera2DZoomToFitTargets : BasePC2D, ISizeOverrider
	{
		private sealed class _Start_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal ProCamera2DZoomToFitTargets _this;

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

			public _Start_c__Iterator0()
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
					this._this._initialCamSize = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
					this._this._targetCamSize = this._this._initialCamSize;
					this._this._targetCamSizeSmoothed = this._this._targetCamSize;
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

		public static string ExtensionName = "Zoom To Fit";

		public float ZoomOutBorder = 0.6f;

		public float ZoomInBorder = 0.4f;

		public float ZoomInSmoothness = 2f;

		public float ZoomOutSmoothness = 1f;

		public float MaxZoomInAmount = 2f;

		public float MaxZoomOutAmount = 4f;

		public bool DisableWhenOneTarget = true;

		private float _zoomVelocity;

		private float _initialCamSize;

		private float _previousCamSize;

		private float _targetCamSize;

		private float _targetCamSizeSmoothed;

		private float _minCameraSize;

		private float _maxCameraSize;

		private int _soOrder;

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
			if (this.ProCamera2D == null)
			{
				return;
			}
			this.ProCamera2D.AddSizeOverrider(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemoveSizeOverrider(this);
		}

		private IEnumerator Start()
		{
			ProCamera2DZoomToFitTargets._Start_c__Iterator0 _Start_c__Iterator = new ProCamera2DZoomToFitTargets._Start_c__Iterator0();
			_Start_c__Iterator._this = this;
			return _Start_c__Iterator;
		}

		public float OverrideSize(float deltaTime, float originalSize)
		{
			if (!base.enabled)
			{
				return originalSize;
			}
			this._targetCamSizeSmoothed = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
			if (this.DisableWhenOneTarget && this.ProCamera2D.CameraTargets.Count <= 1)
			{
				this._targetCamSize = this._initialCamSize;
			}
			else
			{
				if (this._previousCamSize == this.ProCamera2D.ScreenSizeInWorldCoordinates.y)
				{
					this._targetCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
					this._targetCamSizeSmoothed = this._targetCamSize;
					this._zoomVelocity = 0f;
				}
				this.UpdateTargetCamSize();
			}
			this._previousCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y;
			return this._targetCamSizeSmoothed = Mathf.SmoothDamp(this._targetCamSizeSmoothed, this._targetCamSize, ref this._zoomVelocity, (this._targetCamSize >= this._targetCamSizeSmoothed) ? this.ZoomOutSmoothness : this.ZoomInSmoothness);
		}

		public override void OnReset()
		{
			this._zoomVelocity = 0f;
			this._previousCamSize = this._initialCamSize;
			this._targetCamSize = this._initialCamSize;
			this._targetCamSizeSmoothed = this._initialCamSize;
		}

		private void UpdateTargetCamSize()
		{
			float num = float.NegativeInfinity;
			float num2 = float.PositiveInfinity;
			float num3 = float.NegativeInfinity;
			float num4 = float.PositiveInfinity;
			for (int i = 0; i < this.ProCamera2D.CameraTargets.Count; i++)
			{
				Vector2 vector = new Vector2(this.Vector3H(this.ProCamera2D.CameraTargets[i].TargetPosition) + this.ProCamera2D.CameraTargets[i].TargetOffset.x, this.Vector3V(this.ProCamera2D.CameraTargets[i].TargetPosition) + this.ProCamera2D.CameraTargets[i].TargetOffset.y);
				num = ((vector.x <= num) ? num : vector.x);
				num2 = ((vector.x >= num2) ? num2 : vector.x);
				num3 = ((vector.y <= num3) ? num3 : vector.y);
				num4 = ((vector.y >= num4) ? num4 : vector.y);
			}
			float num5 = Mathf.Abs(num - num2) * 0.5f;
			float num6 = Mathf.Abs(num3 - num4) * 0.5f;
			if (num5 > this.ProCamera2D.ScreenSizeInWorldCoordinates.x * this.ZoomOutBorder * 0.5f || num6 > this.ProCamera2D.ScreenSizeInWorldCoordinates.y * this.ZoomOutBorder * 0.5f)
			{
				if (num5 / this.ProCamera2D.ScreenSizeInWorldCoordinates.x >= num6 / this.ProCamera2D.ScreenSizeInWorldCoordinates.y)
				{
					this._targetCamSize = num5 / this.ProCamera2D.GameCamera.aspect / this.ZoomOutBorder;
				}
				else
				{
					this._targetCamSize = num6 / this.ZoomOutBorder;
				}
			}
			else if (num5 < this.ProCamera2D.ScreenSizeInWorldCoordinates.x * this.ZoomInBorder * 0.5f && num6 < this.ProCamera2D.ScreenSizeInWorldCoordinates.y * this.ZoomInBorder * 0.5f)
			{
				if (num5 / this.ProCamera2D.ScreenSizeInWorldCoordinates.x >= num6 / this.ProCamera2D.ScreenSizeInWorldCoordinates.y)
				{
					this._targetCamSize = num5 / this.ProCamera2D.GameCamera.aspect / this.ZoomInBorder;
				}
				else
				{
					this._targetCamSize = num6 / this.ZoomInBorder;
				}
			}
			this._minCameraSize = this._initialCamSize / this.MaxZoomInAmount;
			this._maxCameraSize = this._initialCamSize * this.MaxZoomOutAmount;
			this._targetCamSize = Mathf.Clamp(this._targetCamSize, this._minCameraSize, this._maxCameraSize);
		}
	}
}
