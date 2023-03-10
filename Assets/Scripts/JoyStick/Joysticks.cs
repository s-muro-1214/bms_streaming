using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;

public static class Joysticks
{
    public static List<ButtonControl> Buttons { get; private set; } = new();
    public static StickControl Scratch { get; set; }
}
