using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class RotateTowardsMouse : MonoBehaviour
	{
		public float Ease = 0.15f;

		private Transform Trnsform;

		private void Awake()
		{
			this.Trnsform = base.transform;
		}

		private void Update()
		{
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			Vector3 vector = Camera.main.WorldToScreenPoint(this.Trnsform.localPosition);
			Vector2 vector2 = new Vector2(mousePosition.x - vector.x, mousePosition.y - vector.y);
			float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			this.Trnsform.rotation = Quaternion.Slerp(this.Trnsform.rotation, Quaternion.Euler(0f, -num, 0f), this.Ease);
		}
	}
}
