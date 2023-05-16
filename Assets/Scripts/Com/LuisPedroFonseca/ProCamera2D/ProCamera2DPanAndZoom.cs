using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-pan-and-zoom/")]
	public class ProCamera2DPanAndZoom : BasePC2D, ISizeDeltaChanger, IPreMover
	{
		public static string ExtensionName = "Pan And Zoom";

		public bool AllowZoom = true;

		public float MouseZoomSpeed = 10f;

		public float PinchZoomSpeed = 50f;

		[Range(0f, 2f)]
		public float ZoomSmoothness = 0.2f;

		public float MaxZoomInAmount = 2f;

		public float MaxZoomOutAmount = 2f;

		public bool ZoomToInputCenter = true;

		private float _zoomAmount;

		private float _initialCamSize;

		private bool _zoomStarted;

		private float _origFollowSmoothnessX;

		private float _origFollowSmoothnessY;

		private float _prevZoomAmount;

		private float _zoomVelocity;

		private Vector3 _zoomPoint;

		private float _touchZoomTime;

		public bool AllowPan = true;

		public bool UsePanByDrag = true;

		[Range(0f, 1f)]
		public float StopSpeedOnDragStart = 0.95f;

		public Rect DraggableAreaRect = new Rect(0f, 0f, 1f, 1f);

		public Vector2 DragPanSpeedMultiplier = new Vector2(1f, 1f);

		public bool UsePanByMoveToEdges;

		public Vector2 EdgesPanSpeed = new Vector2(2f, 2f);

		[Range(0f, 0.99f)]
		public float HorizontalPanEdges = 0.9f;

		[Range(0f, 0.99f)]
		public float VerticalPanEdges = 0.9f;

		[HideInInspector]
		public bool ResetPrevPanPoint;

		private Vector2 _panDelta;

		private Transform _panTarget;

		private Vector3 _prevMousePosition;

		private Vector3 _prevTouchPosition;

		private bool _onMaxZoom;

		private bool _onMinZoom;

		private int _prmOrder;

		private int _sdcOrder;

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

		public int SDCOrder
		{
			get
			{
				return this._sdcOrder;
			}
			set
			{
				this._sdcOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			this.UpdateCurrentFollowSmoothness();
			this._panTarget = new GameObject("PC2DPanTarget").transform;
			this.ProCamera2D.AddPreMover(this);
			this.ProCamera2D.AddSizeDeltaChanger(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePreMover(this);
			this.ProCamera2D.RemoveSizeDeltaChanger(this);
		}

		private void Start()
		{
			this._initialCamSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 0.5f;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.CenterPanTargetOnCamera(1f);
			ProCamera2D.Instance.AddCameraTarget(this._panTarget, 1f, 1f, 0f, default(Vector2));
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			this.ResetPrevPanPoint = true;
			this._onMaxZoom = false;
			this._onMinZoom = false;
			this.ProCamera2D.RemoveCameraTarget(this._panTarget, 0f);
		}

		public void PreMove(float deltaTime)
		{
			if (base.enabled && this.AllowPan)
			{
				this.Pan(deltaTime);
			}
		}

		public float AdjustSize(float deltaTime, float originalDelta)
		{
			if (base.enabled && this.AllowZoom)
			{
				return this.Zoom(deltaTime) + originalDelta;
			}
			return originalDelta;
		}

		private void Pan(float deltaTime)
		{
			this._panDelta = Vector2.zero;
			if (Time.time - this._touchZoomTime < 0.1f)
			{
				return;
			}
			if (UnityEngine.Input.touchCount == 1)
			{
				Touch touch = UnityEngine.Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
				{
					this._prevTouchPosition = new Vector3(UnityEngine.Input.GetTouch(0).position.x, UnityEngine.Input.GetTouch(0).position.y, Mathf.Abs(this.Vector3D(this.ProCamera2D.LocalPosition)));
					this.CenterPanTargetOnCamera(this.StopSpeedOnDragStart);
				}
				Vector3 vector = new Vector3(touch.position.x, touch.position.y, Mathf.Abs(this.Vector3D(this.ProCamera2D.LocalPosition)));
				Vector2 normalizedInput = new Vector2(touch.position.x / (float)Screen.width, touch.position.y / (float)Screen.height);
				if (this.InsideDraggableArea(normalizedInput))
				{
					Vector3 a = this.ProCamera2D.GameCamera.ScreenToWorldPoint(this._prevTouchPosition);
					if (this.ResetPrevPanPoint)
					{
						a = this.ProCamera2D.GameCamera.ScreenToWorldPoint(vector);
						this.ResetPrevPanPoint = false;
					}
					Vector3 b = this.ProCamera2D.GameCamera.ScreenToWorldPoint(vector);
					Vector3 arg = a - b;
					this._panDelta = new Vector2(this.Vector3H(arg), this.Vector3V(arg));
				}
				this._prevTouchPosition = vector;
			}
			Vector2 dragPanSpeedMultiplier = this.DragPanSpeedMultiplier;
			if (this._panDelta != Vector2.zero)
			{
				Vector3 translation = this.VectorHV(this._panDelta.x * dragPanSpeedMultiplier.x, this._panDelta.y * dragPanSpeedMultiplier.y);
				this._panTarget.Translate(translation);
			}
			if ((this.ProCamera2D.IsCameraPositionLeftBounded && this.Vector3H(this._panTarget.position) < this.Vector3H(this.ProCamera2D.LocalPosition)) || (this.ProCamera2D.IsCameraPositionRightBounded && this.Vector3H(this._panTarget.position) > this.Vector3H(this.ProCamera2D.LocalPosition)))
			{
				this._panTarget.position = this.VectorHVD(this.Vector3H(this.ProCamera2D.LocalPosition), this.Vector3V(this._panTarget.position), this.Vector3D(this._panTarget.position));
			}
			if ((this.ProCamera2D.IsCameraPositionBottomBounded && this.Vector3V(this._panTarget.position) < this.Vector3V(this.ProCamera2D.LocalPosition)) || (this.ProCamera2D.IsCameraPositionTopBounded && this.Vector3V(this._panTarget.position) > this.Vector3V(this.ProCamera2D.LocalPosition)))
			{
				this._panTarget.position = this.VectorHVD(this.Vector3H(this._panTarget.position), this.Vector3V(this.ProCamera2D.LocalPosition), this.Vector3D(this._panTarget.position));
			}
		}

		private float Zoom(float deltaTime)
		{
			if (this._panDelta != Vector2.zero)
			{
				this.CancelZoom();
				this.RestoreFollowSmoothness();
				return 0f;
			}
			float num = 0f;
			if (UnityEngine.Input.touchCount == 2)
			{
				Touch touch = UnityEngine.Input.GetTouch(0);
				Touch touch2 = UnityEngine.Input.GetTouch(1);
				Vector2 a = touch.position - new Vector2(touch.deltaPosition.x / (float)Screen.width, touch.deltaPosition.y / (float)Screen.height);
				Vector2 b = touch2.position - new Vector2(touch2.deltaPosition.x / (float)Screen.width, touch2.deltaPosition.y / (float)Screen.height);
				float magnitude = (a - b).magnitude;
				float magnitude2 = (touch.position - touch2.position).magnitude;
				num = magnitude - magnitude2;
				Vector2 vector = Vector2.Lerp(touch.position, touch2.position, 0.5f);
				this._zoomPoint = new Vector3(vector.x, vector.y, Mathf.Abs(this.Vector3D(this.ProCamera2D.LocalPosition)));
				if (!this._zoomStarted)
				{
					this._zoomStarted = true;
					this._panTarget.position = this.ProCamera2D.LocalPosition;
					this.UpdateCurrentFollowSmoothness();
					this.RemoveFollowSmoothness();
				}
				this._touchZoomTime = Time.time;
			}
			else if (this._zoomStarted && Mathf.Abs(this._zoomAmount) < 0.001f)
			{
				this.RestoreFollowSmoothness();
				this._zoomStarted = false;
			}
			float num2 = this.PinchZoomSpeed * 10f;
			if ((this._onMaxZoom && num * num2 < 0f) || (this._onMinZoom && num * num2 > 0f))
			{
				this.CancelZoom();
				return 0f;
			}
			this._zoomAmount = Mathf.SmoothDamp(this._prevZoomAmount, num * num2 * deltaTime, ref this._zoomVelocity, this.ZoomSmoothness);
			float num3 = this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f + this._zoomAmount;
			float num4 = this._initialCamSize / this.MaxZoomInAmount;
			float num5 = this.MaxZoomOutAmount * this._initialCamSize;
			this._onMaxZoom = false;
			this._onMinZoom = false;
			if (num3 < num4)
			{
				this._zoomAmount -= num3 - num4;
				this._onMaxZoom = true;
			}
			else if (num3 > num5)
			{
				this._zoomAmount -= num3 - num5;
				this._onMinZoom = true;
			}
			this._prevZoomAmount = this._zoomAmount;
			if (this.ZoomToInputCenter)
			{
				float d = this._zoomAmount / (this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f);
				this._panTarget.position += (this._panTarget.position - this.ProCamera2D.GameCamera.ScreenToWorldPoint(this._zoomPoint)) * d;
			}
			return this._zoomAmount;
		}

		public void UpdateCurrentFollowSmoothness()
		{
			this._origFollowSmoothnessX = this.ProCamera2D.HorizontalFollowSmoothness;
			this._origFollowSmoothnessY = this.ProCamera2D.VerticalFollowSmoothness;
		}

		public void CenterPanTargetOnCamera(float interpolant = 1f)
		{
			if (this._panTarget != null)
			{
				this._panTarget.position = Vector3.Lerp(this._panTarget.position, this.VectorHV(this.Vector3H(this.ProCamera2D.LocalPosition), this.Vector3V(this.ProCamera2D.LocalPosition)), interpolant);
			}
		}

		private void CancelZoom()
		{
			this._zoomAmount = 0f;
			this._prevZoomAmount = 0f;
			this._zoomVelocity = 0f;
		}

		private void RestoreFollowSmoothness()
		{
			this.ProCamera2D.HorizontalFollowSmoothness = this._origFollowSmoothnessX;
			this.ProCamera2D.VerticalFollowSmoothness = this._origFollowSmoothnessY;
		}

		private void RemoveFollowSmoothness()
		{
			this.ProCamera2D.HorizontalFollowSmoothness = 0f;
			this.ProCamera2D.VerticalFollowSmoothness = 0f;
		}

		private bool InsideDraggableArea(Vector2 normalizedInput)
		{
			return (this.DraggableAreaRect.x == 0f && this.DraggableAreaRect.y == 0f && this.DraggableAreaRect.width == 1f && this.DraggableAreaRect.height == 1f) || (normalizedInput.x > this.DraggableAreaRect.x + (1f - this.DraggableAreaRect.width) / 2f && normalizedInput.x < this.DraggableAreaRect.x + this.DraggableAreaRect.width + (1f - this.DraggableAreaRect.width) / 2f && normalizedInput.y > this.DraggableAreaRect.y + (1f - this.DraggableAreaRect.height) / 2f && normalizedInput.y < this.DraggableAreaRect.y + this.DraggableAreaRect.height + (1f - this.DraggableAreaRect.height) / 2f);
		}
	}
}
