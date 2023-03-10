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

    public Beatoraja GetBeatoraja()
    {
        return _beatoraja;
    }

    public void SetBeatoraja(Beatoraja beatoraja)
    {
        _beatoraja = beatoraja;
    }

    public string GetBeatorajaDirectory()
    {
        return _beatoraja.directory;
    }
}

public interface IBeatorajaManager
{
    public Beatoraja GetBeatoraja();

    public void SetBeatoraja(Beatoraja beatoraja);

    public string GetBeatorajaDirectory();
}
