using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BmsTool : MonoBehaviour
{
    private void Awake()
    {
        I = this;
    }

    private void OnEnable()
    {
        // json読み込み処理
        SaveData saveData = SaveManager.Load();
        if (saveData == null)
        {
            saveData = new SaveData()
            {
                displaySettings = new DisplaySettings()
                {
                    kps = true,
                    randomPattern = true,
                    counter = true
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

        SaveDataController.I.SetSaveData(saveData);
    }

    private void Start()
    {
        if (Joystick.current == null)
        {
            return;
        }

        if (!Joystick.current.name.Contains("Controller INF"))
        {
            return;
        }

        KeySettings settings = SaveDataController.I.SaveData.keySettings;

        foreach (InputControl key in Joystick.current.allControls)
        {
            if (settings.GetButtonSettings().Contains(key.name))
            {
                ButtonControls.Add((ButtonControl)key);
            }
            else if (key.name.Equals(settings.scratch))
            {
                Scratch = (StickControl)key;
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveDataController.I.AddTotalCount(KeyLogger.I.TodayCounts);
        SaveManager.Save(SaveDataController.I.SaveData);
    }

    public static BmsTool I { get; private set; }

    public List<ButtonControl> ButtonControls { get; private set; } = new();

    public StickControl Scratch { get; private set; }
}
