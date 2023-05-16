using System;
using System.Collections.Generic;
using UnityEngine;

public class TASData : MonoBehaviour
{
	public static TASData Instance;

	public List<LevelProperties> LstLvPP;

	public List<ItemProperties> LstItemPP;

	public int CountLevel;

	public int CountItem;

	public TextAsset MissionId;

	public TextAsset TxtMissionCount;

	public TextAsset ItemsCost;

	public TextAsset TxtHpPlayer;

	public TextAsset TxtBigGunDamage;

	public TextAsset TxtBigGunSize;

	public TextAsset TxtWingCanonDamage;

	public TextAsset TxtWingCanonSize;

	public TextAsset TxtMagnet;

	public TextAsset TxtWingMissleDamage;

	public TextAsset TxtWingMissleFirerate;

	public TextAsset TxtNuclearBomb;

	public TextAsset TxtLaser;

	public TextAsset TxtShield;

	public TextAsset StarNeed;

	public List<List<int>> Mission = new List<List<int>>();

	public List<List<int>> LstMissionCount = new List<List<int>>();

	public List<List<int>> LstItemsCost = new List<List<int>>();

	public List<int> LstHPPlayer = new List<int>();

	public List<float> LstBigGunDamage = new List<float>();

	public List<float> LstBigGunSize = new List<float>();

	public List<float> LstWingCanonDamage = new List<float>();

	public List<float> LstWingCanonSize = new List<float>();

	public List<float> LstMagnetRad = new List<float>();

	public List<float> LstWingMissleDamage = new List<float>();

	public List<float> LstWingMissleFirerate = new List<float>();

	public List<int> LstNuclearBomb = new List<int>();

	public List<float> LstLaser = new List<float>();

	public List<float> LstShield = new List<float>();

	public List<int> LstStarNeed = new List<int>();

	public List<int> LstCostBuy1 = new List<int>();

	public List<int> LstCostBuy2 = new List<int>();

	public List<int> LstCostBuy3 = new List<int>();

	public TextAsset TxtCost1;

	public TextAsset TxtCost2;

	public TextAsset TxtCost3;

	public int StageSelect;

	public int LvSelect;

	public bool IsHard;

	public int Intro;

	private void Awake()
	{
		TASData.Instance = this;
	}

	private void OnEnable()
	{
		this.Intro = PlayerPrefs.GetInt("Intro", 0);
		this.LoadStarNeed();
		this.LoadItemsCost();
		this.LoadDataItemFromText();
		this.LoadCostBuyItem();
		this.StageSelect = PlayerPrefs.GetInt("Stage", 1);
		this.LvSelect = PlayerPrefs.GetInt("LvSelect", 1);
	}

	public void LoadData()
	{
		this.LoadDataLevel();
		this.LoadDataItem();
	}

	private void Start()
	{
	}

	public void LoadStarNeed()
	{
		string[] array = this.StarNeed.text.Split(new char[]
		{
			'\n'
		});
		this.LstStarNeed = new List<int>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			string[] array3 = text.Split(new char[]
			{
				'-'
			});
			List<int> list = new List<int>();
			string[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				string s = array4[j];
				list.Add(int.Parse(s));
			}
			this.LstStarNeed.Add(list[1]);
		}
	}

	public void LoadCostBuyItem()
	{
		string[] array = this.TxtCost1.text.Split(new char[]
		{
			'\n'
		});
		this.LstCostBuy1 = new List<int>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string s = array2[i];
			this.LstCostBuy1.Add(int.Parse(s));
		}
		string[] array3 = this.TxtCost2.text.Split(new char[]
		{
			'\n'
		});
		this.LstCostBuy2 = new List<int>();
		string[] array4 = array3;
		for (int j = 0; j < array4.Length; j++)
		{
			string s2 = array4[j];
			this.LstCostBuy2.Add(int.Parse(s2));
		}
		string[] array5 = this.TxtCost3.text.Split(new char[]
		{
			'\n'
		});
		this.LstCostBuy3 = new List<int>();
		string[] array6 = array5;
		for (int k = 0; k < array6.Length; k++)
		{
			string s3 = array6[k];
			this.LstCostBuy3.Add(int.Parse(s3));
		}
	}

	public void LoadDataItemFromText()
	{
		string[] array = this.TxtHpPlayer.text.Split(new char[]
		{
			'\n'
		});
		this.LstHPPlayer = new List<int>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string s = array2[i];
			this.LstHPPlayer.Add(int.Parse(s));
		}
		string[] array3 = this.TxtBigGunDamage.text.Split(new char[]
		{
			'\n'
		});
		this.LstBigGunDamage = new List<float>();
		string[] array4 = array3;
		for (int j = 0; j < array4.Length; j++)
		{
			string s2 = array4[j];
			this.LstBigGunDamage.Add(float.Parse(s2));
		}
		string[] array5 = this.TxtWingCanonDamage.text.Split(new char[]
		{
			'\n'
		});
		this.LstWingCanonDamage = new List<float>();
		string[] array6 = array5;
		for (int k = 0; k < array6.Length; k++)
		{
			string s3 = array6[k];
			this.LstWingCanonDamage.Add(float.Parse(s3));
		}
		string[] array7 = this.TxtMagnet.text.Split(new char[]
		{
			'\n'
		});
		this.LstMagnetRad = new List<float>();
		string[] array8 = array7;
		for (int l = 0; l < array8.Length; l++)
		{
			string s4 = array8[l];
			this.LstMagnetRad.Add(float.Parse(s4));
		}
		string[] array9 = this.TxtWingMissleDamage.text.Split(new char[]
		{
			'\n'
		});
		this.LstWingMissleDamage = new List<float>();
		string[] array10 = array9;
		for (int m = 0; m < array10.Length; m++)
		{
			string s5 = array10[m];
			this.LstWingMissleDamage.Add(float.Parse(s5));
		}
		string[] array11 = this.TxtWingMissleFirerate.text.Split(new char[]
		{
			'\n'
		});
		this.LstWingMissleFirerate = new List<float>();
		string[] array12 = array11;
		for (int n = 0; n < array12.Length; n++)
		{
			string s6 = array12[n];
			this.LstWingMissleFirerate.Add(float.Parse(s6));
		}
		string[] array13 = this.TxtNuclearBomb.text.Split(new char[]
		{
			'\n'
		});
		this.LstNuclearBomb = new List<int>();
		string[] array14 = array13;
		for (int num = 0; num < array14.Length; num++)
		{
			string s7 = array14[num];
			this.LstNuclearBomb.Add(int.Parse(s7));
		}
		string[] array15 = this.TxtLaser.text.Split(new char[]
		{
			'\n'
		});
		this.LstLaser = new List<float>();
		string[] array16 = array15;
		for (int num2 = 0; num2 < array16.Length; num2++)
		{
			string s8 = array16[num2];
			this.LstLaser.Add(float.Parse(s8));
		}
		string[] array17 = this.TxtShield.text.Split(new char[]
		{
			'\n'
		});
		this.LstShield = new List<float>();
		string[] array18 = array17;
		for (int num3 = 0; num3 < array18.Length; num3++)
		{
			string s9 = array18[num3];
			this.LstShield.Add(float.Parse(s9));
		}
	}

	public void LoadItemsCost()
	{
		string[] array = this.ItemsCost.text.Split(new char[]
		{
			'\n'
		});
		this.LstItemsCost = new List<List<int>>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			string[] array3 = text.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			string[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				string s = array4[j];
				list.Add(int.Parse(s));
			}
			this.LstItemsCost.Add(list);
		}
	}

	public void LoadDataLevel()
	{
		string[] array = this.MissionId.text.Split(new char[]
		{
			'\n'
		});
		this.Mission = new List<List<int>>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			string[] array3 = text.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			string[] array4 = array3;
			for (int j = 0; j < array4.Length; j++)
			{
				string s = array4[j];
				list.Add(int.Parse(s));
			}
			this.Mission.Add(list);
		}
		string[] array5 = this.TxtMissionCount.text.Split(new char[]
		{
			'\n'
		});
		this.LstMissionCount = new List<List<int>>();
		string[] array6 = array5;
		for (int k = 0; k < array6.Length; k++)
		{
			string text2 = array6[k];
			string[] array7 = text2.Split(new char[]
			{
				','
			});
			List<int> list2 = new List<int>();
			string[] array8 = array7;
			for (int l = 0; l < array8.Length; l++)
			{
				string s2 = array8[l];
				list2.Add(int.Parse(s2));
			}
			this.LstMissionCount.Add(list2);
		}
		string @string = PlayerPrefs.GetString("LevelData");
		if (@string == string.Empty)
		{
			this.FirstInitLevels();
		}
		else if (JsonHelper.FromJson<LevelProperties>(@string).Count < this.CountLevel)
		{
			this.FirstInitLevelsUpdate();
		}
		else
		{
			this.LstLvPP = JsonHelper.FromJson<LevelProperties>(@string);
		}
		this.ChangeStarNeed();
	}

	public void ChangeStarNeed()
	{
		for (int i = 0; i < this.CountLevel; i++)
		{
			this.LstLvPP[i].StarNeed = 0;
		}
		this.SaveDataLevel();
	}

	public void FirstInitLevels()
	{
		this.LstLvPP = new List<LevelProperties>();
		for (int i = 0; i < this.CountLevel; i++)
		{
			LevelProperties levelProperties = new LevelProperties();
			levelProperties.Id = i;
			levelProperties.Unlock = false;
			levelProperties.Passed = false;
			levelProperties.StarNeed = this.LstStarNeed[i];
			levelProperties.ModeLevel = new List<ModeLV>
			{
				new ModeLV
				{
					Unlock = true,
					Certificate = false,
					Mision = this.Mission[i][0],
					MisionCount = this.LstMissionCount[i][0]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][1],
					MisionCount = this.LstMissionCount[i][1]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][2],
					MisionCount = this.LstMissionCount[i][2]
				},
				new ModeLV
				{
					Unlock = true,
					Certificate = false,
					Mision = this.Mission[i][3],
					MisionCount = this.LstMissionCount[i][3]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][4],
					MisionCount = this.LstMissionCount[i][4]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][5],
					MisionCount = this.LstMissionCount[i][5]
				}
			};
			this.LstLvPP.Add(levelProperties);
		}
		this.LstLvPP[0].Unlock = true;
		this.SaveDataLevel();
	}

	public void FirstInitLevelsUpdate()
	{
		this.LstLvPP = JsonHelper.FromJson<LevelProperties>(PlayerPrefs.GetString("LevelData"));
		for (int i = JsonHelper.FromJson<LevelProperties>(PlayerPrefs.GetString("LevelData")).Count; i < this.CountLevel; i++)
		{
			LevelProperties levelProperties = new LevelProperties();
			levelProperties.Id = i;
			levelProperties.Unlock = false;
			levelProperties.Passed = false;
			levelProperties.StarNeed = this.LstStarNeed[i];
			levelProperties.ModeLevel = new List<ModeLV>
			{
				new ModeLV
				{
					Unlock = true,
					Certificate = false,
					Mision = this.Mission[i][0],
					MisionCount = this.LstMissionCount[i][0]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][1],
					MisionCount = this.LstMissionCount[i][1]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][2],
					MisionCount = this.LstMissionCount[i][2]
				},
				new ModeLV
				{
					Unlock = true,
					Certificate = false,
					Mision = this.Mission[i][3],
					MisionCount = this.LstMissionCount[i][3]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][4],
					MisionCount = this.LstMissionCount[i][4]
				},
				new ModeLV
				{
					Unlock = false,
					Certificate = false,
					Mision = this.Mission[i][5],
					MisionCount = this.LstMissionCount[i][5]
				}
			};
			this.LstLvPP.Add(levelProperties);
		}
		if (this.LstLvPP[JsonHelper.FromJson<LevelProperties>(PlayerPrefs.GetString("LevelData")).Count - 1].Passed)
		{
			this.LstLvPP[JsonHelper.FromJson<LevelProperties>(PlayerPrefs.GetString("LevelData")).Count].Unlock = true;
		}
		this.SaveDataLevel();
	}

	[ContextMenu("SaveDataLevel")]
	public void SaveDataLevel()
	{
		string value = JsonHelper.ToJson<LevelProperties>(this.LstLvPP);
		PlayerPrefs.SetString("LevelData", value);
	}

	public void LoadDataItem()
	{
		string @string = PlayerPrefs.GetString("ItemData");
		if (@string == string.Empty)
		{
			this.FirstInitItems();
		}
		else if (JsonHelper.FromJson<ItemProperties>(@string).Count < this.CountItem)
		{
			this.FirstInitItemsUpdate();
		}
		else
		{
			this.LstItemPP = JsonHelper.FromJson<ItemProperties>(@string);
		}
	}

	public void FirstInitItems()
	{
		this.LstItemPP = new List<ItemProperties>();
		for (int i = 0; i < this.CountItem; i++)
		{
			ItemProperties itemProperties = new ItemProperties();
			itemProperties.Id = i;
			itemProperties.Lv = 0;
			itemProperties.Unlock = false;
			this.LstItemPP.Add(itemProperties);
		}
		this.LstItemPP[0].Unlock = true;
		this.LstItemPP[1].Unlock = true;
		this.SaveDataItem();
	}

	public void FirstInitItemsUpdate()
	{
		this.LstItemPP = JsonHelper.FromJson<ItemProperties>(PlayerPrefs.GetString("ItemData"));
		for (int i = JsonHelper.FromJson<ItemProperties>(PlayerPrefs.GetString("ItemData")).Count; i < this.CountItem; i++)
		{
			ItemProperties itemProperties = new ItemProperties();
			itemProperties.Id = i;
			itemProperties.Lv = 0;
			itemProperties.Unlock = false;
			this.LstItemPP.Add(itemProperties);
		}
		this.SaveDataItem();
	}

	public void SaveDataItem()
	{
		string value = JsonHelper.ToJson<ItemProperties>(this.LstItemPP);
		PlayerPrefs.SetString("ItemData", value);
	}
}
