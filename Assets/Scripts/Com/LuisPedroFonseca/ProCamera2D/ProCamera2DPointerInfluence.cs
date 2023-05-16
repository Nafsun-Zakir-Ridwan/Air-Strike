using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-pointer-influence/")]
	public class ProCamera2DPointerInfluence : BasePC2D, IPreMover
	{
		public static string ExtensionName = "Pointer Influence";

		public float MaxHorizontalInfluence = 3f;

		public float MaxVerticalInfluence = 2f;

		public float InfluenceSmoothness = 0.2f;

		private Vector2 _influence;

		private Vector2 _velocity;

		private int _prmOrder = 3000;

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
			ProCamera2D.Instance.AddPreMover(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePreMover(this);
		}

		public override void OnReset()
		{
			this._influence = Vector2.zero;
			this._velocity = Vector2.zero;
		}

		public void PreMove(float deltaTime)
		{
			if (base.enabled)
			{
				this.ApplyInfluence(deltaTime);
			}
		}

		private void ApplyInfluence(float deltaTime)
		{
			Vector3 vector = this.ProCamera2D.GameCamera.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
			float num = vector.x.Remap(0f, 1f, -1f, 1f);
			float num2 = vector.y.Remap(0f, 1f, -1f, 1f);
			float x = num * this.MaxHorizontalInfluence;
			float y = num2 * this.MaxVerticalInfluence;
			this._influence = Vector2.SmoothDamp(this._influence, new Vector2(x, y), ref this._velocity, this.InfluenceSmoothness, float.PositiveInfinity, deltaTime);
			this.ProCamera2D.ApplyInfluence(this._influence);
		}
	}
}
