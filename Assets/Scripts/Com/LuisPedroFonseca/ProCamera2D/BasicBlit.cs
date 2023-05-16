using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[ExecuteInEditMode]
	public class BasicBlit : MonoBehaviour
	{
		public Material CurrentMaterial;

		private void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (this.CurrentMaterial != null)
			{
				Graphics.Blit(src, dst, this.CurrentMaterial);
			}
		}
	}
}
