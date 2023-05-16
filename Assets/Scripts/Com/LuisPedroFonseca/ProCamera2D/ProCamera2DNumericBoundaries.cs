using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-numeric-boundaries/")]
	public class ProCamera2DNumericBoundaries : BasePC2D, IPositionDeltaChanger, ISizeOverrider
	{
		public static string ExtensionName = "Numeric Boundaries";

		public Action OnBoundariesTransitionStarted;

		public Action OnBoundariesTransitionFinished;

		public bool UseNumericBoundaries = true;

		public bool UseTopBoundary;

		public float TopBoundary = 10f;

		public float TargetTopBoundary;

		public bool UseBottomBoundary = true;

		public float BottomBoundary = -10f;

		public float TargetBottomBoundary;

		public bool UseLeftBoundary;

		public float LeftBoundary = -10f;

		public float TargetLeftBoundary;

		public bool UseRightBoundary;

		public float RightBoundary = 10f;

		public float TargetRightBoundary;

		public bool IsCameraPositionHorizontallyBounded;

		public bool IsCameraPositionVerticallyBounded;

		public Coroutine TopBoundaryAnimRoutine;

		public Coroutine BottomBoundaryAnimRoutine;

		public Coroutine LeftBoundaryAnimRoutine;

		public Coroutine RightBoundaryAnimRoutine;

		public ProCamera2DTriggerBoundaries CurrentBoundariesTrigger;

		public Coroutine MoveCameraToTargetRoutine;

		public bool HasFiredTransitionStarted;

		public bool HasFiredTransitionFinished;

		public bool UseElasticBoundaries;

		[Range(0f, 10f)]
		public float HorizontalElasticityDuration = 0.5f;

		public float HorizontalElasticitySize = 2f;

		[Range(0f, 10f)]
		public float VerticalElasticityDuration = 0.5f;

		public float VerticalElasticitySize = 2f;

		public EaseType ElasticityEaseType;

		private float _verticallyBoundedDuration;

		private float _horizontallyBoundedDuration;

		private int _pdcOrder = 4000;

		private int _soOrder = 2000;

		public NumericBoundariesSettings Settings
		{
			get
			{
				return new NumericBoundariesSettings
				{
					UseNumericBoundaries = this.UseNumericBoundaries,
					UseTopBoundary = this.UseTopBoundary,
					TopBoundary = this.TopBoundary,
					UseBottomBoundary = this.UseBottomBoundary,
					BottomBoundary = this.BottomBoundary,
					UseLeftBoundary = this.UseLeftBoundary,
					LeftBoundary = this.LeftBoundary,
					UseRightBoundary = this.UseRightBoundary,
					RightBoundary = this.RightBoundary
				};
			}
			set
			{
				this.UseNumericBoundaries = value.UseNumericBoundaries;
				this.UseTopBoundary = value.UseTopBoundary;
				this.TopBoundary = value.TopBoundary;
				this.UseBottomBoundary = value.UseBottomBoundary;
				this.BottomBoundary = value.BottomBoundary;
				this.UseLeftBoundary = value.UseLeftBoundary;
				this.LeftBoundary = value.LeftBoundary;
				this.UseRightBoundary = value.UseRightBoundary;
				this.RightBoundary = value.RightBoundary;
			}
		}

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
			this.ProCamera2D.AddPositionDeltaChanger(this);
			this.ProCamera2D.AddSizeOverrider(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePositionDeltaChanger(this);
			this.ProCamera2D.RemoveSizeOverrider(this);
		}

		public Vector3 AdjustDelta(float deltaTime, Vector3 originalDelta)
		{
			if (!base.enabled || !this.UseNumericBoundaries)
			{
				return originalDelta;
			}
			this.IsCameraPositionHorizontallyBounded = false;
			this.ProCamera2D.IsCameraPositionLeftBounded = false;
			this.ProCamera2D.IsCameraPositionRightBounded = false;
			this.IsCameraPositionVerticallyBounded = false;
			this.ProCamera2D.IsCameraPositionTopBounded = false;
			this.ProCamera2D.IsCameraPositionBottomBounded = false;
			float num = this.Vector3H(this.ProCamera2D.LocalPosition) + this.Vector3H(originalDelta);
			if (this.UseLeftBoundary && num - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f < this.LeftBoundary)
			{
				num = this.LeftBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
				this.IsCameraPositionHorizontallyBounded = true;
				this.ProCamera2D.IsCameraPositionLeftBounded = true;
			}
			else if (this.UseRightBoundary && num + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f > this.RightBoundary)
			{
				num = this.RightBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
				this.IsCameraPositionHorizontallyBounded = true;
				this.ProCamera2D.IsCameraPositionRightBounded = true;
			}
			float num2 = this.Vector3V(this.ProCamera2D.LocalPosition) + this.Vector3V(originalDelta);
			if (this.UseBottomBoundary && num2 - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f < this.BottomBoundary)
			{
				num2 = this.BottomBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
				this.IsCameraPositionVerticallyBounded = true;
				this.ProCamera2D.IsCameraPositionBottomBounded = true;
			}
			else if (this.UseTopBoundary && num2 + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f > this.TopBoundary)
			{
				num2 = this.TopBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
				this.IsCameraPositionVerticallyBounded = true;
				this.ProCamera2D.IsCameraPositionTopBounded = true;
			}
			if (this.UseElasticBoundaries)
			{
				if (this.IsCameraPositionHorizontallyBounded)
				{
					this._horizontallyBoundedDuration = Mathf.Min(this.HorizontalElasticityDuration, this._horizontallyBoundedDuration + deltaTime);
					float value = 1f;
					if (this.HorizontalElasticityDuration > 0f)
					{
						value = this._horizontallyBoundedDuration / this.HorizontalElasticityDuration;
					}
					if (this.ProCamera2D.IsCameraPositionLeftBounded)
					{
						num = Utils.EaseFromTo(Mathf.Max(this.LeftBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f - this.HorizontalElasticitySize, this.Vector3H(this.Trnsform.localPosition)), this.LeftBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f, value, this.ElasticityEaseType);
					}
					else
					{
						num = Utils.EaseFromTo(Mathf.Min(this.RightBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f + this.HorizontalElasticitySize, this.Vector3H(this.Trnsform.localPosition)), this.RightBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f, value, this.ElasticityEaseType);
					}
				}
				else
				{
					this._horizontallyBoundedDuration = Mathf.Max(0f, this._horizontallyBoundedDuration - deltaTime);
				}
				if (this.IsCameraPositionVerticallyBounded)
				{
					this._verticallyBoundedDuration = Mathf.Min(this.VerticalElasticityDuration, this._verticallyBoundedDuration + deltaTime);
					float value2 = 1f;
					if (this.VerticalElasticityDuration > 0f)
					{
						value2 = this._verticallyBoundedDuration / this.VerticalElasticityDuration;
					}
					if (this.ProCamera2D.IsCameraPositionBottomBounded)
					{
						num2 = Utils.EaseFromTo(Mathf.Max(this.BottomBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f - this.VerticalElasticitySize, this.Vector3V(this.Trnsform.localPosition)), this.BottomBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f, value2, this.ElasticityEaseType);
					}
					else
					{
						num2 = Utils.EaseFromTo(Mathf.Min(this.TopBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f + this.VerticalElasticitySize, this.Vector3V(this.Trnsform.localPosition)), this.TopBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f, value2, this.ElasticityEaseType);
					}
				}
				else
				{
					this._verticallyBoundedDuration = Mathf.Max(0f, this._verticallyBoundedDuration - deltaTime);
				}
			}
			return this.VectorHV(num - this.Vector3H(this.ProCamera2D.LocalPosition), num2 - this.Vector3V(this.ProCamera2D.LocalPosition));
		}

		public float OverrideSize(float deltaTime, float originalSize)
		{
			if (!this.UseNumericBoundaries)
			{
				return originalSize;
			}
			float num = originalSize;
			Vector2 vector = new Vector2(this.RightBoundary - this.LeftBoundary, this.TopBoundary - this.BottomBoundary);
			if (this.UseRightBoundary && this.UseLeftBoundary && originalSize * this.ProCamera2D.GameCamera.aspect * 2f > vector.x)
			{
				num = vector.x / this.ProCamera2D.GameCamera.aspect / 2f;
			}
			if (this.UseTopBoundary && this.UseBottomBoundary && num * 2f > vector.y)
			{
				num = vector.y / 2f;
			}
			return num;
		}
	}
}
