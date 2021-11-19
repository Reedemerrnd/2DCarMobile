using Services.Ads.UnityAds;

namespace Services.Ads
{
    internal interface IAdsService
    {
        IAdsPlayer InterstitialPlayer { get; }
        IAdsPlayer RewardedPlayer { get; }
        IAdsPlayer BannerPlayer { get; }

        public void Init(UnityAdsSettings unityAdsSettings);
    }
}
