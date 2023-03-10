using UnityEngine;

public class ClockTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return _manager.GetUISettingClock().currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        UISetting setting = _manager.GetUISettingClock();
        setting.currentPosition = transform.position;

        _manager.SetUISettingClock(setting);
    }
}
