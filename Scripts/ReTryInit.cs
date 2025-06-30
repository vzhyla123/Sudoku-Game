using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReTryInit : MonoBehaviour
{
    
    

    void Start()
    {
        Button Button = gameObject.GetComponent<Button>();
        Button.onClick.AddListener(Action);
    }

    void Action()
    {
        AdsManager.instance.initializeAds.Init();
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        yield return new WaitForSeconds(4f);
        if (Global.GlobalParam.initComplite == true)
        {
            GameObject.Find("adPanel").SetActive(false);
            GameObject.FindGameObjectWithTag("AdsButton").GetComponent<Button>().interactable = true;
            AdsManager.instance.rewardedAds.LoadRewardedAd();
        }
    }
}
