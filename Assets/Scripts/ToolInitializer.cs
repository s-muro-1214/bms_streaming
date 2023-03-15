using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

public static class ToolInitializer
{
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll", EntryPoint = "SetWindowText")]
    public static extern bool SetWindowText(System.IntPtr hwnd, System.String lpString);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern System.IntPtr FindWindow(System.String className, System.String windowName);
#endif


    public static void Init()
    {
        // FPS制限
        Application.targetFrameRate = 60;

        // 未処理例外の処理
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;

#if UNITY_STANDALONE_WIN
        // タイトルバーの変更
        System.IntPtr windowPointer = FindWindow(null, Application.productName);
        SetWindowText(windowPointer, $"{Application.productName} {Application.version}");
#endif
    }
}
