using UnityEngine;

public class CounterTransform : ObjectTransform
{
    protected override Vector3 GetInitialPosition()
    {
        return SaveDataController.I.GetDisplaySettings().counter.currentPosition;
    }

    protected override void SetCurrentPosition()
    {
        DisplaySettings settings = SaveDataController.I.GetDisplaySettings();
        settings.counter.currentPosition = transform.position;

        SaveDataController.I.SetDisplaySettings(settings);
    }
}
