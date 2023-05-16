using GleyMobileAds;
using System;
using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
	public static AdsManager Ins;

	
	private void Awake()
	{
		if (AdsManager.Ins == null)
		{
			AdsManager.Ins = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else if (AdsManager.Ins != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnEnable()
	{
		Advertisements.Instance.Initialize();
		this.LogSceans("Update New ADS");
	}

	public void ShowBanner()
	{
		if (Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
		}
	}

	public void HideBanner()
	{
		if (Advertisements.Instance.IsBannerAvailable())
		{
			Advertisements.Instance.HideBanner();
		}
	}

	public void Full()
	{
		if (Advertisements.Instance.IsInterstitialAvailable())
		{
			Advertisements.Instance.ShowInterstitial(new UnityAction(this.InterstitialClosed));
		}
	}

	public void Video()
	{
		if (Advertisements.Instance.IsRewardVideoAvailable())
		{
			Advertisements.Instance.ShowRewardedVideo(new UnityAction<bool>(this.CompleteMethod));
		}
	}

	private void InterstitialClosed()
	{
		if (Advertisements.Instance.debug)
		{
			UnityEngine.Debug.Log("Interstitial closed -> Resume Game ");
			ScreenWriter.Write("Interstitial closed -> Resume Game ");
		}
	}

	private void CompleteMethod(bool completed)
	{
		if (Advertisements.Instance.debug)
		{
			UnityEngine.Debug.Log("Completed " + completed);
			ScreenWriter.Write("Completed " + completed);
			if (completed)
			{
			}
		}
	}

	public void LogSceans(string name)
	{
		
	}

	public void LogEvent()
	{
		
	}
}
