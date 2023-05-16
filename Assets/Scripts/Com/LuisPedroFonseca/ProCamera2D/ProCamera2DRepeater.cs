using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-repeater/")]
	public class ProCamera2DRepeater : BasePC2D, IPostMover
	{
		public static string ExtensionName = "Repeater";

		public Transform ObjectToRepeat;

		public Vector2 ObjectSize = new Vector2(2f, 2f);

		public Vector2 ObjectBottomLeft = new Vector2(-1f, -1f);

		public bool ObjectOnStage = true;

		public bool _repeatHorizontal = true;

		public bool _repeatVertical = true;

		public Camera CameraToUse;

		private Transform _cameraToUseTransform;

		private Vector3 _objStartPosition;

		private List<RepeatedObject> _allRepeatedObjects = new List<RepeatedObject>(2);

		private Queue<RepeatedObject> _inactiveRepeatedObjects = new Queue<RepeatedObject>();

		private IntPoint _prevStartIndex;

		private IntPoint _prevEndIndex;

		private Dictionary<IntPoint, bool> _occupiedIndices = new Dictionary<IntPoint, bool>();

		private int _pmOrder = 2000;

		public bool RepeatHorizontal
		{
			get
			{
				return this._repeatHorizontal;
			}
			set
			{
				this._repeatHorizontal = value;
				this.Refresh();
			}
		}

		public bool RepeatVertical
		{
			get
			{
				return this._repeatVertical;
			}
			set
			{
				this._repeatVertical = value;
				this.Refresh();
			}
		}

		public int PMOrder
		{
			get
			{
				return this._pmOrder;
			}
			set
			{
				this._pmOrder = value;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (this.ObjectToRepeat == null)
			{
				UnityEngine.Debug.LogWarning("ProCamera2D Repeater extension - No ObjectToRepeat defined!");
				return;
			}
			this._objStartPosition = new Vector3(this.Vector3H(this.ObjectToRepeat.position), this.Vector3V(this.ObjectToRepeat.position), this.Vector3D(this.ObjectToRepeat.position));
			this._cameraToUseTransform = this.CameraToUse.transform;
			if (this.ObjectOnStage)
			{
				this.InitCopy(this.ObjectToRepeat, true);
			}
			ProCamera2D.Instance.AddPostMover(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePostMover(this);
		}

		public void PostMove(float deltaTime)
		{
			if (!base.enabled)
			{
				return;
			}
			Vector2 screenSizeInWorldCoords = Utils.GetScreenSizeInWorldCoords(this.CameraToUse, this.Vector3D(this.ProCamera2D.LocalPosition - this._objStartPosition));
			Vector3 position = this._cameraToUseTransform.position;
			Vector2 vector = new Vector2(this.Vector3H(position) - screenSizeInWorldCoords.x / 2f, this.Vector3V(position) - screenSizeInWorldCoords.y / 2f);
			Vector2 vector2 = new Vector2(vector.x - this._objStartPosition.x - this.ObjectBottomLeft.x, vector.y - this._objStartPosition.y - this.ObjectBottomLeft.y);
			IntPoint intPoint = new IntPoint(Mathf.FloorToInt(vector2.x / this.ObjectSize.x), Mathf.FloorToInt(vector2.y / this.ObjectSize.y));
			IntPoint intPoint2 = new IntPoint(Mathf.CeilToInt(screenSizeInWorldCoords.x / this.ObjectSize.x), Mathf.CeilToInt(screenSizeInWorldCoords.y / this.ObjectSize.y));
			IntPoint intPoint3 = new IntPoint(intPoint.X + intPoint2.X, intPoint.Y + intPoint2.Y);
			if (!intPoint.Equals(this._prevStartIndex) || !intPoint3.Equals(this._prevEndIndex))
			{
				this.FreeOutOfRangeObjects(intPoint, intPoint3);
				this.FillGrid(intPoint, intPoint3);
			}
			this._prevStartIndex = intPoint;
			this._prevEndIndex = intPoint3;
		}

		private void FreeOutOfRangeObjects(IntPoint startIndex, IntPoint endIndex)
		{
			for (int i = 0; i < this._allRepeatedObjects.Count; i++)
			{
				RepeatedObject repeatedObject = this._allRepeatedObjects[i];
				if ((repeatedObject.GridPos.X != 2147483647 && (repeatedObject.GridPos.X < startIndex.X || repeatedObject.GridPos.X > endIndex.X)) || (repeatedObject.GridPos.Y != 2147483647 && (repeatedObject.GridPos.Y < startIndex.Y || repeatedObject.GridPos.Y > endIndex.Y)))
				{
					this._occupiedIndices.Remove(repeatedObject.GridPos);
					this._inactiveRepeatedObjects.Enqueue(repeatedObject);
					this.PositionObject(repeatedObject, IntPoint.MaxValue);
				}
			}
		}

		private void FillGrid(IntPoint startIndex, IntPoint endIndex)
		{
			if (!this._repeatHorizontal)
			{
				startIndex.X = 0;
				endIndex.X = 0;
			}
			if (!this._repeatVertical)
			{
				startIndex.Y = 0;
				endIndex.Y = 0;
			}
			for (int i = startIndex.X; i <= endIndex.X; i++)
			{
				for (int j = startIndex.Y; j <= endIndex.Y; j++)
				{
					IntPoint intPoint = new IntPoint(i, j);
					bool flag = false;
					if (!this._occupiedIndices.TryGetValue(intPoint, out flag))
					{
						if (this._inactiveRepeatedObjects.Count == 0)
						{
							this.InitCopy(UnityEngine.Object.Instantiate<Transform>(this.ObjectToRepeat), false);
						}
						this._occupiedIndices[intPoint] = true;
						RepeatedObject obj = this._inactiveRepeatedObjects.Dequeue();
						this.PositionObject(obj, intPoint);
					}
				}
			}
		}

		private void InitCopy(Transform newCopy, bool positionOffscreen = true)
		{
			RepeatedObject repeatedObject = new RepeatedObject
			{
				Transform = newCopy
			};
			repeatedObject.Transform.parent = this.ObjectToRepeat.parent;
			this._allRepeatedObjects.Add(repeatedObject);
			this._inactiveRepeatedObjects.Enqueue(repeatedObject);
			if (positionOffscreen)
			{
				this.PositionObject(repeatedObject, IntPoint.MaxValue);
			}
		}

		private void PositionObject(RepeatedObject obj, IntPoint index)
		{
			obj.GridPos = index;
			obj.Transform.position = this.VectorHVD(this._objStartPosition.x + (float)index.X * this.ObjectSize.x, this._objStartPosition.y + (float)index.Y * this.ObjectSize.y, this._objStartPosition.z);
		}

		private void Refresh()
		{
			this.FreeOutOfRangeObjects(IntPoint.MaxValue, IntPoint.MaxValue);
			this.FillGrid(this._prevStartIndex, this._prevEndIndex);
		}
	}
}
