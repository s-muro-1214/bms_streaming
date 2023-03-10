using UnityEngine;

public class KPSTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return _manager.GetUISettingKPS().currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        UISetting setting = _manager.GetUISettingKPS();
        setting.currentPosition = transform.position;

        _manager.SetUISettingKPS(setting);
    }
}
