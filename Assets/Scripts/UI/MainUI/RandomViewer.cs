using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class RandomViewer : MonoBehaviour
{
    private readonly List<Sprite> _sprites = new(8);

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

    // SE関連
    [SerializeField] private AudioSource _source;
    private AudioClip _1pware;
    private AudioClip _2pware;
    private AudioClip _gomi;

    private async void Start()
    {
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

        // SE読み込み
        _1pware = await Addressables.LoadAssetAsync<AudioClip>("SE1pware").Task;
        _2pware = await Addressables.LoadAssetAsync<AudioClip>("SE2pware").Task;
        _gomi = await Addressables.LoadAssetAsync<AudioClip>("SEgomi").Task;

        StartCoroutine(DisplayRandomPatterns());
    }

    private IEnumerator DisplayRandomPatterns()
    {
        while (true)
        {
            yield return new WaitForSeconds(_span);

            if (BeatorajaWatcher.I.IsRandomChanged)
            {
                List<int> randomPatterns = BeatorajaWatcher.I.RandomPatterns;

                _lane1.sprite = _sprites[randomPatterns[0] - 1];
                _lane2.sprite = _sprites[randomPatterns[1] - 1];
                _lane3.sprite = _sprites[randomPatterns[2] - 1];
                _lane4.sprite = _sprites[randomPatterns[3] - 1];
                _lane5.sprite = _sprites[randomPatterns[4] - 1];
                _lane6.sprite = _sprites[randomPatterns[5] - 1];
                _lane7.sprite = _sprites[randomPatterns[6] - 1];
                BeatorajaWatcher.I.IsRandomChanged = false;

                PlayWav(randomPatterns);
            }
        }
    }

    private void PlayWav(List<int> patterns)
    {
        AudioClip clip;
        if (Is1Pware(patterns))
        {
            clip = _1pware;
        }
        else if (Is2Pware(patterns))
        {
            clip = _2pware;
        }
        else if (IsGomifumen(patterns))
        {
            clip = _gomi;
        }
        else
        {
            return;
        }

        _source.PlayOneShot(clip);
    }

    private bool Is1Pware(List<int> patterns)
    {
        return IsAllOdd(patterns, new int[] { 3, 4, 5, 6 });
    }

    private bool Is2Pware(List<int> patterns)
    {
        return IsAllOdd(patterns, new int[] { 0, 1, 2, 3 });
    }

    private bool IsGomifumen(List<int> patterns)
    {
        return IsAllOdd(patterns, new int[] { 1, 2, 4, 5 });
    }

    private bool IsAllOdd(List<int> patterns, int[] indices)
    {
        for (int i = 0; i < indices.Length; i++)
        {
            if (patterns[indices[i]] % 2 == 0)
            {
                return false;
            }
        }

        return true;
    }
}
