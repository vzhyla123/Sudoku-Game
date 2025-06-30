using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class RewardGiving : MonoBehaviour
{
    public int SceneID;
    // Start is called before the first frame update
    void Start()
    {
        Button RewardButton = gameObject.GetComponent<Button>();
        RewardButton.onClick.AddListener(Action);
    }

    void Action()
    {
        SceneManager.LoadScene(SceneID);
        Global.Coins.CoinCount += GameLogic.reward;
        Global.Coins.SaveCoins();
        List<String> data = new List<string>();
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/Levels.dat"))
        {
            FileStream filee = File.Open(Application.persistentDataPath + "/Levels.dat", FileMode.Open);
            data = (List<string>)bf.Deserialize(filee);
            filee.Close();
        }
        data.Add(Global.GlobalParam.LevelId);
        FileStream file = File.Create(Application.persistentDataPath + "/Levels.dat");
        bf.Serialize(file, data);
        file.Close();
    }
}
