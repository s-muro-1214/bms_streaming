using System;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _total;
    [SerializeField] private TextMeshProUGUI _today;
    [SerializeField] private TextMeshProUGUI _density;

    // Start is called before the first frame update
    private void Start()
    {
        ITotalCountManager manager = ServiceLocator.GetInstance<ITotalCountManager>();
        long totalCount = manager.GetTotalSum();

        _total.text = totalCount.ToString();
        _today.text = "0";
        _density.text = "0.0";
    }

    // Update is called once per frame
    private void OnGUI()
    {
        IKeyCountManager keyCountManager = ServiceLocator.GetInstance<IKeyCountManager>();
        ITotalCountManager totalCountManager = ServiceLocator.GetInstance<ITotalCountManager>();
        IPlayTimeManager playTimeManager = ServiceLocator.GetInstance<IPlayTimeManager>();

        int todayCount = keyCountManager.GetTodayCountSum();
        long totalCount = totalCountManager.GetTotalSum() + todayCount;
        double density = Math.Round(todayCount / playTimeManager.GetPlayTime(), 1,
            MidpointRounding.AwayFromZero);

        if (Double.IsNaN(density))
        {
            density = 0.0;
        }

        _total.text = totalCount.ToString();
        _today.text = todayCount.ToString();
        _density.text = density.ToString("F1");
    }
}
