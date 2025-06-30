using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public GameObject soundSet;

    public Sprite on;
    public Sprite off;
    void Start()
    {
        Slider slider = soundSet.GetComponentInChildren<Slider>();
        slider.value = Global.Settings.Volume;
        if (Global.Settings.Volume == 0)
        {
            soundSet.GetComponentInChildren<Image>().sprite = off;
        }
        slider.onValueChanged.AddListener(SoundSet);
    }

    void SoundSet(float a)
    {
        Global.Settings.Volume = a;
        if (a == 0){
            soundSet.GetComponentInChildren<Image>().sprite = off;
        }
        else {
            soundSet.GetComponentInChildren<Image>().sprite = on;
        }

    }
}

