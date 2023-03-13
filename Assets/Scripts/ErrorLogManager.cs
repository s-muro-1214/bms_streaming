using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorLogManager : MonoBehaviour
{
    private void OnEnable()
    {
        if (Application.isEditor)
        {
            return;
        }

        Application.logMessageReceived += SaveReceivedLog;
    }

    private void SaveReceivedLog(string logText, string stackTrace, LogType logType)
    {
        if (logType != LogType.Error && logType != LogType.Exception)
        {
            return;
        }

        StringBuilder builder = new StringBuilder();
        builder.AppendLine("Error Log:");
        builder.AppendLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        builder.AppendFormat("Game Name: {0}", Application.productName);
        builder.AppendLine();
        builder.AppendFormat("Game Version: {0}", Application.version);
        builder.AppendLine();
        builder.AppendFormat("Scene Name: {0}", SceneManager.GetActiveScene().name);
        builder.AppendLine();
        builder.AppendFormat("Error Type: {0}", logType.ToString());
        builder.AppendLine();
        builder.AppendFormat("Error Text: {0}", logText);
        builder.AppendLine();
        builder.AppendLine("Stacktrace:");
        builder.AppendLine(stackTrace);

        File.WriteAllText($"{Application.persistentDataPath}/errorlog.txt", builder.ToString());
    }
}
