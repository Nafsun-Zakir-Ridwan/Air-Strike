using System;
using System.Collections.Generic;

[Serializable]
public class LevelInfor
{
	public string Name;

	public List<TurnInfor> Turns = new List<TurnInfor>();

	public List<float> LstHp = new List<float>();

	public List<float> LstHpHard = new List<float>();

	public List<int> LstDamage = new List<int>();

	public List<int> LstStar = new List<int>();
}
