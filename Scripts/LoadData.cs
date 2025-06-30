using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    void Awake()
    {
        Global.Coins.CoinCount = Global.Coins.LoadCoins();
    }
}
