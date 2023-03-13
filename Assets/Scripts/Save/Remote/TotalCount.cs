using System;

[Serializable]
public class TotalCount
{
    public int scratchL = 0;
    public int scratchR = 0;
    public int key1 = 0;
    public int key2 = 0;
    public int key3 = 0;
    public int key4 = 0;
    public int key5 = 0;
    public int key6 = 0;
    public int key7 = 0;

    public long GetTotalCountSum()
    {
        return scratchL + scratchR + key1 + key2 + key3 + key4 + key5 + key6 + key7;
    }
}
