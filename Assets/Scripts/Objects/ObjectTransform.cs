using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class ObjectTransform : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Vector3 _offset;

    protected IDisplaySettingsManager _manager;

    protected abstract Vector3 GetInitialPosition();

    protected abstract void SetCurrentPosition();

    private void Start()
    {
        _manager = ServiceLocator.GetInstance<IDisplaySettingsManager>();
        transform.position = GetInitialPosition();
    }

    private void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        _offset = transform.position - Camera.main.ScreenToWorldPoint(screenPos);
    }

    private void OnMouseDrag()
    {
        var screen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        transform.position = Camera.main.ScreenToWorldPoint(screen) + _offset;
    }

    private void OnMouseUp()
    {
        SetCurrentPosition();
    }
}
