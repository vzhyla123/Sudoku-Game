using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioClip[] sounds;

    public AudioSource playSound;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Sound").Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ClickSound()
    {
        System.Random random = new System.Random();
        playSound.volume = Global.Settings.Volume * 0.15f;
        playSound.clip = sounds[random.Next(sounds.Length)];
        playSound.Play();
    }
}
