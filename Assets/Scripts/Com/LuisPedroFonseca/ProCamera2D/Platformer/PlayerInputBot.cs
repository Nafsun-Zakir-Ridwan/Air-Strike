using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.Platformer
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerInputBot : MonoBehaviour
	{
		private sealed class _RandomInputJump_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal WaitForEndOfFrame _waitForEndOfFrame___0;

			internal PlayerInputBot _this;

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

			public _RandomInputJump_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._waitForEndOfFrame___0 = new WaitForEndOfFrame();
					break;
				case 1u:
					this._current = this._waitForEndOfFrame___0;
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this._fakeInputJump = false;
					this._current = new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1f));
					if (!this._disposing)
					{
						this._PC = 3;
					}
					return true;
				case 3u:
					break;
				default:
					return false;
				}
				this._this._fakeInputJump = true;
				this._current = this._waitForEndOfFrame___0;
				if (!this._disposing)
				{
					this._PC = 1;
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

		private sealed class _RandomInputSpeed_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal PlayerInputBot _this;

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

			public _RandomInputSpeed_c__Iterator1()
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
				this._this._fakeInputHorizontalAxis = UnityEngine.Random.Range(-1f, 1f);
				this._current = new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
				if (!this._disposing)
				{
					this._PC = 1;
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

		public Transform Body;

		public float gravity = 20f;

		public float runSpeed = 12f;

		public float acceleration = 30f;

		public float jumpHeight = 12f;

		public int jumpsAllowed = 2;

		private float currentSpeed;

		private Vector3 amountToMove;

		private int totalJumps;

		private bool _fakeInputJump;

		private float _fakeInputHorizontalAxis;

		private CharacterController _characterController;

		private void Start()
		{
			this._characterController = base.GetComponent<CharacterController>();
			base.StartCoroutine(this.RandomInputJump());
			base.StartCoroutine(this.RandomInputSpeed());
		}

		private IEnumerator RandomInputJump()
		{
			PlayerInputBot._RandomInputJump_c__Iterator0 _RandomInputJump_c__Iterator = new PlayerInputBot._RandomInputJump_c__Iterator0();
			_RandomInputJump_c__Iterator._this = this;
			return _RandomInputJump_c__Iterator;
		}

		private IEnumerator RandomInputSpeed()
		{
			PlayerInputBot._RandomInputSpeed_c__Iterator1 _RandomInputSpeed_c__Iterator = new PlayerInputBot._RandomInputSpeed_c__Iterator1();
			_RandomInputSpeed_c__Iterator._this = this;
			return _RandomInputSpeed_c__Iterator;
		}

		private void Update()
		{
			if ((this._characterController.collisionFlags & CollisionFlags.Sides) != CollisionFlags.None)
			{
				this.currentSpeed = 0f;
			}
			if ((this._characterController.collisionFlags & CollisionFlags.Below) != CollisionFlags.None)
			{
				this.amountToMove.y = -1f;
				this.totalJumps = 0;
			}
			else
			{
				this.amountToMove.y = this.amountToMove.y - this.gravity * Time.deltaTime;
			}
			if (this._fakeInputJump && this.totalJumps < this.jumpsAllowed)
			{
				this.totalJumps++;
				this.amountToMove.y = this.jumpHeight;
			}
			float target = this._fakeInputHorizontalAxis * this.runSpeed;
			this.currentSpeed = this.IncrementTowards(this.currentSpeed, target, this.acceleration);
			if (base.transform.position.z != 0f)
			{
				this.amountToMove.z = -base.transform.position.z;
			}
			this.amountToMove.x = this.currentSpeed;
			if (this.amountToMove.x != 0f)
			{
				this.Body.localScale = new Vector2(Mathf.Sign(this.amountToMove.x), 1f);
			}
			this._characterController.Move(this.amountToMove * Time.deltaTime);
		}

		private float IncrementTowards(float n, float target, float a)
		{
			if (n == target)
			{
				return n;
			}
			float num = Mathf.Sign(target - n);
			n += a * Time.deltaTime * num;
			return (num != Mathf.Sign(target - n)) ? target : n;
		}
	}
}
