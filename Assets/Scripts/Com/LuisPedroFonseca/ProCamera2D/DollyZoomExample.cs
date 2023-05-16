using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	public class DollyZoomExample : MonoBehaviour
	{
		[Range(0.1f, 179.9f)]
		public float TargetFOV = 30f;

		[Range(0f, 10f)]
		public float Duration = 2f;

		public EaseType EaseType;

		[Range(-1f, 1f)]
		public float ZoomAmount = -0.2f;

		private void OnGUI()
		{
			GUI.Label(new Rect(5f, 5f, 100f, 30f), "Target FOV", new GUIStyle());
			this.TargetFOV = GUI.HorizontalSlider(new Rect(100f, 5f, 100f, 30f), this.TargetFOV, 0.1f, 179.9f);
			GUI.Label(new Rect(5f, 35f, 100f, 30f), "Duration", new GUIStyle());
			this.Duration = GUI.HorizontalSlider(new Rect(100f, 35f, 100f, 30f), this.Duration, 0f, 10f);
			GUI.Label(new Rect(5f, 65f, 100f, 30f), "Zoom Amount", new GUIStyle());
			this.ZoomAmount = GUI.HorizontalSlider(new Rect(100f, 65f, 100f, 30f), this.ZoomAmount, -1f, 1f);
			if (GUI.Button(new Rect(5f, 95f, 150f, 30f), "Dolly Zoom"))
			{
				ProCamera2D.Instance.DollyZoom(this.TargetFOV, this.Duration, this.EaseType);
				ProCamera2D.Instance.Zoom(this.ZoomAmount, this.Duration, this.EaseType);
			}
		}
	}
}
