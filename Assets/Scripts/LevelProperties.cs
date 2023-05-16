using System;
using System.Collections.Generic;

[Serializable]
public class LevelProperties
{
	public int Id;

	public bool Unlock;

	public bool Passed;

	public int StarNeed;

	public int Score;

	public List<ModeLV> ModeLevel;
}
