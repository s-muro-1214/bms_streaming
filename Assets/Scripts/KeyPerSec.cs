using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPerSec : MonoBehaviour
{
    // 表示の更新間隔(sec)
    private readonly float _span = 1.0f;

    [SerializeField] private TextMeshProUGUI _kps;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(DisplayKPS());
    }

    private IEnumerator DisplayKPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(_span);

            _kps.text = KeyLogger.I.TotalKeyPressed.ToString();
            KeyLogger.I.TotalKeyPressed = 0;
        }
    }
}
