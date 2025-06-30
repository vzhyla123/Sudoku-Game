using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    
    void Start()
    {   
        List<String> data = new List<string>();
        if (File.Exists(Application.persistentDataPath + "/Levels.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Levels.dat", FileMode.Open);
            data = (List<string>)bf.Deserialize(file);
            file.Close();
        }
        var list = GameObject.FindGameObjectsWithTag("PlayButton");
        foreach (var item in list)
        {
            if (data.Contains(item.GetComponent<GoToLevel>().LevelId))
            {
                item.GetComponent<Button>().interactable = false;
                item.GetComponent<Image>().color = new Color(0f,1f,0f,1f);
            }
            
        }
        GameObject.Find("oddScrol").SetActive(false);
        GameObject.Find("KillSkrol").SetActive(false);
    }

}
