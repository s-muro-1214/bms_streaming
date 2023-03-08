using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSetting : MonoBehaviour
{
    private readonly List<Color32> _colors = new()
    {
        new Color32(0, 255, 0, 255),
        new Color32(128, 128, 128, 255),
        new Color32(0, 0, 0, 255)
    };

    private int _index = 0;

    [SerializeField] private SpriteRenderer _renderer;

    // Start is called before the first frame update
    private void Start()
    {
        BackgroundColor color = SaveDataController.I.GetDisplaySettings().color;
        _renderer.color = new Color32(color.red, color.green, color.blue, color.alpha);
        if (color.green == 255)
        {
            _index = 0;
        }
        else if (color.green == 128)
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
