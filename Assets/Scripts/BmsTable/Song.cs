public record Song
{
    public string Title { get; }
    public string Artist { get; }
    public string Md5 { get; }
    public string Sha256 { get; }
    public string Level { get; }

    public Song(string title, string artist, string md5, string sha256, string level) =>
        (Title, Artist, Md5, Sha256, Level) = (title, artist, md5, sha256, level);
}
