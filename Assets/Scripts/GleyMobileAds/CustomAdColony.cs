using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GleyMobileAds
{
	public class CustomAdColony : MonoBehaviour, ICustomAds
	{
		public void HideBanner()
		{
		}

		public void InitializeAds(GDPRConsent consent, List<PlatformSettings> platformSettings)
		{
		}

		public bool IsBannerAvailable()
		{
			return false;
		}

		public bool IsInterstitialAvailable()
		{
			return false;
		}

		public bool IsRewardVideoAvailable()
		{
			return false;
		}

		public void ShowBanner(BannerPosition position, BannerType type)
		{
		}

		public void ShowInterstitial(UnityAction InterstitialClosed = null)
		{
		}

		public void ShowInterstitial(UnityAction<string> InterstitialClosed)
		{
		}

		public void ShowRewardVideo(UnityAction<bool> CompleteMethod)
		{
		}

		public void ShowRewardVideo(UnityAction<bool, string> CompleteMethod)
		{
		}

		public void UpdateConsent(GDPRConsent consent)
		{
		}
	}
}
