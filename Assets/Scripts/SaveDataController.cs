using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{

    public void SetSaveData(SaveData saveData)
    {
        SaveData = saveData;
    }

    public void AddTotalCount(List<int> todaysTotal)
    {
        SaveData.totalCount.scratchL += todaysTotal[0];
        SaveData.totalCount.scratchR += todaysTotal[1];
        SaveData.totalCount.key1 += todaysTotal[2];
        SaveData.totalCount.key2 += todaysTotal[3];
        SaveData.totalCount.key3 += todaysTotal[4];
        SaveData.totalCount.key4 += todaysTotal[5];
        SaveData.totalCount.key5 += todaysTotal[6];
        SaveData.totalCount.key6 += todaysTotal[7];
        SaveData.totalCount.key7 += todaysTotal[8];
    }

    public static SaveDataController I { get; private set; }

    public SaveData SaveData { get; private set; }

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        
    }
}
