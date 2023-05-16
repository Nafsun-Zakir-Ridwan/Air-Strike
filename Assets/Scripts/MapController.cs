using System;
using System.IO;
using UnityEngine;

public class MapController : MonoBehaviour
{
	public const string LevelPath = "Levels/";

	public static bool LoadLevel(string levelName, ref string data)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(levelName);
		if (textAsset == null)
		{
			UnityEngine.Debug.Log("Không có asset nào trùng với tên hiện tại!");
			return false;
		}
		data = textAsset.text;
		return true;
	}

	public static void SaveLevel(string levelName, string data)
	{
		string text = string.Format("{0}/{3}/{1}/{2}{4}", new object[]
		{
			Application.dataPath,
			"Levels/",
			levelName,
			"Resources",
			".txt"
		});
		MonoBehaviour.print(text);
		File.WriteAllText(text, data);
	}
}
