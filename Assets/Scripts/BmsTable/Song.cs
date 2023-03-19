using System;

[Serializable]
public class Song
{
    public string title;
    public string artist;
    public string md5;
    public string sha256;
    public string level;

    public Song(string title, string artist, string md5, string sha256, string level)
    {
        this.title = title;
        this.artist = artist;
        this.md5 = md5;
        this.sha256 = sha256;
        this.level = level;
    }
}
