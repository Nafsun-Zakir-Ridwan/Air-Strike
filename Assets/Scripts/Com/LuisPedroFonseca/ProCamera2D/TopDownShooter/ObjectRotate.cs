using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class ObjectRotate : MonoBehaviour
	{
		public Vector3 Rotation = Vector3.one;

		private Transform Trnsform;

		private void Awake()
		{
			this.Trnsform = base.transform;
		}

		private void LateUpdate()
		{
			this.Trnsform.Rotate(this.Rotation);
		}
	}
}
