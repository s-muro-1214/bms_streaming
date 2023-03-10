using UnityEngine;

public class CounterTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return _manager.GetUISettingCounter().currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        UISetting setting = _manager.GetUISettingCounter();
        setting.currentPosition = transform.position;

        _manager.SetUISettingCounter(setting);
    }
}
