using System;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
	public static MissionManager Instance;

	public bool AllEnemy;

	public bool AllCoin;

	public bool StillAlive;

	public bool UseItem;

	public bool Die;

	public int NumItem;

	private void Awake()
	{
		MissionManager.Instance = this;
	}

	public void LoadMission()
	{
		this.AllEnemy = true;
		this.AllCoin = false;
		this.StillAlive = true;
		this.UseItem = false;
		this.Die = false;
		for (int i = 0; i < 3; i++)
		{
			if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 4)
			{
				this.NumItem = TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].MisionCount;
			}
		}
	}

	public void CheckMissionWin()
	{
		if (!TASData.Instance.IsHard)
		{
			for (int i = 0; i < 3; i++)
			{
				if (!TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate)
				{
					if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 1)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate = MissionManager.Instance.AllEnemy;
					}
					else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 2)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate = MissionManager.Instance.StillAlive;
					}
					else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 3)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate = MissionManager.Instance.AllCoin;
					}
					else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 4)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate = MissionManager.Instance.UseItem;
					}
				}
			}
		}
		else
		{
			for (int j = 3; j < 6; j++)
			{
				if (!TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate)
				{
					if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 1)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate = MissionManager.Instance.AllEnemy;
					}
					else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 2)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate = MissionManager.Instance.StillAlive;
					}
					else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 3)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate = MissionManager.Instance.AllCoin;
					}
					else if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 4)
					{
						TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate = MissionManager.Instance.UseItem;
					}
				}
			}
		}
		if (TASData.Instance.LvSelect < 10)
		{
			TASData.Instance.LstLvPP[TASData.Instance.LvSelect].Unlock = true;
		}
		TASData.Instance.SaveDataLevel();
	}

	public void CheckUseItem()
	{
		if (!TASData.Instance.IsHard)
		{
			for (int i = 0; i < 3; i++)
			{
				if (!TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 4)
				{
					TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate = MissionManager.Instance.UseItem;
				}
			}
		}
		else
		{
			for (int j = 3; j < 6; j++)
			{
				if (!TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 4)
				{
					TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate = MissionManager.Instance.UseItem;
				}
			}
		}
		TASData.Instance.SaveDataLevel();
	}

	public void CheckMissionDie()
	{
		if (!TASData.Instance.IsHard)
		{
			for (int i = 0; i < 3; i++)
			{
				if (!TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Mision == 5)
				{
					TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[i].Certificate = MissionManager.Instance.Die;
				}
			}
		}
		else
		{
			for (int j = 3; j < 6; j++)
			{
				if (!TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Mision == 5)
				{
					TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[j].Certificate = MissionManager.Instance.Die;
				}
			}
		}
		TASData.Instance.SaveDataLevel();
	}
}
