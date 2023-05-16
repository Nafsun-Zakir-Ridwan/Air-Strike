using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	public abstract class BaseTrigger : BasePC2D
	{
		private sealed class _TestTriggerRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal WaitForSeconds _waitForSeconds___0;

			internal BaseTrigger _this;

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

			public _TestTriggerRoutine_c__Iterator0()
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
					this._waitForSeconds___0 = new WaitForSeconds(this._this.UpdateInterval);
					break;
				case 2u:
					break;
				default:
					return false;
				}
				this._this.TestTrigger();
				this._current = this._waitForSeconds___0;
				if (!this._disposing)
				{
					this._PC = 2;
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

		public Action OnEnteredTrigger;

		public Action OnExitedTrigger;

		[global::Tooltip("Every X seconds detect collision. Smaller intervals are more precise but also require more processing.")]
		public float UpdateInterval = 0.1f;

		public TriggerShape TriggerShape;

		[global::Tooltip("If enabled, use the targets mid point to know when inside/outside the trigger.")]
		public bool UseTargetsMidPoint = true;

		[global::Tooltip("If UseTargetsMidPoint is disabled, use this transform to know when inside/outside the trigger.")]
		public Transform TriggerTarget;

		protected float _exclusiveInfluencePercentage;

		private Coroutine _testTriggerRoutine;

		protected bool _insideTrigger;

		protected Vector2 _vectorFromPointToCenter;

		protected int _instanceID;

		private bool _triggerEnabled;

		protected override void Awake()
		{
			base.Awake();
			if (this.ProCamera2D == null)
			{
				return;
			}
			this._instanceID = base.GetInstanceID();
			this.UpdateInterval += UnityEngine.Random.Range(-0.02f, 0.02f);
			this.Toggle(true);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (this._triggerEnabled)
			{
				this.Toggle(true);
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			this._testTriggerRoutine = null;
		}

		public void Toggle(bool value)
		{
			if (value)
			{
				if (this._testTriggerRoutine == null)
				{
					this._testTriggerRoutine = base.StartCoroutine(this.TestTriggerRoutine());
				}
				this._triggerEnabled = true;
			}
			else
			{
				if (this._testTriggerRoutine != null)
				{
					base.StopCoroutine(this._testTriggerRoutine);
					this._testTriggerRoutine = null;
				}
				this._triggerEnabled = false;
			}
		}

		public void TestTrigger()
		{
			Vector3 arg = this.ProCamera2D.TargetsMidPoint;
			if (!this.UseTargetsMidPoint && this.TriggerTarget != null)
			{
				arg = this.TriggerTarget.position;
			}
			if (this.TriggerShape == TriggerShape.RECTANGLE && Utils.IsInsideRectangle(this.Vector3H(this.Trnsform.position), this.Vector3V(this.Trnsform.position), this.Vector3H(this.Trnsform.localScale), this.Vector3V(this.Trnsform.localScale), this.Vector3H(arg), this.Vector3V(arg)))
			{
				if (!this._insideTrigger)
				{
					this.EnteredTrigger();
				}
			}
			else if (this.TriggerShape == TriggerShape.CIRCLE && Utils.IsInsideCircle(this.Vector3H(this.Trnsform.position), this.Vector3V(this.Trnsform.position), (this.Vector3H(this.Trnsform.localScale) + this.Vector3V(this.Trnsform.localScale)) * 0.25f, this.Vector3H(arg), this.Vector3V(arg)))
			{
				if (!this._insideTrigger)
				{
					this.EnteredTrigger();
				}
			}
			else if (this._insideTrigger)
			{
				this.ExitedTrigger();
			}
		}

		protected virtual void EnteredTrigger()
		{
			this._insideTrigger = true;
			if (this.OnEnteredTrigger != null)
			{
				this.OnEnteredTrigger();
			}
		}

		protected virtual void ExitedTrigger()
		{
			this._insideTrigger = false;
			if (this.OnExitedTrigger != null)
			{
				this.OnExitedTrigger();
			}
		}

		private IEnumerator TestTriggerRoutine()
		{
			BaseTrigger._TestTriggerRoutine_c__Iterator0 _TestTriggerRoutine_c__Iterator = new BaseTrigger._TestTriggerRoutine_c__Iterator0();
			_TestTriggerRoutine_c__Iterator._this = this;
			return _TestTriggerRoutine_c__Iterator;
		}

		protected float GetDistanceToCenterPercentage(Vector2 point)
		{
			this._vectorFromPointToCenter = point - new Vector2(this.Vector3H(this.Trnsform.position), this.Vector3V(this.Trnsform.position));
			if (this.TriggerShape == TriggerShape.RECTANGLE)
			{
				float f = this.Vector3H(this._vectorFromPointToCenter) / (this.Vector3H(this.Trnsform.localScale) * 0.5f);
				float f2 = this.Vector3V(this._vectorFromPointToCenter) / (this.Vector3V(this.Trnsform.localScale) * 0.5f);
				return Mathf.Max(Mathf.Abs(f), Mathf.Abs(f2)).Remap(this._exclusiveInfluencePercentage, 1f, 0f, 1f);
			}
			return (this._vectorFromPointToCenter.magnitude / ((this.Vector3H(this.Trnsform.localScale) + this.Vector3V(this.Trnsform.localScale)) * 0.25f)).Remap(this._exclusiveInfluencePercentage, 1f, 0f, 1f);
		}
	}
}
