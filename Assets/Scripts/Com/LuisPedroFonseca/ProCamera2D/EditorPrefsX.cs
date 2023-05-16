using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D
{
	public static class EditorPrefsX
	{
		private enum ArrayType
		{
			Float,
			Int32,
			Bool,
			String,
			Vector2,
			Vector3,
			Quaternion,
			Color
		}

		private static int endianDiff1;

		private static int endianDiff2;

		private static int idx;

		private static byte[] byteBlock;

		private static Action<int[], byte[], int> __f__mg_cache0;

		private static Action<float[], byte[], int> __f__mg_cache1;

		private static Action<Vector2[], byte[], int> __f__mg_cache2;

		private static Action<Vector3[], byte[], int> __f__mg_cache3;

		private static Action<Quaternion[], byte[], int> __f__mg_cache4;

		private static Action<Color[], byte[], int> __f__mg_cache5;

		private static Action<List<int>, byte[]> __f__mg_cache6;

		private static Action<List<float>, byte[]> __f__mg_cache7;

		private static Action<List<Vector2>, byte[]> __f__mg_cache8;

		private static Action<List<Vector3>, byte[]> __f__mg_cache9;

		private static Action<List<Quaternion>, byte[]> __f__mg_cacheA;

		private static Action<List<Color>, byte[]> __f__mg_cacheB;

		public static bool SetBool(string name, bool value)
		{
			return true;
		}

		public static bool GetBool(string name)
		{
			return true;
		}

		public static bool GetBool(string name, bool defaultValue)
		{
			return true;
		}

		public static long GetLong(string key, long defaultValue)
		{
			return 0L;
		}

		public static long GetLong(string key)
		{
			return 0L;
		}

		private static void SplitLong(long input, out int lowBits, out int highBits)
		{
			lowBits = (int)((uint)input);
			highBits = (int)((uint)(input >> 32));
		}

		public static void SetLong(string key, long value)
		{
		}

		public static bool SetVector2(string key, Vector2 vector)
		{
			return EditorPrefsX.SetFloatArray(key, new float[]
			{
				vector.x,
				vector.y
			});
		}

		private static Vector2 GetVector2(string key)
		{
			float[] floatArray = EditorPrefsX.GetFloatArray(key);
			if (floatArray.Length < 2)
			{
				return Vector2.zero;
			}
			return new Vector2(floatArray[0], floatArray[1]);
		}

		public static Vector2 GetVector2(string key, Vector2 defaultValue)
		{
			return Vector2.zero;
		}

		public static bool SetVector3(string key, Vector3 vector)
		{
			return EditorPrefsX.SetFloatArray(key, new float[]
			{
				vector.x,
				vector.y,
				vector.z
			});
		}

		public static Vector3 GetVector3(string key)
		{
			float[] floatArray = EditorPrefsX.GetFloatArray(key);
			if (floatArray.Length < 3)
			{
				return Vector3.zero;
			}
			return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
		}

		public static Vector3 GetVector3(string key, Vector3 defaultValue)
		{
			return Vector3.zero;
		}

		public static bool SetQuaternion(string key, Quaternion vector)
		{
			return EditorPrefsX.SetFloatArray(key, new float[]
			{
				vector.x,
				vector.y,
				vector.z,
				vector.w
			});
		}

		public static Quaternion GetQuaternion(string key)
		{
			float[] floatArray = EditorPrefsX.GetFloatArray(key);
			if (floatArray.Length < 4)
			{
				return Quaternion.identity;
			}
			return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
		}

		public static Quaternion GetQuaternion(string key, Quaternion defaultValue)
		{
			return Quaternion.identity;
		}

		public static bool SetColor(string key, Color color)
		{
			return EditorPrefsX.SetFloatArray(key, new float[]
			{
				color.r,
				color.g,
				color.b,
				color.a
			});
		}

		public static Color GetColor(string key)
		{
			float[] floatArray = EditorPrefsX.GetFloatArray(key);
			if (floatArray.Length < 4)
			{
				return new Color(0f, 0f, 0f, 0f);
			}
			return new Color(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
		}

		public static Color GetColor(string key, Color defaultValue)
		{
			return Color.white;
		}

		public static bool SetBoolArray(string key, bool[] boolArray)
		{
			byte[] array = new byte[(boolArray.Length + 7) / 8 + 5];
			array[0] = Convert.ToByte(EditorPrefsX.ArrayType.Bool);
			int num = 1;
			int num2 = 5;
			for (int i = 0; i < boolArray.Length; i++)
			{
				if (boolArray[i])
				{
					byte[] expr_37_cp_0 = array;
					int expr_37_cp_1 = num2;
					expr_37_cp_0[expr_37_cp_1] |= (byte)num;
				}
				num <<= 1;
				if (num > 128)
				{
					num = 1;
					num2++;
				}
			}
			EditorPrefsX.Initialize();
			EditorPrefsX.ConvertInt32ToBytes(boolArray.Length, array);
			return EditorPrefsX.SaveBytes(key, array);
		}

		public static bool[] GetBoolArray(string key)
		{
			if (!PlayerPrefs.HasKey(key))
			{
				return new bool[0];
			}
			byte[] array = Convert.FromBase64String(PlayerPrefs.GetString(key));
			if (array.Length < 5)
			{
				UnityEngine.Debug.LogError("Corrupt preference file for " + key);
				return new bool[0];
			}
			if (array[0] != 2)
			{
				UnityEngine.Debug.LogError(key + " is not a boolean array");
				return new bool[0];
			}
			EditorPrefsX.Initialize();
			int num = EditorPrefsX.ConvertBytesToInt32(array);
			bool[] array2 = new bool[num];
			int num2 = 1;
			int num3 = 5;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = ((array[num3] & (byte)num2) != 0);
				num2 <<= 1;
				if (num2 > 128)
				{
					num2 = 1;
					num3++;
				}
			}
			return array2;
		}

		public static bool[] GetBoolArray(string key, bool defaultValue, int defaultSize)
		{
			return new bool[0];
		}

		public static bool SetStringArray(string key, string[] stringArray)
		{
			return true;
		}

		public static string[] GetStringArray(string key)
		{
			return new string[0];
		}

		public static string[] GetStringArray(string key, string defaultValue, int defaultSize)
		{
			return new string[0];
		}

		public static bool SetIntArray(string key, int[] intArray)
		{
			EditorPrefsX.ArrayType arg_21_2 = EditorPrefsX.ArrayType.Int32;
			int arg_21_3 = 1;
			if (EditorPrefsX.__f__mg_cache0 == null)
			{
				EditorPrefsX.__f__mg_cache0 = new Action<int[], byte[], int>(EditorPrefsX.ConvertFromInt);
			}
			return EditorPrefsX.SetValue<int[]>(key, intArray, arg_21_2, arg_21_3, EditorPrefsX.__f__mg_cache0);
		}

		public static bool SetFloatArray(string key, float[] floatArray)
		{
			EditorPrefsX.ArrayType arg_21_2 = EditorPrefsX.ArrayType.Float;
			int arg_21_3 = 1;
			if (EditorPrefsX.__f__mg_cache1 == null)
			{
				EditorPrefsX.__f__mg_cache1 = new Action<float[], byte[], int>(EditorPrefsX.ConvertFromFloat);
			}
			return EditorPrefsX.SetValue<float[]>(key, floatArray, arg_21_2, arg_21_3, EditorPrefsX.__f__mg_cache1);
		}

		public static bool SetVector2Array(string key, Vector2[] vector2Array)
		{
			EditorPrefsX.ArrayType arg_21_2 = EditorPrefsX.ArrayType.Vector2;
			int arg_21_3 = 2;
			if (EditorPrefsX.__f__mg_cache2 == null)
			{
				EditorPrefsX.__f__mg_cache2 = new Action<Vector2[], byte[], int>(EditorPrefsX.ConvertFromVector2);
			}
			return EditorPrefsX.SetValue<Vector2[]>(key, vector2Array, arg_21_2, arg_21_3, EditorPrefsX.__f__mg_cache2);
		}

		public static bool SetVector3Array(string key, Vector3[] vector3Array)
		{
			EditorPrefsX.ArrayType arg_21_2 = EditorPrefsX.ArrayType.Vector3;
			int arg_21_3 = 3;
			if (EditorPrefsX.__f__mg_cache3 == null)
			{
				EditorPrefsX.__f__mg_cache3 = new Action<Vector3[], byte[], int>(EditorPrefsX.ConvertFromVector3);
			}
			return EditorPrefsX.SetValue<Vector3[]>(key, vector3Array, arg_21_2, arg_21_3, EditorPrefsX.__f__mg_cache3);
		}

		public static bool SetQuaternionArray(string key, Quaternion[] quaternionArray)
		{
			EditorPrefsX.ArrayType arg_21_2 = EditorPrefsX.ArrayType.Quaternion;
			int arg_21_3 = 4;
			if (EditorPrefsX.__f__mg_cache4 == null)
			{
				EditorPrefsX.__f__mg_cache4 = new Action<Quaternion[], byte[], int>(EditorPrefsX.ConvertFromQuaternion);
			}
			return EditorPrefsX.SetValue<Quaternion[]>(key, quaternionArray, arg_21_2, arg_21_3, EditorPrefsX.__f__mg_cache4);
		}

		public static bool SetColorArray(string key, Color[] colorArray)
		{
			EditorPrefsX.ArrayType arg_21_2 = EditorPrefsX.ArrayType.Color;
			int arg_21_3 = 4;
			if (EditorPrefsX.__f__mg_cache5 == null)
			{
				EditorPrefsX.__f__mg_cache5 = new Action<Color[], byte[], int>(EditorPrefsX.ConvertFromColor);
			}
			return EditorPrefsX.SetValue<Color[]>(key, colorArray, arg_21_2, arg_21_3, EditorPrefsX.__f__mg_cache5);
		}

		private static bool SetValue<T>(string key, T array, EditorPrefsX.ArrayType arrayType, int vectorNumber, Action<T, byte[], int> convert) where T : IList
		{
			byte[] array2 = new byte[4 * array.Count * vectorNumber + 1];
			array2[0] = Convert.ToByte(arrayType);
			EditorPrefsX.Initialize();
			for (int i = 0; i < array.Count; i++)
			{
				convert(array, array2, i);
			}
			return EditorPrefsX.SaveBytes(key, array2);
		}

		private static void ConvertFromInt(int[] array, byte[] bytes, int i)
		{
			EditorPrefsX.ConvertInt32ToBytes(array[i], bytes);
		}

		private static void ConvertFromFloat(float[] array, byte[] bytes, int i)
		{
			EditorPrefsX.ConvertFloatToBytes(array[i], bytes);
		}

		private static void ConvertFromVector2(Vector2[] array, byte[] bytes, int i)
		{
			EditorPrefsX.ConvertFloatToBytes(array[i].x, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].y, bytes);
		}

		private static void ConvertFromVector3(Vector3[] array, byte[] bytes, int i)
		{
			EditorPrefsX.ConvertFloatToBytes(array[i].x, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].y, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].z, bytes);
		}

		private static void ConvertFromQuaternion(Quaternion[] array, byte[] bytes, int i)
		{
			EditorPrefsX.ConvertFloatToBytes(array[i].x, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].y, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].z, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].w, bytes);
		}

		private static void ConvertFromColor(Color[] array, byte[] bytes, int i)
		{
			EditorPrefsX.ConvertFloatToBytes(array[i].r, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].g, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].b, bytes);
			EditorPrefsX.ConvertFloatToBytes(array[i].a, bytes);
		}

		public static int[] GetIntArray(string key)
		{
			List<int> list = new List<int>();
			List<int> arg_27_1 = list;
			EditorPrefsX.ArrayType arg_27_2 = EditorPrefsX.ArrayType.Int32;
			int arg_27_3 = 1;
			if (EditorPrefsX.__f__mg_cache6 == null)
			{
				EditorPrefsX.__f__mg_cache6 = new Action<List<int>, byte[]>(EditorPrefsX.ConvertToInt);
			}
			EditorPrefsX.GetValue<List<int>>(key, arg_27_1, arg_27_2, arg_27_3, EditorPrefsX.__f__mg_cache6);
			return list.ToArray();
		}

		public static int[] GetIntArray(string key, int defaultValue, int defaultSize)
		{
			return new int[0];
		}

		public static float[] GetFloatArray(string key)
		{
			List<float> list = new List<float>();
			List<float> arg_27_1 = list;
			EditorPrefsX.ArrayType arg_27_2 = EditorPrefsX.ArrayType.Float;
			int arg_27_3 = 1;
			if (EditorPrefsX.__f__mg_cache7 == null)
			{
				EditorPrefsX.__f__mg_cache7 = new Action<List<float>, byte[]>(EditorPrefsX.ConvertToFloat);
			}
			EditorPrefsX.GetValue<List<float>>(key, arg_27_1, arg_27_2, arg_27_3, EditorPrefsX.__f__mg_cache7);
			return list.ToArray();
		}

		public static float[] GetFloatArray(string key, float defaultValue, int defaultSize)
		{
			return new float[0];
		}

		public static Vector2[] GetVector2Array(string key)
		{
			List<Vector2> list = new List<Vector2>();
			List<Vector2> arg_27_1 = list;
			EditorPrefsX.ArrayType arg_27_2 = EditorPrefsX.ArrayType.Vector2;
			int arg_27_3 = 2;
			if (EditorPrefsX.__f__mg_cache8 == null)
			{
				EditorPrefsX.__f__mg_cache8 = new Action<List<Vector2>, byte[]>(EditorPrefsX.ConvertToVector2);
			}
			EditorPrefsX.GetValue<List<Vector2>>(key, arg_27_1, arg_27_2, arg_27_3, EditorPrefsX.__f__mg_cache8);
			return list.ToArray();
		}

		public static Vector2[] GetVector2Array(string key, Vector2 defaultValue, int defaultSize)
		{
			return new Vector2[0];
		}

		public static Vector3[] GetVector3Array(string key)
		{
			List<Vector3> list = new List<Vector3>();
			List<Vector3> arg_27_1 = list;
			EditorPrefsX.ArrayType arg_27_2 = EditorPrefsX.ArrayType.Vector3;
			int arg_27_3 = 3;
			if (EditorPrefsX.__f__mg_cache9 == null)
			{
				EditorPrefsX.__f__mg_cache9 = new Action<List<Vector3>, byte[]>(EditorPrefsX.ConvertToVector3);
			}
			EditorPrefsX.GetValue<List<Vector3>>(key, arg_27_1, arg_27_2, arg_27_3, EditorPrefsX.__f__mg_cache9);
			return list.ToArray();
		}

		public static Vector3[] GetVector3Array(string key, Vector3 defaultValue, int defaultSize)
		{
			return new Vector3[0];
		}

		public static Quaternion[] GetQuaternionArray(string key)
		{
			List<Quaternion> list = new List<Quaternion>();
			List<Quaternion> arg_27_1 = list;
			EditorPrefsX.ArrayType arg_27_2 = EditorPrefsX.ArrayType.Quaternion;
			int arg_27_3 = 4;
			if (EditorPrefsX.__f__mg_cacheA == null)
			{
				EditorPrefsX.__f__mg_cacheA = new Action<List<Quaternion>, byte[]>(EditorPrefsX.ConvertToQuaternion);
			}
			EditorPrefsX.GetValue<List<Quaternion>>(key, arg_27_1, arg_27_2, arg_27_3, EditorPrefsX.__f__mg_cacheA);
			return list.ToArray();
		}

		public static Quaternion[] GetQuaternionArray(string key, Quaternion defaultValue, int defaultSize)
		{
			return new Quaternion[0];
		}

		public static Color[] GetColorArray(string key)
		{
			List<Color> list = new List<Color>();
			List<Color> arg_27_1 = list;
			EditorPrefsX.ArrayType arg_27_2 = EditorPrefsX.ArrayType.Color;
			int arg_27_3 = 4;
			if (EditorPrefsX.__f__mg_cacheB == null)
			{
				EditorPrefsX.__f__mg_cacheB = new Action<List<Color>, byte[]>(EditorPrefsX.ConvertToColor);
			}
			EditorPrefsX.GetValue<List<Color>>(key, arg_27_1, arg_27_2, arg_27_3, EditorPrefsX.__f__mg_cacheB);
			return list.ToArray();
		}

		public static Color[] GetColorArray(string key, Color defaultValue, int defaultSize)
		{
			return new Color[0];
		}

		private static void GetValue<T>(string key, T list, EditorPrefsX.ArrayType arrayType, int vectorNumber, Action<T, byte[]> convert) where T : IList
		{
		}

		private static void ConvertToInt(List<int> list, byte[] bytes)
		{
			list.Add(EditorPrefsX.ConvertBytesToInt32(bytes));
		}

		private static void ConvertToFloat(List<float> list, byte[] bytes)
		{
			list.Add(EditorPrefsX.ConvertBytesToFloat(bytes));
		}

		private static void ConvertToVector2(List<Vector2> list, byte[] bytes)
		{
			list.Add(new Vector2(EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes)));
		}

		private static void ConvertToVector3(List<Vector3> list, byte[] bytes)
		{
			list.Add(new Vector3(EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes)));
		}

		private static void ConvertToQuaternion(List<Quaternion> list, byte[] bytes)
		{
			list.Add(new Quaternion(EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes)));
		}

		private static void ConvertToColor(List<Color> list, byte[] bytes)
		{
			list.Add(new Color(EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes), EditorPrefsX.ConvertBytesToFloat(bytes)));
		}

		public static void ShowArrayType(string key)
		{
		}

		private static void Initialize()
		{
			if (BitConverter.IsLittleEndian)
			{
				EditorPrefsX.endianDiff1 = 0;
				EditorPrefsX.endianDiff2 = 0;
			}
			else
			{
				EditorPrefsX.endianDiff1 = 3;
				EditorPrefsX.endianDiff2 = 1;
			}
			if (EditorPrefsX.byteBlock == null)
			{
				EditorPrefsX.byteBlock = new byte[4];
			}
			EditorPrefsX.idx = 1;
		}

		private static bool SaveBytes(string key, byte[] bytes)
		{
			return true;
		}

		private static void ConvertFloatToBytes(float f, byte[] bytes)
		{
			EditorPrefsX.byteBlock = BitConverter.GetBytes(f);
			EditorPrefsX.ConvertTo4Bytes(bytes);
		}

		private static float ConvertBytesToFloat(byte[] bytes)
		{
			EditorPrefsX.ConvertFrom4Bytes(bytes);
			return BitConverter.ToSingle(EditorPrefsX.byteBlock, 0);
		}

		private static void ConvertInt32ToBytes(int i, byte[] bytes)
		{
			EditorPrefsX.byteBlock = BitConverter.GetBytes(i);
			EditorPrefsX.ConvertTo4Bytes(bytes);
		}

		private static int ConvertBytesToInt32(byte[] bytes)
		{
			EditorPrefsX.ConvertFrom4Bytes(bytes);
			return BitConverter.ToInt32(EditorPrefsX.byteBlock, 0);
		}

		private static void ConvertTo4Bytes(byte[] bytes)
		{
			bytes[EditorPrefsX.idx] = EditorPrefsX.byteBlock[EditorPrefsX.endianDiff1];
			bytes[EditorPrefsX.idx + 1] = EditorPrefsX.byteBlock[1 + EditorPrefsX.endianDiff2];
			bytes[EditorPrefsX.idx + 2] = EditorPrefsX.byteBlock[2 - EditorPrefsX.endianDiff2];
			bytes[EditorPrefsX.idx + 3] = EditorPrefsX.byteBlock[3 - EditorPrefsX.endianDiff1];
			EditorPrefsX.idx += 4;
		}

		private static void ConvertFrom4Bytes(byte[] bytes)
		{
			EditorPrefsX.byteBlock[EditorPrefsX.endianDiff1] = bytes[EditorPrefsX.idx];
			EditorPrefsX.byteBlock[1 + EditorPrefsX.endianDiff2] = bytes[EditorPrefsX.idx + 1];
			EditorPrefsX.byteBlock[2 - EditorPrefsX.endianDiff2] = bytes[EditorPrefsX.idx + 2];
			EditorPrefsX.byteBlock[3 - EditorPrefsX.endianDiff1] = bytes[EditorPrefsX.idx + 3];
			EditorPrefsX.idx += 4;
		}
	}
}
