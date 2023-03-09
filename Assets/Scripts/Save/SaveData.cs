using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Beatoraja
{
    public string directory;
}

[Serializable]
public struct DisplaySettings
{
    public bool kps;
    public bool randomPattern;
    public bool counter;
    public Color32 color;
}

[Serializable]
public struct KeySettings
{
    public string scratch;
    public string key1;
    public string key2;
    public string key3;
    public string key4;
    public string key5;
    public string key6;
    public string key7;

    public List<string> GetButtonSettings()
    {
        return new List<string>() { key1, key2, key3, key4, key5, key6, key7 };
    }
}

[Serializable]
public struct TotalCount
{
    public int scratchL;
    public int scratchR;
    public int key1;
    public int key2;
    public int key3;
    public int key4;
    public int key5;
    public int key6;
    public int key7;

    public long GetTotalSum()
    {
        return scratchL + scratchR + key1 + key2 + key3 + key4 + key5 + key6 + key7;
    }

    public int GetKeyCount(int index)
    {
        return index switch
        {
            0 => scratchL,
            1 => scratchR,
            2 => key1,
            3 => key2,
            4 => key3,
            5 => key4,
            6 => key5,
            7 => key6,
            8 => key7,
            _ => 0,
        };
    }
}

[Serializable]
public class SaveData
{
    public Beatoraja beatoraja;
    public DisplaySettings displaySettings;
    public KeySettings keySettings;
    public TotalCount totalCount;

    public static SaveData Initialize()
    {
        return new SaveData()
        {
            beatoraja = new Beatoraja()
            {
                directory = "D:/beatoraja0.8.4-jre-win64"
            },
            displaySettings = new DisplaySettings()
            {
                kps = true,
                randomPattern = true,
                counter = true,
                color = Color.green
            },
            keySettings = new KeySettings()
            {
                scratch = "stick",
                key1 = "trigger",
                key2 = "button2",
                key3 = "button3",
                key4 = "button4",
                key5 = "button5",
                key6 = "button6",
                key7 = "button7",
            },
            totalCount = new TotalCount()
            {
                scratchL = 0,
                scratchR = 0,
                key1 = 0,
                key2 = 0,
                key3 = 0,
                key4 = 0,
                key5 = 0,
                key6 = 0,
                key7 = 0
            }
        };
    }
}
