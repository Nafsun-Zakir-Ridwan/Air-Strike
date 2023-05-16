using GleyMobileAds;
using System;
using UnityEngine;
using UnityEngine.Events;

public class TestAds : MonoBehaviour
{
	private float buttonWidth = (float)(Screen.width / 4);

	private float buttonHeight = (float)(Screen.height / 13);

	private int nrOfButtons = 4;

	private bool showDetails;

	private bool bottom = true;

	private void Start()
	{
		if (Advertisements.Instance.UserConsentWasSet())
		{
			Advertisements.Instance.Initialize();
		}
	}

	private void OnGUI()
	{
		if (!Advertisements.Instance.UserConsentWasSet())
		{
			GUI.Label(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), "Do you prefer random ads in your app or ads relevant to you? If you choose UnityEngine.Random no personalized data will be collected. If you choose personal all data collected will be used only to serve ads relevant to you.");
			if (GUI.Button(new Rect(this.buttonWidth, (float)Screen.height - 5f * this.buttonHeight, this.buttonWidth, this.buttonHeight), "Personalized"))
			{
				Advertisements.Instance.SetUserConsent(true);
				Advertisements.Instance.Initialize();
			}
			if (GUI.Button(new Rect(2f * this.buttonWidth, (float)Screen.height - 5f * this.buttonHeight, this.buttonWidth, this.buttonHeight), "Random"))
			{
				Advertisements.Instance.SetUserConsent(false);
				Advertisements.Instance.Initialize();
			}
		}
		else
		{
			if (GUI.Button(new Rect(0f, (float)Screen.height - this.buttonHeight, this.buttonWidth, this.buttonHeight), "Show Details"))
			{
				this.showDetails = !this.showDetails;
			}
			if (GUI.Button(new Rect(this.buttonWidth, (float)Screen.height - this.buttonHeight, this.buttonWidth, this.buttonHeight), "Consent:\nTrue"))
			{
				Advertisements.Instance.SetUserConsent(true);
			}
			if (GUI.Button(new Rect(2f * this.buttonWidth, (float)Screen.height - this.buttonHeight, this.buttonWidth, this.buttonHeight), "Consent:\nFalse"))
			{
				Advertisements.Instance.SetUserConsent(false);
			}
			if (Advertisements.Instance.IsRewardVideoAvailable() && GUI.Button(new Rect(0f, 0f, this.buttonWidth, this.buttonHeight), "Show Rewarded"))
			{
				Advertisements.Instance.ShowRewardedVideo(new UnityAction<bool, string>(this.CompleteMethod));
			}
			if (Advertisements.Instance.IsInterstitialAvailable() && GUI.Button(new Rect(1f * this.buttonWidth, 0f, this.buttonWidth, this.buttonHeight), "Show Interstitial"))
			{
				Advertisements.Instance.ShowInterstitial(new UnityAction<string>(this.InterstitialClosed));
			}
			if (Advertisements.Instance.IsBannerAvailable())
			{
				if (GUI.Button(new Rect(2f * this.buttonWidth, 0f, this.buttonWidth, this.buttonHeight), "Show Banner"))
				{
					Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
				}
				if (GUI.Button(new Rect(3f * this.buttonWidth, 0f, this.buttonWidth, this.buttonHeight), "Hide Banner"))
				{
					Advertisements.Instance.HideBanner();
				}
				if (GUI.Button(new Rect(3f * this.buttonWidth, this.buttonHeight, this.buttonWidth, this.buttonHeight), "Switch Banner"))
				{
					if (this.bottom)
					{
						Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
					}
					else
					{
						Advertisements.Instance.ShowBanner(BannerPosition.TOP, BannerType.SmartBanner);
					}
					this.bottom = !this.bottom;
				}
			}
			if (this.showDetails)
			{
				int num = 0;
				UnityEngine.Debug.Log(Advertisements.Instance.GetRewardedAdvertisers().Count);
				for (int i = 0; i < Advertisements.Instance.GetRewardedAdvertisers().Count; i++)
				{
					if (Advertisements.Instance.GetRewardedAdvertisers()[i].advertiserScript.IsRewardVideoAvailable())
					{
						if (GUI.Button(new Rect((float)(num % this.nrOfButtons) * this.buttonWidth, (float)(2 + num / this.nrOfButtons) * this.buttonHeight, this.buttonWidth, this.buttonHeight), Advertisements.Instance.GetRewardedAdvertisers()[i].advertiser + " Rewarded"))
						{
							Advertisements.Instance.GetRewardedAdvertisers()[i].advertiserScript.ShowRewardVideo(new UnityAction<bool, string>(this.CompleteMethod));
						}
						num++;
					}
				}
				num = 0;
				UnityEngine.Debug.Log(Advertisements.Instance.GetInterstitialAdvertisers().Count);
				for (int j = 0; j < Advertisements.Instance.GetInterstitialAdvertisers().Count; j++)
				{
					if (Advertisements.Instance.GetInterstitialAdvertisers()[j].advertiserScript.IsInterstitialAvailable())
					{
						if (GUI.Button(new Rect((float)(num % this.nrOfButtons) * this.buttonWidth, (float)(5 + num / this.nrOfButtons) * this.buttonHeight, this.buttonWidth, this.buttonHeight), Advertisements.Instance.GetInterstitialAdvertisers()[j].advertiser + " Interstitial"))
						{
							Advertisements.Instance.GetInterstitialAdvertisers()[j].advertiserScript.ShowInterstitial(new UnityAction<string>(this.InterstitialClosed));
						}
						num++;
					}
				}
				num = 0;
				for (int k = 0; k < Advertisements.Instance.GetBannerAdvertisers().Count; k++)
				{
					if (Advertisements.Instance.GetBannerAdvertisers()[k].advertiserScript.IsBannerAvailable())
					{
						if (GUI.Button(new Rect((float)(num % this.nrOfButtons) * this.buttonWidth, (float)(8 + num / this.nrOfButtons) * this.buttonHeight, this.buttonWidth, this.buttonHeight), Advertisements.Instance.GetBannerAdvertisers()[k].advertiser + " Banner"))
						{
							Advertisements.Instance.GetBannerAdvertisers()[k].advertiserScript.ShowBanner(BannerPosition.BOTTOM, BannerType.SmartBanner);
						}
						num++;
					}
				}
			}
		}
	}

	private void InterstitialClosed(string advertiser)
	{
		if (Advertisements.Instance.debug)
		{
			UnityEngine.Debug.Log("Interstitial closed from: " + advertiser + " -> Resume Game ");
			ScreenWriter.Write("Interstitial closed from: " + advertiser + " -> Resume Game ");
		}
	}

	private void CompleteMethod(bool completed, string advertiser)
	{
		if (Advertisements.Instance.debug)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Closed rewarded from: ",
				advertiser,
				" -> Completed ",
				completed
			}));
			ScreenWriter.Write(string.Concat(new object[]
			{
				"Closed rewarded from: ",
				advertiser,
				" -> Completed ",
				completed
			}));
			if (completed)
			{
			}
		}
	}
}
