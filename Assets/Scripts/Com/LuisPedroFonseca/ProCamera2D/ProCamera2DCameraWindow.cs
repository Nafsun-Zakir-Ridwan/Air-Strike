using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-camera-window/")]
	public class ProCamera2DCameraWindow : BasePC2D, IPositionDeltaChanger
	{
		public static string ExtensionName = "Camera Window";

		public Rect CameraWindowRect = new Rect(0f, 0f, 0.3f, 0.3f);

		private Rect _cameraWindowRectInWorldCoords;

		private int _pdcOrder;

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
			this.ProCamera2D.AddPositionDeltaChanger(this);
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
			this._cameraWindowRectInWorldCoords = this.GetRectAroundTransf(this.CameraWindowRect, this.ProCamera2D.ScreenSizeInWorldCoordinates, this.Trnsform);
			float arg = 0f;
			if (this.ProCamera2D.CameraTargetPositionSmoothed.x >= this._cameraWindowRectInWorldCoords.x + this._cameraWindowRectInWorldCoords.width)
			{
				arg = this.ProCamera2D.CameraTargetPositionSmoothed.x - (this.Vector3H(this.Trnsform.localPosition) + this._cameraWindowRectInWorldCoords.width / 2f + this.CameraWindowRect.x);
			}
			else if (this.ProCamera2D.CameraTargetPositionSmoothed.x <= this._cameraWindowRectInWorldCoords.x)
			{
				arg = this.ProCamera2D.CameraTargetPositionSmoothed.x - (this.Vector3H(this.Trnsform.localPosition) - this._cameraWindowRectInWorldCoords.width / 2f + this.CameraWindowRect.x);
			}
			float arg2 = 0f;
			if (this.ProCamera2D.CameraTargetPositionSmoothed.y >= this._cameraWindowRectInWorldCoords.y + this._cameraWindowRectInWorldCoords.height)
			{
				arg2 = this.ProCamera2D.CameraTargetPositionSmoothed.y - (this.Vector3V(this.Trnsform.localPosition) + this._cameraWindowRectInWorldCoords.height / 2f + this.CameraWindowRect.y);
			}
			else if (this.ProCamera2D.CameraTargetPositionSmoothed.y <= this._cameraWindowRectInWorldCoords.y)
			{
				arg2 = this.ProCamera2D.CameraTargetPositionSmoothed.y - (this.Vector3V(this.Trnsform.localPosition) - this._cameraWindowRectInWorldCoords.height / 2f + this.CameraWindowRect.y);
			}
			return this.VectorHV(arg, arg2);
		}

		private Rect GetRectAroundTransf(Rect rectNormalized, Vector2 rectSize, Transform transf)
		{
			Vector2 vector = new Vector2(rectNormalized.width * rectSize.x, rectNormalized.height * rectSize.y);
			float x = this.Vector3H(transf.localPosition) - vector.x / 2f + rectNormalized.x;
			float y = this.Vector3V(transf.localPosition) - vector.y / 2f + rectNormalized.y;
			return new Rect(x, y, vector.x, vector.y);
		}
	}
}
