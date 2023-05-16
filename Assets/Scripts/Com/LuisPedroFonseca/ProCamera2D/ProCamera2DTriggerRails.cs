using System;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	[HelpURL("http://www.procamera2d.com/user-guide/trigger-rails/")]
	public class ProCamera2DTriggerRails : BaseTrigger
	{
		public static string TriggerName = "Rails Trigger";

		[HideInInspector]
		public ProCamera2DRails ProCamera2DRails;

		public TriggerRailsMode Mode;

		public float TransitionDuration = 1f;

		private void Start()
		{
			if (this.ProCamera2D == null)
			{
				return;
			}
			if (this.ProCamera2DRails == null)
			{
				this.ProCamera2DRails = UnityEngine.Object.FindObjectOfType<ProCamera2DRails>();
			}
			if (this.ProCamera2DRails == null)
			{
				UnityEngine.Debug.LogWarning("Rails extension couldn't be found on ProCamera2D. Please enable it to use this trigger.");
			}
		}

		protected override void EnteredTrigger()
		{
			base.EnteredTrigger();
			if (this.Mode == TriggerRailsMode.Enable)
			{
				this.ProCamera2DRails.EnableTargets(this.TransitionDuration);
			}
			else
			{
				this.ProCamera2DRails.DisableTargets(this.TransitionDuration);
			}
		}
	}
}
