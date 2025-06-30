using Global;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iOsAdUnitId;
    private string adUnitId;

    private int reward;

    private void Awake()
    {
        #if UNITY_IOS
                        adUnitId = iOsAdUnitId;
        #elif UNITY_ANDROID
                adUnitId = androidAdUnitId;
        #endif

    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(adUnitId, this);
        Debug.Log("load is going...");
    }

    public void ShowRewardedAd(int amoung)
    {
        Advertisement.Show(adUnitId, this);
        LoadRewardedAd();
        reward = amoung;
        Global.GlobalParam.isAdReady = false;
        if(GameObject.FindGameObjectWithTag("AdsButton") != null)
        {
            GameObject.FindGameObjectWithTag("AdsButton").GetComponent<Button>().interactable = false;
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("load complite");
        Global.GlobalParam.isAdReady = true;
        if (GameObject.FindGameObjectWithTag("AdsButton") != null)
        {
            GameObject.FindGameObjectWithTag("AdsButton").GetComponent<Button>().interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        if (GameObject.Find("adPanel") != null)
        {
            GameObject.Find("adPanel").SetActive(true);
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) {
            Global.Coins.CoinCount += reward;
            Global.Coins.SaveCoins();
            if(GameObject.Find("ShowCoinsPanel") != null)
            {
                GameObject.Find("ShowCoinsPanel").GetComponentInChildren<TMP_Text>().text = Global.Coins.CoinCount.ToString() + " Coins";
            }
        }
    }
}
