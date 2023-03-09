using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DisplayKeyCount : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Material _matSelected;
    private Material _matReleased;

    private bool _isEntered = false;
    private bool _isFocused = false;

    [SerializeField] private Image _image;
    [SerializeField] private int _index;
    [SerializeField] private TextMeshProUGUI _count;

    // Start is called before the first frame update
    private async void Start()
    {
        _matSelected = await Addressables.LoadAssetAsync<Material>("MatSelected").Task;
        _matReleased = await Addressables.LoadAssetAsync<Material>("MatReleased").Task;
    }

    private void OnApplicationFocus(bool focus)
    {
        _isFocused = focus;
    }

    private void Update()
    {
        if (!_isFocused)
        {
            return;
        }

        _image.material = _isEntered ? _matSelected : _matReleased;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isEntered = true;
        _count.text = $"COUNT: {SaveDataController.I.GetTotalCount().GetKeyCount(_index)}";
        _count.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isEntered = false;
        _count.text = "";
        _count.gameObject.SetActive(false);
    }
}
