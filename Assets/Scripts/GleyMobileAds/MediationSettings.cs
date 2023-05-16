using System;

namespace GleyMobileAds
{
	[Serializable]
	public class MediationSettings
	{
		public SupportedAdvertisers advertiser;

		public string advertiserName;

		public AdTypeSettings bannerSettings;

		public AdTypeSettings interstitialSettings;

		public AdTypeSettings rewardedSettings;

		public MediationSettings(SupportedAdvertisers advertiser, AdTypeSettings bannerSettings, AdTypeSettings interstitialSettings, AdTypeSettings rewardedSettings)
		{
			this.advertiser = advertiser;
			this.advertiserName = advertiser.ToString();
			this.bannerSettings = bannerSettings;
			this.interstitialSettings = interstitialSettings;
			this.rewardedSettings = rewardedSettings;
		}

		public MediationSettings(MediationSettings settings)
		{
			this.advertiser = settings.advertiser;
			this.advertiserName = settings.advertiser.ToString();
			this.bannerSettings = settings.bannerSettings;
			this.interstitialSettings = settings.interstitialSettings;
			this.rewardedSettings = settings.rewardedSettings;
		}

		public MediationSettings(SupportedAdvertisers advertiser)
		{
			this.advertiser = advertiser;
			this.advertiserName = advertiser.ToString();
		}

		public SupportedAdvertisers GetAdvertiser()
		{
			return this.advertiser;
		}
	}
}
