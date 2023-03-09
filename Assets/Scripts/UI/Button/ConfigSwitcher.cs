using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSwitcher : MonoBehaviour
{

    [SerializeField] private Toggle[] _toggles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        foreach(Toggle toggle in _toggles)
        {
            toggle.gameObject.SetActive(!toggle.gameObject.activeSelf);
        }
    }
}
