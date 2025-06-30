using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTesting;

    private string gameId;

    public void OnInitializationComplete()
    {
        Global.GlobalParam.initComplite = true;
        Debug.Log("init complite");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void Awake()
    {
        Init();

    }

    public void Init()
    {
#if UNITY_IOS
                gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
                gameId = androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            if(Application.internetReachability != NetworkReachability.NotReachable)
            {
                Advertisement.Initialize(gameId, isTesting, this);
            }
        }
    }
}
