using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class BeatorajaWatcher
{
    private static FileSystemWatcher _watcher;
    private static DateTime _lastWriteTimeSave = DateTime.Now;
    private static readonly string _randomFile = "current_random.txt";
    private static readonly string _stateFile = "current_state.txt";

    private static List<int> _randomPatterns = new();
    private static bool _isRandomChanged = false;
    private static BeatorajaState _state = BeatorajaState.NONE;

    public static void Init()
    {
        IBeatorajaManager manager = ServiceLocator.GetInstance<IBeatorajaManager>();
        _watcher = new FileSystemWatcher(manager.GetBeatorajaDirectory())
        {
            Filter = "*.txt",
            NotifyFilter = NotifyFilters.LastWrite,
            IncludeSubdirectories = false
        };
        _watcher.EnableRaisingEvents = true;

        _watcher.Created += new FileSystemEventHandler(Created);
        _watcher.Changed += new FileSystemEventHandler(Changed);
        _watcher.Deleted += new FileSystemEventHandler(Deleted);
    }

    // ファイルが作成されたとき
    private static void Created(object sender, FileSystemEventArgs e)
    {
        using var fs = new StreamReader(e.FullPath, Encoding.GetEncoding("UTF-8"));
        if (_randomFile.Equals(e.Name))
        {
            _randomPatterns = fs.ReadLine().Split(',').Select(int.Parse).ToList();
            _isRandomChanged = true;
        }
        else if (_stateFile.Equals(e.Name))
        {
            Enum.TryParse<BeatorajaState>(fs.ReadLine(), out _state);
        }
    }

    // ファイルが更新されたとき
    private static void Changed(object sender, FileSystemEventArgs e)
    {
        var file = new FileInfo(e.FullPath);

        if (file.LastWriteTime.Subtract(_lastWriteTimeSave) < new TimeSpan(0, 0, 0, 1)) return;
        _lastWriteTimeSave = file.LastWriteTime;

        using var fs = new StreamReader(e.FullPath, Encoding.GetEncoding("UTF-8"));
        if (_randomFile.Equals(e.Name))
        {
            _randomPatterns = fs.ReadLine().Split(',').Select(int.Parse).ToList();
            _isRandomChanged = true;
        }
        else if (_stateFile.Equals(e.Name))
        {
            Enum.TryParse<BeatorajaState>(fs.ReadLine(), out _state);
        }
    }

    // ファイルが削除されたとき
    private static void Deleted(object sender, FileSystemEventArgs e)
    {
        if (_randomFile.Equals(e.Name))
        {
            // 無地の画像を指定
            _randomPatterns = new() { 8, 8, 8, 8, 8, 8, 8 };
            _isRandomChanged = true;
        }
        else if (_stateFile.Equals(e.Name))
        {
            _state = BeatorajaState.NONE;
        }
    }

    public static List<int> GetRandomPatterns()
    {
        return _randomPatterns;
    }

    public static bool IsRandomChanged()
    {
        return _isRandomChanged;
    }

    public static void ResetRandomChanged()
    {
        _isRandomChanged = false;
    }

    public static BeatorajaState GetState()
    {
        return _state;
    }
}
