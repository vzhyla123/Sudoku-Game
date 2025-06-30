using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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
    }

    public void LoadIntestitalAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowIntestitalAd()
    {
        Advertisement.Show(adUnitId, this);
        LoadIntestitalAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {

    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }
}
