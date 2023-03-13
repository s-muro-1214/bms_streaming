using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotalCountManager : MonoBehaviour, ITotalCountManager
{
    private static readonly string _baseUri = "https://izuna.net/db_supporter/savedata";

    private TotalCount _totalCount;

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<ITotalCountManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<ITotalCountManager>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ITotalCountManager>(this);
    }

    public void LoadTotalCount(string uuid)
    {
        StartCoroutine(LoadTotalCountFromDB(uuid));
    }

    public void SaveTotalCount(string uuid, List<int> todayCounts)
    {
        AddTotalCount(todayCounts);

        StartCoroutine(SaveTotalCountToDB(uuid));
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

    private IEnumerator LoadTotalCountFromDB(string uuid)
    {
        WWWForm form = new();
        form.AddField("uuid", uuid);

        using var request = UnityWebRequest.Post($"{_baseUri}/load.php", form);
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

    private IEnumerator SaveTotalCountToDB(string uuid)
    {
        WWWForm form = new();
        form.AddField("uuid", uuid);
        form.AddField("scratchL", _totalCount.scratchL);
        form.AddField("scratchR", _totalCount.scratchR);
        form.AddField("key1", _totalCount.key1);
        form.AddField("key2", _totalCount.key2);
        form.AddField("key3", _totalCount.key3);
        form.AddField("key4", _totalCount.key4);
        form.AddField("key5", _totalCount.key5);
        form.AddField("key6", _totalCount.key6);
        form.AddField("key7", _totalCount.key7);

        using var request = UnityWebRequest.Post($"{_baseUri}/save.php", form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();
    }
}

public interface ITotalCountManager
{
    public void LoadTotalCount(string uuid);

    public void SaveTotalCount(string uuid, List<int> todayCounts);

    public long GetTotalSum();

    public int GetKeyCount(int index);

    public bool IsTotalCountLoading();
}
