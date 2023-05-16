using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[Serializable]
	public class Room
	{
		public string ID = string.Empty;

		public Rect Dimensions;

		[Range(0f, 10f)]
		public float TransitionDuration;

		public EaseType TransitionEaseType;

		public bool ScaleCameraToFit;

		public bool Zoom;

		[Range(0.1f, 10f)]
		public float ZoomScale;

		public Room(Room otherRoom)
		{
			this.Dimensions = otherRoom.Dimensions;
			this.TransitionDuration = otherRoom.TransitionDuration;
			this.TransitionEaseType = otherRoom.TransitionEaseType;
			this.ScaleCameraToFit = otherRoom.ScaleCameraToFit;
			this.Zoom = otherRoom.Zoom;
			this.ZoomScale = otherRoom.ZoomScale;
		}

		public Room()
		{
		}
	}
}
