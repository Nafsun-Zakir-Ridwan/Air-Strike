using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HSManager : MonoBehaviour
{
	private sealed class _AniBtnPlay_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal HSManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _AniBtnPlay_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.Sprites.Count)
			{
				if (this._i___1 == 0)
				{
					this._this.Sprites[this._i___1].transform.DOScale(new Vector3(1.2f, 1.2f), 0.4f).SetEase(Ease.InQuad).OnComplete(delegate
					{
						this._this.StartCoroutine(this._this.AniBtnPlay2());
					});
				}
				else
				{
					this._this.Sprites[this._i___1].transform.DOScale(new Vector3(1.2f, 1.2f), 0.4f).SetEase(Ease.InQuad);
				}
				this._current = new WaitForSeconds(0.06f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		internal void __m__0()
		{
			this._this.StartCoroutine(this._this.AniBtnPlay2());
		}
	}

	private sealed class _AniBtnPlay2_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal HSManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _AniBtnPlay2_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.Sprites.Count)
			{
				this._this.Sprites[this._i___1].transform.DOScale(new Vector3(1f, 1f), 0.4f).SetEase(Ease.OutQuad);
				this._current = new WaitForSeconds(0.06f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			if (TASData.Instance.Intro == 0)
			{
				TASManagerUI.Instance.ChangeScenes(3);
			}
			else
			{
				TASManagerUI.Instance.ChangeScenes(1);
			}
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public List<GameObject> Sprites;

	public bool IsStart;

	public Image Light;

	public Image BtnMoreLight;

	public Image BtnRateLight;

	public Transform TrnLight;

	public GameObject Bg;

	public Vector3 BgOldpos;

	private bool LoadData;

	private void OnEnable()
	{
		this.IsStart = false;
		this.Light.color = new Color(0f, 0f, 0f, 1f);
		this.Light.DOColor(new Color(0f, 0f, 0f, 0f), 1f).SetEase(Ease.Linear);
		this.BtnMoreLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnRateLight.color = new Color(1f, 1f, 1f, 0f);
		this.Eff();
		if (Advertisements.Instance && Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
		}
	}

	private void Start()
	{
		this.BgOldpos = this.Bg.transform.position;
	}

	private void Update()
	{
		this.Bg.transform.position = Vector3.MoveTowards(this.Bg.transform.position, this.BgOldpos + new Vector3(Input.acceleration.x, Input.acceleration.y), Time.deltaTime);
	}

	private void Eff()
	{
		this.TrnLight.localPosition = new Vector3(-269f, -119f);
		this.TrnLight.DOLocalMove(new Vector3(360f, 130f), 4f, false).SetEase(Ease.InOutExpo).SetDelay(2f).OnComplete(new TweenCallback(this.Eff));
	}

	public void BtnPlayOnClick()
	{
		if (!this.LoadData)
		{
			TASData.Instance.LoadData();
			this.LoadData = true;
		}
		if (!this.IsStart)
		{
			this.IsStart = true;
			for (int i = 0; i < this.Sprites.Count; i++)
			{
				this.Sprites[i].transform.localScale = new Vector3(1f, 1f);
				base.StartCoroutine(this.AniBtnPlay());
			}
		}
	}

	private IEnumerator AniBtnPlay()
	{
		HSManager._AniBtnPlay_c__Iterator0 _AniBtnPlay_c__Iterator = new HSManager._AniBtnPlay_c__Iterator0();
		_AniBtnPlay_c__Iterator._this = this;
		return _AniBtnPlay_c__Iterator;
	}

	private IEnumerator AniBtnPlay2()
	{
		HSManager._AniBtnPlay2_c__Iterator1 _AniBtnPlay2_c__Iterator = new HSManager._AniBtnPlay2_c__Iterator1();
		_AniBtnPlay2_c__Iterator._this = this;
		return _AniBtnPlay2_c__Iterator;
	}

	public void BtnMoreGameOnClick()
	{
		this.BtnMoreLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnMoreLight.DOColor(new Color(1f, 1f, 1f, 1f), 0.6f).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.BtnMoreLight.DOColor(new Color(1f, 1f, 1f, 0f), 0.6f).SetEase(Ease.Linear);
			Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
		});
	}

	public void BtnRateOnClick()
	{
		this.BtnRateLight.color = new Color(1f, 1f, 1f, 0f);
		this.BtnRateLight.DOColor(new Color(1f, 1f, 1f, 1f), 0.6f).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.BtnRateLight.DOColor(new Color(1f, 1f, 1f, 0f), 0.6f).SetEase(Ease.Linear);
            Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
        });
	}

	public void BtnSettingOnClick()
	{
		PopupManager.Instance.SettingScene.SetActive(true);
	}
}
