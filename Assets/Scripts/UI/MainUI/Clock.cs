using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // 表示の更新間隔(sec)
    private readonly float _span = 1.0f;

    [SerializeField] private TextMeshProUGUI _date;
    [SerializeField] private TextMeshProUGUI _time;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateDateTime();

        StartCoroutine(DisplayClock());
    }

    private IEnumerator DisplayClock()
    {
        while (true)
        {
            yield return new WaitForSeconds(_span);

            UpdateDateTime();
        }
    }

    private void UpdateDateTime()
    {
        _date.text = DateTime.Now.ToString("yyyy/MM/dd");
        _time.text = DateTime.Now.ToString("HH:mm");
    }
}
