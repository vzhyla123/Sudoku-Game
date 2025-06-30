using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetDif : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Global.GlobalParam.GlobalDificulty = 25;
        TMP_Dropdown dropdown = gameObject.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(Action);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action(int DifId)
    {
        if (DifId == 0){
            Global.GlobalParam.GlobalDificulty = 25;
        }
        else if (DifId == 1){
            Global.GlobalParam.GlobalDificulty = 40;
        }
        else if (DifId == 2){
            Global.GlobalParam.GlobalDificulty = 55;
        }

    }
}
