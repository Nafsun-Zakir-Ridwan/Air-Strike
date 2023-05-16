using System;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
	public int TimeRecycles;

	private void OnEnable()
	{
		base.Invoke("EffOff", (float)this.TimeRecycles);
	}

	public void EffOff()
	{
		base.gameObject.SetActive(false);
	}
}
