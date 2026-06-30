using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PersistentData.PlayerPrefs
{
    public class PersistentData : IPersistentData
    {
        private readonly string _cachedEmpty = System.Text.Encoding.UTF8.GetString(new byte[0]);

        public T Load<T>(string path)
        {
            var saveString = UnityEngine.PlayerPrefs.GetString(path, _cachedEmpty);

            var bArray = System.Convert.FromBase64String(saveString);

            return (T) ByteArrayToObject(bArray);
        }

        public void Save<T>(T data, string path)
        {
            var bArray = ObjectToByteArray(data);

            var saveString = System.Convert.ToBase64String(bArray);
            UnityEngine.PlayerPrefs.SetString(path, saveString);
            UnityEngine.PlayerPrefs.Save();
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private static object ByteArrayToObject(byte[] arrBytes)
        {
            if (arrBytes.Length == 0) return null;
            using (var memStream = new MemoryStream(arrBytes))
            {
                var binForm = new BinaryFormatter();
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}