using System;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public static CameraShake Ins;

	public Transform camTransform;

	public float shake;

	public float shakeAmount = 0.7f;

	public float decreaseFactor = 1f;

	private Vector3 originalPos;

	private void Awake()
	{
		CameraShake.Ins = this;
		if (this.camTransform == null)
		{
			this.camTransform = (base.GetComponent(typeof(Transform)) as Transform);
		}
	}

	private void OnEnable()
	{
		this.originalPos = this.camTransform.localPosition;
	}

	private void Update()
	{
		if (this.shake > 0f)
		{
			this.camTransform.localPosition = this.originalPos + UnityEngine.Random.insideUnitSphere * this.shakeAmount;
			this.shake -= Time.deltaTime * this.decreaseFactor;
		}
		else
		{
			this.shake = 0f;
			this.camTransform.localPosition = this.originalPos;
		}
	}
}
