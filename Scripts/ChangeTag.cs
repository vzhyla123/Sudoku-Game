using UnityEngine;
using UnityEngine.UI;

public class ChangeTag : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Toggle tagToggle = gameObject.GetComponent<Toggle>();
        tagToggle.onValueChanged.AddListener(Action);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action(bool IsOn)
    {
        Toggle tagToggle = gameObject.GetComponent<Toggle>();
        if (IsOn == true){
            tagToggle.tag = "IsOn";
        }
        else{
            tagToggle.tag = "IsOff";
        }
    }
}
