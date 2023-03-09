using UnityEngine;

public class RandomViewerTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return SaveDataController.I.GetDisplaySettings().randomPattern.currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        DisplaySettings settings = SaveDataController.I.GetDisplaySettings();
        settings.randomPattern.currentPosition = transform.position;

        SaveDataController.I.SetDisplaySettings(settings);
    }
}
