using System;
using System.Collections.Generic;

[Serializable]
public class DisplaySettings
{
    public bool kps;
    public bool randomPattern;
    public bool counter;
}

[Serializable]
public class KeySettings
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
public class TotalCount
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
}


[Serializable]
public class SaveData
{
    public DisplaySettings displaySettings;
    public KeySettings keySettings;
    public TotalCount totalCount;
}
