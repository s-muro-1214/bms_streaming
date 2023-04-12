using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MiniJSON;
using UnityEngine;
using UnityEngine.Networking;

public class TableParser : MonoBehaviour
{
    private static readonly string _baseUri = "https://izuna.net/db_supporter";

    public void OnClickUpdateBmsSongs()
    {
        StartCoroutine(UpdateBmsSongsDB("http://www.ribbit.xyz/bms/tables", "insane.html"));
        StartCoroutine(UpdateBmsSongsDB("https://stellabms.xyz/sl", "table.html"));
        StartCoroutine(UpdateBmsSongsDB("https://stellabms.xyz/st", "table.html"));
    }

    public IEnumerator BmsSongGacha(string level)
    {
        List<Song> songs = null;
        yield return GetSongsFromDB(level, x => songs = x);
        if (songs == null)
        {
            yield break;
        }

        // random
    }

    public IEnumerator UpdateBmsSongsDB(string baseUrl, string subUri)
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

        DateTime tableLastUpdate = DateTime.MinValue;
        yield return GetTableLastUpdateTime(table.name, x => tableLastUpdate = x);

        if (tableLastUpdate.Day <= DateTime.Now.Day)
        {
            yield break;
        }

        List<Song> songs = null;
        yield return GetSongData($"{baseUrl}/{table.dataUrl}", table.symbol, x => songs = x);
        if (songs == null)
        {
            yield break;
        }

        yield return UpdateSongsTable(songs);

        yield return UpdateTableLastUpdateTime(table, baseUrl);
    }

    private IEnumerator GetBmsTableContent(string url, Action<string> callback)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        String content = null;
        if (request.result == UnityWebRequest.Result.Success)
        {
            string pattern = @"<meta\s+name\s*=\s*[""']bmstable[""']\s+content\s*=\s*[""'](?<text>[^<]*)[""']\s*/>";
            Match match = Regex.Match(request.downloadHandler.text, pattern);
            if (match.Success)
            {
                content = match.Groups[1].Captures[0].Value;
            }
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();

        callback(content);
    }

    private IEnumerator GetHeaderJsonData(string url, Action<Table> callback)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        Table table = null;
        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = (IDictionary)Json.Deserialize(request.downloadHandler.text);
            table = new Table((string)json["name"], (string)json["symbol"], (string)json["data_url"]);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();

        callback(table);
    }

    private IEnumerator GetSongData(string url, string symbol, Action<List<Song>> callback)
    {
        using var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        List<Song> songs = null;
        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = (IDictionary)Json.Deserialize("{\"songs\":" + request.downloadHandler.text + "}");
            songs = new(((IList)json["songs"]).Count);
            foreach (IDictionary song in (IList)json["songs"])
            {
                songs.Add(new Song((string)song["title"], (string)song["artist"], (string)song["md5"],
                    (string)song["sha256"], symbol + (string)song["level"]));
            }
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();

        callback(songs);
    }

    private IEnumerator GetTableLastUpdateTime(string name, Action<DateTime> callback)
    {
        WWWForm form = new();
        form.AddField("token", Configuration.TOKEN);
        form.AddField("name", name);

        using var request = UnityWebRequest.Post($"{_baseUri}/tables/get.php", form);
        yield return request.SendWebRequest();

        DateTime dateTime = DateTime.MinValue;
        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = (IDictionary)Json.Deserialize(request.downloadHandler.text);
            dateTime = DateTime.Parse((string)json["updated_timestamp"]);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();

        callback(dateTime);
    }

    private IEnumerator UpdateTableLastUpdateTime(Table table, string baseUrl)
    {
        WWWForm form = new();
        form.AddField("token", Configuration.TOKEN);
        form.AddField("name", table.name);
        form.AddField("symbol", table.symbol);
        form.AddField("url", $"{baseUrl}/{table.dataUrl}");

        using var request = UnityWebRequest.Post($"{_baseUri}/tables/post.php", form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();
    }

    private IEnumerator GetSongsFromDB(string level, Action<List<Song>> callback)
    {
        WWWForm form = new();
        form.AddField("token", Configuration.TOKEN);
        form.AddField("level", level);

        using var request = UnityWebRequest.Post($"{_baseUri}/songs/get.php", form);
        yield return request.SendWebRequest();

        List<Song> songs = null;
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            songs = JsonUtility.FromJson<SongsWrapper>(request.downloadHandler.text).songs;
        }

        request.Dispose();

        callback(songs);
    }

    private IEnumerator UpdateSongsTable(List<Song> songs)
    {
        SongsWrapper wrapper = new SongsWrapper(songs);
        WWWForm form = new();
        form.AddField("token", Configuration.TOKEN);
        form.AddField("songs", JsonUtility.ToJson(wrapper));

        using var request = UnityWebRequest.Post($"{_baseUri}/songs/post.php", form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }

        request.Dispose();
    }
}
