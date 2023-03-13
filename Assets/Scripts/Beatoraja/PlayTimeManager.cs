using UnityEngine;

public class PlayTimeManager : MonoBehaviour, IPlayTimeManager
{
    private float _playTime = 0.0f;

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<IPlayTimeManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<IPlayTimeManager>(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if (StateService.GetState() != BeatorajaState.PLAY)
        {
            return;
        }

        // プレイ時間の加算
        _playTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IPlayTimeManager>(this);
    }

    public float GetPlayTime()
    {
        return _playTime;
    }
}

public interface IPlayTimeManager
{
    public float GetPlayTime();
}