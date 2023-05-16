using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class Door : MonoBehaviour
	{
		private sealed class _MoveRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float delay;

			internal Vector3 _origPos___0;

			internal float _t___0;

			internal float duration;

			internal Vector3 newPos;

			internal Door _this;

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

			public _MoveRoutine_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(this.delay);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._origPos___0 = this._this.transform.position;
					this._t___0 = 0f;
					break;
				case 2u:
					break;
				default:
					return false;
				}
				if (this._t___0 <= 1f)
				{
					this._t___0 += Time.deltaTime / this.duration;
					this._this.transform.position = new Vector3(Utils.EaseFromTo(this._origPos___0.x, this.newPos.x, this._t___0, EaseType.EaseOut), Utils.EaseFromTo(this._origPos___0.y, this.newPos.y, this._t___0, EaseType.EaseOut), Utils.EaseFromTo(this._origPos___0.z, this.newPos.z, this._t___0, EaseType.EaseOut));
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 2;
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

		private bool _isOpen;

		public DoorDirection DoorDirection;

		public float MovementRange = 5f;

		public float AnimDuration = 1f;

		public float OpenDelay;

		public float CloseDelay;

		private Vector3 _origPos;

		private Coroutine _moveCoroutine;

		public bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
		}

		private void Awake()
		{
			this._origPos = base.transform.position;
		}

		public void OpenDoor(float openDelay = -1f)
		{
			if (openDelay == -1f)
			{
				openDelay = this.OpenDelay;
			}
			this._isOpen = true;
			switch (this.DoorDirection)
			{
			case DoorDirection.Left:
				this.Move(this._origPos - new Vector3(this.MovementRange, 0f, 0f), this.AnimDuration, openDelay);
				break;
			case DoorDirection.Right:
				this.Move(this._origPos + new Vector3(this.MovementRange, 0f, 0f), this.AnimDuration, openDelay);
				break;
			case DoorDirection.Up:
				this.Move(this._origPos + new Vector3(0f, 0f, this.MovementRange), this.AnimDuration, openDelay);
				break;
			case DoorDirection.Down:
				this.Move(this._origPos - new Vector3(0f, 0f, this.MovementRange), this.AnimDuration, openDelay);
				break;
			}
		}

		public void CloseDoor()
		{
			this._isOpen = false;
			this.Move(this._origPos, this.AnimDuration, this.CloseDelay);
		}

		private void Move(Vector3 newPos, float duration, float delay)
		{
			if (this._moveCoroutine != null)
			{
				base.StopCoroutine(this._moveCoroutine);
			}
			this._moveCoroutine = base.StartCoroutine(this.MoveRoutine(newPos, duration, delay));
		}

		private IEnumerator MoveRoutine(Vector3 newPos, float duration, float delay)
		{
			Door._MoveRoutine_c__Iterator0 _MoveRoutine_c__Iterator = new Door._MoveRoutine_c__Iterator0();
			_MoveRoutine_c__Iterator.delay = delay;
			_MoveRoutine_c__Iterator.duration = duration;
			_MoveRoutine_c__Iterator.newPos = newPos;
			_MoveRoutine_c__Iterator._this = this;
			return _MoveRoutine_c__Iterator;
		}
	}
}
