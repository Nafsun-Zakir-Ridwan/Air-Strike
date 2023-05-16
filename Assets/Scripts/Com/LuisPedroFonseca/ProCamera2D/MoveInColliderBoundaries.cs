using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	public class MoveInColliderBoundaries
	{
		private Func<Vector3, float> Vector3H;

		private Func<Vector3, float> Vector3V;

		private Func<float, float, Vector3> VectorHV;

		private const float Offset = 0.2f;

		private const float RaySizeCompensation = 0.2f;

		public Transform CameraTransform;

		public Vector2 CameraSize;

		public LayerMask CameraCollisionMask;

		public int TotalHorizontalRays = 3;

		public int TotalVerticalRays = 3;

		private RaycastOrigins _raycastOrigins;

		private CameraCollisionState _cameraCollisionState;

		private RaycastHit _raycastHit;

		private float _verticalDistanceBetweenRays;

		private float _horizontalDistanceBetweenRays;

		private ProCamera2D _proCamera2D;

		private static Func<Vector3, float> __f__am_cache0;

		private static Func<Vector3, float> __f__am_cache1;

		private static Func<float, float, Vector3> __f__am_cache2;

		private static Func<Vector3, float> __f__am_cache3;

		private static Func<Vector3, float> __f__am_cache4;

		private static Func<float, float, Vector3> __f__am_cache5;

		private static Func<Vector3, float> __f__am_cache6;

		private static Func<Vector3, float> __f__am_cache7;

		private static Func<float, float, Vector3> __f__am_cache8;

		public RaycastOrigins RaycastOrigins
		{
			get
			{
				return this._raycastOrigins;
			}
		}

		public CameraCollisionState CameraCollisionState
		{
			get
			{
				return this._cameraCollisionState;
			}
		}

		public MoveInColliderBoundaries(ProCamera2D proCamera2D)
		{
			this._proCamera2D = proCamera2D;
			MovementAxis axis = this._proCamera2D.Axis;
			if (axis != MovementAxis.XY)
			{
				if (axis != MovementAxis.XZ)
				{
					if (axis == MovementAxis.YZ)
					{
						this.Vector3H = ((Vector3 vector) => vector.z);
						this.Vector3V = ((Vector3 vector) => vector.y);
						this.VectorHV = ((float h, float v) => new Vector3(0f, v, h));
					}
				}
				else
				{
					this.Vector3H = ((Vector3 vector) => vector.x);
					this.Vector3V = ((Vector3 vector) => vector.z);
					this.VectorHV = ((float h, float v) => new Vector3(h, 0f, v));
				}
			}
			else
			{
				this.Vector3H = ((Vector3 vector) => vector.x);
				this.Vector3V = ((Vector3 vector) => vector.y);
				this.VectorHV = ((float h, float v) => new Vector3(h, v, 0f));
			}
		}

		public Vector3 Move(Vector3 deltaMovement)
		{
			this.UpdateRaycastOrigins();
			this.GetOffsetAndForceMovement(this._raycastOrigins.BottomLeft, ref deltaMovement, ref this._cameraCollisionState.HBottomLeft, ref this._cameraCollisionState.VBottomLeft, -1f, -1f);
			this.GetOffsetAndForceMovement(this._raycastOrigins.BottomRight, ref deltaMovement, ref this._cameraCollisionState.HBottomRight, ref this._cameraCollisionState.VBottomRight, 1f, -1f);
			this.GetOffsetAndForceMovement(this._raycastOrigins.TopLeft, ref deltaMovement, ref this._cameraCollisionState.HTopLeft, ref this._cameraCollisionState.VTopLeft, -1f, 1f);
			this.GetOffsetAndForceMovement(this._raycastOrigins.TopRight, ref deltaMovement, ref this._cameraCollisionState.HTopRight, ref this._cameraCollisionState.VTopRight, 1f, 1f);
			float arg = 0f;
			if (this.Vector3H(deltaMovement) != 0f)
			{
				arg = this.MoveInAxis(this.Vector3H(deltaMovement), true);
			}
			float arg2 = 0f;
			if (this.Vector3V(deltaMovement) != 0f)
			{
				arg2 = this.MoveInAxis(this.Vector3V(deltaMovement), false);
			}
			return this.VectorHV(arg, arg2);
		}

		private void UpdateRaycastOrigins()
		{
			this._raycastOrigins.BottomRight = this.VectorHV(this.Vector3H(this.CameraTransform.localPosition) + this.CameraSize.x / 2f, this.Vector3V(this.CameraTransform.localPosition) - this.CameraSize.y / 2f);
			this._raycastOrigins.BottomLeft = this.VectorHV(this.Vector3H(this.CameraTransform.localPosition) - this.CameraSize.x / 2f, this.Vector3V(this.CameraTransform.localPosition) - this.CameraSize.y / 2f);
			this._raycastOrigins.TopRight = this.VectorHV(this.Vector3H(this.CameraTransform.localPosition) + this.CameraSize.x / 2f, this.Vector3V(this.CameraTransform.localPosition) + this.CameraSize.y / 2f);
			this._raycastOrigins.TopLeft = this.VectorHV(this.Vector3H(this.CameraTransform.localPosition) - this.CameraSize.x / 2f, this.Vector3V(this.CameraTransform.localPosition) + this.CameraSize.y / 2f);
			this._horizontalDistanceBetweenRays = this.CameraSize.x / (float)(this.TotalVerticalRays - 1);
			this._verticalDistanceBetweenRays = this.CameraSize.y / (float)(this.TotalHorizontalRays - 1);
		}

		private void GetOffsetAndForceMovement(Vector3 rayTargetPos, ref Vector3 deltaMovement, ref bool horizontalCheck, ref bool verticalCheck, float hSign, float vSign)
		{
			Vector3 vector = this.VectorHV(this.Vector3H(this.CameraTransform.localPosition), this.Vector3V(this.CameraTransform.localPosition));
			Vector3 normalized = (rayTargetPos - vector).normalized;
			float num = (rayTargetPos - vector).magnitude + 0.01f + 0.5f;
			this.DrawRay(vector, normalized * num, Color.yellow, 0f);
			if (Physics.Raycast(vector, normalized, out this._raycastHit, num, this.CameraCollisionMask))
			{
				if (Mathf.Abs(this.Vector3H(this._raycastHit.normal)) > Mathf.Abs(this.Vector3V(this._raycastHit.normal)))
				{
					horizontalCheck = !verticalCheck;
					if (this.Vector3H(deltaMovement) == 0f)
					{
						float arg = 0.1f * hSign;
						deltaMovement = this.VectorHV(arg, this.Vector3V(deltaMovement));
						float arg2 = this.MoveInAxis(this.Vector3H(deltaMovement), true);
						deltaMovement = this.VectorHV(arg2, this.Vector3V(deltaMovement));
					}
				}
				else
				{
					verticalCheck = !horizontalCheck;
					if (this.Vector3V(deltaMovement) == 0f)
					{
						float arg3 = 0.1f * vSign;
						deltaMovement = this.VectorHV(this.Vector3H(deltaMovement), arg3);
						float arg4 = this.MoveInAxis(this.Vector3V(deltaMovement), false);
						deltaMovement = this.VectorHV(this.Vector3H(deltaMovement), arg4);
					}
				}
			}
			else
			{
				horizontalCheck = false;
				verticalCheck = false;
			}
		}

		private float MoveInAxis(float deltaMovement, bool isHorizontal)
		{
			bool flag = deltaMovement > 0f;
			float num = Mathf.Abs(deltaMovement) + 0.2f;
			Vector3 vector;
			Vector3 arg;
			if (isHorizontal)
			{
				vector = ((!flag) ? (-this.CameraTransform.right) : this.CameraTransform.right);
				arg = ((!flag) ? this._raycastOrigins.BottomLeft : this._raycastOrigins.BottomRight);
			}
			else
			{
				vector = ((!flag) ? (-this.CameraTransform.up) : this.CameraTransform.up);
				arg = ((!flag) ? this._raycastOrigins.BottomLeft : this._raycastOrigins.TopLeft);
			}
			float num2 = float.NegativeInfinity;
			bool flag2 = false;
			int num3 = (!isHorizontal) ? this.TotalVerticalRays : this.TotalHorizontalRays;
			for (int i = 0; i < num3; i++)
			{
				float num4 = (!isHorizontal) ? (this.Vector3H(arg) + (float)i * this._horizontalDistanceBetweenRays) : this.Vector3H(arg);
				float num5 = (!isHorizontal) ? this.Vector3V(arg) : (this.Vector3V(arg) + (float)i * this._verticalDistanceBetweenRays);
				if (isHorizontal)
				{
					if ((flag && this._cameraCollisionState.VBottomRight && i == 0) || (!flag && this._cameraCollisionState.VBottomLeft && i == 0))
					{
						num5 += 0.2f;
					}
					if ((flag && this._cameraCollisionState.VTopRight && i == num3 - 1) || (!flag && this._cameraCollisionState.VTopLeft && i == num3 - 1))
					{
						num5 -= 0.2f;
					}
				}
				else
				{
					if ((flag && this._cameraCollisionState.HTopLeft && i == 0) || (!flag && this._cameraCollisionState.HBottomLeft && i == 0))
					{
						num4 += 0.2f;
					}
					if ((flag && this._cameraCollisionState.HTopRight && i == num3 - 1) || (!flag && this._cameraCollisionState.HBottomRight && i == num3 - 1))
					{
						num4 -= 0.2f;
					}
				}
				Vector3 vector2 = this.VectorHV(num4, num5);
				if (Physics.Raycast(vector2, vector, out this._raycastHit, num, this.CameraCollisionMask))
				{
					this.DrawRay(vector2, vector * num, Color.red, 0f);
					if ((!isHorizontal || !flag2 || !flag || num2 > this.Vector3H(this._raycastHit.point)) && (flag || num2 < this.Vector3H(this._raycastHit.point)))
					{
						if ((!flag2 || !flag || num2 > this.Vector3V(this._raycastHit.point)) && (flag || num2 < this.Vector3V(this._raycastHit.point)))
						{
							flag2 = true;
							deltaMovement = ((!isHorizontal) ? (this.Vector3V(this._raycastHit.point) - this.Vector3V(vector2) + ((!flag) ? 0.2f : -0.2f)) : (this.Vector3H(this._raycastHit.point) - this.Vector3H(vector2) + ((!flag) ? 0.2f : -0.2f)));
							num2 = ((!isHorizontal) ? this.Vector3V(this._raycastHit.point) : this.Vector3H(this._raycastHit.point));
						}
					}
				}
				else
				{
					this.DrawRay(vector2, vector * num, Color.cyan, 0f);
				}
			}
			return deltaMovement;
		}

		private void DrawRay(Vector3 start, Vector3 dir, Color color, float duration = 0f)
		{
			if (duration != 0f)
			{
				UnityEngine.Debug.DrawRay(start, dir, color, duration);
			}
			else
			{
				UnityEngine.Debug.DrawRay(start, dir, color);
			}
		}
	}
}
