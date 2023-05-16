using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class ObjectMove : MonoBehaviour
	{
		public float Amplitude = 1f;

		public float Frequency = 1f;

		private Transform Trnsform;

		private void Awake()
		{
			this.Trnsform = base.transform;
		}

		private void LateUpdate()
		{
			this.Trnsform.position += this.Amplitude * (Mathf.Sin(6.28318548f * this.Frequency * Time.time) - Mathf.Sin(6.28318548f * this.Frequency * (Time.time - Time.deltaTime))) * Vector3.up;
		}
	}
}
