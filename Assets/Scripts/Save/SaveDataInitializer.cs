using UnityEngine;

public class SaveDataInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _kps;
    [SerializeField] private GameObject _random;
    [SerializeField] private GameObject _controller;
    [SerializeField] private GameObject _counter;
    [SerializeField] private GameObject _clock;

    private void Start()
    {
        // json読み込み処理
        SaveData saveData = SaveDataService.Load();
        if (saveData == null)
        {
            Vector3 kps = _kps.transform.position;
            Vector3 random = _random.transform.position;
            Vector3 controller = _controller.transform.position;
            Vector3 counter = _counter.transform.position;
            Vector3 clock = _clock.transform.position;

            saveData = SaveData.Initialize(kps, random, controller, counter, clock);
        }

        IBeatorajaManager beatorajaManager = ServiceLocator.GetInstance<IBeatorajaManager>();
        beatorajaManager.SetSaveDataValue(saveData.beatoraja);

        IDisplaySettingsManager displaySettingsManager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        displaySettingsManager.SetSaveDataValue(saveData.displaySettings);

        IKeySettingsManager keySettingsManager = ServiceLocator.GetInstance<IKeySettingsManager>();
        keySettingsManager.SetSaveDataValue(saveData.keySettings);

        ITotalCountManager totalCountManager = ServiceLocator.GetInstance<ITotalCountManager>();
        totalCountManager.SetSaveDataValue(saveData.totalCount);
    }

    private void OnApplicationQuit()
    {
        var saveData = SaveData.New();

        IBeatorajaManager beatorajaManager = ServiceLocator.GetInstance<IBeatorajaManager>();
        saveData.beatoraja = beatorajaManager.GetSaveDataValue();

        IDisplaySettingsManager displaySettingsManager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        saveData.displaySettings = displaySettingsManager.GetSaveDataValue();

        IKeySettingsManager keySettingsManager = ServiceLocator.GetInstance<IKeySettingsManager>();
        saveData.keySettings = keySettingsManager.GetSaveDataValue();

        ITotalCountManager totalCountManager = ServiceLocator.GetInstance<ITotalCountManager>();
        IKeyCountManager keyCountManager = ServiceLocator.GetInstance<IKeyCountManager>();
        totalCountManager.AddTotalCount(keyCountManager.GetAllKeyCountToday());
        saveData.totalCount = totalCountManager.GetSaveDataValue();

        SaveDataService.Save(saveData);
    }
}
