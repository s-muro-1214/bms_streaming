using UnityEngine;

public class RandomViewerTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return _manager.GetUISettingRandom().currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        UISetting setting = _manager.GetUISettingRandom();
        setting.currentPosition = transform.position;

        _manager.SetUISettingRandom(setting);
    }
}
