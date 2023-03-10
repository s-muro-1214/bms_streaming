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
        beatorajaManager.SetBeatoraja(saveData.beatoraja);

        IDisplaySettingsManager displaySettingsManager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        displaySettingsManager.SetDisplaySettings(saveData.displaySettings);

        IKeySettingsManager keySettingsManager = ServiceLocator.GetInstance<IKeySettingsManager>();
        keySettingsManager.SetKeySettings(saveData.keySettings);

        ITotalCountManager totalCountManager = ServiceLocator.GetInstance<ITotalCountManager>();
        totalCountManager.SetTotalCount(saveData.totalCount);

        BeatorajaWatcher.Init();
    }

    private void OnApplicationQuit()
    {
        var saveData = SaveData.New();

        IBeatorajaManager beatorajaManager = ServiceLocator.GetInstance<IBeatorajaManager>();
        saveData.beatoraja = beatorajaManager.GetBeatoraja();

        IDisplaySettingsManager displaySettingsManager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        saveData.displaySettings = displaySettingsManager.GetDisplaySettings();

        IKeySettingsManager keySettingsManager = ServiceLocator.GetInstance<IKeySettingsManager>();
        saveData.keySettings = keySettingsManager.GetKeySettings();

        ITotalCountManager totalCountManager = ServiceLocator.GetInstance<ITotalCountManager>();
        IKeyCountManager keyCountManager = ServiceLocator.GetInstance<IKeyCountManager>();
        totalCountManager.AddTotalCount(keyCountManager.GetAllKeyCountToday());
        saveData.totalCount = totalCountManager.GetTotalCount();

        SaveDataService.Save(saveData);
    }
}
