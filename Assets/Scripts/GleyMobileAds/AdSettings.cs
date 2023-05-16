using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GleyMobileAds
{
	public class AdSettings : ScriptableObject
	{
		private sealed class _GetAdvertiserSettings_c__AnonStorey0
		{
			internal SupportedAdvertisers advertiser;

			internal bool __m__0(MediationSettings cond)
			{
				return cond.advertiser == this.advertiser;
			}
		}

		private sealed class _GetPlaftormSettings_c__AnonStorey1
		{
			internal SupportedAdvertisers advertiser;

			internal bool __m__0(AdvertiserSettings cond)
			{
				return cond.advertiser == this.advertiser;
			}
		}

		public List<MediationSettings> mediationSettings = new List<MediationSettings>();

		public List<AdvertiserSettings> advertiserSettings = new List<AdvertiserSettings>();

		public bool debugMode;

		public SupportedMediation bannerMediation;

		public SupportedMediation interstitialMediation;

		public SupportedMediation rewardedMediation;

		public string externalFileUrl = "Paste your external config file url here";

		public MediationSettings GetAdvertiserSettings(SupportedAdvertisers advertiser)
		{
			return this.mediationSettings.FirstOrDefault((MediationSettings cond) => cond.advertiser == advertiser);
		}

		public List<PlatformSettings> GetPlaftormSettings(SupportedAdvertisers advertiser)
		{
			return this.advertiserSettings.FirstOrDefault((AdvertiserSettings cond) => cond.advertiser == advertiser).platformSettings;
		}
	}
}
