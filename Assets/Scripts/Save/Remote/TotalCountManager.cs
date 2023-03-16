using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotalCountManager : MonoBehaviour, ITotalCountManager
{
    private static readonly string _baseUri = "https://izuna.net/db_supporter";

    private TotalCount _totalCount;
    private string _uuid;
    private bool _isAlreadySaved = false;

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<ITotalCountManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<ITotalCountManager>(this);
    }

    private void Update()
    {
        if(StateService.CurrentState == BeatorajaState.RESULT && !_isAlreadySaved)
        {
            IKeyCountManager keyCountManager = ServiceLocator.GetInstance<IKeyCountManager>();
            SaveTotalCount(keyCountManager.GetAllKeyCountToday());
            _isAlreadySaved = true;
        }
        else if (StateService.CurrentState != BeatorajaState.RESULT)
        {
            _isAlreadySaved = false;
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ITotalCountManager>(this);
    }

    public void LoadTotalCountAndSetUUID(string uuid)
    {
        _uuid = uuid;
        StartCoroutine(LoadTotalCountFromDB());
    }

    public void SaveTotalCount(List<int> todayCounts)
    {
        StartCoroutine(SaveTotalCountToDB(todayCounts));
    }

    public long GetTotalSum()
    {
        return _totalCount.GetTotalCountSum();
    }

    public int GetKeyCount(int index)
    {
        return index switch
        {
            0 => _totalCount.scratchL,
            1 => _totalCount.scratchR,
            2 => _totalCount.key1,
            3 => _totalCount.key2,
            4 => _totalCount.key3,
            5 => _totalCount.key4,
            6 => _totalCount.key5,
            7 => _totalCount.key6,
            8 => _totalCount.key7,
            _ => 0,
        };
    }

    public bool IsTotalCountLoading()
    {
        return _totalCount == null;
    }

    private void AddTotalCount(List<int> todayCounts)
    {
        _totalCount.scratchL += todayCounts[0];
        _totalCount.scratchR += todayCounts[1];
        _totalCount.key1 += todayCounts[2];
        _totalCount.key2 += todayCounts[3];
        _totalCount.key3 += todayCounts[4];
        _totalCount.key4 += todayCounts[5];
        _totalCount.key5 += todayCounts[6];
        _totalCount.key6 += todayCounts[7];
        _totalCount.key7 += todayCounts[8];
    }

    private IEnumerator LoadTotalCountFromDB()
    {
        WWWForm form = new();
        form.AddField("token", Configuration.TOKEN);
        form.AddField("uuid", _uuid);

        using var request = UnityWebRequest.Post($"{_baseUri}/savedata/load.php", form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            _totalCount = JsonUtility.FromJson<TotalCount>(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            _totalCount = new();
        }

        request.Dispose();
    }

    private IEnumerator SaveTotalCountToDB(List<int> todayCounts)
    {
        WWWForm form = new();
        form.AddField("token", Configuration.TOKEN);
        form.AddField("uuid", _uuid);
        form.AddField("scratchL", _totalCount.scratchL + todayCounts[0]);
        form.AddField("scratchR", _totalCount.scratchR + todayCounts[1]);
        form.AddField("key1", _totalCount.key1 + todayCounts[2]);
        form.AddField("key2", _totalCount.key2 + todayCounts[3]);
        form.AddField("key3", _totalCount.key3 + todayCounts[4]);
        form.AddField("key4", _totalCount.key4 + todayCounts[5]);
        form.AddField("key5", _totalCount.key5 + todayCounts[6]);
        form.AddField("key6", _totalCount.key6 + todayCounts[7]);
        form.AddField("key7", _totalCount.key7 + todayCounts[8]);

        using var request = UnityWebRequest.Post($"{_baseUri}/savedata/save.php", form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        } else
        {
            Debug.Log(request.result);
        }

        request.Dispose();
    }
}

public interface ITotalCountManager
{
    public void LoadTotalCountAndSetUUID(string uuid);

    public void SaveTotalCount(List<int> todayCounts);

    public long GetTotalSum();

    public int GetKeyCount(int index);

    public bool IsTotalCountLoading();
}
