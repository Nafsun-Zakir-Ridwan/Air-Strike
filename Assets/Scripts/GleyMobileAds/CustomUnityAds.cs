using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace GleyMobileAds
{
	public class CustomUnityAds : MonoBehaviour, ICustomAds
	{
		private sealed class _ShowInterstitial_c__AnonStorey0
		{
			internal UnityAction InterstitialClosed;

			internal CustomUnityAds _this;

			internal void __m__0(ShowResult result)
			{
				if (result == ShowResult.Finished || result == ShowResult.Skipped || result == ShowResult.Failed)
				{
					if (this._this.debug)
					{
						UnityEngine.Debug.Log(this._this + "Interstitial result: " + result.ToString());
						ScreenWriter.Write(this._this + "Interstitial result: " + result.ToString());
					}
					if (this.InterstitialClosed != null)
					{
						this.InterstitialClosed();
					}
				}
			}
		}

		private sealed class _ShowInterstitial_c__AnonStorey1
		{
			internal UnityAction<string> InterstitialClosed;

			internal CustomUnityAds _this;

			internal void __m__0(ShowResult result)
			{
				if (result == ShowResult.Finished || result == ShowResult.Skipped || result == ShowResult.Failed)
				{
					if (this._this.debug)
					{
						UnityEngine.Debug.Log(this._this + "Interstitial result: " + result.ToString());
						ScreenWriter.Write(this._this + "Interstitial result: " + result.ToString());
					}
					if (this.InterstitialClosed != null)
					{
						this.InterstitialClosed(SupportedAdvertisers.Unity.ToString());
					}
				}
			}
		}

		private sealed class _ShowRewardVideo_c__AnonStorey2
		{
			internal UnityAction<bool> CompleteMethod;

			internal CustomUnityAds _this;

			internal void __m__0(ShowResult result)
			{
				if (this._this.debug)
				{
					UnityEngine.Debug.Log(this._this + "Complete method result: " + result.ToString());
					ScreenWriter.Write(this._this + "Complete method result: " + result.ToString());
				}
				if (result == ShowResult.Finished)
				{
					this.CompleteMethod(true);
				}
				else
				{
					this.CompleteMethod(false);
				}
			}
		}

		private sealed class _ShowRewardVideo_c__AnonStorey3
		{
			internal UnityAction<bool, string> CompleteMethod;

			internal CustomUnityAds _this;

			internal void __m__0(ShowResult result)
			{
				if (this._this.debug)
				{
					UnityEngine.Debug.Log(this._this + "Complete method result: " + result.ToString());
					ScreenWriter.Write(this._this + "Complete method result: " + result.ToString());
				}
				if (result == ShowResult.Finished)
				{
					this.CompleteMethod(true, SupportedAdvertisers.Unity.ToString());
				}
				else
				{
					this.CompleteMethod(false, SupportedAdvertisers.Unity.ToString());
				}
			}
		}

		private string unityAdsId;

		private string videoAdPlacement;

		private string rewardedVideoAdPlacement;

		private bool debug;

		private static Func<PlatformSettings, bool> __f__am_cache0;

		public void InitializeAds(GDPRConsent consent, List<PlatformSettings> platformSettings)
		{
			this.debug = Advertisements.Instance.debug;
			PlatformSettings platformSettings2 = platformSettings.First((PlatformSettings cond) => cond.platform == SupportedPlatforms.Android);
			this.unityAdsId = platformSettings2.appId.id;
			this.videoAdPlacement = platformSettings2.idInterstitial.id;
			this.rewardedVideoAdPlacement = platformSettings2.idRewarded.id;
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Initialization Started");
				ScreenWriter.Write(this + " Initialization Started");
				UnityEngine.Debug.Log(this + " App ID: " + this.unityAdsId);
				ScreenWriter.Write(this + " App ID: " + this.unityAdsId);
				UnityEngine.Debug.Log(this + " Interstitial Placement ID: " + this.videoAdPlacement);
				ScreenWriter.Write(this + " Interstitial Placement ID: " + this.videoAdPlacement);
				UnityEngine.Debug.Log(this + " Rewarded Video Placement ID: " + this.rewardedVideoAdPlacement);
				ScreenWriter.Write(this + " Rewarded Video Placement ID: " + this.rewardedVideoAdPlacement);
			}
			if (Advertisement.isSupported)
			{
				if (consent != GDPRConsent.Unset)
				{
					MetaData metaData = new MetaData("gdpr");
					if (consent == GDPRConsent.Accept)
					{
						metaData.Set("consent", "true");
					}
					else
					{
						metaData.Set("consent", "false");
					}
					Advertisement.SetMetaData(metaData);
				}
				Advertisement.Initialize(this.unityAdsId);
			}
			else if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Platform not supported");
				ScreenWriter.Write(this + " Platform not supported");
			}
		}

		public void UpdateConsent(GDPRConsent consent)
		{
			if (consent != GDPRConsent.Unset)
			{
				MetaData metaData = new MetaData("gdpr");
				if (consent == GDPRConsent.Accept)
				{
					metaData.Set("consent", "true");
				}
				else
				{
					metaData.Set("consent", "false");
				}
				Advertisement.SetMetaData(metaData);
			}
			UnityEngine.Debug.Log(this + " Update consent to " + consent);
			ScreenWriter.Write(this + " Update consent to " + consent);
		}

		public bool IsInterstitialAvailable()
		{
			return Advertisement.IsReady(this.videoAdPlacement);
		}

		public void ShowInterstitial(UnityAction InterstitialClosed)
		{
			if (this.IsInterstitialAvailable())
			{
				Advertisement.Show(this.videoAdPlacement, new ShowOptions
				{
					resultCallback = delegate(ShowResult result)
					{
						if (result == ShowResult.Finished || result == ShowResult.Skipped || result == ShowResult.Failed)
						{
							if (this.debug)
							{
								UnityEngine.Debug.Log(this + "Interstitial result: " + result.ToString());
								ScreenWriter.Write(this + "Interstitial result: " + result.ToString());
							}
							if (InterstitialClosed != null)
							{
								InterstitialClosed();
							}
						}
					}
				});
			}
		}

		public void ShowInterstitial(UnityAction<string> InterstitialClosed)
		{
			if (this.IsInterstitialAvailable())
			{
				Advertisement.Show(this.videoAdPlacement, new ShowOptions
				{
					resultCallback = delegate(ShowResult result)
					{
						if (result == ShowResult.Finished || result == ShowResult.Skipped || result == ShowResult.Failed)
						{
							if (this.debug)
							{
								UnityEngine.Debug.Log(this + "Interstitial result: " + result.ToString());
								ScreenWriter.Write(this + "Interstitial result: " + result.ToString());
							}
							if (InterstitialClosed != null)
							{
								InterstitialClosed(SupportedAdvertisers.Unity.ToString());
							}
						}
					}
				});
			}
		}

		public bool IsRewardVideoAvailable()
		{
			return Advertisement.IsReady(this.rewardedVideoAdPlacement);
		}

		public void ShowRewardVideo(UnityAction<bool> CompleteMethod)
		{
			Advertisement.Show(this.rewardedVideoAdPlacement, new ShowOptions
			{
				resultCallback = delegate(ShowResult result)
				{
					if (this.debug)
					{
						UnityEngine.Debug.Log(this + "Complete method result: " + result.ToString());
						ScreenWriter.Write(this + "Complete method result: " + result.ToString());
					}
					if (result == ShowResult.Finished)
					{
						CompleteMethod(true);
					}
					else
					{
						CompleteMethod(false);
					}
				}
			});
		}

		public void ShowRewardVideo(UnityAction<bool, string> CompleteMethod)
		{
			Advertisement.Show(this.rewardedVideoAdPlacement, new ShowOptions
			{
				resultCallback = delegate(ShowResult result)
				{
					if (this.debug)
					{
						UnityEngine.Debug.Log(this + "Complete method result: " + result.ToString());
						ScreenWriter.Write(this + "Complete method result: " + result.ToString());
					}
					if (result == ShowResult.Finished)
					{
						CompleteMethod(true, SupportedAdvertisers.Unity.ToString());
					}
					else
					{
						CompleteMethod(false, SupportedAdvertisers.Unity.ToString());
					}
				}
			});
		}

		public bool IsBannerAvailable()
		{
			return false;
		}

		public void ShowBanner(BannerPosition position, BannerType bannerType)
		{
		}

		public void HideBanner()
		{
		}
	}
}
