using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Banner : MonoBehaviour
{
    #region Fields
    private BannerView bannerView;
    #endregion

    #region UnityEngine
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        GameManager.Instance.GetBannerView(bannerView);
    }
    #endregion

    #region Func
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6565752735223045/4357496145";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6565752735223045/4357496145";
#else
        string adUnitId = "unexpected_platform";
#endif
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
    #endregion
}
