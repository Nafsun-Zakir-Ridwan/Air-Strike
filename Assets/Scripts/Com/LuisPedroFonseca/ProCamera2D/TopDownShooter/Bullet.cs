using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class Bullet : MonoBehaviour
	{
		public float BulletDuration = 1f;

		public float BulletSpeed = 50f;

		public float SkinWidth = 0.1f;

		public LayerMask CollisionMask;

		public float BulletDamage = 10f;

		private Transform Trnsform;

		private RaycastHit _raycastHit;

		private Vector2 _collisionPoint;

		private float _startTime;

		private bool _exploding;

		private Vector3 _lastPos;

		private void Awake()
		{
			this.Trnsform = base.transform;
		}

		private void OnEnable()
		{
			this._exploding = false;
			this._startTime = Time.time;
		}

		private void Update()
		{
			if (this._exploding)
			{
				return;
			}
			this._lastPos = this.Trnsform.position;
			this.Trnsform.Translate(Vector3.right * this.BulletSpeed * Time.deltaTime);
			if (Physics.Raycast(this._lastPos, this.Trnsform.position - this._lastPos, out this._raycastHit, (this._lastPos - this.Trnsform.position).magnitude + this.SkinWidth, this.CollisionMask))
			{
				this._collisionPoint = this._raycastHit.point;
				this.Trnsform.up = this._raycastHit.normal;
				this.Collide();
			}
			if (Time.time - this._startTime > this.BulletDuration)
			{
				this.Disable();
			}
		}

		private void Collide()
		{
			this._exploding = true;
			this.Trnsform.position = this._collisionPoint;
			this._raycastHit.collider.SendMessageUpwards("Hit", this.BulletDamage, SendMessageOptions.DontRequireReceiver);
			this.Disable();
		}

		private void Disable()
		{
			base.gameObject.SetActive(false);
		}
	}
}
