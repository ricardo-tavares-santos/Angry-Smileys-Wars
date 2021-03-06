using UnityEngine;
using System.Collections;

namespace EasyMobile
{
    // List of all supported ad networks
    public enum AdNetwork
    {
        None,
        AdColony,
        AdMob,
        AudienceNetwork,
        Chartboost,
        Heyzap,
        IronSource,
        MoPub,
        TapJoy,
        UnityAds,
    }

    public enum BannerAdNetwork
    {
        None = AdNetwork.None,
        AdMob = AdNetwork.AdMob,
        AudienceNetwork = AdNetwork.AudienceNetwork,
        Heyzap = AdNetwork.Heyzap,
        IronSource = AdNetwork.IronSource,
        MoPub = AdNetwork.MoPub,
    }

    public enum InterstitialAdNetwork
    {
        None = AdNetwork.None,
        AdColony = AdNetwork.AdColony,
        AdMob = AdNetwork.AdMob,
        AudienceNetwork = AdNetwork.AudienceNetwork,
        Chartboost = AdNetwork.Chartboost,
        Heyzap = AdNetwork.Heyzap,
        IronSource = AdNetwork.IronSource,
        MoPub = AdNetwork.MoPub,
        TapJoy = AdNetwork.TapJoy,
        UnityAds = AdNetwork.UnityAds,
    }

    public enum RewardedAdNetwork
    {
        None = AdNetwork.None,
        AdColony = AdNetwork.AdColony,
        AdMob = AdNetwork.AdMob,
        AudienceNetwork = AdNetwork.AudienceNetwork,
        Chartboost = AdNetwork.Chartboost,
        Heyzap = AdNetwork.Heyzap,
        IronSource = AdNetwork.IronSource,
        MoPub = AdNetwork.MoPub,
        TapJoy = AdNetwork.TapJoy,
        UnityAds = AdNetwork.UnityAds,
    }
}