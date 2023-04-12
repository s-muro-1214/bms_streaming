using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class JoystickInitializer : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        if (Joystick.current == null)
        {
            _canvas.enabled = true;
            Application.Quit();
        }

        if (!Joystick.current.name.Contains("Controller INF"))
        {
            _canvas.enabled = true;
            Application.Quit();
        }

        IKeySettingsManager manager = ServiceLocator.GetInstance<IKeySettingsManager>();
        foreach (InputControl key in Joystick.current.allControls)
        {
            if (manager.GetButtonSettings().Contains(key.name))
            {
                Joysticks.Buttons.Add((ButtonControl)key);
            }
            else if (key.name.Equals(manager.GetScratchName()))
            {
                Joysticks.Scratch = (StickControl)key;
            }
        }
    }
}
