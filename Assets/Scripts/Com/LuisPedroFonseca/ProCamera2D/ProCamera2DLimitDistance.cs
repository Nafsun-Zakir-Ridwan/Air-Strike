using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-limit-distance/")]
	public class ProCamera2DLimitDistance : BasePC2D, IPositionDeltaChanger
	{
		public static string ExtensionName = "Limit Distance";

		public bool LimitHorizontalCameraDistance = true;

		public float MaxHorizontalTargetDistance = 0.8f;

		public bool LimitVerticalCameraDistance = true;

		public float MaxVerticalTargetDistance = 0.8f;

		private int _pdcOrder = 2000;

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
			float num = this.Vector3H(originalDelta);
			bool flag = false;
			if (this.LimitHorizontalCameraDistance)
			{
				float num2 = this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f * this.MaxHorizontalTargetDistance;
				if (this.ProCamera2D.CameraTargetPosition.x > num + this.Vector3H(this.ProCamera2D.LocalPosition) + num2)
				{
					num = this.ProCamera2D.CameraTargetPosition.x - (this.Vector3H(this.ProCamera2D.LocalPosition) + num2);
					flag = true;
				}
				else if (this.ProCamera2D.CameraTargetPosition.x < num + this.Vector3H(this.ProCamera2D.LocalPosition) - num2)
				{
					num = this.ProCamera2D.CameraTargetPosition.x - (this.Vector3H(this.ProCamera2D.LocalPosition) - num2);
					flag = true;
				}
			}
			float num3 = this.Vector3V(originalDelta);
			bool flag2 = false;
			if (this.LimitVerticalCameraDistance)
			{
				float num4 = this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f * this.MaxVerticalTargetDistance;
				if (this.ProCamera2D.CameraTargetPosition.y > num3 + this.Vector3V(this.ProCamera2D.LocalPosition) + num4)
				{
					num3 = this.ProCamera2D.CameraTargetPosition.y - (this.Vector3V(this.ProCamera2D.LocalPosition) + num4);
					flag2 = true;
				}
				else if (this.ProCamera2D.CameraTargetPosition.y < num3 + this.Vector3V(this.ProCamera2D.LocalPosition) - num4)
				{
					num3 = this.ProCamera2D.CameraTargetPosition.y - (this.Vector3V(this.ProCamera2D.LocalPosition) - num4);
					flag2 = true;
				}
			}
			this.ProCamera2D.CameraTargetPositionSmoothed = new Vector2((!flag) ? this.ProCamera2D.CameraTargetPositionSmoothed.x : (this.Vector3H(this.ProCamera2D.LocalPosition) + num), (!flag2) ? this.ProCamera2D.CameraTargetPositionSmoothed.y : (this.Vector3V(this.ProCamera2D.LocalPosition) + num3));
			return this.VectorHV(num, num3);
		}
	}
}
