using System.Collections.Generic;
using UnityEngine;

public class KeySettingsManager : MonoBehaviour, IKeySettingsManager
{
    private KeySettings _settings;

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<IKeySettingsManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<IKeySettingsManager>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IKeySettingsManager>(this);
    }

    public KeySettings GetKeySettings()
    {
        return _settings;
    }

    public void SetKeySettings(KeySettings settings)
    {
        _settings = settings;
    }

    public List<string> GetButtonSettings()
    {
        return new List<string>()
        {
            _settings.key1,
            _settings.key2,
            _settings.key3,
            _settings.key4,
            _settings.key5,
            _settings.key6,
            _settings.key7
        };
    }

    public string GetScratchName()
    {
        return _settings.scratch;
    }
}

public interface IKeySettingsManager
{
    public KeySettings GetKeySettings();

    public void SetKeySettings(KeySettings settings);

    public List<string> GetButtonSettings();

    public string GetScratchName();
}
