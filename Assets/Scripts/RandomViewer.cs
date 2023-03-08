using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class RandomViewer : MonoBehaviour
{
    // ファイル監視用
    private FileSystemWatcher _watcher;
    private DateTime _lastWriteTimeSave = DateTime.Now;
    private bool _isRandomChanged = false;

    private readonly List<Sprite> _sprites = new(8);
    private List<int> _randomPatterns = new(8);

    // 表示の更新間隔(秒)
    private readonly float _span = 1.0f;

    // 画像を表示するとこ
    [SerializeField] private Image _lane1;
    [SerializeField] private Image _lane2;
    [SerializeField] private Image _lane3;
    [SerializeField] private Image _lane4;
    [SerializeField] private Image _lane5;
    [SerializeField] private Image _lane6;
    [SerializeField] private Image _lane7;

    private async void Start()
    {
        _watcher = new FileSystemWatcher("D:/beatoraja0.8.4-jre-win64", "current_random.txt")
        {
            NotifyFilter = NotifyFilters.LastWrite,
            IncludeSubdirectories = false
        };
        _watcher.EnableRaisingEvents = true;

        _watcher.Created += new FileSystemEventHandler(Created);
        _watcher.Changed += new FileSystemEventHandler(Changed);
        _watcher.Deleted += new FileSystemEventHandler(Deleted);

        List<string> keys = new()
        {
            "Key1",
            "Key2",
            "Key3",
            "Key4",
            "Key5",
            "Key6",
            "Key7",
            "KeyNone"
        };

        foreach (string key in keys)
        {
            _sprites.Add(await Addressables.LoadAssetAsync<Sprite>(key).Task);
        }

        StartCoroutine(DisplayRandomPatterns());
    }

    // ファイルが作成されたとき
    private void Created(object sender, FileSystemEventArgs e)
    {
        using var fs = new StreamReader(e.FullPath, Encoding.GetEncoding("UTF-8"));
        _randomPatterns = fs.ReadLine().Split(',').Select(int.Parse).ToList();
        _isRandomChanged = true;
    }

    // ファイルが更新されたとき
    private void Changed(object sender, FileSystemEventArgs e)
    {
        var file = new FileInfo(e.FullPath);

        if (file.LastWriteTime.Subtract(_lastWriteTimeSave) < new TimeSpan(0, 0, 0, 1)) return;
        _lastWriteTimeSave = file.LastWriteTime;

        using var fs = new StreamReader(e.FullPath, Encoding.GetEncoding("UTF-8"));
        _randomPatterns = fs.ReadLine().Split(',').Select(int.Parse).ToList();
        _isRandomChanged = true;
    }

    // ファイルが削除されたとき
    private void Deleted(object sender, FileSystemEventArgs e)
    {
        // 無地の画像を指定
        _randomPatterns = new() { 8, 8, 8, 8, 8, 8, 8 };
        _isRandomChanged = true;
    }

    private IEnumerator DisplayRandomPatterns()
    {
        while (true)
        {
            yield return new WaitForSeconds(_span);

            if (_isRandomChanged)
            {
                _lane1.sprite = _sprites[_randomPatterns[0] - 1];
                _lane2.sprite = _sprites[_randomPatterns[1] - 1];
                _lane3.sprite = _sprites[_randomPatterns[2] - 1];
                _lane4.sprite = _sprites[_randomPatterns[3] - 1];
                _lane5.sprite = _sprites[_randomPatterns[4] - 1];
                _lane6.sprite = _sprites[_randomPatterns[5] - 1];
                _lane7.sprite = _sprites[_randomPatterns[6] - 1];
                _isRandomChanged = false;
            }
        }
    }
}
