using System;
using System.Linq;
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
        long totalCount = SaveDataController.I.GetTotalCount().GetTotalSum();

        _total.text = totalCount.ToString();
        _today.text = "0";
        _density.text = "0.0";
    }

    // Update is called once per frame
    private void OnGUI()
    {
        int todayCount = KeyLogger.I.TodayCounts.Sum();
        long totalCount = SaveDataController.I.GetTotalCount().GetTotalSum() + todayCount;
        double density = Math.Round(todayCount / KeyLogger.I.PlayTime, 1, MidpointRounding.AwayFromZero);

        if (Double.IsNaN(density))
        {
            density = 0.0;
        }

        _total.text = totalCount.ToString();
        _today.text = todayCount.ToString();
        _density.text = density.ToString("F1");
    }
}
