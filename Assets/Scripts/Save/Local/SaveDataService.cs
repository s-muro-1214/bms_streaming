using System.IO;
using UnityEngine;

public static class SaveDataService
{
    private static string SaveDataPath()
    {
        return $"{Application.persistentDataPath}/data.json";
    }

    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SaveDataPath(), json);
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
