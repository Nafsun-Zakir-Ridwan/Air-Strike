using System;
using UnityEngine;

namespace GleyMobileAds
{
	public class ScreenWriter : MonoBehaviour
	{
		private static string logMessage;

		private static ScreenWriter instance;

		public static void Write(object message)
		{
			if (Advertisements.Instance.debug)
			{
				if (ScreenWriter.instance == null)
				{
					ScreenWriter.instance = new GameObject
					{
						name = "DebugMessagesHolder"
					}.AddComponent<ScreenWriter>();
					ScreenWriter.logMessage += "\nDebugMessages instance created on DebugMessagesHolder";
				}
				ScreenWriter.logMessage = ScreenWriter.logMessage + "\n" + message.ToString();
			}
		}

		private void OnGUI()
		{
			if (Advertisements.Instance.debug && ScreenWriter.logMessage != null)
			{
				GUI.Label(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), ScreenWriter.logMessage);
				if (GUI.Button(new Rect((float)(Screen.width - 100), (float)(Screen.height - 100), 100f, 100f), "Clear"))
				{
					ScreenWriter.logMessage = null;
				}
			}
		}
	}
}
