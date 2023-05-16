using System;

namespace GleyMobileAds
{
	[Serializable]
	public class AdTypeSettings
	{
		public SupportedAdTypes adType;

		public int orderAndroid;

		public int orderiOS;

		public int orderWindows;

		public int weightAndroid;

		public int weightiOS;

		public int weightWindows;

		private int percentAndroid;

		private int percentiOS;

		private int percentWindows;

		public int Order
		{
			get
			{
				return this.orderAndroid;
			}
			set
			{
				this.orderWindows = value;
				this.orderiOS = value;
				this.orderAndroid = value;
			}
		}

		public int Percent
		{
			get
			{
				return this.percentAndroid;
			}
			set
			{
				this.percentWindows = value;
				this.percentiOS = value;
				this.percentAndroid = value;
			}
		}

		public int Weight
		{
			get
			{
				return this.weightAndroid;
			}
			set
			{
				this.weightWindows = value;
				this.weightiOS = value;
				this.weightAndroid = value;
			}
		}

		public AdTypeSettings(SupportedAdTypes type)
		{
			this.adType = type;
		}

		public AdTypeSettings(AdTypeSettings settings)
		{
			this.adType = settings.adType;
			this.orderAndroid = settings.orderAndroid;
			this.orderiOS = settings.orderiOS;
			this.orderWindows = settings.orderWindows;
			this.weightAndroid = settings.weightAndroid;
			this.weightiOS = settings.weightiOS;
			this.weightWindows = settings.weightWindows;
			this.percentAndroid = settings.percentAndroid;
			this.percentiOS = settings.percentiOS;
			this.percentWindows = settings.percentWindows;
		}
	}
}
