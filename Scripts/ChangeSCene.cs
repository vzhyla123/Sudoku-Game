using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSCene : MonoBehaviour
{
    public int SceneId;
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Action);
    }

    public void Action()
    {
        SceneManager.LoadScene(SceneId);
    }
}
