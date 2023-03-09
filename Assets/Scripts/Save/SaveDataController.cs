using System.Collections.Generic;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    private SaveData _saveData;

    public void SetSaveData(SaveData saveData)
    {
        _saveData = saveData;
    }

    public void AddTotalCount(List<int> todaysTotal)
    {
        _saveData.totalCount.scratchL += todaysTotal[0];
        _saveData.totalCount.scratchR += todaysTotal[1];
        _saveData.totalCount.key1 += todaysTotal[2];
        _saveData.totalCount.key2 += todaysTotal[3];
        _saveData.totalCount.key3 += todaysTotal[4];
        _saveData.totalCount.key4 += todaysTotal[5];
        _saveData.totalCount.key5 += todaysTotal[6];
        _saveData.totalCount.key6 += todaysTotal[7];
        _saveData.totalCount.key7 += todaysTotal[8];
    }

    public void SetCurrentBgColor(Color32 color)
    {
        _saveData.displaySettings.color = color;
    }

    public static SaveDataController I { get; private set; }

    public SaveData GetSaveData()
    {
        return _saveData;
    }

    public DisplaySettings GetDisplaySettings()
    {
        return _saveData.displaySettings;
    }

    public KeySettings GetKeySettings()
    {
        return _saveData.keySettings;
    }

    public TotalCount GetTotalCount()
    {
        return _saveData.totalCount;
    }

    public Beatoraja GetBeatoraja()
    {
        return _saveData.beatoraja;
    }

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {

    }
}
