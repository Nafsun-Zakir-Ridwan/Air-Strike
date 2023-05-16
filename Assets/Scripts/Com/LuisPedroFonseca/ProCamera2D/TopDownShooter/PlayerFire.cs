using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class PlayerFire : MonoBehaviour
	{
		private sealed class _Fire_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal GameObject _bullet___1;

			internal float _angle___1;

			internal float _radians___1;

			internal Vector2 _vForce___1;

			internal PlayerFire _this;

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

			public _Fire_c__Iterator0()
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
				if (UnityEngine.Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
				{
					this._bullet___1 = this._this.BulletPool.nextThing;
					this._bullet___1.transform.position = this._this.WeaponTip.position;
					this._bullet___1.transform.rotation = this._this.Trnsform.rotation;
					this._angle___1 = this._this.Trnsform.rotation.eulerAngles.y - 90f;
					this._radians___1 = this._angle___1 * 0.0174532924f;
					this._vForce___1 = new Vector2(Mathf.Sin(this._radians___1), Mathf.Cos(this._radians___1)) * this._this.FireShakeForce;
					ProCamera2DShake.Instance.ApplyShakesTimed(new Vector2[]
					{
						this._vForce___1
					}, new Vector3[]
					{
						Vector3.zero
					}, new float[]
					{
						this._this.FireShakeDuration
					}, 0.1f, false);
					this._current = new WaitForSeconds(this._this.FireRate);
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

		public Pool BulletPool;

		public Transform WeaponTip;

		public float FireRate = 0.3f;

		public float FireShakeForce = 0.2f;

		public float FireShakeDuration = 0.05f;

		private Transform Trnsform;

		private void Awake()
		{
			this.Trnsform = base.transform;
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				base.StartCoroutine(this.Fire());
			}
		}

		private IEnumerator Fire()
		{
			PlayerFire._Fire_c__Iterator0 _Fire_c__Iterator = new PlayerFire._Fire_c__Iterator0();
			_Fire_c__Iterator._this = this;
			return _Fire_c__Iterator;
		}
	}
}
