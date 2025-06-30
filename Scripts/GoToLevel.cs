using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public int SceneId;
    public bool IsPreLoaded;
    public string LevelId;
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Action);
    }

    public void Action()
    {
        if (LevelId != "")
        {
            Global.GlobalParam.LevelId = LevelId;
        }
        Global.GlobalParam.IsPreLoaded = IsPreLoaded;
        SceneManager.LoadScene(SceneId);
    }
}
