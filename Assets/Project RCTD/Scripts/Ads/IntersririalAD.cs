using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class IntersririalAD : MonoBehaviour
{
    #region Fields
    private InterstitialAd interstitial;
    #endregion

    #region Property
    public InterstitialAd Interstitial => interstitial;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        RequestInterstitial();
    }
    private void Start()
    {
        GameManager.Instance.GetInterstitialAd(Interstitial);
    }
    #endregion

    #region Func
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6565752735223045/9226679440";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6565752735223045/9226679440";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.Interstitial.LoadAd(request);
    }
    #endregion

}
