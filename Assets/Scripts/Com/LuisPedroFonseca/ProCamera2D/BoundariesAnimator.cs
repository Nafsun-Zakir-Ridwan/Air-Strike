using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	public class BoundariesAnimator
	{
		private sealed class _LeftTransitionRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialLeftBoundary___0;

			internal float _t___0;

			internal float duration;

			internal bool turnOffBoundaryAfterwards;

			internal BoundariesAnimator _this;

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

			public _LeftTransitionRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._initialLeftBoundary___0 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition) - this._this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
					this._this.NumericBoundaries.TargetLeftBoundary = this._this.LeftBoundary;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.duration;
					if (this._this.UseLeftBoundary && this._this.UseRightBoundary && this._this.LeftBoundary < this._initialLeftBoundary___0)
					{
						this._this.NumericBoundaries.LeftBoundary = this._this.LeftBoundary;
					}
					else if (this._this.UseLeftBoundary)
					{
						this._this.NumericBoundaries.LeftBoundary = Utils.EaseFromTo(this._initialLeftBoundary___0, this._this.LeftBoundary, this._t___0, this._this.TransitionEaseType);
						float num2 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition) - this._this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
						if (num2 < this._this.NumericBoundaries.TargetLeftBoundary && this._this.NumericBoundaries.LeftBoundary < num2)
						{
							this._this.NumericBoundaries.LeftBoundary = num2;
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				if (this.turnOffBoundaryAfterwards)
				{
					this._this.NumericBoundaries.UseLeftBoundary = false;
					this._this.UseLeftBoundary = false;
				}
				if (!this._this.NumericBoundaries.HasFiredTransitionFinished && this._this.OnTransitionFinished != null)
				{
					this._this.NumericBoundaries.HasFiredTransitionStarted = false;
					this._this.NumericBoundaries.HasFiredTransitionFinished = true;
					this._this.OnTransitionFinished();
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

		private sealed class _RightTransitionRoutine_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialRightBoundary___0;

			internal float _t___0;

			internal float duration;

			internal bool turnOffBoundaryAfterwards;

			internal BoundariesAnimator _this;

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

			public _RightTransitionRoutine_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._initialRightBoundary___0 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition) + this._this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
					this._this.NumericBoundaries.TargetRightBoundary = this._this.RightBoundary;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.duration;
					if (this._this.UseRightBoundary && this._this.UseLeftBoundary && this._this.RightBoundary > this._initialRightBoundary___0)
					{
						this._this.NumericBoundaries.RightBoundary = this._this.RightBoundary;
					}
					else if (this._this.UseRightBoundary)
					{
						this._this.NumericBoundaries.RightBoundary = Utils.EaseFromTo(this._initialRightBoundary___0, this._this.RightBoundary, this._t___0, this._this.TransitionEaseType);
						float num2 = this._this.Vector3H(this._this.ProCamera2D.LocalPosition) + this._this.ProCamera2D.ScreenSizeInWorldCoordinates.x / 2f;
						if (num2 > this._this.NumericBoundaries.TargetRightBoundary && this._this.NumericBoundaries.RightBoundary > num2)
						{
							this._this.NumericBoundaries.RightBoundary = num2;
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				if (this.turnOffBoundaryAfterwards)
				{
					this._this.NumericBoundaries.UseRightBoundary = false;
					this._this.UseRightBoundary = false;
				}
				if (!this._this.NumericBoundaries.HasFiredTransitionFinished && this._this.OnTransitionFinished != null)
				{
					this._this.NumericBoundaries.HasFiredTransitionStarted = false;
					this._this.NumericBoundaries.HasFiredTransitionFinished = true;
					this._this.OnTransitionFinished();
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

		private sealed class _TopTransitionRoutine_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialTopBoundary___0;

			internal float _t___0;

			internal float duration;

			internal bool turnOffBoundaryAfterwards;

			internal BoundariesAnimator _this;

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

			public _TopTransitionRoutine_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._initialTopBoundary___0 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition) + this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
					this._this.NumericBoundaries.TargetTopBoundary = this._this.TopBoundary;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.duration;
					if (this._this.UseTopBoundary && this._this.UseBottomBoundary && this._this.TopBoundary > this._initialTopBoundary___0)
					{
						this._this.NumericBoundaries.TopBoundary = this._this.TopBoundary;
					}
					else if (this._this.UseTopBoundary)
					{
						this._this.NumericBoundaries.TopBoundary = Utils.EaseFromTo(this._initialTopBoundary___0, this._this.TopBoundary, this._t___0, this._this.TransitionEaseType);
						float num2 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition) + this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
						if (num2 > this._this.NumericBoundaries.TargetTopBoundary && this._this.NumericBoundaries.TopBoundary > num2)
						{
							this._this.NumericBoundaries.TopBoundary = num2;
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				if (this.turnOffBoundaryAfterwards)
				{
					this._this.NumericBoundaries.UseTopBoundary = false;
					this._this.UseTopBoundary = false;
				}
				if (!this._this.NumericBoundaries.HasFiredTransitionFinished && this._this.OnTransitionFinished != null)
				{
					this._this.NumericBoundaries.HasFiredTransitionStarted = false;
					this._this.NumericBoundaries.HasFiredTransitionFinished = true;
					this._this.OnTransitionFinished();
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

		private sealed class _BottomTransitionRoutine_c__Iterator3 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float _initialBottomBoundary___0;

			internal float _t___0;

			internal float duration;

			internal bool turnOffBoundaryAfterwards;

			internal BoundariesAnimator _this;

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

			public _BottomTransitionRoutine_c__Iterator3()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._initialBottomBoundary___0 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition) - this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
					this._this.NumericBoundaries.TargetBottomBoundary = this._this.BottomBoundary;
					this._t___0 = 0f;
					break;
				case 1u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += this._this.ProCamera2D.DeltaTime / this.duration;
					if (this._this.UseBottomBoundary && this._this.UseTopBoundary && this._this.BottomBoundary < this._initialBottomBoundary___0)
					{
						this._this.NumericBoundaries.BottomBoundary = this._this.BottomBoundary;
					}
					else if (this._this.UseBottomBoundary)
					{
						this._this.NumericBoundaries.BottomBoundary = Utils.EaseFromTo(this._initialBottomBoundary___0, this._this.BottomBoundary, this._t___0, this._this.TransitionEaseType);
						float num2 = this._this.Vector3V(this._this.ProCamera2D.LocalPosition) - this._this.ProCamera2D.ScreenSizeInWorldCoordinates.y / 2f;
						if (num2 < this._this.NumericBoundaries.TargetBottomBoundary && this._this.NumericBoundaries.BottomBoundary < num2)
						{
							this._this.NumericBoundaries.BottomBoundary = num2;
						}
					}
					this._current = this._this.ProCamera2D.GetYield();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				}
				if (this.turnOffBoundaryAfterwards)
				{
					this._this.NumericBoundaries.UseBottomBoundary = false;
					this._this.UseBottomBoundary = false;
				}
				if (!this._this.NumericBoundaries.HasFiredTransitionFinished && this._this.OnTransitionFinished != null)
				{
					this._this.NumericBoundaries.HasFiredTransitionStarted = false;
					this._this.NumericBoundaries.HasFiredTransitionFinished = true;
					this._this.OnTransitionFinished();
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

		public Action OnTransitionStarted;

		public Action OnTransitionFinished;

		public bool UseTopBoundary;

		public float TopBoundary;

		public bool UseBottomBoundary;

		public float BottomBoundary;

		public bool UseLeftBoundary;

		public float LeftBoundary;

		public bool UseRightBoundary;

		public float RightBoundary;

		public float TransitionDuration = 1f;

		public EaseType TransitionEaseType;

		private ProCamera2D ProCamera2D;

		private ProCamera2DNumericBoundaries NumericBoundaries;

		private Func<Vector3, float> Vector3H;

		private Func<Vector3, float> Vector3V;

		private static Func<Vector3, float> __f__am_cache0;

		private static Func<Vector3, float> __f__am_cache1;

		private static Func<Vector3, float> __f__am_cache2;

		private static Func<Vector3, float> __f__am_cache3;

		private static Func<Vector3, float> __f__am_cache4;

		private static Func<Vector3, float> __f__am_cache5;

		public BoundariesAnimator(ProCamera2D proCamera2D, ProCamera2DNumericBoundaries numericBoundaries)
		{
			this.ProCamera2D = proCamera2D;
			this.NumericBoundaries = numericBoundaries;
			MovementAxis axis = this.ProCamera2D.Axis;
			if (axis != MovementAxis.XY)
			{
				if (axis != MovementAxis.XZ)
				{
					if (axis == MovementAxis.YZ)
					{
						this.Vector3H = ((Vector3 vector) => vector.z);
						this.Vector3V = ((Vector3 vector) => vector.y);
					}
				}
				else
				{
					this.Vector3H = ((Vector3 vector) => vector.x);
					this.Vector3V = ((Vector3 vector) => vector.z);
				}
			}
			else
			{
				this.Vector3H = ((Vector3 vector) => vector.x);
				this.Vector3V = ((Vector3 vector) => vector.y);
			}
		}

		public int GetAnimsCount()
		{
			int num = 0;
			if (this.UseLeftBoundary)
			{
				num++;
			}
			else if (!this.UseLeftBoundary && this.NumericBoundaries.UseLeftBoundary && this.UseRightBoundary && this.RightBoundary < this.NumericBoundaries.TargetLeftBoundary)
			{
				num++;
			}
			if (this.UseRightBoundary)
			{
				num++;
			}
			else if (!this.UseRightBoundary && this.NumericBoundaries.UseRightBoundary && this.UseLeftBoundary && this.LeftBoundary > this.NumericBoundaries.TargetRightBoundary)
			{
				num++;
			}
			if (this.UseTopBoundary)
			{
				num++;
			}
			else if (!this.UseTopBoundary && this.NumericBoundaries.UseTopBoundary && this.UseBottomBoundary && this.BottomBoundary > this.NumericBoundaries.TargetTopBoundary)
			{
				num++;
			}
			if (this.UseBottomBoundary)
			{
				num++;
			}
			else if (!this.UseBottomBoundary && this.NumericBoundaries.UseBottomBoundary && this.UseTopBoundary && this.TopBoundary < this.NumericBoundaries.TargetBottomBoundary)
			{
				num++;
			}
			return num;
		}

		public void Transition()
		{
			if (!this.NumericBoundaries.HasFiredTransitionStarted && this.OnTransitionStarted != null)
			{
				this.NumericBoundaries.HasFiredTransitionStarted = true;
				this.OnTransitionStarted();
			}
			this.NumericBoundaries.HasFiredTransitionFinished = false;
			this.NumericBoundaries.UseNumericBoundaries = true;
			if (this.UseLeftBoundary)
			{
				this.NumericBoundaries.UseLeftBoundary = true;
				if (this.NumericBoundaries.LeftBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.LeftBoundaryAnimRoutine);
				}
				this.NumericBoundaries.LeftBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.LeftTransitionRoutine(this.TransitionDuration, false));
			}
			else if (!this.UseLeftBoundary && this.NumericBoundaries.UseLeftBoundary && this.UseRightBoundary && this.RightBoundary < this.NumericBoundaries.TargetLeftBoundary)
			{
				this.NumericBoundaries.UseLeftBoundary = true;
				this.UseLeftBoundary = true;
				this.LeftBoundary = this.RightBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.x * 100f;
				if (this.NumericBoundaries.LeftBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.LeftBoundaryAnimRoutine);
				}
				this.NumericBoundaries.LeftBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.LeftTransitionRoutine(this.TransitionDuration, true));
			}
			else if (!this.UseLeftBoundary)
			{
				this.NumericBoundaries.UseLeftBoundary = false;
			}
			if (this.UseRightBoundary)
			{
				this.NumericBoundaries.UseRightBoundary = true;
				if (this.NumericBoundaries.RightBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.RightBoundaryAnimRoutine);
				}
				this.NumericBoundaries.RightBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.RightTransitionRoutine(this.TransitionDuration, false));
			}
			else if (!this.UseRightBoundary && this.NumericBoundaries.UseRightBoundary && this.UseLeftBoundary && this.LeftBoundary > this.NumericBoundaries.TargetRightBoundary)
			{
				this.NumericBoundaries.UseRightBoundary = true;
				this.UseRightBoundary = true;
				this.RightBoundary = this.LeftBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.x * 100f;
				if (this.NumericBoundaries.RightBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.RightBoundaryAnimRoutine);
				}
				this.NumericBoundaries.RightBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.RightTransitionRoutine(this.TransitionDuration, true));
			}
			else if (!this.UseRightBoundary)
			{
				this.NumericBoundaries.UseRightBoundary = false;
			}
			if (this.UseTopBoundary)
			{
				this.NumericBoundaries.UseTopBoundary = true;
				if (this.NumericBoundaries.TopBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.TopBoundaryAnimRoutine);
				}
				this.NumericBoundaries.TopBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.TopTransitionRoutine(this.TransitionDuration, false));
			}
			else if (!this.UseTopBoundary && this.NumericBoundaries.UseTopBoundary && this.UseBottomBoundary && this.BottomBoundary > this.NumericBoundaries.TargetTopBoundary)
			{
				this.NumericBoundaries.UseTopBoundary = true;
				this.UseTopBoundary = true;
				this.TopBoundary = this.BottomBoundary + this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 100f;
				if (this.NumericBoundaries.TopBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.TopBoundaryAnimRoutine);
				}
				this.NumericBoundaries.TopBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.TopTransitionRoutine(this.TransitionDuration, true));
			}
			else if (!this.UseTopBoundary)
			{
				this.NumericBoundaries.UseTopBoundary = false;
			}
			if (this.UseBottomBoundary)
			{
				this.NumericBoundaries.UseBottomBoundary = true;
				if (this.NumericBoundaries.BottomBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.BottomBoundaryAnimRoutine);
				}
				this.NumericBoundaries.BottomBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.BottomTransitionRoutine(this.TransitionDuration, false));
			}
			else if (!this.UseBottomBoundary && this.NumericBoundaries.UseBottomBoundary && this.UseTopBoundary && this.TopBoundary < this.NumericBoundaries.TargetBottomBoundary)
			{
				this.NumericBoundaries.UseBottomBoundary = true;
				this.UseBottomBoundary = true;
				this.BottomBoundary = this.TopBoundary - this.ProCamera2D.ScreenSizeInWorldCoordinates.y * 100f;
				if (this.NumericBoundaries.BottomBoundaryAnimRoutine != null)
				{
					this.NumericBoundaries.StopCoroutine(this.NumericBoundaries.BottomBoundaryAnimRoutine);
				}
				this.NumericBoundaries.BottomBoundaryAnimRoutine = this.NumericBoundaries.StartCoroutine(this.BottomTransitionRoutine(this.TransitionDuration, true));
			}
			else if (!this.UseBottomBoundary)
			{
				this.NumericBoundaries.UseBottomBoundary = false;
			}
		}

		private IEnumerator LeftTransitionRoutine(float duration, bool turnOffBoundaryAfterwards = false)
		{
			BoundariesAnimator._LeftTransitionRoutine_c__Iterator0 _LeftTransitionRoutine_c__Iterator = new BoundariesAnimator._LeftTransitionRoutine_c__Iterator0();
			_LeftTransitionRoutine_c__Iterator.duration = duration;
			_LeftTransitionRoutine_c__Iterator.turnOffBoundaryAfterwards = turnOffBoundaryAfterwards;
			_LeftTransitionRoutine_c__Iterator._this = this;
			return _LeftTransitionRoutine_c__Iterator;
		}

		private IEnumerator RightTransitionRoutine(float duration, bool turnOffBoundaryAfterwards = false)
		{
			BoundariesAnimator._RightTransitionRoutine_c__Iterator1 _RightTransitionRoutine_c__Iterator = new BoundariesAnimator._RightTransitionRoutine_c__Iterator1();
			_RightTransitionRoutine_c__Iterator.duration = duration;
			_RightTransitionRoutine_c__Iterator.turnOffBoundaryAfterwards = turnOffBoundaryAfterwards;
			_RightTransitionRoutine_c__Iterator._this = this;
			return _RightTransitionRoutine_c__Iterator;
		}

		private IEnumerator TopTransitionRoutine(float duration, bool turnOffBoundaryAfterwards = false)
		{
			BoundariesAnimator._TopTransitionRoutine_c__Iterator2 _TopTransitionRoutine_c__Iterator = new BoundariesAnimator._TopTransitionRoutine_c__Iterator2();
			_TopTransitionRoutine_c__Iterator.duration = duration;
			_TopTransitionRoutine_c__Iterator.turnOffBoundaryAfterwards = turnOffBoundaryAfterwards;
			_TopTransitionRoutine_c__Iterator._this = this;
			return _TopTransitionRoutine_c__Iterator;
		}

		private IEnumerator BottomTransitionRoutine(float duration, bool turnOffBoundaryAfterwards = false)
		{
			BoundariesAnimator._BottomTransitionRoutine_c__Iterator3 _BottomTransitionRoutine_c__Iterator = new BoundariesAnimator._BottomTransitionRoutine_c__Iterator3();
			_BottomTransitionRoutine_c__Iterator.duration = duration;
			_BottomTransitionRoutine_c__Iterator.turnOffBoundaryAfterwards = turnOffBoundaryAfterwards;
			_BottomTransitionRoutine_c__Iterator._this = this;
			return _BottomTransitionRoutine_c__Iterator;
		}
	}
}
