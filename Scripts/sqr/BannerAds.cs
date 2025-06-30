using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iOsAdUnitId;
    private string adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
                adUnitId = iOsAdUnitId;
        #elif UNITY_ANDROID
                adUnitId = androidAdUnitId;
        #endif
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions Options = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };

        Advertisement.Banner.Load(adUnitId, Options);
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHiden
        };
        Advertisement.Banner.Show(adUnitId, options);
    }

    public void HideBannerAd() {
        Advertisement.Banner.Hide();
    }

    private void BannerHiden()
    {
    }

    private void BannerClicked()
    {
    }

    private void BannerShown()
    {
    }

    private void BannerLoadedError(string message)
    {
    }

    private void BannerLoaded()
    {
    }
}
