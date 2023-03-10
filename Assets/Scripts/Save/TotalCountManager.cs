using System.Collections.Generic;
using UnityEngine;

public class TotalCountManager : MonoBehaviour, ITotalCountManager
{
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

    public TotalCount GetTotalCount()
    {
        return _totalCount;
    }

    public void SetTotalCount(TotalCount totalCount)
    {
        _totalCount = totalCount;
    }

    public long GetTotalSum()
    {
        return _totalCount.scratchL + _totalCount.scratchR + _totalCount.key1
            + _totalCount.key2 + _totalCount.key3 + _totalCount.key4
            + _totalCount.key5 + _totalCount.key6 + _totalCount.key7;
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

    public void AddTotalCount(List<int> todayCounts)
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
}

public interface ITotalCountManager
{
    public TotalCount GetTotalCount();

    public void SetTotalCount(TotalCount totalCount);

    public long GetTotalSum();

    public int GetKeyCount(int index);

    public void AddTotalCount(List<int> todayCounts);
}
