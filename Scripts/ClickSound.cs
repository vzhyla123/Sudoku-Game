using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Action);
    }

    public void Action()
    {
        var s = GameObject.Find("SoundManager").GetComponent<PlaySound>();
        s.ClickSound();
    }
}
