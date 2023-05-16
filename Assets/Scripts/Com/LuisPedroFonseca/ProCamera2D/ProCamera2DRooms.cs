using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-rooms/")]
	public class ProCamera2DRooms : BasePC2D, IPositionOverrider, ISizeOverrider
	{
		private sealed class _EnterRoom_c__AnonStorey2
		{
			internal string roomID;

			internal bool __m__0(Room room)
			{
				return room.ID == this.roomID;
			}
		}

		private sealed class _TestRoomRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal WaitForSeconds _waitForSeconds___0;

			internal ProCamera2DRooms _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _TestRoomRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForEndOfFrame();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._waitForSeconds___0 = new WaitForSeconds(this._this.UpdateInterval);
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (this._this.AutomaticRoomActivation)
				{
					this._this.TestRoom();
				}
				this._current = this._waitForSeconds___0;
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _TransitionRoutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialSize___0;

			internal float _initialCamPosH___0;

			internal float _initialCamPosV___0;

			internal float _t___0;

			internal float transitionDuration;

			internal float targetSize;

			internal EaseType transitionEaseType;

			internal float _targetPosH___1;

			internal float _targetPosV___1;

			internal NumericBoundariesSettings numericBoundariesSettings;

			internal float _newPosH___1;

			internal float _newPosV___1;

			internal ProCamera2DRooms _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _TransitionRoutine_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._this.Trnsitioning = true;
					this._this._numericBoundaries.UseNumericBoundaries = false;
					this._initialSize___0 = this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
					this._initialCamPosH___0 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition);
					this._initialCamPosV___0 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition);
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.transitionDuration;
					this._this._newSize = Utils.EaseFromTo(this._initialSize___0, this.targetSize, this._t___0, this.transitionEaseType);
					this._targetPosH___1 = this._this.ProCamera2D.CameraTargetPositionSmoothed.x;
					this._targetPosV___1 = this._this.ProCamera2D.CameraTargetPositionSmoothed.y;
					this._this.LimitToNumericBoundaries(ref this._targetPosH___1, ref this._targetPosV___1, this.targetSize * this._this.ProCamera2D.GameCamera.aspect, this.targetSize, this.numericBoundariesSettings);
					this._newPosH___1 = Utils.EaseFromTo(this._initialCamPosH___0, this._targetPosH___1, this._t___0, this.transitionEaseType);
					this._newPosV___1 = Utils.EaseFromTo(this._initialCamPosV___0, this._targetPosV___1, this._t___0, this.transitionEaseType);
					this._this._newPos = this._this.VectorHVD(this._newPosH___1, this._newPosV___1, 0f);
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this.Trnsitioning = false;
				this._this._numericBoundaries.Settings = this.numericBoundariesSettings;
				this._this.TrnsitionRoutine = null;
				if (this._this.OnFinishedTransition != null)
				{
					this._this.OnFinishedTransition.Invoke(this._this._currentRoomIndex, this._this._previousRoomIndex);
				}
				this._this._previousRoomIndex = this._this._currentRoomIndex;
				this._PC = -1;
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		public const string ExtensionName = "Rooms";

		private int _currentRoomIndex = -1;

		private int _previousRoomIndex = -1;

		public List<Room> Rooms = new List<Room>();

		public float UpdateInterval = 0.1f;

		public bool UseTargetsMidPoint = true;

		public Transform TriggerTarget;

		public bool TransitionInstanlyOnStart = true;

		public bool RestoreOnRoomExit;

		public float RestoreDuration = 1f;

		public EaseType RestoreEaseType;

		public bool AutomaticRoomActivation = true;

		public RoomEvent OnStartedTransition;

		public RoomEvent OnFinishedTransition;

		public UnityEvent OnExitedAllRooms;

		private ProCamera2DNumericBoundaries _numericBoundaries;

		private NumericBoundariesSettings _defaultNumericBoundariesSettings;

		private bool Trnsitioning;

		private Vector3 _newPos;

		private float _newSize;

		private Coroutine TrnsitionRoutine;

		private float _originalSize;

		private int _poOrder = 1001;

		private int _soOrder = 3001;

		public int CurrentRoomIndex
		{
			get
			{
				return this._currentRoomIndex;
			}
		}

		public int PreviousRoomIndex
		{
			get
			{
				return this._previousRoomIndex;
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
			this._numericBoundaries = this.ProCamera2D.GetComponent<ProCamera2DNumericBoundaries>();
			this._defaultNumericBoundariesSettings = this._numericBoundaries.Settings;
			this._originalSize = this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
			ProCamera2D.Instance.AddPositionOverrider(this);
			ProCamera2D.Instance.AddSizeOverrider(this);
		}

		private void Start()
		{
			base.StartCoroutine(this.TestRoomRoutine());
			if (this.TransitionInstanlyOnStart)
			{
				Vector3 targetPos = this.ProCamera2D.TargetsMidPoint;
				if (!this.UseTargetsMidPoint && this.TriggerTarget != null)
				{
					targetPos = this.TriggerTarget.position;
				}
				int num = this.ComputeCurrentRoom(targetPos);
				if (num != -1)
				{
					this._currentRoomIndex = num;
					this.TransitionToRoom(new Room(this.Rooms[this._currentRoomIndex])
					{
						TransitionDuration = 0f
					});
				}
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.ProCamera2D.RemovePositionOverrider(this);
			this.ProCamera2D.RemoveSizeOverrider(this);
		}

		private void Reset()
		{
			if (this.Rooms.Count == 0)
			{
				this.Rooms.Add(new Room
				{
					Dimensions = new Rect(0f, 0f, 10f, 10f),
					TransitionDuration = 1f,
					ZoomScale = 1.5f
				});
			}
		}

		public Vector3 OverridePosition(float deltaTime, Vector3 originalPosition)
		{
			if (!base.enabled)
			{
				return originalPosition;
			}
			if (this.Trnsitioning)
			{
				return this._newPos;
			}
			return originalPosition;
		}

		public float OverrideSize(float deltaTime, float originalSize)
		{
			if (!base.enabled)
			{
				return originalSize;
			}
			if (this.Trnsitioning)
			{
				return this._newSize;
			}
			return originalSize;
		}

		public void TestRoom()
		{
			Vector3 targetPos = this.ProCamera2D.TargetsMidPoint;
			if (!this.UseTargetsMidPoint && this.TriggerTarget != null)
			{
				targetPos = this.TriggerTarget.position;
			}
			int num = this.ComputeCurrentRoom(targetPos);
			if (num != -1 && this._currentRoomIndex != num)
			{
				this.EnterRoom(num);
			}
			if (num == -1 && this._currentRoomIndex != -1)
			{
				this.ExitRoom();
			}
		}

		public int ComputeCurrentRoom(Vector3 targetPos)
		{
			int result = -1;
			for (int i = 0; i < this.Rooms.Count; i++)
			{
				if (Utils.IsInsideRectangle(this.Rooms[i].Dimensions.x, this.Rooms[i].Dimensions.y, this.Rooms[i].Dimensions.width, this.Rooms[i].Dimensions.height, this.Vector3H(targetPos), this.Vector3V(targetPos)))
				{
					result = i;
				}
			}
			return result;
		}

		public void EnterRoom(int roomIndex)
		{
			if (roomIndex < 0 || roomIndex > this.Rooms.Count - 1)
			{
				UnityEngine.Debug.LogError("Can't find room with index: " + roomIndex);
				return;
			}
			if (roomIndex == this._currentRoomIndex)
			{
				return;
			}
			this._previousRoomIndex = this._currentRoomIndex;
			this._currentRoomIndex = roomIndex;
			this.TransitionToRoom(this.Rooms[this._currentRoomIndex]);
			if (this.OnStartedTransition != null)
			{
				this.OnStartedTransition.Invoke(roomIndex, this._previousRoomIndex);
			}
		}

		public void EnterRoom(string roomID)
		{
			this.EnterRoom(this.Rooms.FindIndex((Room room) => room.ID == roomID));
		}

		public void ExitRoom()
		{
			if (this.RestoreOnRoomExit)
			{
				this._currentRoomIndex = -1;
				if (this.OnStartedTransition != null)
				{
					this.OnStartedTransition.Invoke(this._currentRoomIndex, this._previousRoomIndex);
				}
				if (this.TrnsitionRoutine != null)
				{
					base.StopCoroutine(this.TrnsitionRoutine);
				}
				this.TrnsitionRoutine = base.StartCoroutine(this.TransitionRoutine(this._defaultNumericBoundariesSettings, this._originalSize, this.RestoreDuration, this.RestoreEaseType));
			}
			if (this.OnExitedAllRooms != null)
			{
				this.OnExitedAllRooms.Invoke();
			}
		}

		public void AddRoom(float roomX, float roomY, float roomWidth, float roomHeight, float transitionDuration = 1f, EaseType transitionEaseType = EaseType.EaseInOut, bool scaleToFit = false, bool zoom = false, float zoomScale = 1.5f, string id = "")
		{
			Room item = new Room
			{
				ID = id,
				Dimensions = new Rect(roomX, roomY, roomWidth, roomHeight),
				TransitionDuration = transitionDuration,
				TransitionEaseType = transitionEaseType,
				ScaleCameraToFit = scaleToFit,
				Zoom = zoom,
				ZoomScale = zoomScale
			};
			this.Rooms.Add(item);
		}

		public void SetDefaultNumericBoundariesSettings(NumericBoundariesSettings settings)
		{
			this._defaultNumericBoundariesSettings = settings;
		}

		private IEnumerator TestRoomRoutine()
		{
			ProCamera2DRooms._TestRoomRoutine_c__Iterator0 _TestRoomRoutine_c__Iterator = new ProCamera2DRooms._TestRoomRoutine_c__Iterator0();
			_TestRoomRoutine_c__Iterator._this = this;
			return _TestRoomRoutine_c__Iterator;
		}

		private void TransitionToRoom(Room room)
		{
			if (this.TrnsitionRoutine != null)
			{
				base.StopCoroutine(this.TrnsitionRoutine);
			}
			NumericBoundariesSettings numericBoundariesSettings = new NumericBoundariesSettings
			{
				UseNumericBoundaries = true,
				UseTopBoundary = true,
				TopBoundary = room.Dimensions.y + room.Dimensions.height / 2f,
				UseBottomBoundary = true,
				BottomBoundary = room.Dimensions.y - room.Dimensions.height / 2f,
				UseLeftBoundary = true,
				LeftBoundary = room.Dimensions.x - room.Dimensions.width / 2f,
				UseRightBoundary = true,
				RightBoundary = room.Dimensions.x + room.Dimensions.width / 2f
			};
			float num = this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
			float cameraSizeForRoom = this.GetCameraSizeForRoom(room.Dimensions);
			if (room.ScaleCameraToFit)
			{
				num = cameraSizeForRoom;
			}
			else if (room.Zoom && this._originalSize * room.ZoomScale < cameraSizeForRoom)
			{
				num = this._originalSize * room.ZoomScale;
			}
			else if (cameraSizeForRoom < num)
			{
				num = cameraSizeForRoom;
			}
			this.TrnsitionRoutine = base.StartCoroutine(this.TransitionRoutine(numericBoundariesSettings, num, room.TransitionDuration, room.TransitionEaseType));
		}

		private IEnumerator TransitionRoutine(NumericBoundariesSettings numericBoundariesSettings, float targetSize, float transitionDuration = 1f, EaseType transitionEaseType = EaseType.EaseOut)
		{
			ProCamera2DRooms._TransitionRoutine_c__Iterator1 _TransitionRoutine_c__Iterator = new ProCamera2DRooms._TransitionRoutine_c__Iterator1();
			_TransitionRoutine_c__Iterator.transitionDuration = transitionDuration;
			_TransitionRoutine_c__Iterator.targetSize = targetSize;
			_TransitionRoutine_c__Iterator.transitionEaseType = transitionEaseType;
			_TransitionRoutine_c__Iterator.numericBoundariesSettings = numericBoundariesSettings;
			_TransitionRoutine_c__Iterator._this = this;
			return _TransitionRoutine_c__Iterator;
		}

		private void LimitToNumericBoundaries(ref float horizontalPos, ref float verticalPos, float halfCameraWidth, float halfCameraHeight, NumericBoundariesSettings numericBoundaries)
		{
			if (numericBoundaries.UseLeftBoundary && horizontalPos - halfCameraWidth < numericBoundaries.LeftBoundary)
			{
				horizontalPos = numericBoundaries.LeftBoundary + halfCameraWidth;
			}
			else if (numericBoundaries.UseRightBoundary && horizontalPos + halfCameraWidth > numericBoundaries.RightBoundary)
			{
				horizontalPos = numericBoundaries.RightBoundary - halfCameraWidth;
			}
			if (numericBoundaries.UseBottomBoundary && verticalPos - halfCameraHeight < numericBoundaries.BottomBoundary)
			{
				verticalPos = numericBoundaries.BottomBoundary + halfCameraHeight;
			}
			else if (numericBoundaries.UseTopBoundary && verticalPos + halfCameraHeight > numericBoundaries.TopBoundary)
			{
				verticalPos = numericBoundaries.TopBoundary - halfCameraHeight;
			}
		}

		private float GetCameraSizeForRoom(Rect roomRect)
		{
			float num = roomRect.width / this.ProCamera2D.ScreenSizeInWorldCoordinates.x;
			float num2 = roomRect.height / this.ProCamera2D.ScreenSizeInWorldCoordinates.y;
			if (num < num2)
			{
				return roomRect.width / this.ProCamera2D.GameCamera.aspect / 2f;
			}
			return roomRect.height / 2f;
		}
	}
}
