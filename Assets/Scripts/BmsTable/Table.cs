public record Table
{
    public string Name { get; }
    public string Symbol { get; }
    public string DataUrl { get; }

    public Table(string name, string symbol, string dataUrl) =>
        (Name, Symbol, DataUrl) = (name, symbol, dataUrl);
}
