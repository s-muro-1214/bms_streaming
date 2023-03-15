using System;
using UnityEngine;

public class SaveDataInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _kps;
    [SerializeField] private GameObject _random;
    [SerializeField] private GameObject _controller;
    [SerializeField] private GameObject _counter;
    [SerializeField] private GameObject _clock;

    private SaveData _saveData;

    private void Start()
    {
        // json読み込み処理
        _saveData = SaveDataService.Load();
        if (_saveData == null)
        {
            string uuid = Guid.NewGuid().ToString("N");
            Vector3 kps = _kps.transform.position;
            Vector3 random = _random.transform.position;
            Vector3 controller = _controller.transform.position;
            Vector3 counter = _counter.transform.position;
            Vector3 clock = _clock.transform.position;

            _saveData = SaveData.Initialize(uuid, kps, random, controller, counter, clock);
        }

        IBeatorajaManager beatorajaManager = ServiceLocator.GetInstance<IBeatorajaManager>();
        beatorajaManager.SetSaveDataValue(_saveData.beatoraja);

        IDisplaySettingsManager displaySettingsManager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        displaySettingsManager.SetSaveDataValue(_saveData.displaySettings);

        IKeySettingsManager keySettingsManager = ServiceLocator.GetInstance<IKeySettingsManager>();
        keySettingsManager.SetSaveDataValue(_saveData.keySettings);

        ITotalCountManager totalCountManager = ServiceLocator.GetInstance<ITotalCountManager>();
        totalCountManager.LoadTotalCountAndSetUUID(_saveData.uuid);
    }

    private void OnApplicationQuit()
    {
        IBeatorajaManager beatorajaManager = ServiceLocator.GetInstance<IBeatorajaManager>();
        _saveData.beatoraja = beatorajaManager.GetSaveDataValue();

        IDisplaySettingsManager displaySettingsManager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        _saveData.displaySettings = displaySettingsManager.GetSaveDataValue();

        IKeySettingsManager keySettingsManager = ServiceLocator.GetInstance<IKeySettingsManager>();
        _saveData.keySettings = keySettingsManager.GetSaveDataValue();

        SaveDataService.Save(_saveData);
    }
}
