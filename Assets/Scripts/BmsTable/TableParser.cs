using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;

public class TableParser : MonoBehaviour
{
    public void OnClick()
    {
        StartCoroutine(GetBmsSongs("https://stellabms.xyz/sl", "table.html"));
    }

    public IEnumerator GetBmsSongs(string baseUrl, string subUri)
    {
        string header = null;
        yield return GetBmsTableContent($"{baseUrl}/{subUri}", x => header = x);
        if (header == null)
        {
            yield break;
        }

        Table table = null;
        yield return GetHeaderJsonData($"{baseUrl}/{header}", x => table = x);
        if (table == null)
        {
            yield break;
        }

        List<Song> songs = null;
        yield return GetSongData($"{baseUrl}/{table.DataUrl}", table.Symbol, x => songs = x);
        Debug.Log(songs[0]);
    }

    private IEnumerator GetBmsTableContent(string url, Action<string> action)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string pattern = @"<meta\s+name\s*=\s*[""']bmstable[""']\s+content\s*=\s*[""'](?<text>[^<]*)[""']\s*/>";
            Match match = Regex.Match(request.downloadHandler.text, pattern);
            if (match.Success)
            {
                action(match.Groups[1].Captures[0].Value);
            }
            else
            {
                action(null);
            }
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            action(null);
        }

        request.Dispose();
    }

    private IEnumerator GetHeaderJsonData(string url, Action<Table> action)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = (IDictionary)Json.Deserialize(request.downloadHandler.text);
            action(new Table((string)json["name"], (string)json["symbol"], (string)json["data_url"]));
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            action(null);
        }

        request.Dispose();
    }

    private IEnumerator GetSongData(string url, string symbol, Action<List<Song>> action)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = (IDictionary)Json.Deserialize("{\"songs\":" + request.downloadHandler.text + "}");
            List<Song> songs = new(((IList)json["songs"]).Count);
            foreach (IDictionary song in (IList)json["songs"])
            {
                songs.Add(new Song((string)song["title"], (string)song["artist"], (string)song["md5"],
                    (string)song["sha256"], symbol + (string)song["level"]));
            }
            action(songs);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            action(null);
        }

        request.Dispose();
    }
}
