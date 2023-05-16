using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/trigger-zoom/")]
	public class ProCamera2DTriggerZoom : BaseTrigger
	{
		private sealed class _InsideTriggerRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector2 _targetPos___1;

			internal float _distancePercentage___1;

			internal float _finalTargetSize___1;

			internal float _newTargetOrtographicSize___1;

			internal ProCamera2DTriggerZoom _this;

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

			public _InsideTriggerRoutine_c__Iterator0()
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
					if (this._this._previousCamSize == this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y)
					{
						this._this._targetCamSize = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
						this._this._targetCamSizeSmoothed = this._this._targetCamSize;
						this._this._zoomVelocity = 0f;
					}
					break;
				default:
					return false;
				}
				if (this._this._insideTrigger && this._this._instanceID == this._this.ProCamera2D.CurrentZoomTriggerID)
				{
					this._this._exclusiveInfluencePercentage = this._this.ExclusiveInfluencePercentage;
					this._targetPos___1 = new Vector2(this._this.Vector3H((!this._this.UseTargetsMidPoint) ? this._this.TriggerTarget.position : this._this.ProCamera2D.TargetsMidPoint), this._this.Vector3V((!this._this.UseTargetsMidPoint) ? this._this.TriggerTarget.position : this._this.ProCamera2D.TargetsMidPoint));
					this._distancePercentage___1 = this._this.GetDistanceToCenterPercentage(this._targetPos___1);
					if (this._this.SetSizeAsMultiplier)
					{
						this._finalTargetSize___1 = this._this._startCamSize / this._this.TargetZoom;
					}
					else if (this._this.ProCamera2D.GameCamera.orthographic)
					{
						this._finalTargetSize___1 = this._this.TargetZoom;
					}
					else
					{
						this._finalTargetSize___1 = Mathf.Abs(this._this._initialCamDepth) * Mathf.Tan(this._this.TargetZoom * 0.5f * 0.0174532924f);
					}
					this._newTargetOrtographicSize___1 = this._this._initialCamSize * this._distancePercentage___1 + this._finalTargetSize___1 * (1f - this._distancePercentage___1);
					if ((this._finalTargetSize___1 > this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f && this._newTargetOrtographicSize___1 > this._this._targetCamSize) || (this._finalTargetSize___1 < this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f && this._newTargetOrtographicSize___1 < this._this._targetCamSize) || this._this.ResetSizeOnExit)
					{
						this._this._targetCamSize = this._newTargetOrtographicSize___1;
					}
					this._this._previousCamSize = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y;
					if (Mathf.Abs(this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f - this._this._targetCamSize) > 0.0001f)
					{
						this._this.UpdateScreenSize((!this._this.ResetSizeOnExit) ? this._this.ZoomSmoothness : this._this.ResetSizeSmoothness);
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

		private sealed class _OutsideTriggerRoutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal ProCamera2DTriggerZoom _this;

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

			public _OutsideTriggerRoutine_c__Iterator1()
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
				if (!this._this._insideTrigger && this._this._instanceID == this._this.ProCamera2D.CurrentZoomTriggerID && Mathf.Abs(this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f - this._this._targetCamSize) > 0.0001f)
				{
					this._this.UpdateScreenSize((!this._this.ResetSizeOnExit) ? this._this.ZoomSmoothness : this._this.ResetSizeSmoothness);
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this._zoomVelocity = 0f;
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

		public static string TriggerName = "Zoom Trigger";

		public bool SetSizeAsMultiplier = true;

		public float TargetZoom = 1.5f;

		public float ZoomSmoothness = 1f;

		[Range(0f, 1f)]
		public float ExclusiveInfluencePercentage = 0.25f;

		public bool ResetSizeOnExit;

		public float ResetSizeSmoothness = 1f;

		private float _startCamSize;

		private float _initialCamSize;

		private float _targetCamSize;

		private float _targetCamSizeSmoothed;

		private float _previousCamSize;

		private float _zoomVelocity;

		private float _initialCamDepth;

		private void Start()
		{
			if (this.ProCamera2D == null)
			{
				return;
			}
			this._startCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
			this._initialCamSize = this._startCamSize;
			this._targetCamSize = this._startCamSize;
			this._targetCamSizeSmoothed = this._startCamSize;
			this._initialCamDepth = this.Vector3D(this.ProCamera2D.LocalPosition);
		}

		protected override void EnteredTrigger()
		{
			base.EnteredTrigger();
			this.ProCamera2D.CurrentZoomTriggerID = this._instanceID;
			if (this.ResetSizeOnExit)
			{
				this._initialCamSize = this._startCamSize;
				this._targetCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
				this._targetCamSizeSmoothed = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
			}
			else
			{
				this._initialCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
				this._targetCamSize = this._initialCamSize;
				this._targetCamSizeSmoothed = this._initialCamSize;
			}
			base.StartCoroutine(this.InsideTriggerRoutine());
		}

		protected override void ExitedTrigger()
		{
			base.ExitedTrigger();
			if (this.ResetSizeOnExit)
			{
				this._targetCamSize = this._startCamSize;
				base.StartCoroutine(this.OutsideTriggerRoutine());
			}
		}

		private IEnumerator InsideTriggerRoutine()
		{
			ProCamera2DTriggerZoom._InsideTriggerRoutine_c__Iterator0 _InsideTriggerRoutine_c__Iterator = new ProCamera2DTriggerZoom._InsideTriggerRoutine_c__Iterator0();
			_InsideTriggerRoutine_c__Iterator._this = this;
			return _InsideTriggerRoutine_c__Iterator;
		}

		private IEnumerator OutsideTriggerRoutine()
		{
			ProCamera2DTriggerZoom._OutsideTriggerRoutine_c__Iterator1 _OutsideTriggerRoutine_c__Iterator = new ProCamera2DTriggerZoom._OutsideTriggerRoutine_c__Iterator1();
			_OutsideTriggerRoutine_c__Iterator._this = this;
			return _OutsideTriggerRoutine_c__Iterator;
		}

		protected void UpdateScreenSize(float smoothness)
		{
			this._targetCamSizeSmoothed = Mathf.SmoothDamp(this._targetCamSizeSmoothed, this._targetCamSize, ref this._zoomVelocity, smoothness);
			this.ProCamera2D.UpdateScreenSize(this._targetCamSizeSmoothed, 0f, EaseType.EaseInOut);
		}
	}
}
