using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/trigger-influence/")]
	public class ProCamera2DTriggerInfluence : BaseTrigger
	{
		private sealed class _InsideTriggerRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _previousDistancePercentage___0;

			internal float _distancePercentage___1;

			internal Vector2 _vectorFromPointToFocus___1;

			internal ProCamera2DTriggerInfluence _this;

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
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._previousDistancePercentage___0 = 1f;
					this._this._tempExclusivePoint = this._this.VectorHV(this._this.Vector3H(this._this.ProCamera2D.transform.position), this._this.Vector3V(this._this.ProCamera2D.transform.position));
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (this._this._insideTrigger)
				{
					this._this._exclusiveInfluencePercentage = this._this.ExclusiveInfluencePercentage;
					this._distancePercentage___1 = this._this.GetDistanceToCenterPercentage(new Vector2(this._this.Vector3H(this._this.ProCamera2D.TargetsMidPoint), this._this.Vector3V(this._this.ProCamera2D.TargetsMidPoint)));
					this._vectorFromPointToFocus___1 = new Vector2(this._this.Vector3H(this._this.ProCamera2D.TargetsMidPoint) + this._this.Vector3H(this._this.ProCamera2D.TargetsMidPoint) - this._this.Vector3H(this._this.ProCamera2D.PreviousTargetsMidPoint), this._this.Vector3V(this._this.ProCamera2D.TargetsMidPoint) + this._this.Vector3V(this._this.ProCamera2D.TargetsMidPoint) - this._this.Vector3V(this._this.ProCamera2D.PreviousTargetsMidPoint)) - new Vector2(this._this.Vector3H(this._this.FocusPoint.position), this._this.Vector3V(this._this.FocusPoint.position));
					if (this._distancePercentage___1 == 0f)
					{
						this._this.ProCamera2D.ExclusiveTargetPosition = new Vector3?(Vector3.SmoothDamp(this._this._tempExclusivePoint, this._this.VectorHV(this._this.Vector3H(this._this.FocusPoint.position), this._this.Vector3V(this._this.FocusPoint.position)), ref this._this._exclusivePointVelocity, this._this.InfluenceSmoothness));
						this._this._tempExclusivePoint = this._this.ProCamera2D.ExclusiveTargetPosition.Value;
						this._this._influence = -this._vectorFromPointToFocus___1 * (1f - this._distancePercentage___1);
						this._this.ProCamera2D.ApplyInfluence(this._this._influence);
					}
					else
					{
						if (this._previousDistancePercentage___0 == 0f)
						{
							this._this._influence = new Vector2(this._this.Vector3H(this._this.ProCamera2D.CameraTargetPositionSmoothed), this._this.Vector3V(this._this.ProCamera2D.CameraTargetPositionSmoothed)) - new Vector2(this._this.Vector3H(this._this.ProCamera2D.TargetsMidPoint) + this._this.Vector3H(this._this.ProCamera2D.TargetsMidPoint) - this._this.Vector3H(this._this.ProCamera2D.PreviousTargetsMidPoint), this._this.Vector3V(this._this.ProCamera2D.TargetsMidPoint) + this._this.Vector3V(this._this.ProCamera2D.TargetsMidPoint) - this._this.Vector3V(this._this.ProCamera2D.PreviousTargetsMidPoint)) + new Vector2(this._this.Vector3H(this._this.ProCamera2D.ParentPosition), this._this.Vector3V(this._this.ProCamera2D.ParentPosition));
						}
						this._this._influence = Vector2.SmoothDamp(this._this._influence, -this._vectorFromPointToFocus___1 * (1f - this._distancePercentage___1), ref this._this._velocity, this._this.InfluenceSmoothness, float.PositiveInfinity, Time.deltaTime);
						this._this.ProCamera2D.ApplyInfluence(this._this._influence);
						this._this._tempExclusivePoint = this._this.VectorHV(this._this.Vector3H(this._this.ProCamera2D.CameraTargetPosition), this._this.Vector3V(this._this.ProCamera2D.CameraTargetPosition)) + this._this.VectorHV(this._this.Vector3H(this._this.ProCamera2D.ParentPosition), this._this.Vector3V(this._this.ProCamera2D.ParentPosition));
					}
					this._previousDistancePercentage___0 = this._distancePercentage___1;
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 2;
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
			internal ProCamera2DTriggerInfluence _this;

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
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (!this._this._insideTrigger && this._this._influence != Vector2.zero)
				{
					this._this._influence = Vector2.SmoothDamp(this._this._influence, Vector2.zero, ref this._this._velocity, this._this.InfluenceSmoothness, float.PositiveInfinity, Time.deltaTime);
					this._this.ProCamera2D.ApplyInfluence(this._this._influence);
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 2;
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

		public static string TriggerName = "Influence Trigger";

		public Transform FocusPoint;

		public float InfluenceSmoothness = 0.3f;

		[Range(0f, 1f)]
		public float ExclusiveInfluencePercentage = 0.25f;

		private Vector2 _influence;

		private Vector2 _velocity;

		private Vector3 _exclusivePointVelocity;

		private Vector3 _tempExclusivePoint;

		private void Start()
		{
			if (this.FocusPoint == null)
			{
				this.FocusPoint = base.transform.Find("FocusPoint");
			}
			if (this.FocusPoint == null)
			{
				this.FocusPoint = base.transform;
			}
		}

		protected override void EnteredTrigger()
		{
			base.EnteredTrigger();
			base.StartCoroutine(this.InsideTriggerRoutine());
		}

		protected override void ExitedTrigger()
		{
			base.ExitedTrigger();
			base.StartCoroutine(this.OutsideTriggerRoutine());
		}

		private IEnumerator InsideTriggerRoutine()
		{
			ProCamera2DTriggerInfluence._InsideTriggerRoutine_c__Iterator0 _InsideTriggerRoutine_c__Iterator = new ProCamera2DTriggerInfluence._InsideTriggerRoutine_c__Iterator0();
			_InsideTriggerRoutine_c__Iterator._this = this;
			return _InsideTriggerRoutine_c__Iterator;
		}

		private IEnumerator OutsideTriggerRoutine()
		{
			ProCamera2DTriggerInfluence._OutsideTriggerRoutine_c__Iterator1 _OutsideTriggerRoutine_c__Iterator = new ProCamera2DTriggerInfluence._OutsideTriggerRoutine_c__Iterator1();
			_OutsideTriggerRoutine_c__Iterator._this = this;
			return _OutsideTriggerRoutine_c__Iterator;
		}
	}
}
