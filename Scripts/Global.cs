using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Global
{
    class GlobalParam
    {
        public static int GlobalDificulty = 25;

        public static int Mod = 0;

        public static bool IsPreLoaded;

        public static string LevelId;

        public static int u = 1;

        public static int[,] GlobalLol;

        public static bool initComplite = false;

        public static bool isAdReady = false;


    }

    class Settings
    {
        public static float Volume = 1;
    }

    public class Coins
    {
        public static int CoinCount;

        public Coins()
        {
            CoinCount = 0;
        }

        public static int LoadCoins()
        {
            if (File.Exists(Application.persistentDataPath + "/CData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/CData.dat", FileMode.Open);
                int data = (int)bf.Deserialize(file);
                file.Close();
                return data;
            }
            else{
                return 0; 
            }
        }

        public static void SaveCoins()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/CData.dat");
            bf.Serialize(file, CoinCount);
            file.Close();
        }
    }
}