using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.Platformer
{
	[RequireComponent(typeof(SphereCollider)), RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBehaviour
	{
		public float PlayerSpeed = 5.5f;

		public MovementAxis Axis;

		private Vector3 _targetVelocity = Vector3.zero;

		private void FixedUpdate()
		{
			MovementAxis axis = this.Axis;
			if (axis != MovementAxis.XY)
			{
				if (axis != MovementAxis.XZ)
				{
					if (axis == MovementAxis.YZ)
					{
						this._targetVelocity = new Vector3(0f, UnityEngine.Input.GetAxis("Vertical"), UnityEngine.Input.GetAxis("Horizontal"));
					}
				}
				else
				{
					this._targetVelocity = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0f, UnityEngine.Input.GetAxis("Vertical"));
				}
			}
			else
			{
				this._targetVelocity = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"), 0f);
			}
			this._targetVelocity *= this.PlayerSpeed;
			base.GetComponent<Rigidbody>().AddForce(this._targetVelocity, ForceMode.Force);
		}
	}
}
