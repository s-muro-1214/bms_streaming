using UnityEngine;

public class DisplaySettingsManager : MonoBehaviour, IDisplaySettingsManager
{
    private DisplaySettings _settings;

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<IDisplaySettingsManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<IDisplaySettingsManager>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IDisplaySettingsManager>(this);
    }

    public DisplaySettings GetDisplaySettings()
    {
        return _settings;
    }

    public void SetDisplaySettings(DisplaySettings settings)
    {
        _settings = settings;
    }

    public UISetting GetUISettingKPS()
    {
        return _settings.kps;
    }

    public void SetUISettingKPS(UISetting setting)
    {
        _settings.kps = setting;
    }

    public UISetting GetUISettingRandom()
    {
        return _settings.random;
    }

    public void SetUISettingRandom(UISetting setting)
    {
        _settings.random = setting;
    }

    public UISetting GetUISettingController()
    {
        return _settings.controller;
    }

    public void SetUISettingController(UISetting setting)
    {
        _settings.controller = setting;
    }

    public UISetting GetUISettingCounter()
    {
        return _settings.counter;
    }

    public void SetUISettingCounter(UISetting setting)
    {
        _settings.counter = setting;
    }

    public UISetting GetUISettingClock()
    {
        return _settings.clock;
    }

    public void SetUISettingClock(UISetting setting)
    {
        _settings.clock = setting;
    }

    public Color32 GetBackgroundColor()
    {
        return _settings.backgroundColor;
    }

    public void SetBackgroundColor(Color32 color)
    {
        _settings.backgroundColor = color;
    }
}

public interface IDisplaySettingsManager
{
    public DisplaySettings GetDisplaySettings();

    public void SetDisplaySettings(DisplaySettings settings);

    public UISetting GetUISettingKPS();

    public void SetUISettingKPS(UISetting setting);

    public UISetting GetUISettingRandom();

    public void SetUISettingRandom(UISetting setting);

    public UISetting GetUISettingController();

    public void SetUISettingController(UISetting setting);

    public UISetting GetUISettingCounter();

    public void SetUISettingCounter(UISetting setting);

    public UISetting GetUISettingClock();

    public void SetUISettingClock(UISetting setting);

    public Color32 GetBackgroundColor();

    public void SetBackgroundColor(Color32 color);
}
