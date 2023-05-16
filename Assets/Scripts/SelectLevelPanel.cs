using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : MonoBehaviour
{
	public static SelectLevelPanel Instance;

	public List<LevelPoint> LevelItem;

	public Sprite Cer;

	public Sprite NotCer;

	public Sprite Stage1;

	public Sprite Stage2;

	public Sprite Stage3;

	public Sprite Stage4;

	public Sprite Stage1Lock;

	public Sprite Stage2Lock;

	public Sprite Stage3Lock;

	public Sprite Stage4Lock;

	public Sprite BossStage1;

	public Sprite BossStage2;

	public Sprite BossStage3;

	public Sprite BossStage4;

	public Sprite BossStage1Lock;

	public Sprite BossStage2Lock;

	public Sprite BossStage3Lock;

	public Sprite BossStage4Lock;

	public Text TxtStageLv;

	public Text TxtNameStage;

	public GameObject Content;

	public bool LevelUnlock;

	public bool EnoughStar;

	private void Awake()
	{
		SelectLevelPanel.Instance = this;
	}

	private void OnEnable()
	{
		this.LoadLevelPoints();
		this.ShowDesLv();
	}

	private void Start()
	{
	}

	public void Navigation()
	{
		bool flag = false;
		for (int i = 0; i < TASData.Instance.LstLvPP.Count; i++)
		{
			if (flag)
			{
				break;
			}
			if (TASData.Instance.LstLvPP[i].Unlock)
			{
				for (int j = 0; j < TASData.Instance.LstLvPP[i].ModeLevel.Count; j++)
				{
					if (!TASData.Instance.LstLvPP[i].ModeLevel[j].Certificate)
					{
						this.LevelItem[i].BtnLevelOnClick();
						this.LoadEffect(i);
						this.SetPosScroll(this.LevelItem[i].transform.localPosition);
						flag = true;
					}
				}
			}
		}
	}

	public void LoadEffect(int id)
	{
		for (int i = 0; i < this.LevelItem.Count; i++)
		{
			if (i == id)
			{
				this.LevelItem[i].LevelSelect.SetActive(true);
				this.LevelItem[i].transform.localScale = new Vector3(0.7f, 0.7f);
			}
			else
			{
				this.LevelItem[i].LevelSelect.SetActive(false);
				this.LevelItem[i].transform.localScale = new Vector3(0.4f, 0.4f);
			}
		}
	}

	public void SetPosScroll(Vector2 posLevel)
	{
		Vector3 localPosition = this.Content.transform.localPosition;
		localPosition.x = -posLevel.x;
		this.Content.transform.localPosition = localPosition;
	}

	public void ScaleMaplevel()
	{
		for (int i = 0; i < this.LevelItem.Count; i++)
		{
			if (i == PlayerPrefs.GetInt("LvSelect", 1) - 1)
			{
				this.LevelItem[i].transform.localScale = new Vector3(0.7f, 0.7f);
			}
			else
			{
				this.LevelItem[i].transform.localScale = new Vector3(0.4f, 0.4f);
			}
		}
	}

	public void LoadLevelPoints()
	{
		int num = 1;
		int num2 = 1;
		for (int i = 0; i < this.LevelItem.Count; i++)
		{
			this.LevelItem[i].gameObject.SetActive(true);
			this.LevelItem[i].LoadDataLevelPoints(i, num2, num);
			if (i == PlayerPrefs.GetInt("LvSelect", 1) - 1)
			{
				this.SetPosScroll(this.LevelItem[i].transform.localPosition);
				this.LoadEffect(i);
			}
			if (num == 5)
			{
				num = 1;
				num2++;
			}
			else
			{
				num++;
			}
		}
	}

	public void ShowDesLv()
	{
		int @int = PlayerPrefs.GetInt("Stage", 1);
		int int2 = PlayerPrefs.GetInt("Lv", 1);
		int int3 = PlayerPrefs.GetInt("LvSelect", 1);
		this.TxtStageLv.text = string.Concat(new object[]
		{
			"Stage ",
			@int,
			" - Level ",
			int2,
			":"
		});
		if (@int == 1)
		{
			this.TxtNameStage.text = "EARTH";
		}
		else if (@int == 2)
		{
			this.TxtNameStage.text = "DARK OF THE MOON";
		}
		else if (@int == 3)
		{
			this.TxtNameStage.text = "SUN LIGHT";
		}
		else if (@int == 4)
		{
			this.TxtNameStage.text = "SIRIUS";
		}
		if (TASData.Instance.LstLvPP[int3 - 1].Unlock)
		{
			this.LevelUnlock = true;
			if (MLSManager.Instance.NumStarCollect >= TASData.Instance.LstLvPP[int3 - 1].StarNeed)
			{
				this.EnoughStar = true;
				MLSManager.Instance.LvUnlockInfo.SetActive(true);
				MLSManager.Instance.Warnning.SetActive(false);
				MLSManager.Instance.WarnningLevelLock.SetActive(false);
				MLSManager.Instance.WarnningHard.SetActive(false);
				MLSManager.Instance.ModeBar.gameObject.SetActive(true);
			}
			else
			{
				this.EnoughStar = false;
				MLSManager.Instance.LvUnlockInfo.SetActive(false);
				MLSManager.Instance.Warnning.SetActive(true);
				MLSManager.Instance.WarnningLevelLock.SetActive(false);
				MLSManager.Instance.WarnningHard.SetActive(false);
				MLSManager.Instance.ModeBar.gameObject.SetActive(false);
			}
		}
		else
		{
			this.LevelUnlock = false;
			MLSManager.Instance.LvUnlockInfo.SetActive(false);
			MLSManager.Instance.Warnning.SetActive(false);
			MLSManager.Instance.WarnningLevelLock.SetActive(true);
			MLSManager.Instance.WarnningHard.SetActive(false);
			MLSManager.Instance.ModeBar.gameObject.SetActive(false);
		}
	}
}
