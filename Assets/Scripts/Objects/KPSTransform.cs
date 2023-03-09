using UnityEngine;

public class KPSTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return SaveDataController.I.GetDisplaySettings().kps.currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        DisplaySettings settings = SaveDataController.I.GetDisplaySettings();
        settings.kps.currentPosition = transform.position;

        SaveDataController.I.SetDisplaySettings(settings);
    }
}
