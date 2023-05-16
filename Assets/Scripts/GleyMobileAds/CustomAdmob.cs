using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace GleyMobileAds
{
	public class CustomAdmob : MonoBehaviour, ICustomAds
	{
		private sealed class _CompleteMethodInterstitial_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal CustomAdmob _this;

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

			public _CompleteMethodInterstitial_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (this._this.OnInterstitialClosed != null)
					{
						this._this.OnInterstitialClosed();
						this._this.OnInterstitialClosed = null;
					}
					if (this._this.OnInterstitialClosedWithAdvertiser != null)
					{
						this._this.OnInterstitialClosedWithAdvertiser(SupportedAdvertisers.Admob.ToString());
						this._this.OnInterstitialClosedWithAdvertiser = null;
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

		private sealed class _CompleteMethodRewardedVideo_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal bool val;

			internal CustomAdmob _this;

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

			public _CompleteMethodRewardedVideo_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = null;
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					if (this._this.OnCompleteMethod != null)
					{
						this._this.OnCompleteMethod(this.val);
						this._this.OnCompleteMethod = null;
					}
					if (this._this.OnCompleteMethodWithAdvertiser != null)
					{
						this._this.OnCompleteMethodWithAdvertiser(this.val, SupportedAdvertisers.Admob.ToString());
						this._this.OnCompleteMethodWithAdvertiser = null;
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

		private UnityAction<bool> OnCompleteMethod;

		private UnityAction<bool, string> OnCompleteMethodWithAdvertiser;

		private UnityAction OnInterstitialClosed;

		private UnityAction<string> OnInterstitialClosedWithAdvertiser;

		private InterstitialAd interstitial;

		private BannerView banner;

		private RewardBasedVideoAd rewardedVideo;

		private BannerPosition position;

		private BannerType bannerType;

		private string rewardedVideoId;

		private string interstitialId;

		private string bannerId;

		private string consent;

		private string designedForFamilies;

		private bool directedForChildren;

		private int maxRetryCount = 10;

		private int currentRetryRewardedVideo;

		private bool debug;

		private bool initialized;

		private bool triggerCompleteMethod;

		private bool bannerLoaded;

		private static Func<PlatformSettings, bool> __f__am_cache0;

		public void InitializeAds(GDPRConsent consent, List<PlatformSettings> platformSettings)
		{
			this.debug = Advertisements.Instance.debug;
			if (!this.initialized)
			{
				if (this.debug)
				{
					UnityEngine.Debug.Log(this + " Start Initialization");
					ScreenWriter.Write(this + " Start Initialization");
				}
				PlatformSettings platformSettings2 = platformSettings.First((PlatformSettings cond) => cond.platform == SupportedPlatforms.Android);
				this.interstitialId = platformSettings2.idInterstitial.id;
				this.bannerId = platformSettings2.idBanner.id;
				this.rewardedVideoId = platformSettings2.idRewarded.id;
				if (platformSettings2.directedForChildren)
				{
					this.designedForFamilies = "true";
				}
				else
				{
					this.designedForFamilies = "false";
				}
				this.directedForChildren = platformSettings2.directedForChildren;
				MobileAds.SetiOSAppPauseOnBackground(true);
				if (this.debug)
				{
					UnityEngine.Debug.Log(this + " Banner ID: " + this.bannerId);
					ScreenWriter.Write(this + " Banner ID: " + this.bannerId);
					UnityEngine.Debug.Log(this + " Interstitial ID: " + this.interstitialId);
					ScreenWriter.Write(this + " Interstitial ID: " + this.interstitialId);
					UnityEngine.Debug.Log(this + " Rewarded Video ID: " + this.rewardedVideoId);
					ScreenWriter.Write(this + " Rewarded Video ID: " + this.rewardedVideoId);
					UnityEngine.Debug.Log(this + " Directed for children: " + this.directedForChildren);
					ScreenWriter.Write(this + " Directed for children: " + this.directedForChildren);
				}
				if (consent == GDPRConsent.Unset || consent == GDPRConsent.Accept)
				{
					this.consent = "0";
				}
				else
				{
					this.consent = "1";
				}
				this.rewardedVideo = RewardBasedVideoAd.Instance;
				this.rewardedVideo.OnAdLoaded += new EventHandler<EventArgs>(this.RewardedVideoLoaded);
				this.rewardedVideo.OnAdRewarded += new EventHandler<Reward>(this.RewardedVideoWatched);
				this.rewardedVideo.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.RewardedVideoFailed);
				this.rewardedVideo.OnAdClosed += new EventHandler<EventArgs>(this.OnAdClosed);
				this.LoadRewardedVideo();
				this.LoadInterstitial();
				this.initialized = true;
			}
		}

		public void UpdateConsent(GDPRConsent consent)
		{
			if (consent == GDPRConsent.Unset || consent == GDPRConsent.Accept)
			{
				this.consent = "0";
			}
			else
			{
				this.consent = "1";
			}
			UnityEngine.Debug.Log(this + " Update consent to " + consent);
			ScreenWriter.Write(this + " Update consent to " + consent);
		}

		public bool IsInterstitialAvailable()
		{
			return this.interstitial != null && this.interstitial.IsLoaded();
		}

		public void ShowInterstitial(UnityAction InterstitialClosed)
		{
			if (this.interstitial.IsLoaded())
			{
				this.OnInterstitialClosed = InterstitialClosed;
				this.interstitial.Show();
			}
		}

		public void ShowInterstitial(UnityAction<string> InterstitialClosed)
		{
			if (this.interstitial.IsLoaded())
			{
				this.OnInterstitialClosedWithAdvertiser = InterstitialClosed;
				this.interstitial.Show();
			}
		}

		public bool IsRewardVideoAvailable()
		{
			return this.rewardedVideo != null && this.rewardedVideo.IsLoaded();
		}

		public void ShowRewardVideo(UnityAction<bool> CompleteMethod)
		{
			if (this.IsRewardVideoAvailable())
			{
				this.OnCompleteMethod = CompleteMethod;
				this.triggerCompleteMethod = true;
				this.rewardedVideo.Show();
			}
		}

		public void ShowRewardVideo(UnityAction<bool, string> CompleteMethod)
		{
			if (this.IsRewardVideoAvailable())
			{
				this.OnCompleteMethodWithAdvertiser = CompleteMethod;
				this.triggerCompleteMethod = true;
				this.rewardedVideo.Show();
			}
		}

		public bool IsBannerAvailable()
		{
			return true;
		}

		public void ShowBanner(BannerPosition position, BannerType bannerType)
		{
			this.bannerLoaded = false;
			if (this.banner != null)
			{
				if (this.position == position && this.bannerType == bannerType)
				{
					if (this.debug)
					{
						UnityEngine.Debug.Log(this + " Show banner");
						ScreenWriter.Write(this + " Show Banner");
					}
					this.bannerLoaded = true;
					this.banner.Show();
				}
				else
				{
					this.LoadBanner(position, bannerType);
				}
			}
			else
			{
				this.LoadBanner(position, bannerType);
			}
		}

		public void HideBanner()
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Hide banner");
				ScreenWriter.Write(this + " Hide banner");
			}
			if (this.banner != null)
			{
				if (!this.bannerLoaded)
				{
					this.banner.Destroy();
				}
				else
				{
					this.banner.Hide();
				}
			}
		}

		private void LoadBanner(BannerPosition position, BannerType bannerType)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Start Loading Banner");
				ScreenWriter.Write(this + " Start Loading Banner");
			}
			if (this.banner != null)
			{
				this.banner.Destroy();
			}
			this.position = position;
			this.bannerType = bannerType;
			if (position != BannerPosition.BOTTOM)
			{
				if (position == BannerPosition.TOP)
				{
					if (bannerType == BannerType.SmartBanner)
					{
						this.banner = new BannerView(this.bannerId, AdSize.SmartBanner, AdPosition.Top);
					}
					else
					{
						this.banner = new BannerView(this.bannerId, AdSize.Banner, AdPosition.Top);
					}
				}
			}
			else if (bannerType == BannerType.SmartBanner)
			{
				this.banner = new BannerView(this.bannerId, AdSize.SmartBanner, AdPosition.Bottom);
			}
			else
			{
				this.banner = new BannerView(this.bannerId, AdSize.Banner, AdPosition.Bottom);
			}
			this.banner.OnAdLoaded += new EventHandler<EventArgs>(this.BannerLoadSucces);
			this.banner.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.BannerLoadFailed);
			AdRequest request = new AdRequest.Builder().AddExtra("npa", this.consent).AddExtra("is_designed_for_families", this.designedForFamilies).TagForChildDirectedTreatment(this.directedForChildren).Build();
			this.banner.LoadAd(request);
		}

		private void BannerLoadSucces(object sender, EventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Banner Loaded");
				ScreenWriter.Write(this + " Banner Loaded");
			}
			this.bannerLoaded = true;
		}

		private void BannerLoadFailed(object sender, AdFailedToLoadEventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Banner Failed To Load " + e.Message);
				ScreenWriter.Write(this + " Banner Failed To Load " + e.Message);
			}
			this.bannerLoaded = false;
		}

		private void LoadInterstitial()
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Start Loading Interstitial");
				ScreenWriter.Write(this + " Start Loading Interstitial");
			}
			if (this.interstitial != null)
			{
				this.interstitial.Destroy();
			}
			this.interstitial = new InterstitialAd(this.interstitialId);
			this.interstitial.OnAdLoaded += new EventHandler<EventArgs>(this.InterstitialLoaded);
			this.interstitial.OnAdClosed += new EventHandler<EventArgs>(this.InterstitialClosed);
			this.interstitial.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.InterstitialFailed);
			AdRequest request = new AdRequest.Builder().AddExtra("npa", this.consent).AddExtra("is_designed_for_families", this.designedForFamilies).TagForChildDirectedTreatment(this.directedForChildren).Build();
			this.interstitial.LoadAd(request);
		}

		private void InterstitialLoaded(object sender, EventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Interstitial Loaded");
				ScreenWriter.Write(this + " Interstitial Loaded");
			}
		}

		private void InterstitialClosed(object sender, EventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Reload Interstitial");
				ScreenWriter.Write(this + " Reload Interstitial");
			}
			this.LoadInterstitial();
			base.StartCoroutine(this.CompleteMethodInterstitial());
		}

		private IEnumerator CompleteMethodInterstitial()
		{
			CustomAdmob._CompleteMethodInterstitial_c__Iterator0 _CompleteMethodInterstitial_c__Iterator = new CustomAdmob._CompleteMethodInterstitial_c__Iterator0();
			_CompleteMethodInterstitial_c__Iterator._this = this;
			return _CompleteMethodInterstitial_c__Iterator;
		}

		private void InterstitialFailed(object sender, AdFailedToLoadEventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Interstitial Failed To Load " + e.Message);
				ScreenWriter.Write(this + " Interstitial Failed To Load " + e.Message);
			}
		}

		private void LoadRewardedVideo()
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Start Loading Rewarded Video");
				ScreenWriter.Write(this + " Start Loading Rewarded Video");
			}
			AdRequest request = new AdRequest.Builder().AddExtra("npa", this.consent).AddExtra("is_designed_for_families", this.designedForFamilies).TagForChildDirectedTreatment(this.directedForChildren).Build();
			this.rewardedVideo.LoadAd(request, this.rewardedVideoId);
		}

		private void OnAdClosed(object sender, EventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " OnAdClosed");
				ScreenWriter.Write(this + " OnAdClosed");
			}
			this.LoadRewardedVideo();
			if (this.triggerCompleteMethod)
			{
				base.StartCoroutine(this.CompleteMethodRewardedVideo(false));
			}
		}

		private IEnumerator CompleteMethodRewardedVideo(bool val)
		{
			CustomAdmob._CompleteMethodRewardedVideo_c__Iterator1 _CompleteMethodRewardedVideo_c__Iterator = new CustomAdmob._CompleteMethodRewardedVideo_c__Iterator1();
			_CompleteMethodRewardedVideo_c__Iterator.val = val;
			_CompleteMethodRewardedVideo_c__Iterator._this = this;
			return _CompleteMethodRewardedVideo_c__Iterator;
		}

		private void RewardedVideoFailed(object sender, AdFailedToLoadEventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Rewarded Video Failed " + e.Message);
				ScreenWriter.Write(this + " Rewarded Video Failed " + e.Message);
			}
			if (this.currentRetryRewardedVideo < this.maxRetryCount)
			{
				this.currentRetryRewardedVideo++;
				if (this.debug)
				{
					UnityEngine.Debug.Log(this + " RETRY " + this.currentRetryRewardedVideo);
					ScreenWriter.Write(this + " RETRY " + this.currentRetryRewardedVideo);
				}
				this.LoadRewardedVideo();
			}
		}

		private void RewardedVideoWatched(object sender, Reward e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " RewardedVideoWatched");
				ScreenWriter.Write(this + " RewardedVideoWatched");
			}
			this.triggerCompleteMethod = false;
			base.StartCoroutine(this.CompleteMethodRewardedVideo(true));
		}

		private void RewardedVideoLoaded(object sender, EventArgs e)
		{
			if (this.debug)
			{
				UnityEngine.Debug.Log(this + " Rewarded Video Loaded");
				ScreenWriter.Write(this + " Rewarded Video Loaded");
			}
			this.currentRetryRewardedVideo = 0;
		}
	}
}
