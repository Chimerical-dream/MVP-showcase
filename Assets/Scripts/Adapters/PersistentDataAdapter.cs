namespace Calculator.Adapters
{
    public class PersistentDataAdapter : Model.IPersistentData
    {
        private PersistentData.IPersistentData _persistentDataService;

        public PersistentDataAdapter(PersistentData.IPersistentData persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        public T Load<T>(string path) => _persistentDataService.Load<T>(path);

        public void Save<T>(string path, T data) => _persistentDataService.Save(data, path);
    }
}
