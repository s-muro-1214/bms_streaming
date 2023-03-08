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
            saveData = SaveData.Initialize();
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

        KeySettings settings = SaveDataController.I.GetKeySettings();

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
        SaveManager.Save(SaveDataController.I.GetSaveData());
    }

    public static BmsTool I { get; private set; }

    public List<ButtonControl> ButtonControls { get; private set; } = new();

    public StickControl Scratch { get; private set; }
}
