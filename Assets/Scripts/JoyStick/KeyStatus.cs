public class ButtonStatus : IKeyStatus
{
    public bool IsPressed { get; set; } = false;
    public int Count { get; set; } = 0;
}

public class ScratchStatus : IKeyStatus
{
    public bool IsScratchLRotated { get; set; } = false;
    public bool IsScratchRRotated { get; set; } = false;
    public int CountL { get; set; } = 0;
    public int CountR { get; set; } = 0;
}

public interface IKeyStatus
{

}