public interface ISaveDataManager<T> where T : class
{
    public T GetSaveDataValue();

    public void SetSaveDataValue(T settings);
}
