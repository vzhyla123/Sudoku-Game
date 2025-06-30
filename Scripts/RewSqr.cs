using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewSqr : MonoBehaviour
{
    public GameObject aaa;
    public TMP_Text text;
    void Start()
    {
        
    }

    public void AAA()
    {
        aaa.SetActive(true);
        text.text = "Reward:\n 2 coins";
    }

}
