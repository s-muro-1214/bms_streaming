using UnityEngine;

public class ControllerTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return _manager.GetUISettingController().currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        UISetting setting = _manager.GetUISettingController();
        setting.currentPosition = transform.position;

        _manager.SetUISettingController(setting);
    }
}
