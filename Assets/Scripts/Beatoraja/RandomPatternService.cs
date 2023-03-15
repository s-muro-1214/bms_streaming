using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

public class RandomPatternService : WebSocketBehavior
{
    public static readonly string URI = "/random";

    private static List<int> _randomPatterns = new(7);
    private static bool _isRandomChanged = false;

    protected override void OnMessage(MessageEventArgs e)
    {
        _randomPatterns = e.Data.Split(',').Select(int.Parse).ToList();
        _isRandomChanged = true;
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
}
