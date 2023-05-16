using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/core/"), RequireComponent(typeof(Camera))]
	public class ProCamera2D : MonoBehaviour, ISerializationCallbackReceiver
	{
		private sealed class _ApplyInfluencesTimedRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _count___0;

			internal float[] durations;

			internal float _duration___1;

			internal IList<Vector2> influences;

			internal ProCamera2D _this;

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

			public _ApplyInfluencesTimedRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._count___0 = -1;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._count___0 < this.durations.Length - 1)
				{
					this._count___0++;
					this._duration___1 = this.durations[this._count___0];
					this._current = this._this.StartCoroutine(this._this.ApplyInfluenceTimedRoutine(this.influences[this._count___0], this._duration___1));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
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

		private sealed class _ApplyInfluenceTimedRoutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float duration;

			internal Vector2 influence;

			internal ProCamera2D _this;

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

			public _ApplyInfluenceTimedRoutine_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this.duration > 0f)
				{
					this.duration -= this._this._deltaTime;
					this._this.ApplyInfluence(this.influence);
					this._current = this._this.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
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

		private sealed class _AdjustTargetInfluenceRoutine_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal CameraTarget cameraTarget;

			internal float _startInfluenceH___0;

			internal float _startInfluenceV___0;

			internal float _t___0;

			internal float duration;

			internal float influenceH;

			internal float influenceV;

			internal bool removeIfZeroInfluence;

			internal ProCamera2D _this;

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

			public _AdjustTargetInfluenceRoutine_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._startInfluenceH___0 = this.cameraTarget.TargetInfluenceH;
					this._startInfluenceV___0 = this.cameraTarget.TargetInfluenceV;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this._deltaTime / this.duration;
					this.cameraTarget.TargetInfluenceH = Utils.EaseFromTo(this._startInfluenceH___0, this.influenceH, this._t___0, EaseType.Linear);
					this.cameraTarget.TargetInfluenceV = Utils.EaseFromTo(this._startInfluenceV___0, this.influenceV, this._t___0, EaseType.Linear);
					this._current = this._this.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				if (this.removeIfZeroInfluence && this.cameraTarget.TargetInfluenceH <= 0f && this.cameraTarget.TargetInfluenceV <= 0f)
				{
					this._this.CameraTargets.Remove(this.cameraTarget);
				}
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

		private sealed class _UpdateScreenSizeRoutine_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _startSize___0;

			internal float _newSize___0;

			internal float _t___0;

			internal float duration;

			internal float finalSize;

			internal EaseType easeType;

			internal ProCamera2D _this;

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

			public _UpdateScreenSizeRoutine_c__Iterator3()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._startSize___0 = this._this._screenSizeInWorldCoordinates.y * 0.5f;
					this._newSize___0 = this._startSize___0;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this._deltaTime / this.duration;
					this._newSize___0 = Utils.EaseFromTo(this._startSize___0, this.finalSize, this._t___0, this.easeType);
					this._this.SetScreenSize(this._newSize___0);
					this._current = this._this.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this._updateScreenSizeCoroutine = null;
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

		private sealed class _DollyZoomRoutine_c__Iterator4 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _startFOV___0;

			internal float _newFOV___0;

			internal float _t___0;

			internal float duration;

			internal float finalFOV;

			internal EaseType easeType;

			internal ProCamera2D _this;

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

			public _DollyZoomRoutine_c__Iterator4()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._startFOV___0 = this._this.GameCamera.fieldOfView;
					this._newFOV___0 = this._startFOV___0;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this._deltaTime / this.duration;
					this._newFOV___0 = Utils.EaseFromTo(this._startFOV___0, this.finalFOV, this._t___0, this.easeType);
					this._this.GameCamera.fieldOfView = this._newFOV___0;
					this._this.Trnsform.localPosition = this._this.VectorHVD(this._this.Vector3H(this._this.Trnsform.localPosition), this._this.Vector3V(this._this.Trnsform.localPosition), this._this.GetCameraDistanceForFOV(this._newFOV___0, this._this._screenSizeInWorldCoordinates.y) * this._this._originalCameraDepthSign);
					this._current = this._this.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this._dollyZoomRoutine = null;
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

		public static readonly Version Version = new Version("2.2.8");

		public List<CameraTarget> CameraTargets = new List<CameraTarget>();

		public bool CenterTargetOnStart;

		public MovementAxis Axis;

		public UpdateType UpdateType;

		public bool FollowHorizontal = true;

		public float HorizontalFollowSmoothness = 0.15f;

		public bool FollowVertical = true;

		public float VerticalFollowSmoothness = 0.15f;

		public Vector2 OverallOffset;

		public bool ZoomWithFOV;

		private static ProCamera2D _instance;

		private float _cameraTargetHorizontalPositionSmoothed;

		private float _cameraTargetVerticalPositionSmoothed;

		private Vector2 _screenSizeInWorldCoordinates;

		private Vector3 _previousTargetsMidPoint;

		private Vector3 _targetsMidPoint;

		private Vector3 _cameraTargetPosition;

		private float _deltaTime;

		private Vector3 _parentPosition;

		public Action<float> PreMoveUpdate;

		public Action<float> PostMoveUpdate;

		public Action<Vector2> OnCameraResize;

		public Action OnReset;

		public Vector3? ExclusiveTargetPosition;

		public int CurrentZoomTriggerID;

		public bool IsCameraPositionLeftBounded;

		public bool IsCameraPositionRightBounded;

		public bool IsCameraPositionTopBounded;

		public bool IsCameraPositionBottomBounded;

		public Camera GameCamera;

		private Func<Vector3, float> Vector3H;

		private Func<Vector3, float> Vector3V;

		private Func<Vector3, float> Vector3D;

		private Func<float, float, Vector3> VectorHV;

		private Func<float, float, float, Vector3> VectorHVD;

		private Vector2 _startScreenSizeInWorldCoordinates;

		private Coroutine _updateScreenSizeCoroutine;

		private Coroutine _dollyZoomRoutine;

		private List<Vector3> _influences = new List<Vector3>();

		private Vector3 _influencesSum = Vector3.zero;

		private float _originalCameraDepthSign;

		private float _previousCameraTargetHorizontalPositionSmoothed;

		private float _previousCameraTargetVerticalPositionSmoothed;

		private int _previousScreenWidth;

		private int _previousScreenHeight;

		private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

		private Transform Trnsform;

		private List<IPreMover> _preMovers = new List<IPreMover>();

		private List<IPositionDeltaChanger> _positionDeltaChangers = new List<IPositionDeltaChanger>();

		private List<IPositionOverrider> _positionOverriders = new List<IPositionOverrider>();

		private List<ISizeDeltaChanger> _sizeDeltaChangers = new List<ISizeDeltaChanger>();

		private List<ISizeOverrider> _sizeOverriders = new List<ISizeOverrider>();

		private List<IPostMover> _postMovers = new List<IPostMover>();

		private static Func<Vector3, float> __f__am_cache0;

		private static Func<Vector3, float> __f__am_cache1;

		private static Func<Vector3, float> __f__am_cache2;

		private static Func<float, float, Vector3> __f__am_cache3;

		private static Func<float, float, float, Vector3> __f__am_cache4;

		private static Func<Vector3, float> __f__am_cache5;

		private static Func<Vector3, float> __f__am_cache6;

		private static Func<Vector3, float> __f__am_cache7;

		private static Func<float, float, Vector3> __f__am_cache8;

		private static Func<float, float, float, Vector3> __f__am_cache9;

		private static Func<Vector3, float> __f__am_cacheA;

		private static Func<Vector3, float> __f__am_cacheB;

		private static Func<Vector3, float> __f__am_cacheC;

		private static Func<float, float, Vector3> __f__am_cacheD;

		private static Func<float, float, float, Vector3> __f__am_cacheE;

		private static Func<IPreMover, int> __f__am_cacheF;

		private static Func<IPositionDeltaChanger, int> __f__am_cache10;

		private static Func<IPositionOverrider, int> __f__am_cache11;

		private static Func<ISizeDeltaChanger, int> __f__am_cache12;

		private static Func<ISizeOverrider, int> __f__am_cache13;

		private static Func<IPostMover, int> __f__am_cache14;

		public static ProCamera2D Instance
		{
			get
			{
				if (object.Equals(ProCamera2D._instance, null))
				{
					ProCamera2D._instance = (UnityEngine.Object.FindObjectOfType(typeof(ProCamera2D)) as ProCamera2D);
					if (object.Equals(ProCamera2D._instance, null))
					{
						throw new UnityException("ProCamera2D does not exist.");
					}
				}
				return ProCamera2D._instance;
			}
		}

		public Rect Rect
		{
			get
			{
				return this.GameCamera.rect;
			}
			set
			{
				this.GameCamera.rect = value;
				ProCamera2DParallax componentInChildren = base.GetComponentInChildren<ProCamera2DParallax>();
				if (componentInChildren != null)
				{
					for (int i = 0; i < componentInChildren.ParallaxLayers.Count; i++)
					{
						componentInChildren.ParallaxLayers[i].ParallaxCamera.rect = value;
					}
				}
			}
		}

		public Vector2 CameraTargetPositionSmoothed
		{
			get
			{
				return new Vector2(this._cameraTargetHorizontalPositionSmoothed, this._cameraTargetVerticalPositionSmoothed);
			}
			set
			{
				this._cameraTargetHorizontalPositionSmoothed = value.x;
				this._cameraTargetVerticalPositionSmoothed = value.y;
			}
		}

		public Vector3 LocalPosition
		{
			get
			{
				return this.Trnsform.localPosition;
			}
			set
			{
				this.Trnsform.localPosition = value;
			}
		}

		public Vector2 ScreenSizeInWorldCoordinates
		{
			get
			{
				return this._screenSizeInWorldCoordinates;
			}
		}

		public Vector3 PreviousTargetsMidPoint
		{
			get
			{
				return this._previousTargetsMidPoint;
			}
		}

		public Vector3 TargetsMidPoint
		{
			get
			{
				return this._targetsMidPoint;
			}
		}

		public Vector3 CameraTargetPosition
		{
			get
			{
				return this._cameraTargetPosition;
			}
		}

		public float DeltaTime
		{
			get
			{
				return this._deltaTime;
			}
		}

		public Vector3 ParentPosition
		{
			get
			{
				return this._parentPosition;
			}
		}

		private void Awake()
		{
			ProCamera2D._instance = this;
			this.Trnsform = base.transform;
			if (this.Trnsform.parent != null)
			{
				this._parentPosition = this.Trnsform.parent.position;
			}
			if (this.GameCamera == null)
			{
				this.GameCamera = base.GetComponent<Camera>();
			}
			if (this.GameCamera == null)
			{
				UnityEngine.Debug.LogError("Unity Camera not set and not found on the GameObject: " + base.gameObject.name);
			}
			this.ResetAxisFunctions();
			for (int i = 0; i < this.CameraTargets.Count; i++)
			{
				if (this.CameraTargets[i].TargetTransform == null)
				{
					this.CameraTargets.RemoveAt(i);
				}
			}
			this.CalculateScreenSize();
			this._startScreenSizeInWorldCoordinates = this._screenSizeInWorldCoordinates;
			this._originalCameraDepthSign = Mathf.Sign(this.Vector3D(this.Trnsform.localPosition));
		}

		private void Start()
		{
			this.SortPreMovers();
			this.SortPositionDeltaChangers();
			this.SortPositionOverriders();
			this.SortSizeDeltaChangers();
			this.SortSizeOverriders();
			this.SortPostMovers();
			this._targetsMidPoint = this.GetTargetsWeightedMidPoint(this.CameraTargets);
			this._cameraTargetHorizontalPositionSmoothed = this.Vector3H(this._targetsMidPoint);
			this._cameraTargetVerticalPositionSmoothed = this.Vector3V(this._targetsMidPoint);
			this._deltaTime = Time.deltaTime;
			if (this.CenterTargetOnStart && this.CameraTargets.Count > 0)
			{
				Vector3 targetsWeightedMidPoint = this.GetTargetsWeightedMidPoint(this.CameraTargets);
				float x = (!this.FollowHorizontal) ? this.Vector3H(this.Trnsform.localPosition) : this.Vector3H(targetsWeightedMidPoint);
				float y = (!this.FollowVertical) ? this.Vector3V(this.Trnsform.localPosition) : this.Vector3V(targetsWeightedMidPoint);
				Vector2 vector = new Vector2(x, y);
				vector += new Vector2(this.OverallOffset.x - this.Vector3H(this._parentPosition), this.OverallOffset.y - this.Vector3V(this._parentPosition));
				this.MoveCameraInstantlyToPosition(vector);
			}
			else
			{
				this._cameraTargetPosition = this.Trnsform.position - this._parentPosition;
				this._cameraTargetHorizontalPositionSmoothed = this.Vector3H(this._cameraTargetPosition);
				this._previousCameraTargetHorizontalPositionSmoothed = this._cameraTargetHorizontalPositionSmoothed;
				this._cameraTargetVerticalPositionSmoothed = this.Vector3V(this._cameraTargetPosition);
				this._previousCameraTargetVerticalPositionSmoothed = this._cameraTargetVerticalPositionSmoothed;
			}
		}

		private void LateUpdate()
		{
			if (this.UpdateType == UpdateType.LateUpdate)
			{
				this.Move(Time.deltaTime);
			}
		}

		private void FixedUpdate()
		{
			if (this.UpdateType == UpdateType.FixedUpdate)
			{
				this.Move(Time.fixedDeltaTime);
			}
		}

		private void OnApplicationQuit()
		{
			ProCamera2D._instance = null;
		}

		public void ApplyInfluence(Vector2 influence)
		{
			if (Time.deltaTime < 0.0001f || float.IsNaN(influence.x) || float.IsNaN(influence.y))
			{
				return;
			}
			this._influences.Add(this.VectorHV(influence.x, influence.y));
		}

		public Coroutine ApplyInfluencesTimed(Vector2[] influences, float[] durations)
		{
			return base.StartCoroutine(this.ApplyInfluencesTimedRoutine(influences, durations));
		}

		public CameraTarget AddCameraTarget(Transform targetTransform, float targetInfluenceH = 1f, float targetInfluenceV = 1f, float duration = 0f, Vector2 targetOffset = default(Vector2))
		{
			CameraTarget cameraTarget = new CameraTarget
			{
				TargetTransform = targetTransform,
				TargetInfluenceH = targetInfluenceH,
				TargetInfluenceV = targetInfluenceV,
				TargetOffset = targetOffset
			};
			this.CameraTargets.Add(cameraTarget);
			if (duration > 0f)
			{
				cameraTarget.TargetInfluence = 0f;
				base.StartCoroutine(this.AdjustTargetInfluenceRoutine(cameraTarget, targetInfluenceH, targetInfluenceV, duration, false));
			}
			return cameraTarget;
		}

		public void AddCameraTargets(IList<Transform> targetsTransforms, float targetsInfluenceH = 1f, float targetsInfluenceV = 1f, float duration = 0f, Vector2 targetOffset = default(Vector2))
		{
			for (int i = 0; i < targetsTransforms.Count; i++)
			{
				this.AddCameraTarget(targetsTransforms[i], targetsInfluenceH, targetsInfluenceV, duration, targetOffset);
			}
		}

		public CameraTarget GetCameraTarget(Transform targetTransform)
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

		public void RemoveCameraTarget(Transform targetTransform, float duration = 0f)
		{
			for (int i = 0; i < this.CameraTargets.Count; i++)
			{
				if (this.CameraTargets[i].TargetTransform.GetInstanceID() == targetTransform.GetInstanceID())
				{
					if (duration > 0f)
					{
						base.StartCoroutine(this.AdjustTargetInfluenceRoutine(this.CameraTargets[i], 0f, 0f, duration, true));
					}
					else
					{
						this.CameraTargets.Remove(this.CameraTargets[i]);
					}
				}
			}
		}

		public void RemoveAllCameraTargets(float duration = 0f)
		{
			if (duration == 0f)
			{
				this.CameraTargets.Clear();
			}
			else
			{
				for (int i = 0; i < this.CameraTargets.Count; i++)
				{
					base.StartCoroutine(this.AdjustTargetInfluenceRoutine(this.CameraTargets[i], 0f, 0f, duration, true));
				}
			}
		}

		public Coroutine AdjustCameraTargetInfluence(CameraTarget cameraTarget, float targetInfluenceH, float targetInfluenceV, float duration = 0f)
		{
			if (duration > 0f)
			{
				return base.StartCoroutine(this.AdjustTargetInfluenceRoutine(cameraTarget, targetInfluenceH, targetInfluenceV, duration, false));
			}
			cameraTarget.TargetInfluenceH = targetInfluenceH;
			cameraTarget.TargetInfluenceV = targetInfluenceV;
			return null;
		}

		public Coroutine AdjustCameraTargetInfluence(Transform cameraTargetTransf, float targetInfluenceH, float targetInfluenceV, float duration = 0f)
		{
			CameraTarget cameraTarget = this.GetCameraTarget(cameraTargetTransf);
			if (cameraTarget == null)
			{
				return null;
			}
			return this.AdjustCameraTargetInfluence(cameraTarget, targetInfluenceH, targetInfluenceV, duration);
		}

		public void MoveCameraInstantlyToPosition(Vector2 cameraPos)
		{
			this.Trnsform.localPosition = this.VectorHVD(cameraPos.x, cameraPos.y, this.Vector3D(this.Trnsform.localPosition));
			this.ResetMovement();
		}

		public void Reset(bool centerOnTargets = true, bool resetSize = true, bool resetExtensions = true)
		{
			if (centerOnTargets)
			{
				this.CenterOnTargets();
			}
			else
			{
				this.ResetMovement();
			}
			if (resetSize)
			{
				this.ResetSize();
			}
			if (resetExtensions)
			{
				this.ResetExtensions();
			}
		}

		public void ResetMovement()
		{
			this._cameraTargetPosition = this.Trnsform.localPosition;
			this._cameraTargetHorizontalPositionSmoothed = this.Vector3H(this._cameraTargetPosition);
			this._cameraTargetVerticalPositionSmoothed = this.Vector3V(this._cameraTargetPosition);
			this._previousCameraTargetHorizontalPositionSmoothed = this._cameraTargetHorizontalPositionSmoothed;
			this._previousCameraTargetVerticalPositionSmoothed = this._cameraTargetVerticalPositionSmoothed;
		}

		public void ResetSize()
		{
			this.SetScreenSize(this._startScreenSizeInWorldCoordinates.y / 2f);
		}

		public void ResetExtensions()
		{
			if (this.OnReset != null)
			{
				this.OnReset();
			}
		}

		public void CenterOnTargets()
		{
			Vector3 targetsWeightedMidPoint = this.GetTargetsWeightedMidPoint(this.CameraTargets);
			Vector2 vector = new Vector2(this.Vector3H(targetsWeightedMidPoint), this.Vector3V(targetsWeightedMidPoint));
			vector += new Vector2(this.OverallOffset.x, this.OverallOffset.y);
			this.MoveCameraInstantlyToPosition(vector);
		}

		public void UpdateScreenSize(float newSize, float duration = 0f, EaseType easeType = EaseType.EaseInOut)
		{
			if (!base.enabled)
			{
				return;
			}
			if (this._updateScreenSizeCoroutine != null)
			{
				base.StopCoroutine(this._updateScreenSizeCoroutine);
			}
			if (duration > 0f)
			{
				this._updateScreenSizeCoroutine = base.StartCoroutine(this.UpdateScreenSizeRoutine(newSize, duration, easeType));
			}
			else
			{
				this.SetScreenSize(newSize);
			}
		}

		public void Zoom(float zoomAmount, float duration = 0f, EaseType easeType = EaseType.EaseInOut)
		{
			this.UpdateScreenSize(this._screenSizeInWorldCoordinates.y * 0.5f + zoomAmount, duration, easeType);
		}

		public void DollyZoom(float targetFOV, float duration = 1f, EaseType easeType = EaseType.EaseInOut)
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.GameCamera.orthographic)
			{
				UnityEngine.Debug.LogWarning("Dolly zooming is only supported on perspective cameras");
				return;
			}
			if (this._dollyZoomRoutine != null)
			{
				base.StopCoroutine(this._dollyZoomRoutine);
			}
			targetFOV = Mathf.Clamp(targetFOV, 0.1f, 179.9f);
			if (duration <= 0f)
			{
				this.GameCamera.fieldOfView = targetFOV;
				this.Trnsform.localPosition = this.VectorHVD(this.Vector3H(this.Trnsform.localPosition), this.Vector3V(this.Trnsform.localPosition), this.GetCameraDistanceForFOV(this.GameCamera.fieldOfView, this._screenSizeInWorldCoordinates.y) * this._originalCameraDepthSign);
			}
			else
			{
				base.StartCoroutine(this.DollyZoomRoutine(targetFOV, duration, easeType));
			}
		}

		public void Move(float deltaTime)
		{
			if (Screen.width != this._previousScreenWidth || Screen.height != this._previousScreenHeight)
			{
				this.CalculateScreenSize();
			}
			this._deltaTime = deltaTime;
			if (this._deltaTime < 0.0001f)
			{
				return;
			}
			if (this.PreMoveUpdate != null)
			{
				this.PreMoveUpdate(this._deltaTime);
			}
			for (int i = 0; i < this._preMovers.Count; i++)
			{
				this._preMovers[i].PreMove(deltaTime);
			}
			this._previousTargetsMidPoint = this._targetsMidPoint;
			this._targetsMidPoint = this.GetTargetsWeightedMidPoint(this.CameraTargets);
			this._cameraTargetPosition = this._targetsMidPoint;
			this._influencesSum = Utils.GetVectorsSum(this._influences);
			this._cameraTargetPosition += this._influencesSum;
			this._influences.Clear();
			float num = (!this.FollowHorizontal) ? this.Vector3H(this.Trnsform.localPosition) : this.Vector3H(this._cameraTargetPosition);
			float num2 = (!this.FollowVertical) ? this.Vector3V(this.Trnsform.localPosition) : this.Vector3V(this._cameraTargetPosition);
			this._cameraTargetPosition = this.VectorHV(num - this.Vector3H(this._parentPosition), num2 - this.Vector3V(this._parentPosition));
			if (this.ExclusiveTargetPosition.HasValue)
			{
				this._cameraTargetPosition = this.VectorHV(this.Vector3H(this.ExclusiveTargetPosition.Value) - this.Vector3H(this._parentPosition), this.Vector3V(this.ExclusiveTargetPosition.Value) - this.Vector3V(this._parentPosition));
				this.ExclusiveTargetPosition = null;
			}
			this._cameraTargetPosition += this.VectorHV((!this.FollowHorizontal) ? 0f : this.OverallOffset.x, (!this.FollowVertical) ? 0f : this.OverallOffset.y);
			this._cameraTargetHorizontalPositionSmoothed = Utils.SmoothApproach(this._cameraTargetHorizontalPositionSmoothed, this._previousCameraTargetHorizontalPositionSmoothed, this.Vector3H(this._cameraTargetPosition), 1f / this.HorizontalFollowSmoothness, this._deltaTime);
			this._previousCameraTargetHorizontalPositionSmoothed = this._cameraTargetHorizontalPositionSmoothed;
			this._cameraTargetVerticalPositionSmoothed = Utils.SmoothApproach(this._cameraTargetVerticalPositionSmoothed, this._previousCameraTargetVerticalPositionSmoothed, this.Vector3V(this._cameraTargetPosition), 1f / this.VerticalFollowSmoothness, this._deltaTime);
			this._previousCameraTargetVerticalPositionSmoothed = this._cameraTargetVerticalPositionSmoothed;
			float arg = this._cameraTargetHorizontalPositionSmoothed - this.Vector3H(this.Trnsform.localPosition);
			float arg2 = this._cameraTargetVerticalPositionSmoothed - this.Vector3V(this.Trnsform.localPosition);
			Vector3 vector = this.VectorHV(arg, arg2);
			float num3 = 0f;
			for (int j = 0; j < this._sizeDeltaChangers.Count; j++)
			{
				num3 = this._sizeDeltaChangers[j].AdjustSize(deltaTime, num3);
			}
			float num4 = this._screenSizeInWorldCoordinates.y * 0.5f + num3;
			for (int k = 0; k < this._sizeOverriders.Count; k++)
			{
				num4 = this._sizeOverriders[k].OverrideSize(deltaTime, num4);
			}
			if (num4 != this._screenSizeInWorldCoordinates.y * 0.5f)
			{
				this.SetScreenSize(num4);
			}
			for (int l = 0; l < this._positionDeltaChangers.Count; l++)
			{
				vector = this._positionDeltaChangers[l].AdjustDelta(deltaTime, vector);
			}
			Vector3 vector2 = this.LocalPosition + vector;
			for (int m = 0; m < this._positionOverriders.Count; m++)
			{
				vector2 = this._positionOverriders[m].OverridePosition(deltaTime, vector2);
			}
			this.Trnsform.localPosition = this.VectorHVD(this.Vector3H(vector2), this.Vector3V(vector2), this.Vector3D(this.Trnsform.localPosition));
			for (int n = 0; n < this._postMovers.Count; n++)
			{
				this._postMovers[n].PostMove(deltaTime);
			}
			if (this.PostMoveUpdate != null)
			{
				this.PostMoveUpdate(this._deltaTime);
			}
		}

		public YieldInstruction GetYield()
		{
			UpdateType updateType = this.UpdateType;
			if (updateType != UpdateType.FixedUpdate)
			{
				return null;
			}
			return this._waitForFixedUpdate;
		}

		private void ResetAxisFunctions()
		{
			MovementAxis axis = this.Axis;
			if (axis != MovementAxis.XY)
			{
				if (axis != MovementAxis.XZ)
				{
					if (axis == MovementAxis.YZ)
					{
						this.Vector3H = ((Vector3 vector) => vector.z);
						this.Vector3V = ((Vector3 vector) => vector.y);
						this.Vector3D = ((Vector3 vector) => vector.x);
						this.VectorHV = ((float h, float v) => new Vector3(0f, v, h));
						this.VectorHVD = ((float h, float v, float d) => new Vector3(d, v, h));
					}
				}
				else
				{
					this.Vector3H = ((Vector3 vector) => vector.x);
					this.Vector3V = ((Vector3 vector) => vector.z);
					this.Vector3D = ((Vector3 vector) => vector.y);
					this.VectorHV = ((float h, float v) => new Vector3(h, 0f, v));
					this.VectorHVD = ((float h, float v, float d) => new Vector3(h, d, v));
				}
			}
			else
			{
				this.Vector3H = ((Vector3 vector) => vector.x);
				this.Vector3V = ((Vector3 vector) => vector.y);
				this.Vector3D = ((Vector3 vector) => vector.z);
				this.VectorHV = ((float h, float v) => new Vector3(h, v, 0f));
				this.VectorHVD = ((float h, float v, float d) => new Vector3(h, v, d));
			}
		}

		private Vector3 GetTargetsWeightedMidPoint(IList<CameraTarget> targets)
		{
			float num = 0f;
			float num2 = 0f;
			int count = targets.Count;
			if (count == 0)
			{
				return base.transform.localPosition;
			}
			float num3 = 0f;
			float num4 = 0f;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < count; i++)
			{
				if (targets[i] != null)
				{
					num += (this.Vector3H(targets[i].TargetPosition) + targets[i].TargetOffset.x) * targets[i].TargetInfluenceH;
					num2 += (this.Vector3V(targets[i].TargetPosition) + targets[i].TargetOffset.y) * targets[i].TargetInfluenceV;
					num3 += targets[i].TargetInfluenceH;
					num4 += targets[i].TargetInfluenceV;
					if (targets[i].TargetInfluenceH > 0f)
					{
						num5++;
					}
					if (targets[i].TargetInfluenceV > 0f)
					{
						num6++;
					}
				}
			}
			if (num3 < 1f && num5 == 1)
			{
				num3 += 1f - num3;
			}
			if (num4 < 1f && num6 == 1)
			{
				num4 += 1f - num4;
			}
			if (num3 > 0.0001f)
			{
				num /= num3;
			}
			if (num4 > 0.0001f)
			{
				num2 /= num4;
			}
			return this.VectorHV(num, num2);
		}

		private IEnumerator ApplyInfluencesTimedRoutine(IList<Vector2> influences, float[] durations)
		{
			ProCamera2D._ApplyInfluencesTimedRoutine_c__Iterator0 _ApplyInfluencesTimedRoutine_c__Iterator = new ProCamera2D._ApplyInfluencesTimedRoutine_c__Iterator0();
			_ApplyInfluencesTimedRoutine_c__Iterator.durations = durations;
			_ApplyInfluencesTimedRoutine_c__Iterator.influences = influences;
			_ApplyInfluencesTimedRoutine_c__Iterator._this = this;
			return _ApplyInfluencesTimedRoutine_c__Iterator;
		}

		private IEnumerator ApplyInfluenceTimedRoutine(Vector2 influence, float duration)
		{
			ProCamera2D._ApplyInfluenceTimedRoutine_c__Iterator1 _ApplyInfluenceTimedRoutine_c__Iterator = new ProCamera2D._ApplyInfluenceTimedRoutine_c__Iterator1();
			_ApplyInfluenceTimedRoutine_c__Iterator.duration = duration;
			_ApplyInfluenceTimedRoutine_c__Iterator.influence = influence;
			_ApplyInfluenceTimedRoutine_c__Iterator._this = this;
			return _ApplyInfluenceTimedRoutine_c__Iterator;
		}

		private IEnumerator AdjustTargetInfluenceRoutine(CameraTarget cameraTarget, float influenceH, float influenceV, float duration, bool removeIfZeroInfluence = false)
		{
			ProCamera2D._AdjustTargetInfluenceRoutine_c__Iterator2 _AdjustTargetInfluenceRoutine_c__Iterator = new ProCamera2D._AdjustTargetInfluenceRoutine_c__Iterator2();
			_AdjustTargetInfluenceRoutine_c__Iterator.cameraTarget = cameraTarget;
			_AdjustTargetInfluenceRoutine_c__Iterator.duration = duration;
			_AdjustTargetInfluenceRoutine_c__Iterator.influenceH = influenceH;
			_AdjustTargetInfluenceRoutine_c__Iterator.influenceV = influenceV;
			_AdjustTargetInfluenceRoutine_c__Iterator.removeIfZeroInfluence = removeIfZeroInfluence;
			_AdjustTargetInfluenceRoutine_c__Iterator._this = this;
			return _AdjustTargetInfluenceRoutine_c__Iterator;
		}

		private IEnumerator UpdateScreenSizeRoutine(float finalSize, float duration, EaseType easeType)
		{
			ProCamera2D._UpdateScreenSizeRoutine_c__Iterator3 _UpdateScreenSizeRoutine_c__Iterator = new ProCamera2D._UpdateScreenSizeRoutine_c__Iterator3();
			_UpdateScreenSizeRoutine_c__Iterator.duration = duration;
			_UpdateScreenSizeRoutine_c__Iterator.finalSize = finalSize;
			_UpdateScreenSizeRoutine_c__Iterator.easeType = easeType;
			_UpdateScreenSizeRoutine_c__Iterator._this = this;
			return _UpdateScreenSizeRoutine_c__Iterator;
		}

		private IEnumerator DollyZoomRoutine(float finalFOV, float duration, EaseType easeType)
		{
			ProCamera2D._DollyZoomRoutine_c__Iterator4 _DollyZoomRoutine_c__Iterator = new ProCamera2D._DollyZoomRoutine_c__Iterator4();
			_DollyZoomRoutine_c__Iterator.duration = duration;
			_DollyZoomRoutine_c__Iterator.finalFOV = finalFOV;
			_DollyZoomRoutine_c__Iterator.easeType = easeType;
			_DollyZoomRoutine_c__Iterator._this = this;
			return _DollyZoomRoutine_c__Iterator;
		}

		private void SetScreenSize(float newSize)
		{
			if (this.GameCamera.orthographic)
			{
				newSize = Mathf.Max(newSize, 0.1f);
				this.GameCamera.orthographicSize = newSize;
			}
			else if (this.ZoomWithFOV)
			{
				float value = 2f * Mathf.Atan(newSize / Mathf.Abs(this.Vector3D(this.Trnsform.localPosition))) * 57.29578f;
				this.GameCamera.fieldOfView = Mathf.Clamp(value, 0.1f, 179.9f);
			}
			else
			{
				this.Trnsform.localPosition = this.VectorHVD(this.Vector3H(this.Trnsform.localPosition), this.Vector3V(this.Trnsform.localPosition), newSize / Mathf.Tan(this.GameCamera.fieldOfView * 0.5f * 0.0174532924f) * this._originalCameraDepthSign);
			}
			this._screenSizeInWorldCoordinates = new Vector2(newSize * 2f * this.GameCamera.aspect, newSize * 2f);
			if (this.OnCameraResize != null)
			{
				this.OnCameraResize(this._screenSizeInWorldCoordinates);
			}
		}

		private void CalculateScreenSize()
		{
			this.GameCamera.ResetAspect();
			this._screenSizeInWorldCoordinates = Utils.GetScreenSizeInWorldCoords(this.GameCamera, Mathf.Abs(this.Vector3D(this.Trnsform.localPosition)));
			this._previousScreenWidth = Screen.width;
			this._previousScreenHeight = Screen.height;
		}

		private float GetCameraDistanceForFOV(float fov, float cameraHeight)
		{
			return cameraHeight / (2f * Mathf.Tan(0.5f * fov * 0.0174532924f));
		}

		public void AddPreMover(IPreMover mover)
		{
			this._preMovers.Add(mover);
		}

		public void RemovePreMover(IPreMover mover)
		{
			this._preMovers.Remove(mover);
		}

		public void SortPreMovers()
		{
			this._preMovers = (from a in this._preMovers
			orderby a.PrMOrder
			select a).ToList<IPreMover>();
		}

		public void AddPositionDeltaChanger(IPositionDeltaChanger changer)
		{
			this._positionDeltaChangers.Add(changer);
		}

		public void RemovePositionDeltaChanger(IPositionDeltaChanger changer)
		{
			this._positionDeltaChangers.Remove(changer);
		}

		public void SortPositionDeltaChangers()
		{
			this._positionDeltaChangers = (from a in this._positionDeltaChangers
			orderby a.PDCOrder
			select a).ToList<IPositionDeltaChanger>();
		}

		public void AddPositionOverrider(IPositionOverrider overrider)
		{
			this._positionOverriders.Add(overrider);
		}

		public void RemovePositionOverrider(IPositionOverrider overrider)
		{
			this._positionOverriders.Remove(overrider);
		}

		public void SortPositionOverriders()
		{
			this._positionOverriders = (from a in this._positionOverriders
			orderby a.POOrder
			select a).ToList<IPositionOverrider>();
		}

		public void AddSizeDeltaChanger(ISizeDeltaChanger changer)
		{
			this._sizeDeltaChangers.Add(changer);
		}

		public void RemoveSizeDeltaChanger(ISizeDeltaChanger changer)
		{
			this._sizeDeltaChangers.Remove(changer);
		}

		public void SortSizeDeltaChangers()
		{
			this._sizeDeltaChangers = (from a in this._sizeDeltaChangers
			orderby a.SDCOrder
			select a).ToList<ISizeDeltaChanger>();
		}

		public void AddSizeOverrider(ISizeOverrider overrider)
		{
			this._sizeOverriders.Add(overrider);
		}

		public void RemoveSizeOverrider(ISizeOverrider overrider)
		{
			this._sizeOverriders.Remove(overrider);
		}

		public void SortSizeOverriders()
		{
			this._sizeOverriders = (from a in this._sizeOverriders
			orderby a.SOOrder
			select a).ToList<ISizeOverrider>();
		}

		public void AddPostMover(IPostMover mover)
		{
			this._postMovers.Add(mover);
		}

		public void RemovePostMover(IPostMover mover)
		{
			this._postMovers.Remove(mover);
		}

		public void SortPostMovers()
		{
			this._postMovers = (from a in this._postMovers
			orderby a.PMOrder
			select a).ToList<IPostMover>();
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			this.ResetAxisFunctions();
		}
	}
}
