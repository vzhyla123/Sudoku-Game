using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    //easy - 25
    //middle - 40
    //hard - 60
    private int Dificulty = Global.GlobalParam.GlobalDificulty;

    public Button HintButton;

    public Button checkButton;

    public Sprite spr;

    public GameObject WinPanel;

    public GameObject LosePanel;

    public TMP_Text CoinText;

    public static int reward = 0;

    public GameObject SumPole;

    public Sprite p1,p2,p3,p4,p5,p6,p7,p8,p9;

    public GameObject NotEnoughtCoins;

    public void Start()
    {
        int[,] lol = CreateGamePole();
        Global.GlobalParam.GlobalLol = lol;
        var text = FindObjectsByType<Toggle>(0);
        Toggle[] pole = SortTexts(text);
        int[,] GameMas = new int[9,9];
        if (Global.GlobalParam.Mod == 2)
        {
            Dificulty = Dificulty * 2 - Dificulty/3;
        }
        if (Global.GlobalParam.IsPreLoaded == false)
        {
            Vyvod(lol);
            Debug.Log(CheckGamePole(lol));
            GameMas = CreateGame(lol, Dificulty);
            Vyvod(GameMas);
            int u = Global.GlobalParam.u;
            VyvodFull(GameMas,lol,u);
            Global.GlobalParam.u++;
        } else if (Global.GlobalParam.IsPreLoaded == true)
        {
            GameMas = Levels.LevelTemps.templates[Global.GlobalParam.LevelId];
            lol = Levels.LevelTemps.templates[Global.GlobalParam.LevelId + "lol"];
            Global.GlobalParam.GlobalLol = lol;
        }
        switch(Global.GlobalParam.Mod){
            case 0:
                SetPole(pole, GameMas);
                break;
            case 1:
                SetNechPole(pole, GameMas, lol);
                break;
            case 2:
                int[,] killMas = new int[9,9];
                if (Global.GlobalParam.IsPreLoaded == true)
                {
                    killMas = Levels.LevelTemps.templates[Global.GlobalParam.LevelId + "kill"];
                }
                else {
                    killMas = CreateKillerSudoku();
                }
                Vyvod(killMas);
                Dictionary<int,int> SumPolesMas = CreateSums(lol, killMas);
                SetKillPole(pole, GameMas, lol ,killMas, SumPolesMas);
                break;
        }

        checkButton.onClick.AddListener(Checkwin);
        HintButton.onClick.AddListener(GetHint);
        if (Global.GlobalParam.IsPreLoaded == true)
        {
            switch (Global.GlobalParam.LevelId[0])
            {
                case 'e':
                    reward = 1;
                    break;
                case 'm':
                    reward = 3;
                    break;
                case 'h':
                    reward = 5;
                    break;
            }
        }
        else
        {
            if (Global.GlobalParam.GlobalDificulty == 25)
            {
                reward = 1;
            }
            else if (Global.GlobalParam.GlobalDificulty == 40)
            {
                reward = 3;
            }
            else if (Global.GlobalParam.GlobalDificulty == 55)
            {
                reward = 5;
            }
        }
    }

    static int[,] CreateGamePole()
    {
        System.Random random = new();
        List<int> ch = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[,] lol = new int[9, 9];
        int l;
        int kep = 0;
        int pek = 0;
        int kol = 0;
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                List<int> ch1 = new List<int>(ch);
                for (int j = 0; j < 9; j++)
                {
                    l = random.Next(ch1.Count);
                    lol[i, j] = ch1[l];
                    ch1.RemoveAt(l);
                    kol++;
                }
            }
            else
            {

                for (int j = 0; j < 9; j++)
                {
                    List<int> ch1 = new List<int>(ch);
                    for (int f = 0; f < i; f++)
                    {
                        ch1.Remove(lol[f, j]);
                    }
                    for (int f = 0; f < 9; f++)
                    {
                        ch1.Remove(lol[i, f]);
                    }
                    for (int r = 0; r < 3; r++)
                    {
                        for (int r1 = 0; r1 < 3; r1++)
                        {
                            ch1.Remove(lol[r + 3 * ((int)i / 3), r1 + 3 * ((int)j / 3)]);
                        }
                    }
                    if (ch1.Count == 0)
                    {
                        if (pek == 1)
                        {
                            i = 0;
                            j = -1;
                            for (int t = 0; t < 9; t++)
                            {
                                for (int r = 0; r < 9; r++)
                                {
                                    lol[t, r] = 0;
                                }
                            }
                            pek = 0;
                            continue;
                        }
                        if (j > 4)
                        {
                            lol[i, j - 1] = 0;
                            lol[i, j - 2] = 0;
                            j -= 3;
                            if (kep == 0)
                            {
                                kep = 1;
                            }
                            else if (kep == 1)
                            {
                                lol[i, j] = 0;
                                j -= 1;
                                kep = 2;
                            }
                            else if (kep == 2)
                            {
                                lol[i, j] = 0;
                                lol[i, j - 1] = 0;
                                j -= 2;
                                kep = 3;
                            }
                            else if (kep == 3)
                            {
                                lol[i, j] = 0;
                                lol[i, j - 1] = 0;
                                lol[i, j - 2] = 0;
                                j -= 3;
                                kep = 4;
                            }
                            else if (kep == 4)
                            {
                                for (int h = 0; h < j; h++)
                                {
                                    lol[i, h] = 0;
                                }
                                i -= 1;
                                j = -1;
                                for (int h = 0; h < 9; h++)
                                {
                                    lol[i, h] = 0;
                                }
                                kep = 0;
                                pek++;
                                continue;
                            }
                            continue;
                        }
                        else
                        {
                            for (int h = 0; h < j; h++)
                            {
                                lol[i, h] = 0;
                            }
                            i -= 1;
                            j = -1;
                            for (int h = 0; h < 9; h++)
                            {
                                lol[i, h] = 0;
                            }
                            continue;
                        }

                    }
                    l = random.Next(ch1.Count);
                    lol[i, j] = ch1[l];
                    kol++;
                }
            }
            pek = 0;
            kep = 0;
        }
        return lol;
    }

    bool CheckGamePole(int[,] lol)
    {
        int a = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                a = lol[i, j];
                for (int f = 0; f < 9; f++)
                {
                    if (i != f && j != f)
                    {
                        if (lol[i, f] == a)
                        {
                            return false;
                        }
                        if (lol[f, j] == a)
                        {
                            return false;
                        }
                    }
                }
                for (int r = 0; r < 3; r++)
                {
                    for (int r1 = 0; r1 < 3; r1++)
                    {
                        if (lol[r + 3 * ((int)i / 3), r1 + 3 * ((int)j / 3)] != lol[i, j])
                        {
                            if (lol[r + 3 * ((int)i / 3), r1 + 3 * ((int)j / 3)] == a)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }

    int[,] CreateGame(int[,] Gamemas, int Dificulty)
    {
        int[,] mas = Copy2DMass(Gamemas);
        System.Random random = new();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int ran = random.Next(1, 100);
                if (ran < Dificulty)
                {
                    mas[i, j] = 0;
                }
            }
        }
        return mas;
    }

    int[,] Copy2DMass(int[,] mas)
    {
        int[,] mas2 = new int[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                mas2[i, j] = mas[i, j];
            }
        }
        return mas2;
    }

    void Vyvod(int[,] mas)
    {
        string a = "";

        a += "{";
        for (int i = 0;i < 9; i++)
        {
            a += "{";
            for (int j = 0;j < 9; j++)
            {
                a += mas[i, j];
                if (j != 8)
                {
                    a += ", ";
                }
            }
            if (i == 8)
            {
                a += "}";
            }
            else
            {
                a += "},";
            }
        }
        a += "};";
        Debug.Log(a);
    }

    Toggle[] SortTexts(Toggle[] mas)
    {
        Toggle[] ret = new Toggle[81];
        char[] MyChar = {'p','o','l','e'};
        foreach (Toggle text in mas){
            int point = int.Parse(text.name.TrimStart(MyChar));
            ret[point-1] = text;
        }
        return ret;
    }

    void SetPole(Toggle[] pole, int[,] GameMas) {
        int l = 0;
        bool o = false;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var label = pole[l].GetComponentInChildren<Text>();
                if (GameMas[i,j] != 0){
                    label.text = GameMas[i, j].ToString();
                    pole[l].GetComponentInChildren<Text>().color = new Color(110f/255, 110f / 255, 110f / 255, 1);
                }
                else{
                    pole[l].interactable = true;
                    pole[l].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f,1);
                    pole[l].GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
                    if (o == false)
                    {
                        pole[l].isOn = true;
                        pole[l].tag = "IsOn";
                        o = true;
                    }
                    label.text = "";
                }
                l++;
            }
        }
    }

    void Checkwin(){
        var text = FindObjectsByType<Toggle>(0);
        Toggle[] pole = SortTexts(text);
        for (int i = 0; i < pole.Length; i++){
            if(pole[i].GetComponentInChildren<Text>().text == ""){
                LosePanel.SetActive(true);
                Debug.Log("false");
                return;
            }
        }
        int[,] checklol = new int[9,9];
        int l = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                checklol[i, j] = int.Parse(pole[l].GetComponentInChildren<Text>().text);
                l++;
            }
        }
        bool Check = CheckGamePole(checklol);
        if (Check == true)
        {
            WinPanel.SetActive(true);
            CoinText.text = "+" + reward.ToString() + " Coins";
        }
        else if(Check == false){
            LosePanel.SetActive(true);
            Debug.Log("false");
        }
    }

    void GetHint() {
        if (Global.Coins.CoinCount-1 > 0)
        {
            var text = FindObjectsByType<Toggle>(0);
            List<Toggle> nul = new List<Toggle> { };
            foreach (Toggle pol in text)
            {
                if (pol.GetComponentInChildren<Text>().text == "")
                {
                    nul.Add(pol);
                }
            }
            int[,] lol = Global.GlobalParam.GlobalLol;
            if (nul.Count > 0)
            {
                Global.Coins.CoinCount -= 2;
                Global.Coins.SaveCoins();
                if (Global.Coins.CoinCount != 1)
                {
                    GameObject.Find("ShowCoinsPanel").GetComponentInChildren<TMP_Text>().text = Global.Coins.CoinCount.ToString() + " Coins";
                }
                else
                {
                    GameObject.Find("ShowCoinsPanel").GetComponentInChildren<TMP_Text>().text = Global.Coins.CoinCount.ToString() + " Coin";
                }
                Toggle Hint = nul[Random.Range(0, nul.Count)];
                char[] MyChar = { 'p', 'o', 'l', 'e' };
                int point = int.Parse(Hint.name.TrimStart(MyChar));
                Hint.GetComponentInChildren<Text>().text = lol[(point - 1) / 9, ((point - 1) % 9)].ToString();
            }
        }
        else
        {
            NotEnoughtCoins.SetActive(true);
        }
    }

    void SetNechPole(Toggle[] pole, int[,] GameMas, int[,] lol)
    {
        int l = 0;
        bool o = false;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var label = pole[l].GetComponentInChildren<Text>();
                if (GameMas[i, j] != 0)
                {
                    if (GameMas[i, j] % 2 == 1)
                    {
                        pole[l].GetComponentInChildren<Image>().sprite = spr;
                        pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
                        pole[l].GetComponentInChildren<Text>().color = new Color(110f / 255, 110f / 255, 110f / 255, 1);
                    }
                    else
                    {
                        pole[l].GetComponentInChildren<Image>().color = new Color(0f, 0f, 0f, 0f);
                        pole[l].GetComponentInChildren<Text>().color = new Color(110f / 255, 110f / 255, 110f / 255, 1);
                    }
                    label.text = GameMas[i, j].ToString();
                }
                else
                {
                    if (lol[i, j] % 2 == 1)
                    {
                        pole[l].GetComponentInChildren<Image>().sprite = spr;
                        pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
                    }
                    pole[l].GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, 1);
                    pole[l].interactable = true;
                    if (o == false)
                    {
                        pole[l].isOn = true;
                        pole[l].tag = "IsOn";
                        o = true;
                    }
                    label.text = "";
                }
                l++;
            }
        }
    }

    void VyvodFull(int[,] GameMas, int[,] lolMas, int u)
    {
        string a = "";
        a += "private static int[,] hcl" + u + " = new int[9, 9] {";
        for (int i = 0; i < 9; i++)
        {
            a += "{";
            for (int j = 0; j < 9; j++)
            {
                a += GameMas[i, j];
                if (j != 8)
                {
                    a += ", ";
                }
            }
            if (i == 8)
            {
                a += "}";
            }
            else
            {
                a += "},";
            }
        }
        a += "};\n";

        a += "private static int[,] hcl" + u + "lol = new int[9, 9] {";
        for (int i = 0; i < 9; i++)
        {
            a += "{";
            for (int j = 0; j < 9; j++)
            {
                a += lolMas[i, j];
                if (j != 8)
                {
                    a += ", ";
                }
            }
            if (i == 8)
            {
                a += "}";
            }
            else
            {
                a += "},";
            }
        }
        a += "};\n";

        a += "{\"mcl"+u+ "\", mcl"+u+"},\r\n                {\"mcl"+u+"lol\", mcl"+u+"lol},";
        Debug.Log(a);
    }

    int[,] CreateKillerSudoku()
    {
        System.Random random = new();
        int[,] KilSudIdsP = new int[9, 9];
        int id = 1;
        int count;
        int[] po = new int[42];
        int[] lol = new int[42];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (KilSudIdsP[i, j] == 0)
                {
                    KilSudIdsP[i, j] = id;
                    id++;
                    count = random.Next(2, 7);
                    int leftRingt = 0;
                    int UpDown = 0;
                    for (int s = 2; s <= count; s++)
                    {
                        int lr = random.Next(0, 2);
                        switch (lr)
                        {
                            case 0:
                                leftRingt++;
                                break;
                            case 1:
                                UpDown++;
                                break;
                        }
                        if (j + leftRingt < 9 && i + UpDown < 9)
                        {
                            KilSudIdsP[i + UpDown, j + leftRingt] = KilSudIdsP[i, j];
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i - 1 >= 0)
                {
                    if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j])
                    {
                        continue;
                    }
                }
                if (j - 1 >= 0)
                {
                    if (KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                    {
                        continue;
                    }
                }
                if (i + 1 <= 8)
                {
                    if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j])
                    {
                        continue;
                    }
                }
                if (j + 1 <= 8)
                {
                    if (KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                    {
                        continue;
                    }
                }


                if (i + 1 <= 8)
                {
                    KilSudIdsP[i, j] = KilSudIdsP[i + 1, j];
                }
                else if (j + 1 <= 8)
                {
                    KilSudIdsP[i, j] = KilSudIdsP[i, j + 1];
                }
                else if (i - 1 >= 0)
                {
                    KilSudIdsP[i, j] = KilSudIdsP[i - 1, j];
                }
                else if (j - 1 >= 0)
                {
                    KilSudIdsP[i, j] = KilSudIdsP[i, j - 1];
                }


            }
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                po[KilSudIdsP[i, j]] += 1;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (lol[KilSudIdsP[i, j]] == 0)
                {
                    List<int[]> ints = new();
                    int[] ints2 = new int[3];
                    ints2[0] = i;
                    ints2[1] = j;
                    ints2[2] = 0;
                    ints.Add(ints2);
                    bool d = false;
                    List<int> gg = new();
                    gg.Add(i);
                    gg.Add(j);
                    while (d == false)
                    {
                        foreach (int[] point in ints.ToList())
                        {
                            if (point[2] == 0)
                            {
                                if (point[0] - 1 >= 0)
                                {
                                    bool r = false;
                                    for (int q = 0; q <= gg.Count - 1; q += 2)
                                    {
                                        if (gg[q] == point[0] - 1 && gg[q + 1] == point[1])
                                        {
                                            r = true;
                                        }
                                    }
                                    if (KilSudIdsP[point[0] - 1, point[1]] == KilSudIdsP[point[0], point[1]] && r == false)
                                    {
                                        int[] ints1 = new int[3];
                                        ints1[0] = point[0] - 1; ints1[1] = point[1]; ints1[2] = 0;
                                        ints.Add(ints1);
                                        gg.Add(point[0] - 1);
                                        gg.Add(point[1]);
                                    }
                                }
                                if (point[1] - 1 >= 0)
                                {
                                    bool r = false;
                                    for (int q = 0; q <= gg.Count - 1; q += 2)
                                    {
                                        if (gg[q] == point[0] && gg[q + 1] == point[1] - 1)
                                        {
                                            r = true;
                                        }
                                    }
                                    if (KilSudIdsP[point[0], point[1] - 1] == KilSudIdsP[point[0], point[1]] && r == false)
                                    {
                                        int[] ints1 = new int[3];
                                        ints1[0] = point[0]; ints1[1] = point[1]-1; ints1[2] = 0;
                                        ints.Add(ints1);
                                        gg.Add(point[0]);
                                        gg.Add(point[1] - 1);
                                    }
                                }
                                if (point[0] + 1 <= 8)
                                {
                                    bool r = false;
                                    for (int q = 0; q <= gg.Count - 1; q += 2)
                                    {
                                        if (gg[q] == point[0] + 1 && gg[q + 1] == point[1])
                                        {
                                            r = true;
                                        }
                                    }
                                    if (KilSudIdsP[point[0] + 1, point[1]] == KilSudIdsP[point[0], point[1]] && r == false)
                                    {
                                        int[] ints1 = new int[3];
                                        ints1[0] = point[0]+1; ints1[1] = point[1]; ints1[2] = 0;
                                        ints.Add(ints1);
                                        gg.Add(point[0] + 1);
                                        gg.Add(point[1]);
                                    }
                                }
                                if (point[1] + 1 <= 8)
                                {
                                    bool r = false;
                                    for (int q = 0; q <= gg.Count - 1; q += 2)
                                    {
                                        if (gg[q] == point[0] && gg[q + 1] == point[1] + 1)
                                        {
                                            r = true;
                                        }
                                    }
                                    if (KilSudIdsP[point[0], point[1] + 1] == KilSudIdsP[point[0], point[1]] && r == false)
                                    {
                                        int[] ints1 = new int[3];
                                        ints1[0] = point[0]; ints1[1] = point[1] + 1; ints1[2] = 0;
                                        ints.Add(ints1);
                                        gg.Add(point[0]);
                                        gg.Add(point[1] + 1);
                                    }
                                }
                                point[2] = 1;
                            }

                        }
                        foreach (int[] point in ints.ToList())
                        {
                            if (point[2] == 0)
                            {
                                d = false;
                                break;
                            }
                            d = true;
                        }
                    }
                    if (po[KilSudIdsP[i, j]] != ints.Count)
                    {
                        for (int q = 0; q <= gg.Count - 1; q += 2)
                        {
                            KilSudIdsP[gg[q], gg[q + 1]] = id;
                        }
                        lol[KilSudIdsP[i, j]] = 1;
                        id++;
                    }
                    else
                    {
                        lol[KilSudIdsP[i, j]] = 1;
                    }

                }
            }
        }
        return KilSudIdsP;
    }

    Dictionary<int, int> CreateSums(int[,] lol, int[,] kill)
    {
        Dictionary<int, int> SumPolesMas = new();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (SumPolesMas.ContainsKey(kill[i, j]))
                {
                    SumPolesMas[kill[i, j]] += lol[i, j];
                }
                else
                {
                    SumPolesMas.Add(kill[i, j], lol[i, j]);
                }
            }
        }
        return SumPolesMas;
    }

    void SetKillPole(Toggle[] pole, int[,] GameMas, int[,] lol ,int[,] KilSudIdsP, Dictionary<int, int> SumPolesMas) {
        int l = 0;
        int[] points = new int[50];
        SetPole(pole, GameMas);
        for (int i = 0; i < 9; i++){
            for(int j = 0;j < 9; j++){
                int f = 0;
                
                for (int k = -1; k <= 1; k++) {
                    if (i + k >= 0 && i + k < 9) { 
                        if (k != 0){
                            if (KilSudIdsP[i,j] == KilSudIdsP[i+k,j]){
                                f++;
                            }
                        }
                    }
                    if (j + k >= 0 && j + k < 9){
                        if (k != 0){
                            if (KilSudIdsP[i, j] == KilSudIdsP[i, j + k]){
                                f++;
                            }
                        }
                    }
                }

                if (f == 1)
                {
                    if (i - 1 >= 0)
                    {
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 90);
                            pole[l].GetComponentInChildren<Image>().sprite = p1;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j - 1 >= 0)
                    {
                        if (KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 180);
                            pole[l].GetComponentInChildren<Image>().sprite = p1;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (i + 1 <= 8)
                    {
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, -90);
                            pole[l].GetComponentInChildren<Image>().sprite = p1;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j + 1 <= 8)
                    {
                        if (KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 0);
                            pole[l].GetComponentInChildren<Image>().sprite = p1;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                }
                if (f == 2){
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 90);
                            pole[l].GetComponentInChildren<Image>().sprite = p5;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p2;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j - 1 >= 0 && i + 1 <= 8)
                    {
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 180);
                            pole[l].GetComponentInChildren<Image>().sprite = p5;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p2;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (i + 1 <= 8 && j + 1 <= 8)
                    {
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, -90);
                            pole[l].GetComponentInChildren<Image>().sprite = p5;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p2;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j + 1 <= 8 && i - 1 >= 0)
                    {
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 0);
                            pole[l].GetComponentInChildren<Image>().sprite = p5;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p2;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j + 1 <= 8 && j - 1 >= 0)
                    {
                        if (KilSudIdsP[i, j-1] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 0);
                            pole[l].GetComponentInChildren<Image>().sprite = p4;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        
                    }
                    if (i + 1 <= 8 && i - 1 >= 0)
                    {
                        if (KilSudIdsP[i-1, j] == KilSudIdsP[i, j] && KilSudIdsP[i+1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 90);
                            pole[l].GetComponentInChildren<Image>().sprite = p4;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }

                    }
                }
                if (f == 3){
                    if (i - 1 >= 0 && j - 1 >= 0 && i + 1 <= 8)
                    {
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 90);
                            pole[l].GetComponentInChildren<Image>().sprite = p6;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p8;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p9;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p3;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j - 1 >= 0 && i + 1 <= 8 && j + 1 <= 8)
                    {
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 180);
                            pole[l].GetComponentInChildren<Image>().sprite = p6;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p8;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p9;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p3;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (i + 1 <= 8 && j + 1 <= 8 && i - 1 >= 0)
                    {
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, -90);
                            pole[l].GetComponentInChildren<Image>().sprite = p6;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p8;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p9;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i + 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i + 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j + 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p3;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                    if (j + 1 <= 8 && i - 1 >= 0 && j - 1 >= 0)
                    {
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].transform.Find("Background").Rotate(0, 0, 0);
                            pole[l].GetComponentInChildren<Image>().sprite = p6;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p8;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p9;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                        if (KilSudIdsP[i - 1, j] == KilSudIdsP[i, j] && KilSudIdsP[i, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j + 1] == KilSudIdsP[i, j] && KilSudIdsP[i, j - 1] == KilSudIdsP[i, j] && KilSudIdsP[i - 1, j - 1] == KilSudIdsP[i, j])
                        {
                            pole[l].GetComponentInChildren<Image>().sprite = p3;
                            pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                        }
                    }
                }
                if (f == 4){
                    pole[l].GetComponentInChildren<Image>().sprite = p7;
                    pole[l].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 180 / 255f);
                }

                if (points[KilSudIdsP[i, j]] == 0) {
                    points[KilSudIdsP[i, j]] = 1;
                    Instantiate(SumPole, parent: pole[l].transform, false);
                    pole[l].GetComponentInChildren<TMP_Text>().text = SumPolesMas[KilSudIdsP[i,j]].ToString();
                }
                l++;
            }
        }
    }

    void VyvodFullKill(int[,] GameMas, int[,] lolMas, int u, int[,] killMas, Dictionary<int, int> SumPolesMas)
    {
        string a = "";
        a += "private static int[,] mcl" + u + " = new int[9, 9] {";
        for (int i = 0; i < 9; i++)
        {
            a += "{";
            for (int j = 0; j < 9; j++)
            {
                a += GameMas[i, j];
                if (j != 8)
                {
                    a += ", ";
                }
            }
            if (i == 8)
            {
                a += "}";
            }
            else
            {
                a += "},";
            }
        }
        a += "};\n";

        a += "private static int[,] mcl" + u + "lol = new int[9, 9] {";
        for (int i = 0; i < 9; i++)
        {
            a += "{";
            for (int j = 0; j < 9; j++)
            {
                a += lolMas[i, j];
                if (j != 8)
                {
                    a += ", ";
                }
            }
            if (i == 8)
            {
                a += "}";
            }
            else
            {
                a += "},";
            }
        }
        a += "};\n";

        a += "{\"mcl" + u + "\", mcl" + u + "},\r\n                {\"mcl" + u + "lol\", mcl" + u + "lol},";
        Debug.Log(a);
    }
}