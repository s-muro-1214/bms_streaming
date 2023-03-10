using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BmsTool : MonoBehaviour
{
    [SerializeField] private GameObject _kps;
    [SerializeField] private GameObject _random;
    [SerializeField] private GameObject _controller;
    [SerializeField] private GameObject _counter;
    [SerializeField] private GameObject _clock;

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
            Vector3 kps = _kps.transform.position;
            Vector3 random = _random.transform.position;
            Vector3 controller = _controller.transform.position;
            Vector3 counter = _counter.transform.position;
            Vector3 clock = _clock.transform.position;

            saveData = SaveData.Initialize(kps, random, controller, counter, clock);
        }

        SaveDataController.I.SetSaveData(saveData);

        BeatorajaWatcher.Init();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

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
