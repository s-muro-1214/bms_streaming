using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSetting : MonoBehaviour
{
    private readonly List<Color32> _colors = new()
    {
        Color.green,
        Color.gray,
        Color.black
    };

    private int _index = 0;

    [SerializeField] private SpriteRenderer _renderer;

    // Start is called before the first frame update
    private void Start()
    {
        Color32 color = SaveDataController.I.GetDisplaySettings().backgroundColor;
        _renderer.color = color;
        if (color.g == 255)
        {
            _index = 0;
        }
        else if (color.g == 128)
        {
            _index = 1;
        }
        else
        {
            _index = 2;
        }
    }

    public void SwitchBgColor()
    {
        _index++;
        if(_index >= _colors.Count)
        {
            _index = 0;
        }

        _renderer.color = _colors[_index];
        SaveDataController.I.SetCurrentBgColor(_colors[_index]);
    }
}
