using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-limit-speed/")]
	public class ProCamera2DLimitSpeed : BasePC2D, IPositionDeltaChanger
	{
		public static string ExtensionName = "Limit Speed";

		public bool LimitHorizontalSpeed = true;

		public float MaxHorizontalSpeed = 2f;

		public bool LimitVerticalSpeed = true;

		public float MaxVerticalSpeed = 2f;

		private int _pdcOrder = 1000;

		public int PDCOrder
		{
			get
			{
				return this._pdcOrder;
			}
			set
			{
				this._pdcOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			ProCamera2D.Instance.AddPositionDeltaChanger(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePositionDeltaChanger(this);
		}

		public Vector3 AdjustDelta(float deltaTime, Vector3 originalDelta)
		{
			if (!base.enabled)
			{
				return originalDelta;
			}
			float num = 1f / deltaTime;
			float num2 = this.Vector3H(originalDelta) * num;
			float num3 = this.Vector3V(originalDelta) * num;
			if (this.LimitHorizontalSpeed)
			{
				num2 = Mathf.Clamp(num2, -this.MaxHorizontalSpeed, this.MaxHorizontalSpeed);
			}
			if (this.LimitVerticalSpeed)
			{
				num3 = Mathf.Clamp(num3, -this.MaxVerticalSpeed, this.MaxVerticalSpeed);
			}
			return this.VectorHV(num2 * deltaTime, num3 * deltaTime);
		}
	}
}
