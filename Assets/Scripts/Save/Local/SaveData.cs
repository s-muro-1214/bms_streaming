﻿using System;
using UnityEngine;

[Serializable]
public class Beatoraja
{
    public string directory = "D:/beatoraja0.8.4-jre-win64";
    public int webSocketPort = 30127;
}

[Serializable]
public class UISetting
{
    public bool isEnabled = false;
    public Vector3 currentPosition = new(0, 0, 0);

    public UISetting()
    {
    }

    public UISetting(Vector3 initialPosition)
    {
        currentPosition = initialPosition;
    }
}

[Serializable]
public class DisplaySettings
{
    public UISetting kps = new();
    public UISetting random = new();
    public UISetting controller = new();
    public UISetting counter = new();
    public UISetting clock = new();
    public Color32 backgroundColor = Color.green;

    public DisplaySettings()
    {
    }

    public DisplaySettings(Vector3 kps, Vector3 random, Vector3 controller, Vector3 counter,
        Vector3 clock)
    {
        this.kps = new(kps);
        this.random = new(random);
        this.controller = new(controller);
        this.counter = new(counter);
        this.clock = new(clock);
    }
}

[Serializable]
public class KeySettings
{
    public string scratch = "stick";
    public string key1 = "trigger";
    public string key2 = "button2";
    public string key3 = "button3";
    public string key4 = "button4";
    public string key5 = "button5";
    public string key6 = "button6";
    public string key7 = "button7";
}


[Serializable]
public class SaveData
{
    public string uuid;
    public Beatoraja beatoraja;
    public DisplaySettings displaySettings;
    public KeySettings keySettings;

    public static SaveData Initialize(string uuid, Vector3 kps, Vector3 random, Vector3 controller, Vector3 counter,
        Vector3 clock)
    {
        return new SaveData()
        {
            uuid = uuid,
            beatoraja = new Beatoraja(),
            displaySettings = new DisplaySettings(kps, random, controller, counter, clock),
            keySettings = new KeySettings()
        };
    }
}