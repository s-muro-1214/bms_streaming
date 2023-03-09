using UnityEngine;

public class ClockTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return SaveDataController.I.GetDisplaySettings().clock.currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        DisplaySettings settings = SaveDataController.I.GetDisplaySettings();
        settings.clock.currentPosition = transform.position;

        SaveDataController.I.SetDisplaySettings(settings);
    }
}
