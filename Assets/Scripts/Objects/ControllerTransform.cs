using UnityEngine;

public class ControllerTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return SaveDataController.I.GetDisplaySettings().controller.currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        DisplaySettings settings = SaveDataController.I.GetDisplaySettings();
        settings.controller.currentPosition = transform.position;

        SaveDataController.I.SetDisplaySettings(settings);
    }
}
