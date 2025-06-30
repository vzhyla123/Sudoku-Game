using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMod : MonoBehaviour
{
    public Button ButtonRight;
    public Button ButtonLeft;

    public GameObject ClassicPanel;
    public GameObject OddPanel;
    public GameObject KillPanel;

    void Start()
    {
        Global.GlobalParam.Mod = 0;
        ButtonLeft.onClick.AddListener(SwipeLeft);
        ButtonRight.onClick.AddListener(SwipeRight);
    }

    

    void SwipeLeft()
    {   
        if (Global.GlobalParam.Mod != 0)
        {
            Global.GlobalParam.Mod -= 1;
        }
    }

    void SwipeRight()
    {
        if (Global.GlobalParam.Mod != 2)
        {
            Global.GlobalParam.Mod += 1;
        }
    }
}
