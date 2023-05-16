using DG.Tweening;
using System;
using UnityEngine;

public class Pbullet2 : PBulletCs
{
	public override void OnEnable()
	{
		base.OnEnable();
		this.Trns.localScale = new Vector3(0.5f, 0.5f);
		this.Trns.DOScale(new Vector3(1f, 1f), 0.2f);
		this.Damage = TASData.Instance.LstWingCanonDamage[TASData.Instance.LstItemPP[2].Lv];
	}

	private void Update()
	{
		base.transform.Translate(Vector3.up * this.Speed * Time.deltaTime);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		this.Trns.DOKill(false);
	}
}
