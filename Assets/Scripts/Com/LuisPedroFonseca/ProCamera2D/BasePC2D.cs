using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	public abstract class BasePC2D : MonoBehaviour
	{
		public ProCamera2D ProCamera2D;

		protected Func<Vector3, float> Vector3H;

		protected Func<Vector3, float> Vector3V;

		protected Func<Vector3, float> Vector3D;

		protected Func<float, float, Vector3> VectorHV;

		protected Func<float, float, float, Vector3> VectorHVD;

		protected Transform Trnsform;

		private bool _enabled;

		private static Func<Vector3, float> __f__am_cache0;

		private static Func<Vector3, float> __f__am_cache1;

		private static Func<Vector3, float> __f__am_cache2;

		private static Func<float, float, Vector3> __f__am_cache3;

		private static Func<float, float, float, Vector3> __f__am_cache4;

		private static Func<Vector3, float> __f__am_cache5;

		private static Func<Vector3, float> __f__am_cache6;

		private static Func<Vector3, float> __f__am_cache7;

		private static Func<float, float, Vector3> __f__am_cache8;

		private static Func<float, float, float, Vector3> __f__am_cache9;

		private static Func<Vector3, float> __f__am_cacheA;

		private static Func<Vector3, float> __f__am_cacheB;

		private static Func<Vector3, float> __f__am_cacheC;

		private static Func<float, float, Vector3> __f__am_cacheD;

		private static Func<float, float, float, Vector3> __f__am_cacheE;

		protected virtual void Awake()
		{
			this.Trnsform = base.transform;
			if (this.ProCamera2D == null && Camera.main != null)
			{
				this.ProCamera2D = Camera.main.GetComponent<ProCamera2D>();
			}
			else if (this.ProCamera2D == null)
			{
				this.ProCamera2D = (UnityEngine.Object.FindObjectOfType(typeof(ProCamera2D)) as ProCamera2D);
			}
			if (this.ProCamera2D == null)
			{
				UnityEngine.Debug.LogWarning(base.GetType().Name + ": ProCamera2D not found! Please add the ProCamera2D.cs component to your main camera.");
				return;
			}
			if (base.enabled)
			{
				this.Enable();
			}
			this.ResetAxisFunctions();
		}

		protected virtual void OnEnable()
		{
			this.Enable();
		}

		protected virtual void OnDisable()
		{
			this.Disable();
		}

		protected virtual void OnDestroy()
		{
			this.Disable();
		}

		public virtual void OnReset()
		{
		}

		private void Enable()
		{
			if (this._enabled || this.ProCamera2D == null)
			{
				return;
			}
			this._enabled = true;
			ProCamera2D expr_2A = this.ProCamera2D;
			expr_2A.OnReset = (Action)Delegate.Combine(expr_2A.OnReset, new Action(this.OnReset));
		}

		private void Disable()
		{
			if (this.ProCamera2D != null && this._enabled)
			{
				this._enabled = false;
				ProCamera2D expr_29 = this.ProCamera2D;
				expr_29.OnReset = (Action)Delegate.Remove(expr_29.OnReset, new Action(this.OnReset));
			}
		}

		private void ResetAxisFunctions()
		{
			if (this.Vector3H != null)
			{
				return;
			}
			MovementAxis axis = this.ProCamera2D.Axis;
			if (axis != MovementAxis.XY)
			{
				if (axis != MovementAxis.XZ)
				{
					if (axis == MovementAxis.YZ)
					{
						this.Vector3H = ((Vector3 vector) => vector.z);
						this.Vector3V = ((Vector3 vector) => vector.y);
						this.Vector3D = ((Vector3 vector) => vector.x);
						this.VectorHV = ((float h, float v) => new Vector3(0f, v, h));
						this.VectorHVD = ((float h, float v, float d) => new Vector3(d, v, h));
					}
				}
				else
				{
					this.Vector3H = ((Vector3 vector) => vector.x);
					this.Vector3V = ((Vector3 vector) => vector.z);
					this.Vector3D = ((Vector3 vector) => vector.y);
					this.VectorHV = ((float h, float v) => new Vector3(h, 0f, v));
					this.VectorHVD = ((float h, float v, float d) => new Vector3(h, d, v));
				}
			}
			else
			{
				this.Vector3H = ((Vector3 vector) => vector.x);
				this.Vector3V = ((Vector3 vector) => vector.y);
				this.Vector3D = ((Vector3 vector) => vector.z);
				this.VectorHV = ((float h, float v) => new Vector3(h, v, 0f));
				this.VectorHVD = ((float h, float v, float d) => new Vector3(h, v, d));
			}
		}
	}
}
