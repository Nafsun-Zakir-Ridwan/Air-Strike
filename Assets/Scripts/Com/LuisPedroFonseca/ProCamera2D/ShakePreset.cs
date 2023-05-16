using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[Serializable]
	public struct ShakePreset
	{
		public string Name;

		public Vector3 Strength;

		public float Duration;

		public int Vibrato;

		public float Randomness;

		public float Smoothness;

		public float InitialAngle;

		public Vector3 Rotation;

		public bool IgnoreTimeScale;
	}
}
