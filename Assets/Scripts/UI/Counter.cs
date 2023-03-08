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
        long totalCount = SaveDataController.I.SaveData.totalCount.GetTotalSum();

        _total.text = totalCount.ToString();
        _today.text = "0";
        _density.text = "0.0";
    }

    // Update is called once per frame
    private void OnGUI()
    {
        int todayCount = KeyLogger.I.TodayCounts.Sum();
        long totalCount = SaveDataController.I.SaveData.totalCount.GetTotalSum() + todayCount;
        double density = Math.Round(todayCount / Time.time, 1);

        _total.text = totalCount.ToString();
        _today.text = todayCount.ToString();
        _density.text = density.ToString();
    }
}
