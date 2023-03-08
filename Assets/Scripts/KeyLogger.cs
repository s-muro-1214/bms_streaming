using System.Collections.Generic;
using UnityEngine;

public class KeyLogger : MonoBehaviour
{
    private bool _isScratchActive = false;
    private Vector2 _oldScratchPos = new(2f, 2f);
    private Vector2 _initScratchPos = new(2f, 2f);

    private void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _oldScratchPos = BmsTool.I.Scratch.ReadValue();
        _initScratchPos = _oldScratchPos;
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: beatorajaのStateがPLAYの場合だけカウント

        // 鍵盤の回数カウント
        for (int i = 0; i < 7; i++)
        {
            if (BmsTool.I.ButtonControls[i].isPressed)
            {
                if (!Status[i + 2])
                {
                    Status[i + 2] = true;
                    TotalKeyPressed++;
                    TodayCounts[i + 2] += 1;
                }
            }
            else if (!BmsTool.I.ButtonControls[i].isPressed && Status[i + 2])
            {
                Status[i + 2] = false;
            }
        }

        // 皿の回数カウント
        // 自分の環境 -> 定常状態：(-0.71,0.71)。時計回り：(-0.01,1.00)。反時計回り：(0.00,1.00)ぽい
        Vector2 currentScratchPos = BmsTool.I.Scratch.ReadValue();
        if (_oldScratchPos.x != currentScratchPos.x && currentScratchPos.y != _initScratchPos.y)
        {
            _isScratchActive = true;
            _oldScratchPos = currentScratchPos;
            TotalKeyPressed++;

            // 今日の回数カウント用分岐
            if (currentScratchPos.x < 0.00f) // xがマイナスの場合は時計回り(のはず)
            {
                TodayCounts[1] += 1;
                Status[0] = false;
                Status[1] = true;
            }
            else
            {
                TodayCounts[0] += 1;
                Status[0] = true;
                Status[1] = false;
            }
        }

        if (currentScratchPos == _initScratchPos && _isScratchActive)
        {
            _isScratchActive = false;
            _oldScratchPos = currentScratchPos;
            Status[0] = false;
            Status[1] = false;
        }
    }

    public static KeyLogger I { get; private set; }

    public int TotalKeyPressed { get; set; }

    // 鍵盤と皿の状態保持用 皿↑、皿↓、1～7の順
    public List<bool> Status { get; } = new(9)
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false
    };

    // 各鍵盤と皿の打鍵回数(Today) 皿↑、皿↓、1～7の順
    public List<int> TodayCounts { get; } = new(9) { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
}
