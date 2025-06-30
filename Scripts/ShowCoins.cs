using Global;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCoins : MonoBehaviour
{

    void Start()
    {
        TMP_Text ShowCoinsText = gameObject.GetComponent<TMP_Text>();
        if (Global.Coins.CoinCount != 1)
        {
            ShowCoinsText.text = Global.Coins.CoinCount.ToString() + " Coins";
        }
        else
        {
            ShowCoinsText.text = Global.Coins.CoinCount.ToString() + " Coin";
        }
    }

    void Update()
    {
        
    }
}
