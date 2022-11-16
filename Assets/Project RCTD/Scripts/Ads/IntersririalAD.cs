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
        // 전면 ID 넣기
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
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
