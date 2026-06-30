namespace Calculator.Model
{
    public interface IPersistentData
    {
        void Save<T>(string path, T data);

        T Load<T>(string path);
    }
}
