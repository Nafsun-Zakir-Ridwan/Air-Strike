using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelPoint : MonoBehaviour
{
	public int Id;

	public int Stage;

	public int Lv;

	public List<Image> LstCer;

	public Image StageImage;

	public GameObject LevelSelect;

	public Text Rerequired;

	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.BtnLevelOnClick));
	}

	public void LoadDataLevelPoints(int id, int stage, int lv)
	{
		this.Id = id;
		this.Stage = stage;
		this.Lv = lv;
		for (int i = 0; i < 6; i++)
		{
			if (TASData.Instance.LstLvPP[this.Id].ModeLevel[i].Certificate)
			{
				this.LstCer[i].sprite = SelectLevelPanel.Instance.Cer;
			}
			else
			{
				this.LstCer[i].sprite = SelectLevelPanel.Instance.NotCer;
			}
		}
		if (this.Lv % 5 != 0)
		{
			if (MLSManager.Instance.NumStarCollect >= TASData.Instance.LstLvPP[this.Id].StarNeed)
			{
				this.Rerequired.gameObject.SetActive(false);
			}
			else
			{
				this.Rerequired.gameObject.SetActive(true);
				this.Rerequired.text = TASData.Instance.LstLvPP[this.Id].StarNeed.ToString();
			}
			if (TASData.Instance.LstLvPP[this.Id].Unlock)
			{
				if (this.Stage == 1)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.Stage1;
				}
				else if (this.Stage == 2)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.Stage2;
				}
				else if (this.Stage == 3)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.Stage3;
				}
				else if (this.Stage == 4)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.Stage4;
				}
			}
			else if (this.Stage == 1)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.Stage1Lock;
			}
			else if (this.Stage == 2)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.Stage2Lock;
			}
			else if (this.Stage == 3)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.Stage3Lock;
			}
			else if (this.Stage == 4)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.Stage4Lock;
			}
		}
		else
		{
			this.Rerequired.gameObject.SetActive(false);
			if (TASData.Instance.LstLvPP[this.Id].Unlock)
			{
				if (this.Stage == 1)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.BossStage1;
				}
				else if (this.Stage == 2)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.BossStage2;
				}
				else if (this.Stage == 3)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.BossStage3;
				}
				else if (this.Stage == 4)
				{
					this.StageImage.sprite = SelectLevelPanel.Instance.BossStage4;
				}
			}
			else if (this.Stage == 1)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.BossStage1Lock;
			}
			else if (this.Stage == 2)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.BossStage2Lock;
			}
			else if (this.Stage == 3)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.BossStage3Lock;
			}
			else if (this.Stage == 4)
			{
				this.StageImage.sprite = SelectLevelPanel.Instance.BossStage4Lock;
			}
		}
		if (TASData.Instance.LstLvPP[this.Id].Unlock)
		{
			if (MLSManager.Instance.NumStarCollect >= TASData.Instance.LstLvPP[this.Id].StarNeed)
			{
				for (int j = 0; j < this.LstCer.Count; j++)
				{
					this.LstCer[j].gameObject.SetActive(true);
				}
			}
			else
			{
				for (int k = 0; k < this.LstCer.Count; k++)
				{
					this.LstCer[k].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			for (int l = 0; l < this.LstCer.Count; l++)
			{
				this.LstCer[l].gameObject.SetActive(false);
			}
		}
	}

	public void BtnLevelOnClick()
	{
		if (TASData.Instance.LstLvPP[this.Id].Unlock)
		{
			if (this.Id == 5 || this.Id == 10 || this.Id == 15)
			{
				if (MLSManager.Instance.NumStarCollect >= TASData.Instance.LstLvPP[this.Id].StarNeed)
				{
					TASData.Instance.LvSelect = this.Id + 1;
					PlayerPrefs.SetInt("LvSelect", this.Id + 1);
					TASData.Instance.StageSelect = this.Stage;
					PlayerPrefs.SetInt("Stage", this.Stage);
					PlayerPrefs.SetInt("Lv", this.Lv);
					SelectLevelPanel.Instance.ShowDesLv();
					if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[0].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[1].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[2].Certificate)
					{
						MLSManager.Instance.CheckMission(true);
					}
					else
					{
						MLSManager.Instance.CheckMission(false);
					}
					MLSManager.Instance.LvUnlockInfo.SetActive(true);
					MLSManager.Instance.Warnning.SetActive(false);
					MLSManager.Instance.WarnningHard.SetActive(false);
					SelectLevelPanel.Instance.LoadEffect(this.Id);
				}
				else
				{
					TASData.Instance.LvSelect = this.Id + 1;
					PlayerPrefs.SetInt("LvSelect", this.Id + 1);
					TASData.Instance.StageSelect = this.Stage;
					PlayerPrefs.SetInt("Stage", this.Stage);
					PlayerPrefs.SetInt("Lv", this.Lv);
					SelectLevelPanel.Instance.ShowDesLv();
					SelectLevelPanel.Instance.LoadEffect(this.Id);
					MLSManager.Instance.LvUnlockInfo.SetActive(false);
					MLSManager.Instance.Warnning.SetActive(true);
					MLSManager.Instance.WarnningHard.SetActive(false);
					MLSManager.Instance.ModeBar.gameObject.SetActive(false);
				}
			}
			else
			{
				SelectLevelPanel.Instance.LoadEffect(this.Id);
				MLSManager.Instance.LvUnlockInfo.SetActive(true);
				MLSManager.Instance.Warnning.SetActive(false);
				MLSManager.Instance.WarnningHard.SetActive(false);
				TASData.Instance.LvSelect = this.Id + 1;
				PlayerPrefs.SetInt("LvSelect", this.Id + 1);
				TASData.Instance.StageSelect = this.Stage;
				MLSManager.Instance.Warnning.SetActive(false);
				PlayerPrefs.SetInt("Stage", this.Stage);
				PlayerPrefs.SetInt("Lv", this.Lv);
				SelectLevelPanel.Instance.ShowDesLv();
				if (TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[0].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[1].Certificate && TASData.Instance.LstLvPP[TASData.Instance.LvSelect - 1].ModeLevel[2].Certificate)
				{
					MLSManager.Instance.CheckMission(true);
				}
				else
				{
					MLSManager.Instance.CheckMission(false);
				}
			}
		}
		else
		{
			TASData.Instance.LvSelect = this.Id + 1;
			PlayerPrefs.SetInt("LvSelect", this.Id + 1);
			TASData.Instance.StageSelect = this.Stage;
			PlayerPrefs.SetInt("Stage", this.Stage);
			PlayerPrefs.SetInt("Lv", this.Lv);
			SelectLevelPanel.Instance.ShowDesLv();
			SelectLevelPanel.Instance.LoadEffect(this.Id);
		}
		MLSManager.Instance.ChangeBG();
	}
}
