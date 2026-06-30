namespace PersistentData
{
    public interface IPersistentData
    {
        void Save<T>(T data, string path);

        T Load<T>(string path);
    }
}
