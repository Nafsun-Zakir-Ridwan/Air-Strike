using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-geometry-boundaries/")]
	public class ProCamera2DGeometryBoundaries : BasePC2D, IPositionDeltaChanger
	{
		public static string ExtensionName = "Geometry Boundaries";

		[global::Tooltip("The layer that contains the (3d) colliders that define the boundaries for the camera")]
		public LayerMask BoundariesLayerMask;

		private MoveInColliderBoundaries _cameraMoveInColliderBoundaries;

		private int _pdcOrder = 3000;

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
			this._cameraMoveInColliderBoundaries = new MoveInColliderBoundaries(this.ProCamera2D);
			this._cameraMoveInColliderBoundaries.CameraTransform = this.ProCamera2D.transform;
			this._cameraMoveInColliderBoundaries.CameraCollisionMask = this.BoundariesLayerMask;
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
			this._cameraMoveInColliderBoundaries.CameraSize = this.ProCamera2D.ScreenSizeInWorldCoordinates;
			return this._cameraMoveInColliderBoundaries.Move(originalDelta);
		}
	}
}
