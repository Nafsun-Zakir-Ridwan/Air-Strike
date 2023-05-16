using DG.Tweening;
using System;
using UnityEngine;

public class EBullet12 : EBulletCs
{
	public override void OnEnable()
	{
		base.OnEnable();
		this.Sprite.transform.localScale = new Vector3(1f, 0.1f);
		this.Sprite.transform.DOScale(new Vector3(1f, 0.5f), 0.3f);
	}
}
