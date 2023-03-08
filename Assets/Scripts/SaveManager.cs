using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string SaveDataPath()
    {
        return $"{Application.persistentDataPath}/data.json";
    }

    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SaveDataPath(), json);

        Debug.Log(json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(SaveDataPath()))
        {
            return default;
        }

        string data = File.ReadAllText(SaveDataPath());
        SaveData saveData = JsonUtility.FromJson<SaveData>(data);

        return saveData;
    }
}
