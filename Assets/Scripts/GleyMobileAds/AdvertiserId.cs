using System;

namespace GleyMobileAds
{
	[Serializable]
	public class AdvertiserId
	{
		public string id;

		public string displayName;

		public bool notRequired;

		public AdvertiserId(string displayName)
		{
			this.displayName = displayName;
			this.notRequired = false;
		}

		public AdvertiserId()
		{
			this.notRequired = true;
		}
	}
}
