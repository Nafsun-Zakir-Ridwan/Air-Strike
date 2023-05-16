using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-pixel-perfect/")]
	public class ProCamera2DPixelPerfect : BasePC2D, IPositionOverrider
	{
		public static string ExtensionName = "Pixel Perfect";

		public float PixelsPerUnit = 32f;

		public AutoScaleMode ViewportAutoScale = AutoScaleMode.Floor;

		public Vector2 TargetViewportSizeInPixels = new Vector2(80f, 50f);

		[Range(1f, 32f)]
		public int Zoom = 1;

		public bool SnapMovementToGrid;

		public bool SnapCameraToGrid = true;

		public bool DrawGrid;

		public Color GridColor = new Color(1f, 0f, 0f, 0.1f);

		public float GridDensity;

		private float _pixelStep = -1f;

		private float _viewportScale;

		private Transform _parent;

		private int _poOrder = 2000;

		public float PixelStep
		{
			get
			{
				return this._pixelStep;
			}
		}

		public int POOrder
		{
			get
			{
				return this._poOrder;
			}
			set
			{
				this._poOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (!this.ProCamera2D.GameCamera.orthographic)
			{
				base.enabled = false;
				return;
			}
			this.ResizeCameraToPixelPerfect();
			this.ProCamera2D.AddPositionOverrider(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePositionOverrider(this);
		}

		public Vector3 OverridePosition(float deltaTime, Vector3 originalPosition)
		{
			if (!base.enabled)
			{
				return originalPosition;
			}
			float gridSize = this._pixelStep;
			if (this.SnapMovementToGrid && !this.SnapCameraToGrid)
			{
				gridSize = 1f / (this.PixelsPerUnit * this._viewportScale * (float)this.Zoom);
			}
			this._parent = this.Trnsform.parent;
			if (this._parent != null && this._parent.position != Vector3.zero)
			{
				this._parent.position = this.VectorHVD(Utils.AlignToGrid(this.Vector3H(this._parent.position), gridSize), Utils.AlignToGrid(this.Vector3V(this._parent.position), gridSize), this.Vector3D(this._parent.position));
			}
			return this.VectorHVD(Utils.AlignToGrid(this.Vector3H(originalPosition), gridSize), Utils.AlignToGrid(this.Vector3V(originalPosition), gridSize), 0f);
		}

		public void ResizeCameraToPixelPerfect()
		{
			this._viewportScale = this.CalculateViewportScale();
			this._pixelStep = this.CalculatePixelStep(this._viewportScale);
			float newSize = (float)this.ProCamera2D.GameCamera.pixelHeight * 0.5f * (1f / this.PixelsPerUnit) / ((float)this.Zoom * this._viewportScale);
			this.ProCamera2D.UpdateScreenSize(newSize, 0f, EaseType.EaseInOut);
		}

		public float CalculateViewportScale()
		{
			if (this.ViewportAutoScale == AutoScaleMode.None)
			{
				return 1f;
			}
			float num = (float)this.ProCamera2D.GameCamera.pixelWidth / this.TargetViewportSizeInPixels.x;
			float num2 = (float)this.ProCamera2D.GameCamera.pixelHeight / this.TargetViewportSizeInPixels.y;
			float num3 = (num <= num2) ? num : num2;
			AutoScaleMode viewportAutoScale = this.ViewportAutoScale;
			if (viewportAutoScale != AutoScaleMode.Floor)
			{
				if (viewportAutoScale != AutoScaleMode.Ceil)
				{
					if (viewportAutoScale == AutoScaleMode.Round)
					{
						num3 = (float)Math.Round((double)num3, MidpointRounding.AwayFromZero);
					}
				}
				else
				{
					num3 = (float)Mathf.CeilToInt(num3);
				}
			}
			else
			{
				num3 = (float)Mathf.FloorToInt(num3);
			}
			if (num3 < 1f)
			{
				num3 = 1f;
			}
			return num3;
		}

		private float CalculatePixelStep(float viewportScale)
		{
			return (!this.SnapMovementToGrid) ? (1f / (this.PixelsPerUnit * viewportScale * (float)this.Zoom)) : (1f / this.PixelsPerUnit);
		}
	}
}
