using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMod2_0 : MonoBehaviour
{
    public Button ButtonRight;
    public Button ButtonLeft;
    public TMP_Text TMP_Text;
    public GameObject ClassicPanel;
    public GameObject OddPanel;
    public GameObject KillPanel;
    void Start()
    {
        Global.GlobalParam.Mod = 0;
        ButtonLeft.onClick.AddListener(SwipeLeft);
        ButtonRight.onClick.AddListener(SwipeRight);
    }

    void SwipeLeft()
    {
        if (Global.GlobalParam.Mod != 0)
        {
            Global.GlobalParam.Mod -= 1;
            switch (Global.GlobalParam.Mod) {
                case 0:
                    ClassicPanel.SetActive(true);
                    OddPanel.SetActive(false);
                    KillPanel.SetActive(false);
                    if(TMP_Text != null) { TMP_Text.text = "Classic Sudoku"; }
                    break;
                case 1:
                    ClassicPanel.SetActive(false);
                    OddPanel.SetActive(true);
                    KillPanel.SetActive(false);
                    if (TMP_Text != null)
                    {
                        TMP_Text.text = "Odd Sudoku";
                    }
                    break;
                case 2:
                    ClassicPanel.SetActive(false);
                    OddPanel.SetActive(false);
                    KillPanel.SetActive(true);
                    if (TMP_Text != null)
                    {
                        TMP_Text.text = "Killer Sudoku";
                    }
                    break;
            }
        }
    }

    void SwipeRight()
    {
        if (Global.GlobalParam.Mod != 2)
        {
            Global.GlobalParam.Mod += 1;
            switch (Global.GlobalParam.Mod)
            {
                case 0:
                    ClassicPanel.SetActive(true);
                    OddPanel.SetActive(false);
                    KillPanel.SetActive(false);
                    if (TMP_Text != null)
                    {
                        TMP_Text.text = "Classic Sudoku";
                    }
                    break;
                case 1:
                    ClassicPanel.SetActive(false);
                    OddPanel.SetActive(true);
                    KillPanel.SetActive(false);
                    if (TMP_Text != null)
                    {
                        TMP_Text.text = "Odd Sudoku";
                    }
                    break;
                case 2:
                    ClassicPanel.SetActive(false);
                    OddPanel.SetActive(false);
                    KillPanel.SetActive(true);
                    if (TMP_Text != null)
                    {
                        TMP_Text.text = "Killer Sudoku";
                    }
                    break;
            }
        }
    }


}
