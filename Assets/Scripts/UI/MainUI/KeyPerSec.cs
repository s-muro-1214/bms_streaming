using System.Collections;
using TMPro;
using UnityEngine;

public class KeyPerSec : MonoBehaviour
{
    // 表示の更新間隔(sec)
    private readonly float _span = 1.0f;

    private readonly float _barMax = 50.0f;
    private int _maxValue = 0;

    [SerializeField] private TextMeshProUGUI _kpsMax;
    [SerializeField] private TextMeshProUGUI _kpsCurrent;
    [SerializeField] private MeshRenderer _renderer;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(DisplayKPS());
    }

    private void Update()
    {
        if (!BeatorajaWatcher.I.State.Equals("PLAY"))
        {
            _maxValue = 0;
            _kpsMax.text = _maxValue.ToString();
        }
    }

    private IEnumerator DisplayKPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(_span);

            int kps = KeyLogger.I.TotalKeyPressed;
            _kpsCurrent.text = kps.ToString();
            _renderer.material.SetFloat("_Fillpercentage", kps / _barMax);

            KeyLogger.I.TotalKeyPressed = 0;

            if (kps > _maxValue)
            {
                _maxValue = kps;
                _kpsMax.text = kps.ToString();
                _kpsMax.color = new Color32(255, 38, 38, 255);
            }
            else
            {
                _kpsMax.color = Color.white;
            }
        }
    }
}
