using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyCoins : MonoBehaviour
{
    public int amount;
    public GameObject adPanel;

    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Action);
        if(Global.GlobalParam.initComplite == true && Global.GlobalParam.isAdReady == true)
        {
            button.interactable = true;
        }
        if (Global.GlobalParam.initComplite == false)
        {
            adPanel.SetActive(true);
        }
    }

    private void Action()
    {
        AdsManager.instance.rewardedAds.ShowRewardedAd(amount);
        Debug.Log("Coins: " + Global.Coins.CoinCount);
    }

}
