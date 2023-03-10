using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class KeyCountManager : MonoBehaviour, IKeyCountManager
{
    private bool _isScratchActive = false;
    private Vector2 _oldScratchPos = new(2f, 2f);
    private Vector2 _initScratchPos = new(2f, 2f);

    private int _totalKeyPressedCount = 0;

    private readonly Dictionary<InputControl, IKeyStatus> _joystickStatusPairs = new();

    private void Awake()
    {
        if (ServiceLocator.IsRegistered<IKeyCountManager>())
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Register<IKeyCountManager>(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _joystickStatusPairs.Add(Joysticks.Scratch, new ScratchStatus());
        foreach (ButtonControl button in Joysticks.Buttons)
        {
            _joystickStatusPairs.Add(button, new ButtonStatus());
        }

        _oldScratchPos = Joysticks.Scratch.ReadValue();
        _initScratchPos = _oldScratchPos;
    }

    // Update is called once per frame
    private void Update()
    {
        if (BeatorajaWatcher.GetState() != BeatorajaState.PLAY)
        {
            return;
        }

        // 鍵盤の回数カウント
        for (int i = 0; i < 7; i++)
        {
            ButtonControl button = Joysticks.Buttons[i];
            var buttonStatus = (ButtonStatus)_joystickStatusPairs[button];
            if (button.isPressed)
            {
                if (!buttonStatus.IsPressed)
                {
                    buttonStatus.IsPressed = true;
                    _totalKeyPressedCount++;
                    buttonStatus.Count += 1;
                }
            }
            else if (!Joysticks.Buttons[i].isPressed && buttonStatus.IsPressed)
            {
                buttonStatus.IsPressed = false;
            }
        }

        // 皿の回数カウント
        // 自分の環境 -> 定常状態：(-0.71,0.71)。時計回り：(-0.01,1.00)。反時計回り：(0.00,1.00)ぽい
        Vector2 currentScratchPos = Joysticks.Scratch.ReadValue();
        var scratchStatus = (ScratchStatus)_joystickStatusPairs[Joysticks.Scratch];
        if (_oldScratchPos.x != currentScratchPos.x && currentScratchPos.y != _initScratchPos.y)
        {
            _isScratchActive = true;
            _oldScratchPos = currentScratchPos;
            _totalKeyPressedCount++;

            // 今日の回数カウント用分岐
            if (currentScratchPos.x < 0.00f) // xがマイナスの場合は時計回り(のはず)
            {
                scratchStatus.CountR += 1;
                scratchStatus.IsScratchLRotated = false;
                scratchStatus.IsScratchRRotated = true;
            }
            else
            {
                scratchStatus.CountL += 1;
                scratchStatus.IsScratchLRotated = true;
                scratchStatus.IsScratchRRotated = false;
            }
        }

        if (currentScratchPos == _initScratchPos && _isScratchActive)
        {
            _isScratchActive = false;
            _oldScratchPos = currentScratchPos;
            scratchStatus.IsScratchLRotated = false;
            scratchStatus.IsScratchRRotated = false;
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<IKeyCountManager>(this);
    }

    public int GetTotalKeyPressedCount()
    {
        return _totalKeyPressedCount;
    }

    public void ResetTotalKeyPressedCount()
    {
        _totalKeyPressedCount = 0;
    }

    public List<int> GetAllKeyCountToday()
    {
        List<int> todayCounts = new();

        var scratchStatus = (ScratchStatus)_joystickStatusPairs[Joysticks.Scratch];
        todayCounts.Add(scratchStatus.CountL);
        todayCounts.Add(scratchStatus.CountR);

        foreach (ButtonControl button in Joysticks.Buttons)
        {
            var buttonStatus = (ButtonStatus)_joystickStatusPairs[button];
            todayCounts.Add(buttonStatus.Count);
        }

        return todayCounts;
    }

    public int GetTodayCountSum()
    {
        return GetAllKeyCountToday().Sum();
    }

    public List<bool> GetAllKeyStatus()
    {
        List<bool> allKeyStatus = new();

        var scratchStatus = (ScratchStatus)_joystickStatusPairs[Joysticks.Scratch];
        allKeyStatus.Add(scratchStatus.IsScratchLRotated);
        allKeyStatus.Add(scratchStatus.IsScratchRRotated);

        foreach (ButtonControl button in Joysticks.Buttons)
        {
            var buttonStatus = (ButtonStatus)_joystickStatusPairs[button];
            allKeyStatus.Add(buttonStatus.IsPressed);
        }

        return allKeyStatus;
    }
}

public interface IKeyCountManager
{
    public int GetTotalKeyPressedCount();

    public void ResetTotalKeyPressedCount();

    public List<int> GetAllKeyCountToday();

    public int GetTodayCountSum();

    public List<bool> GetAllKeyStatus();
}