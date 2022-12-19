using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DataManager
{
    public static bool SaveJsonData()
    {
        GameData sd = GameObject.Find("DataManager").GetComponent<GameData>();
        sd.saveState();
        Debug.Log(sd.ToJson());

        if (FileManager.WriteToFile("GameData01.dat", sd.ToJson()))
        {
            return true;
        }

        return false;
    }
    
    public static bool LoadJsonData()
    {
        if (FileManager.LoadFromFile("GameData01.dat", out var json))
        {
            GameData sd = GameObject.Find("DataManager").GetComponent<GameData>();
            sd.LoadFromJson(json);
            sd.loadState();
            Debug.Log(sd.ToJson());

            return true;
        }
        return false;
    }
}