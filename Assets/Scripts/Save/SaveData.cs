using System;
using System.Collections.Generic;


[Serializable]
public class Beatoraja
{
    public string directory;
}

[Serializable]
public class BackgroundColor
{
    public byte red;
    public byte green;
    public byte blue;
    public byte alpha;
}

[Serializable]
public class DisplaySettings
{
    public bool kps;
    public bool randomPattern;
    public bool counter;
    public BackgroundColor color;
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

    public int GetKeyCount(int index)
    {
        switch (index)
        {
            case 0:
                return scratchL;
            case 1:
                return scratchR;
            case 2:
                return key1;
            case 3:
                return key2;
            case 4:
                return key3;
            case 5:
                return key4;
            case 6:
                return key5;
            case 7:
                return key6;
            case 8:
                return key7;
            default:
                return 0;
        }
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
                color = new BackgroundColor()
                {
                    red = 0,
                    green = 255,
                    blue = 0,
                    alpha = 255
                }
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
