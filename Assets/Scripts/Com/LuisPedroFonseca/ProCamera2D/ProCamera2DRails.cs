using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-rails/")]
	public class ProCamera2DRails : BasePC2D, IPreMover
	{
		public static string ExtensionName = "Rails";

		[HideInInspector]
		public List<Vector3> RailNodes = new List<Vector3>();

		public FollowMode FollowMode;

		public List<CameraTarget> CameraTargets = new List<CameraTarget>();

		private Dictionary<CameraTarget, Transform> _cameraTargetsOnRails = new Dictionary<CameraTarget, Transform>();

		private List<CameraTarget> _tempCameraTargets = new List<CameraTarget>();

		private KDTree _kdTree;

		private int _prmOrder = 1000;

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
			this._kdTree = KDTree.MakeFromPoints(this.RailNodes.ToArray());
			for (int i = 0; i < this.CameraTargets.Count; i++)
			{
				Transform transform = new GameObject(this.CameraTargets[i].TargetTransform.name + "_OnRails").transform;
				this._cameraTargetsOnRails.Add(this.CameraTargets[i], transform);
				CameraTarget cameraTarget = this.ProCamera2D.AddCameraTarget(transform, 1f, 1f, 0f, default(Vector2));
				cameraTarget.TargetOffset = this.CameraTargets[i].TargetOffset;
			}
			if (this.CameraTargets.Count == 0)
			{
				base.enabled = false;
			}
			ProCamera2D.Instance.AddPreMover(this);
			this.Step();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePreMover(this);
		}

		public void PreMove(float deltaTime)
		{
			if (base.enabled)
			{
				this.Step();
			}
		}

		private void Step()
		{
			Vector3 pos = Vector3.zero;
			for (int i = 0; i < this.CameraTargets.Count; i++)
			{
				FollowMode followMode = this.FollowMode;
				if (followMode != FollowMode.HorizontalAxis)
				{
					if (followMode != FollowMode.VerticalAxis)
					{
						if (followMode == FollowMode.BothAxis)
						{
							pos = this.VectorHVD(this.Vector3H(this.CameraTargets[i].TargetPosition) * this.CameraTargets[i].TargetInfluenceH, this.Vector3V(this.CameraTargets[i].TargetPosition) * this.CameraTargets[i].TargetInfluenceV, 0f);
						}
					}
					else
					{
						pos = this.VectorHVD(this.Vector3H(this.ProCamera2D.LocalPosition), this.Vector3V(this.CameraTargets[i].TargetPosition) * this.CameraTargets[i].TargetInfluenceV, 0f);
					}
				}
				else
				{
					pos = this.VectorHVD(this.Vector3H(this.CameraTargets[i].TargetPosition) * this.CameraTargets[i].TargetInfluenceH, this.Vector3V(this.ProCamera2D.LocalPosition), 0f);
				}
				this._cameraTargetsOnRails[this.CameraTargets[i]].position = this.GetPositionOnRail(pos);
			}
		}

		public void AddRailsTarget(Transform targetTransform, float targetInfluenceH = 1f, float targetInfluenceV = 1f, Vector2 targetOffset = default(Vector2))
		{
			if (this.GetRailsTarget(targetTransform) != null)
			{
				return;
			}
			CameraTarget cameraTarget = new CameraTarget
			{
				TargetTransform = targetTransform,
				TargetInfluenceH = targetInfluenceH,
				TargetInfluenceV = targetInfluenceV,
				TargetOffset = targetOffset
			};
			this.CameraTargets.Add(cameraTarget);
			Transform transform = new GameObject(targetTransform.name + "_OnRails").transform;
			this._cameraTargetsOnRails.Add(cameraTarget, transform);
			this.ProCamera2D.AddCameraTarget(transform, 1f, 1f, 0f, default(Vector2));
			base.enabled = true;
		}

		public void RemoveRailsTarget(Transform targetTransform)
		{
			CameraTarget railsTarget = this.GetRailsTarget(targetTransform);
			if (railsTarget != null)
			{
				this.CameraTargets.Remove(railsTarget);
				this.ProCamera2D.RemoveCameraTarget(this._cameraTargetsOnRails[railsTarget], 0f);
			}
		}

		public CameraTarget GetRailsTarget(Transform targetTransform)
		{
			for (int i = 0; i < this.CameraTargets.Count; i++)
			{
				if (this.CameraTargets[i].TargetTransform.GetInstanceID() == targetTransform.GetInstanceID())
				{
					return this.CameraTargets[i];
				}
			}
			return null;
		}

		public void DisableTargets(float transitionDuration = 0f)
		{
			if (this._tempCameraTargets.Count != 0)
			{
				return;
			}
			for (int i = 0; i < this._cameraTargetsOnRails.Count; i++)
			{
				this.ProCamera2D.RemoveCameraTarget(this._cameraTargetsOnRails[this.CameraTargets[i]], transitionDuration);
				this._tempCameraTargets.Add(this.ProCamera2D.AddCameraTarget(this.CameraTargets[i].TargetTransform, this.CameraTargets[i].TargetInfluenceH, this.CameraTargets[i].TargetInfluenceV, transitionDuration, this.CameraTargets[i].TargetOffset));
			}
		}

		public void EnableTargets(float transitionDuration = 0f)
		{
			for (int i = 0; i < this._tempCameraTargets.Count; i++)
			{
				this.ProCamera2D.RemoveCameraTarget(this._tempCameraTargets[i].TargetTransform, transitionDuration);
				this.ProCamera2D.AddCameraTarget(this._cameraTargetsOnRails[this.CameraTargets[i]], 1f, 1f, transitionDuration, default(Vector2));
			}
			this._tempCameraTargets.Clear();
		}

		private Vector3 GetPositionOnRail(Vector3 pos)
		{
			int num = this._kdTree.FindNearest(pos);
			if (num == 0)
			{
				return this.GetPositionOnRailSegment(this.RailNodes[0], this.RailNodes[1], pos);
			}
			if (num == this.RailNodes.Count - 1)
			{
				return this.GetPositionOnRailSegment(this.RailNodes[this.RailNodes.Count - 1], this.RailNodes[this.RailNodes.Count - 2], pos);
			}
			Vector3 positionOnRailSegment = this.GetPositionOnRailSegment(this.RailNodes[num - 1], this.RailNodes[num], pos);
			Vector3 positionOnRailSegment2 = this.GetPositionOnRailSegment(this.RailNodes[num + 1], this.RailNodes[num], pos);
			if ((pos - positionOnRailSegment).sqrMagnitude <= (pos - positionOnRailSegment2).sqrMagnitude)
			{
				return positionOnRailSegment;
			}
			return positionOnRailSegment2;
		}

		private Vector3 GetPositionOnRailSegment(Vector3 node1, Vector3 node2, Vector3 pos)
		{
			Vector3 rhs = pos - node1;
			Vector3 normalized = (node2 - node1).normalized;
			float num = Vector3.Dot(normalized, rhs);
			if (num < 0f)
			{
				return node1;
			}
			if (num * num > (node2 - node1).sqrMagnitude)
			{
				return node2;
			}
			Vector3 b = normalized * num;
			return node1 + b;
		}
	}
}
