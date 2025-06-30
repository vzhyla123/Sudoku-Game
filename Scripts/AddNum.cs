using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Action);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action() {
        Button button = gameObject.GetComponent<Button>();
        var ActivePole = GameObject.FindGameObjectWithTag("IsOn");
        if (button.GetComponentInChildren<Text>().text != "X"){
            ActivePole.GetComponentInChildren<Text>().text = button.GetComponentInChildren<Text>().text;
        }
        else{
            ActivePole.GetComponentInChildren<Text>().text = "";
        }
    }

}
