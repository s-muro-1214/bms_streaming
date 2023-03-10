using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private Material _matPressed;
    private Material _matReleased;

    [SerializeField] private Image _scratchL;
    [SerializeField] private Image _scratchR;
    [SerializeField] private Image _key1;
    [SerializeField] private Image _key2;
    [SerializeField] private Image _key3;
    [SerializeField] private Image _key4;
    [SerializeField] private Image _key5;
    [SerializeField] private Image _key6;
    [SerializeField] private Image _key7;

    // Start is called before the first frame update
    private async void Start()
    {
        _matPressed = await Addressables.LoadAssetAsync<Material>("MatPressed").Task;
        _matReleased = await Addressables.LoadAssetAsync<Material>("MatReleased").Task;
    }

    // Update is called once per frame
    private void OnGUI()
    {
        IKeyCountManager manager = ServiceLocator.GetInstance<IKeyCountManager>();
        List<bool> status = manager.GetAllKeyStatus();

        _scratchL.material = status[0] ? _matPressed : _matReleased;
        _scratchR.material = status[1] ? _matPressed : _matReleased;
        _key1.material = status[2] ? _matPressed : _matReleased;
        _key2.material = status[3] ? _matPressed : _matReleased;
        _key3.material = status[4] ? _matPressed : _matReleased;
        _key4.material = status[5] ? _matPressed : _matReleased;
        _key5.material = status[6] ? _matPressed : _matReleased;
        _key6.material = status[7] ? _matPressed : _matReleased;
        _key7.material = status[8] ? _matPressed : _matReleased;
    }
}
