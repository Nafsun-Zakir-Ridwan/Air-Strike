using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/extension-shake/")]
	public class ProCamera2DShake : BasePC2D
	{
		private sealed class _ShakeRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Vector3 _newShakePosition___1;

			internal Vector3 _newShakePositionSmoothed___1;

			internal bool ignoreTimeScale;

			internal float smoothness;

			internal Quaternion _rotationTargetSmoothed___1;

			internal ProCamera2DShake _this;

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

			public _ShakeRoutine_c__Iterator0()
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
				if (this._this._shakePositions.Count > 0 || this._this._shakeParent.localPosition != this._this._influencesSum || this._this.Trnsform.localRotation != this._this._originalRotation)
				{
					this._newShakePosition___1 = Utils.GetVectorsSum(this._this._shakePositions) + this._this._influencesSum;
					this._newShakePositionSmoothed___1 = Vector3.zero;
					if (this.ignoreTimeScale)
					{
						this._newShakePositionSmoothed___1 = Vector3.SmoothDamp(this._this._shakeParent.localPosition, this._newShakePosition___1, ref this._this._shakeVelocity, this.smoothness, 3.40282347E+38f, Time.unscaledDeltaTime);
					}
					else if (this._this.ProCamera2D.DeltaTime > 0f)
					{
						this._newShakePositionSmoothed___1 = Vector3.SmoothDamp(this._this._shakeParent.localPosition, this._newShakePosition___1, ref this._this._shakeVelocity, this.smoothness);
					}
					this._this._shakeParent.localPosition = this._newShakePositionSmoothed___1;
					this._this._shakePositions.Clear();
					if (this.ignoreTimeScale)
					{
						this._this._rotationTime = Mathf.SmoothDamp(this._this._rotationTime, 1f, ref this._this._rotationVelocity, this.smoothness, 3.40282347E+38f, Time.unscaledDeltaTime);
					}
					else if (this._this.ProCamera2D.DeltaTime > 0f)
					{
						this._this._rotationTime = Mathf.SmoothDamp(this._this._rotationTime, 1f, ref this._this._rotationVelocity, this.smoothness);
					}
					this._rotationTargetSmoothed___1 = Quaternion.Slerp(this._this.Trnsform.localRotation, this._this._rotationTarget, this._this._rotationTime);
					this._this.Trnsform.localRotation = this._rotationTargetSmoothed___1;
					this._this._rotationTarget = this._this._originalRotation;
					this._current = this._this.ProCamera2D.GetYield();
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

		private sealed class _ApplyShakesTimedRoutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal int _count___0;

			internal float[] durations;

			internal float _duration___1;

			internal IList<Vector2> shakes;

			internal IList<Quaternion> rotations;

			internal bool ignoreTimeScale;

			internal ProCamera2DShake _this;

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

			public _ApplyShakesTimedRoutine_c__Iterator1()
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
					this._current = this._this.StartCoroutine(this._this.ApplyShakeTimedRoutine(this.shakes[this._count___0], this.rotations[this._count___0], this._duration___1, this.ignoreTimeScale));
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				this._this._shakeParent.localPosition = this._this._influencesSum;
				this._this.Trnsform.localRotation = this._this._originalRotation;
				this._this._shakeCoroutine = null;
				if (this._this.OnShakeCompleted != null)
				{
					this._this.OnShakeCompleted();
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

		private sealed class _ApplyShakeTimedRoutine_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float duration;

			internal bool ignoreTimeScale;

			internal Vector2 shake;

			internal Quaternion rotation;

			internal ProCamera2DShake _this;

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

			public _ApplyShakeTimedRoutine_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._this._rotationTime = 0f;
					this._this._rotationVelocity = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this.duration > 0f)
				{
					if (this.ignoreTimeScale)
					{
						this.duration -= Time.unscaledDeltaTime;
					}
					else
					{
						this.duration -= this._this.ProCamera2D.DeltaTime;
					}
					this._this._shakePositions.Add(this._this.VectorHV(this.shake.x, this.shake.y));
					this._this._rotationTarget = this.rotation;
					this._current = this._this.ProCamera2D.GetYield();
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

		public static string ExtensionName = "Shake";

		private static ProCamera2DShake _instance;

		public Action OnShakeCompleted;

		public Vector2 Strength = new Vector2(10f, 10f);

		[Range(0.02f, 3f)]
		public float Duration = 0.5f;

		[Range(1f, 100f)]
		public int Vibrato = 10;

		[Range(0f, 1f)]
		public float Randomness = 0.1f;

		[Range(0f, 0.5f)]
		public float Smoothness = 0.1f;

		[Range(0f, 360f)]
		public float InitialAngle;

		public bool UseRandomInitialAngle = true;

		public Vector3 Rotation;

		public bool IgnoreTimeScale;

		public List<ShakePreset> ShakePresets;

		private Transform _shakeParent;

		private List<Coroutine> _applyInfluencesCoroutines = new List<Coroutine>();

		private Coroutine _shakeCoroutine;

		private Vector3 _shakeVelocity;

		private List<Vector3> _shakePositions = new List<Vector3>();

		private Quaternion _rotationTarget;

		private Quaternion _originalRotation;

		private float _rotationTime;

		private float _rotationVelocity;

		private List<Vector3> _influences = new List<Vector3>();

		private Vector3 _influencesSum = Vector3.zero;

		public static ProCamera2DShake Instance
		{
			get
			{
				if (object.Equals(ProCamera2DShake._instance, null))
				{
					ProCamera2DShake._instance = (UnityEngine.Object.FindObjectOfType(typeof(ProCamera2DShake)) as ProCamera2DShake);
					if (object.Equals(ProCamera2DShake._instance, null))
					{
						throw new UnityException("ProCamera2D does not have a Shake extension.");
					}
				}
				return ProCamera2DShake._instance;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			ProCamera2DShake._instance = this;
			if (this.ProCamera2D.transform.parent != null)
			{
				this._shakeParent = new GameObject("ProCamera2DShakeContainer").transform;
				this._shakeParent.parent = this.ProCamera2D.transform.parent;
				this._shakeParent.localPosition = Vector3.zero;
				this.ProCamera2D.transform.parent = this._shakeParent;
			}
			else
			{
				Transform transform = new GameObject("ProCamera2DShakeContainer").transform;
				this.ProCamera2D.transform.parent = transform;
				this._shakeParent = transform;
			}
			this._originalRotation = this.Trnsform.localRotation;
		}

		private void Update()
		{
			this._influencesSum = Vector3.zero;
			if (this._influences.Count > 0)
			{
				this._influencesSum = Utils.GetVectorsSum(this._influences);
				this._influences.Clear();
				this._shakeParent.localPosition = this._influencesSum;
			}
		}

		public void Shake()
		{
			this.Shake(this.Duration, this.Strength, this.Vibrato, this.Randomness, (!this.UseRandomInitialAngle) ? this.InitialAngle : -1f, this.Rotation, this.Smoothness, this.IgnoreTimeScale);
		}

		public void Shake(float duration, Vector2 strength, int vibrato = 10, float randomness = 0.1f, float initialAngle = -1f, Vector3 rotation = default(Vector3), float smoothness = 0.1f, bool ignoreTimeScale = false)
		{
			if (!base.enabled)
			{
				return;
			}
			vibrato++;
			if (vibrato < 2)
			{
				vibrato = 2;
			}
			float[] array = new float[vibrato];
			float num = 0f;
			for (int i = 0; i < vibrato; i++)
			{
				float num2 = (float)(i + 1) / (float)vibrato;
				float num3 = duration * num2;
				num += num3;
				array[i] = num3;
			}
			float num4 = duration / num;
			for (int j = 0; j < vibrato; j++)
			{
				array[j] *= num4;
			}
			float num5 = strength.magnitude;
			float num6 = num5 / (float)vibrato;
			float num7 = (initialAngle == -1f) ? UnityEngine.Random.Range(0f, 360f) : initialAngle;
			Vector2[] array2 = new Vector2[vibrato];
			array2[vibrato - 1] = Vector2.zero;
			Quaternion[] array3 = new Quaternion[vibrato];
			array3[vibrato - 1] = this._originalRotation;
			Quaternion a = Quaternion.Euler(rotation);
			for (int k = 0; k < vibrato - 1; k++)
			{
				if (k > 0)
				{
					num7 = num7 - 180f + (float)UnityEngine.Random.Range(-90, 90) * randomness;
				}
				Quaternion rotation2 = Quaternion.AngleAxis((float)UnityEngine.Random.Range(-90, 90) * randomness, Vector3.up);
				float f = num7 * 0.0174532924f;
				Vector3 point = new Vector3(num5 * Mathf.Cos(f), num5 * Mathf.Sin(f), 0f);
				Vector2 vector = rotation2 * point;
				vector.x = Vector2.ClampMagnitude(vector, strength.x).x;
				vector.y = Vector2.ClampMagnitude(vector, strength.y).y;
				array2[k] = vector;
				num5 -= num6;
				strength = Vector2.ClampMagnitude(strength, num5);
				int num8 = (k % 2 != 0) ? (-1) : 1;
				float t = (float)k / (float)(vibrato - 1);
				array3[k] = ((num8 != 1) ? (Quaternion.Inverse(Quaternion.Lerp(a, Quaternion.identity, t)) * this._originalRotation) : (Quaternion.Lerp(a, Quaternion.identity, t) * this._originalRotation));
			}
			this._applyInfluencesCoroutines.Add(this.ApplyShakesTimed(array2, array3, array, smoothness, ignoreTimeScale));
		}

		public void ShakeUsingPreset(int presetIndex)
		{
			if (presetIndex <= this.ShakePresets.Count - 1)
			{
				this.Shake(this.ShakePresets[presetIndex].Duration, this.ShakePresets[presetIndex].Strength, this.ShakePresets[presetIndex].Vibrato, this.ShakePresets[presetIndex].Randomness, this.ShakePresets[presetIndex].InitialAngle, this.ShakePresets[presetIndex].Rotation, this.ShakePresets[presetIndex].Smoothness, this.ShakePresets[presetIndex].IgnoreTimeScale);
			}
			else
			{
				UnityEngine.Debug.LogWarning("Could not find a shake preset with the index: " + presetIndex);
			}
		}

		public void ShakeUsingPreset(string presetName)
		{
			for (int i = 0; i < this.ShakePresets.Count; i++)
			{
				if (this.ShakePresets[i].Name == presetName)
				{
					this.Shake(this.ShakePresets[i].Duration, this.ShakePresets[i].Strength, this.ShakePresets[i].Vibrato, this.ShakePresets[i].Randomness, this.ShakePresets[i].InitialAngle, this.ShakePresets[i].Rotation, this.ShakePresets[i].Smoothness, this.ShakePresets[i].IgnoreTimeScale);
					return;
				}
			}
			UnityEngine.Debug.LogWarning("Could not find a shake preset with the name: " + presetName);
		}

		public void StopShaking()
		{
			for (int i = 0; i < this._applyInfluencesCoroutines.Count; i++)
			{
				base.StopCoroutine(this._applyInfluencesCoroutines[i]);
			}
			this._shakePositions.Clear();
			if (this._shakeCoroutine != null)
			{
				base.StopCoroutine(this._shakeCoroutine);
				this._shakeCoroutine = null;
			}
		}

		public Coroutine ApplyShakesTimed(Vector2[] shakes, Vector3[] rotations, float[] durations, float smoothness = 0.1f, bool ignoreTimeScale = false)
		{
			if (!base.enabled)
			{
				return null;
			}
			Quaternion[] array = new Quaternion[rotations.Length];
			for (int i = 0; i < rotations.Length; i++)
			{
				array[i] = Quaternion.Euler(rotations[i]) * this._originalRotation;
			}
			return this.ApplyShakesTimed(shakes, array, durations, 0.1f, false);
		}

		public void ApplyInfluenceIgnoringBoundaries(Vector2 influence)
		{
			if (Time.deltaTime < 0.0001f || float.IsNaN(influence.x) || float.IsNaN(influence.y))
			{
				return;
			}
			this._influences.Add(this.VectorHV(influence.x, influence.y));
		}

		private Coroutine ApplyShakesTimed(Vector2[] shakes, Quaternion[] rotations, float[] durations, float smoothness = 0.1f, bool ignoreTimeScale = false)
		{
			Coroutine result = base.StartCoroutine(this.ApplyShakesTimedRoutine(shakes, rotations, durations, ignoreTimeScale));
			if (this._shakeCoroutine == null)
			{
				this._shakeCoroutine = base.StartCoroutine(this.ShakeRoutine(smoothness, ignoreTimeScale));
			}
			return result;
		}

		private IEnumerator ShakeRoutine(float smoothness, bool ignoreTimeScale = false)
		{
			ProCamera2DShake._ShakeRoutine_c__Iterator0 _ShakeRoutine_c__Iterator = new ProCamera2DShake._ShakeRoutine_c__Iterator0();
			_ShakeRoutine_c__Iterator.ignoreTimeScale = ignoreTimeScale;
			_ShakeRoutine_c__Iterator.smoothness = smoothness;
			_ShakeRoutine_c__Iterator._this = this;
			return _ShakeRoutine_c__Iterator;
		}

		private IEnumerator ApplyShakesTimedRoutine(IList<Vector2> shakes, IList<Quaternion> rotations, float[] durations, bool ignoreTimeScale = false)
		{
			ProCamera2DShake._ApplyShakesTimedRoutine_c__Iterator1 _ApplyShakesTimedRoutine_c__Iterator = new ProCamera2DShake._ApplyShakesTimedRoutine_c__Iterator1();
			_ApplyShakesTimedRoutine_c__Iterator.durations = durations;
			_ApplyShakesTimedRoutine_c__Iterator.shakes = shakes;
			_ApplyShakesTimedRoutine_c__Iterator.rotations = rotations;
			_ApplyShakesTimedRoutine_c__Iterator.ignoreTimeScale = ignoreTimeScale;
			_ApplyShakesTimedRoutine_c__Iterator._this = this;
			return _ApplyShakesTimedRoutine_c__Iterator;
		}

		private IEnumerator ApplyShakeTimedRoutine(Vector2 shake, Quaternion rotation, float duration, bool ignoreTimeScale = false)
		{
			ProCamera2DShake._ApplyShakeTimedRoutine_c__Iterator2 _ApplyShakeTimedRoutine_c__Iterator = new ProCamera2DShake._ApplyShakeTimedRoutine_c__Iterator2();
			_ApplyShakeTimedRoutine_c__Iterator.duration = duration;
			_ApplyShakeTimedRoutine_c__Iterator.ignoreTimeScale = ignoreTimeScale;
			_ApplyShakeTimedRoutine_c__Iterator.shake = shake;
			_ApplyShakeTimedRoutine_c__Iterator.rotation = rotation;
			_ApplyShakeTimedRoutine_c__Iterator._this = this;
			return _ApplyShakeTimedRoutine_c__Iterator;
		}
	}
}
