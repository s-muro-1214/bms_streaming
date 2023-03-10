using UnityEngine;

public class BeatorajaManager : MonoBehaviour, IBeatorajaManager
{
    private Beatoraja _beatoraja;

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<IBeatorajaManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<IBeatorajaManager>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IBeatorajaManager>(this);
    }

    public Beatoraja GetSaveDataValue()
    {
        return _beatoraja;
    }

    public void SetSaveDataValue(Beatoraja settings)
    {
        _beatoraja = settings;
    }

    public string GetBeatorajaDirectory()
    {
        return _beatoraja.directory;
    }
}

public interface IBeatorajaManager : ISaveDataManager<Beatoraja>
{
    public string GetBeatorajaDirectory();
}
