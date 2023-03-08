using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class BeatorajaWatcher : MonoBehaviour
{
    // ファイル監視用
    private FileSystemWatcher _watcher;
    private DateTime _lastWriteTimeSave = DateTime.Now;
    private readonly string _randomFile = "current_random.txt";
    private readonly string _stateFile = "current_state.txt";

    private void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _watcher = new FileSystemWatcher(SaveDataController.I.GetBeatoraja().directory)
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
    private void Created(object sender, FileSystemEventArgs e)
    {
        using var fs = new StreamReader(e.FullPath, Encoding.GetEncoding("UTF-8"));
        if (_randomFile.Equals(e.Name))
        {
            RandomPatterns = fs.ReadLine().Split(',').Select(int.Parse).ToList();
            IsRandomChanged = true;
        }
        else if (_stateFile.Equals(e.Name))
        {
            State = fs.ReadLine();
        }
    }

    // ファイルが更新されたとき
    private void Changed(object sender, FileSystemEventArgs e)
    {
        var file = new FileInfo(e.FullPath);

        if (file.LastWriteTime.Subtract(_lastWriteTimeSave) < new TimeSpan(0, 0, 0, 1)) return;
        _lastWriteTimeSave = file.LastWriteTime;

        using var fs = new StreamReader(e.FullPath, Encoding.GetEncoding("UTF-8"));
        if (_randomFile.Equals(e.Name))
        {
            RandomPatterns = fs.ReadLine().Split(',').Select(int.Parse).ToList();
            IsRandomChanged = true;
        }
        else if (_stateFile.Equals(e.Name))
        {
            State = fs.ReadLine();
        }
    }

    // ファイルが削除されたとき
    private void Deleted(object sender, FileSystemEventArgs e)
    {
        if (_randomFile.Equals(e.Name))
        {
            // 無地の画像を指定
            RandomPatterns = new() { 8, 8, 8, 8, 8, 8, 8 };
            IsRandomChanged = true;
        }
        else if (_stateFile.Equals(e.Name))
        {
            State = "";
        }
    }

    public static BeatorajaWatcher I { get; private set; }

    public List<int> RandomPatterns { get; private set; } = new(8);

    public bool IsRandomChanged { get; set; } = false;

    public string State { get; private set; } = "";
}
