using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnInfor
{
	public int TurnId;

	public TurnType TurnType;

	public float TimeCreatTurn;

	public Vector3 PathPosVector3;

	public int IdPath;

	public bool Overturned;

	public bool Join;

	public List<EnemyProperties> EProperties = new List<EnemyProperties>();

	public float Frequency;

	public IName ItemName;

	public float TurnDuration = 5f;

	public EndFlyStyle EndFlyStyle;

	public FlyStyle FlyStyle;
}
