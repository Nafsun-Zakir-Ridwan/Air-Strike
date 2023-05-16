using GleyMobileAds;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Advertisements : MonoBehaviour
{
	private sealed class _ShowInterstitial_c__AnonStorey1
	{
		internal SupportedAdvertisers advertiser;

		internal bool __m__0(Advertiser cond)
		{
			return cond.advertiser == this.advertiser;
		}
	}

	private sealed class _ShowRewardedVideo_c__AnonStorey2
	{
		internal SupportedAdvertisers advertiser;

		internal bool __m__0(Advertiser cond)
		{
			return cond.advertiser == this.advertiser;
		}
	}

	private sealed class _LoadFile_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string url;

		internal WWW _www___0;

		internal Advertisements _this;

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

		public _LoadFile_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log(this.url);
				this._www___0 = new WWW(this.url);
				this._current = this._www___0;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (string.IsNullOrEmpty(this._www___0.error))
				{
					try
					{
						string message = this._www___0.text.Trim();
						UnityEngine.Debug.Log(message);
						AdOrder adOrder = JsonUtility.FromJson<AdOrder>(this._www___0.text);
						this._this.UpdateSettings(adOrder);
					}
					catch
					{
						if (this._this.debug)
						{
							UnityEngine.Debug.LogWarning("File was not in correct format");
							ScreenWriter.Write("File was not in correct format");
						}
					}
				}
				else if (this._this.debug)
				{
					UnityEngine.Debug.LogWarning("Could not download config file " + this._www___0.error);
					ScreenWriter.Write("Could not download config file " + this._www___0.error);
				}
				this._PC = -1;
				break;
			}
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

	private const string userConsent = "UserConsent";

	private static Advertisements instance;

	private static bool initialized;

	private static GameObject go;

	private List<Advertiser> allAdvertisers = new List<Advertiser>();

	private List<Advertiser> bannerAdvertisers = new List<Advertiser>();

	private List<Advertiser> interstitialAdvertisers = new List<Advertiser>();

	private List<Advertiser> rewardedAdvertisers = new List<Advertiser>();

	private SupportedMediation bannerMediation;

	private SupportedMediation interstitialMediation;

	private SupportedMediation rewardedMediation;

	internal bool debug;

	internal AdSettings adSettings;

	private static Func<AdvertiserSettings, bool> __f__am_cache0;

	private static Func<AdvertiserSettings, bool> __f__am_cache1;

	private static Func<AdvertiserSettings, bool> __f__am_cache2;

	private static Func<AdvertiserSettings, bool> __f__am_cache3;

	private static Func<AdvertiserSettings, bool> __f__am_cache4;

	private static Func<AdvertiserSettings, bool> __f__am_cache5;

	private static Func<Advertiser, int> __f__am_cache6;

	private static Func<Advertiser, int> __f__am_cache7;

	private static Func<Advertiser, int> __f__am_cache8;

	private static Func<Advertiser, int> __f__am_cache9;

	private static Func<Advertiser, int> __f__am_cacheA;

	private static Func<Advertiser, int> __f__am_cacheB;

	public static Advertisements Instance
	{
		get
		{
			if (Advertisements.instance == null)
			{
				Advertisements.go = new GameObject();
				Advertisements.go.name = "MobieAdsScripts";
				UnityEngine.Object.DontDestroyOnLoad(Advertisements.go);
				Advertisements.instance = Advertisements.go.AddComponent<Advertisements>();
			}
			return Advertisements.instance;
		}
	}

	public void SetUserConsent(bool accept)
	{
		if (accept)
		{
			PlayerPrefs.SetInt("UserConsent", 1);
		}
		else
		{
			PlayerPrefs.SetInt("UserConsent", 2);
		}
		if (Advertisements.initialized)
		{
			this.UpdateUserConsent();
		}
	}

	public GDPRConsent GetConsent()
	{
		if (!this.UserConsentWasSet())
		{
			return GDPRConsent.Unset;
		}
		return (GDPRConsent)PlayerPrefs.GetInt("UserConsent");
	}

	public bool UserConsentWasSet()
	{
		return PlayerPrefs.HasKey("UserConsent");
	}

	public void Initialize()
	{
		if (!Advertisements.initialized)
		{
			this.adSettings = Resources.Load<AdSettings>("AdSettingsData");
			if (this.adSettings == null)
			{
				UnityEngine.Debug.LogError("Gley Ads Plugin is not properly configured. Go to Window->Gley->Ads to set up the plugin. See the documentation");
				return;
			}
			this.bannerMediation = this.adSettings.bannerMediation;
			this.interstitialMediation = this.adSettings.interstitialMediation;
			this.rewardedMediation = this.adSettings.rewardedMediation;
			this.debug = this.adSettings.debugMode;
			Advertisements.initialized = true;
			if (this.adSettings.advertiserSettings.First((AdvertiserSettings cond) => cond.advertiser == SupportedAdvertisers.Admob).useSDK)
			{
				this.allAdvertisers.Add(new Advertiser(Advertisements.go.AddComponent<CustomAdmob>(), this.adSettings.GetAdvertiserSettings(SupportedAdvertisers.Admob), this.adSettings.GetPlaftormSettings(SupportedAdvertisers.Admob)));
			}
			if (this.adSettings.advertiserSettings.First((AdvertiserSettings cond) => cond.advertiser == SupportedAdvertisers.Vungle).useSDK)
			{
				this.allAdvertisers.Add(new Advertiser(Advertisements.go.AddComponent<CustomVungle>(), this.adSettings.GetAdvertiserSettings(SupportedAdvertisers.Vungle), this.adSettings.GetPlaftormSettings(SupportedAdvertisers.Vungle)));
			}
			if (this.adSettings.advertiserSettings.First((AdvertiserSettings cond) => cond.advertiser == SupportedAdvertisers.AdColony).useSDK)
			{
				this.allAdvertisers.Add(new Advertiser(Advertisements.go.AddComponent<CustomAdColony>(), this.adSettings.GetAdvertiserSettings(SupportedAdvertisers.AdColony), this.adSettings.GetPlaftormSettings(SupportedAdvertisers.AdColony)));
			}
			if (this.adSettings.advertiserSettings.First((AdvertiserSettings cond) => cond.advertiser == SupportedAdvertisers.Chartboost).useSDK)
			{
				this.allAdvertisers.Add(new Advertiser(Advertisements.go.AddComponent<CustomChartboost>(), this.adSettings.GetAdvertiserSettings(SupportedAdvertisers.Chartboost), this.adSettings.GetPlaftormSettings(SupportedAdvertisers.Chartboost)));
			}
			if (this.adSettings.advertiserSettings.First((AdvertiserSettings cond) => cond.advertiser == SupportedAdvertisers.Unity).useSDK)
			{
				this.allAdvertisers.Add(new Advertiser(Advertisements.go.AddComponent<CustomUnityAds>(), this.adSettings.GetAdvertiserSettings(SupportedAdvertisers.Unity), this.adSettings.GetPlaftormSettings(SupportedAdvertisers.Unity)));
			}
			if (this.adSettings.advertiserSettings.First((AdvertiserSettings cond) => cond.advertiser == SupportedAdvertisers.Heyzap).useSDK)
			{
				this.allAdvertisers.Add(new Advertiser(Advertisements.go.AddComponent<CustomHeyzap>(), this.adSettings.GetAdvertiserSettings(SupportedAdvertisers.Heyzap), this.adSettings.GetPlaftormSettings(SupportedAdvertisers.Heyzap)));
			}
			if (this.debug)
			{
				ScreenWriter.Write("User GDPR consent is set to: " + this.GetConsent());
			}
			for (int i = 0; i < this.allAdvertisers.Count; i++)
			{
				this.allAdvertisers[i].advertiserScript.InitializeAds(this.GetConsent(), this.allAdvertisers[i].platformSettings);
			}
			this.ApplySettings();
			this.LoadFile();
		}
	}

	private void UpdateUserConsent()
	{
		for (int i = 0; i < this.allAdvertisers.Count; i++)
		{
			this.allAdvertisers[i].advertiserScript.UpdateConsent(this.GetConsent());
		}
	}

	public void ShowInterstitial(UnityAction InterstitialClosed = null)
	{
		ICustomAds interstitialAdvertiser = this.GetInterstitialAdvertiser();
		if (interstitialAdvertiser != null)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Interstitial loaded from " + interstitialAdvertiser);
				ScreenWriter.Write("Interstitial loaded from " + interstitialAdvertiser);
			}
			interstitialAdvertiser.ShowInterstitial(InterstitialClosed);
		}
	}

	public void ShowInterstitial(UnityAction<string> InterstitialClosed)
	{
		ICustomAds interstitialAdvertiser = this.GetInterstitialAdvertiser();
		if (interstitialAdvertiser != null)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Interstitial loaded from " + interstitialAdvertiser);
				ScreenWriter.Write("Interstitial loaded from " + interstitialAdvertiser);
			}
			interstitialAdvertiser.ShowInterstitial(InterstitialClosed);
		}
	}

	public void ShowInterstitial(SupportedAdvertisers advertiser, UnityAction InterstitialClosed = null)
	{
		Advertiser advertiser2 = this.GetInterstitialAdvertisers().First((Advertiser cond) => cond.advertiser == advertiser);
		if (advertiser2.advertiserScript.IsInterstitialAvailable())
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Interstitial from " + advertiser + " is available");
				ScreenWriter.Write("Interstitial from " + advertiser + " is available");
			}
			advertiser2.advertiserScript.ShowInterstitial(InterstitialClosed);
		}
		else
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Interstitial from " + advertiser + " is NOT available");
				ScreenWriter.Write("Interstitial from " + advertiser + " is NOT available");
			}
			this.ShowInterstitial(InterstitialClosed);
		}
	}

	private ICustomAds GetInterstitialAdvertiser()
	{
		if (this.interstitialMediation == SupportedMediation.OrderMediation)
		{
			return this.UseOrder(this.interstitialAdvertisers, SupportedAdTypes.Interstitial);
		}
		return this.UsePercent(this.interstitialAdvertisers, SupportedAdTypes.Interstitial);
	}

	public void ShowRewardedVideo(UnityAction<bool> CompleteMethod)
	{
		ICustomAds customAds;
		if (this.rewardedMediation == SupportedMediation.OrderMediation)
		{
			customAds = this.UseOrder(this.rewardedAdvertisers, SupportedAdTypes.Rewarded);
		}
		else
		{
			customAds = this.UsePercent(this.rewardedAdvertisers, SupportedAdTypes.Rewarded);
		}
		if (customAds != null)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Rewarded video loaded from " + customAds);
				ScreenWriter.Write("Rewarded video loaded from " + customAds);
			}
			customAds.ShowRewardVideo(CompleteMethod);
		}
	}

	public void ShowRewardedVideo(UnityAction<bool, string> CompleteMethod)
	{
		ICustomAds customAds;
		if (this.rewardedMediation == SupportedMediation.OrderMediation)
		{
			customAds = this.UseOrder(this.rewardedAdvertisers, SupportedAdTypes.Rewarded);
		}
		else
		{
			customAds = this.UsePercent(this.rewardedAdvertisers, SupportedAdTypes.Rewarded);
		}
		if (customAds != null)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Rewarded video loaded from " + customAds);
				ScreenWriter.Write("Rewarded video loaded from " + customAds);
			}
			customAds.ShowRewardVideo(CompleteMethod);
		}
	}

	public void ShowRewardedVideo(SupportedAdvertisers advertiser, UnityAction<bool> CompleteMethod)
	{
		Advertiser advertiser2 = this.GetRewardedAdvertisers().First((Advertiser cond) => cond.advertiser == advertiser);
		if (advertiser2.advertiserScript.IsRewardVideoAvailable())
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Rewarded Video from " + advertiser + " is available");
				ScreenWriter.Write("Rewarded Video from " + advertiser + " is available");
			}
			advertiser2.advertiserScript.ShowRewardVideo(CompleteMethod);
		}
		else
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Rewarded Video from " + advertiser + " is NOT available");
				ScreenWriter.Write("Rewarded Video from " + advertiser + " is NOT available");
			}
			this.ShowRewardedVideo(CompleteMethod);
		}
	}

	public void ShowBanner(BannerPosition position, BannerType bannerType = BannerType.SmartBanner)
	{
		ICustomAds customAds;
		if (this.bannerMediation == SupportedMediation.OrderMediation)
		{
			customAds = this.UseOrder(this.bannerAdvertisers, SupportedAdTypes.Banner);
		}
		else
		{
			customAds = this.UsePercent(this.bannerAdvertisers, SupportedAdTypes.Banner);
		}
		if (customAds != null)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log("Banner loaded from " + customAds);
				ScreenWriter.Write("Banner loaded from " + customAds);
			}
			customAds.ShowBanner(position, bannerType);
		}
	}

	public void HideBanner()
	{
		for (int i = 0; i < this.allAdvertisers.Count; i++)
		{
			this.allAdvertisers[i].advertiserScript.HideBanner();
		}
	}

	private ICustomAds UsePercent(List<Advertiser> advertisers, SupportedAdTypes adType)
	{
		List<Advertiser> list = new List<Advertiser>();
		List<int> list2 = new List<int>();
		int num = 0;
		for (int i = 0; i < advertisers.Count; i++)
		{
			if (adType != SupportedAdTypes.Banner)
			{
				if (adType != SupportedAdTypes.Interstitial)
				{
					if (adType == SupportedAdTypes.Rewarded)
					{
						if (advertisers[i].advertiserScript.IsRewardVideoAvailable())
						{
							list.Add(advertisers[i]);
							num += advertisers[i].mediationSettings.rewardedSettings.Weight;
							list2.Add(num);
						}
					}
				}
				else if (advertisers[i].advertiserScript.IsInterstitialAvailable())
				{
					list.Add(advertisers[i]);
					num += advertisers[i].mediationSettings.interstitialSettings.Weight;
					list2.Add(num);
				}
			}
			else if (advertisers[i].advertiserScript.IsBannerAvailable())
			{
				list.Add(advertisers[i]);
				num += advertisers[i].mediationSettings.bannerSettings.Weight;
				list2.Add(num);
			}
		}
		int num2 = UnityEngine.Random.Range(0, num);
		if (this.debug)
		{
			for (int j = 0; j < list.Count; j++)
			{
				ScreenWriter.Write(list[j].advertiser + " weight " + list2[j]);
				UnityEngine.Debug.Log(list[j].advertiser + " weight " + list2[j]);
			}
		}
		for (int k = 0; k < list2.Count; k++)
		{
			if (num2 < list2[k])
			{
				if (this.debug)
				{
					ScreenWriter.Write(string.Concat(new object[]
					{
						"SHOW AD FROM: ",
						list[k].advertiser,
						" weight ",
						num2
					}));
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						"SHOW AD FROM: ",
						list[k].advertiser,
						" weight ",
						num2
					}));
				}
				return list[k].advertiserScript;
			}
		}
		return null;
	}

	private ICustomAds UseOrder(List<Advertiser> advertisers, SupportedAdTypes adType)
	{
		for (int i = 0; i < advertisers.Count; i++)
		{
			if (adType != SupportedAdTypes.Banner)
			{
				if (adType != SupportedAdTypes.Interstitial)
				{
					if (adType == SupportedAdTypes.Rewarded)
					{
						if (advertisers[i].advertiserScript.IsRewardVideoAvailable())
						{
							return advertisers[i].advertiserScript;
						}
					}
				}
				else if (advertisers[i].advertiserScript.IsInterstitialAvailable())
				{
					return advertisers[i].advertiserScript;
				}
			}
			else if (advertisers[i].advertiserScript.IsBannerAvailable())
			{
				return advertisers[i].advertiserScript;
			}
		}
		return null;
	}

	private void LoadFile()
	{
		if (this.adSettings.externalFileUrl != string.Empty && (this.adSettings.externalFileUrl.StartsWith("http") || this.adSettings.externalFileUrl.StartsWith("file")))
		{
			base.StartCoroutine(this.LoadFile(this.adSettings.externalFileUrl));
		}
	}

	private IEnumerator LoadFile(string url)
	{
		Advertisements._LoadFile_c__Iterator0 _LoadFile_c__Iterator = new Advertisements._LoadFile_c__Iterator0();
		_LoadFile_c__Iterator.url = url;
		_LoadFile_c__Iterator._this = this;
		return _LoadFile_c__Iterator;
	}

	private void UpdateSettings(AdOrder adOrder)
	{
		for (int i = 0; i < adOrder.advertisers.Count; i++)
		{
			for (int j = 0; j < this.allAdvertisers.Count; j++)
			{
				if (this.allAdvertisers[j].mediationSettings.GetAdvertiser() == adOrder.advertisers[i].GetAdvertiser())
				{
					this.allAdvertisers[j].mediationSettings = adOrder.advertisers[i];
				}
			}
		}
		this.ApplySettings();
	}

	private void ApplySettings()
	{
		this.bannerAdvertisers = new List<Advertiser>();
		this.interstitialAdvertisers = new List<Advertiser>();
		this.rewardedAdvertisers = new List<Advertiser>();
		for (int i = 0; i < this.allAdvertisers.Count; i++)
		{
			if (this.bannerMediation == SupportedMediation.OrderMediation)
			{
				if (this.allAdvertisers[i].mediationSettings.bannerSettings.Order != 0)
				{
					this.bannerAdvertisers.Add(this.allAdvertisers[i]);
				}
			}
			else if (this.allAdvertisers[i].mediationSettings.bannerSettings.Weight != 0)
			{
				this.bannerAdvertisers.Add(this.allAdvertisers[i]);
			}
			if (this.interstitialMediation == SupportedMediation.OrderMediation)
			{
				if (this.allAdvertisers[i].mediationSettings.interstitialSettings.Order != 0)
				{
					this.interstitialAdvertisers.Add(this.allAdvertisers[i]);
				}
			}
			else if (this.allAdvertisers[i].mediationSettings.interstitialSettings.Weight != 0)
			{
				this.interstitialAdvertisers.Add(this.allAdvertisers[i]);
			}
			if (this.rewardedMediation == SupportedMediation.OrderMediation)
			{
				if (this.allAdvertisers[i].mediationSettings.rewardedSettings.Order != 0)
				{
					this.rewardedAdvertisers.Add(this.allAdvertisers[i]);
				}
			}
			else if (this.allAdvertisers[i].mediationSettings.rewardedSettings.Weight != 0)
			{
				this.rewardedAdvertisers.Add(this.allAdvertisers[i]);
			}
		}
		if (this.bannerMediation == SupportedMediation.OrderMediation)
		{
			this.bannerAdvertisers = (from cond in this.bannerAdvertisers
			orderby cond.mediationSettings.bannerSettings.Order
			select cond).ToList<Advertiser>();
		}
		else
		{
			this.bannerAdvertisers = (from cond in this.bannerAdvertisers
			orderby cond.mediationSettings.bannerSettings.Weight descending
			select cond).ToList<Advertiser>();
		}
		if (this.interstitialMediation == SupportedMediation.OrderMediation)
		{
			this.interstitialAdvertisers = (from cond in this.interstitialAdvertisers
			orderby cond.mediationSettings.interstitialSettings.Order
			select cond).ToList<Advertiser>();
		}
		else
		{
			this.interstitialAdvertisers = (from cond in this.interstitialAdvertisers
			orderby cond.mediationSettings.interstitialSettings.Weight descending
			select cond).ToList<Advertiser>();
		}
		if (this.rewardedMediation == SupportedMediation.OrderMediation)
		{
			this.rewardedAdvertisers = (from cond in this.rewardedAdvertisers
			orderby cond.mediationSettings.rewardedSettings.Order
			select cond).ToList<Advertiser>();
		}
		else
		{
			this.rewardedAdvertisers = (from cond in this.rewardedAdvertisers
			orderby cond.mediationSettings.rewardedSettings.Weight descending
			select cond).ToList<Advertiser>();
		}
	}

	public bool IsRewardVideoAvailable()
	{
		for (int i = 0; i < this.rewardedAdvertisers.Count; i++)
		{
			if (this.rewardedAdvertisers[i].advertiserScript.IsRewardVideoAvailable())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsInterstitialAvailable()
	{
		for (int i = 0; i < this.interstitialAdvertisers.Count; i++)
		{
			if (this.interstitialAdvertisers[i].advertiserScript.IsInterstitialAvailable())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsBannerAvailable()
	{
		for (int i = 0; i < this.bannerAdvertisers.Count; i++)
		{
			if (this.bannerAdvertisers[i].advertiserScript.IsBannerAvailable())
			{
				return true;
			}
		}
		return false;
	}

	private void DisplayAdvertisers(List<Advertiser> advertisers)
	{
		for (int i = 0; i < advertisers.Count; i++)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				advertisers[i].advertiser,
				" banner order ",
				advertisers[i].mediationSettings.bannerSettings.Order,
				" interstitial order ",
				advertisers[i].mediationSettings.interstitialSettings.Order,
				" rewarded order ",
				advertisers[i].mediationSettings.interstitialSettings.Order
			}));
		}
	}

	public List<Advertiser> GetAllAdvertisers()
	{
		return this.allAdvertisers;
	}

	public List<Advertiser> GetBannerAdvertisers()
	{
		return this.bannerAdvertisers;
	}

	public List<Advertiser> GetInterstitialAdvertisers()
	{
		return this.interstitialAdvertisers;
	}

	public List<Advertiser> GetRewardedAdvertisers()
	{
		return this.rewardedAdvertisers;
	}
}
