using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
	[Serializable]
	private class Wrapper<T>
	{
		public List<T> Items;
	}

	public static List<T> FromJson<T>(string json)
	{
		JsonHelper.Wrapper<T> wrapper = JsonUtility.FromJson<JsonHelper.Wrapper<T>>(json);
		return wrapper.Items;
	}

	public static string ToJson<T>(List<T> array)
	{
		return JsonUtility.ToJson(new JsonHelper.Wrapper<T>
		{
			Items = array
		});
	}

	public static string ToJson<T>(List<T> array, bool prettyPrint)
	{
		return JsonUtility.ToJson(new JsonHelper.Wrapper<T>
		{
			Items = array
		}, prettyPrint);
	}
}
