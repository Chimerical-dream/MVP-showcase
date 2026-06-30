using System.Threading.Tasks;

namespace LoadingSystem
{
    public interface ILoadingSystem
    {
        ValueTask LoadAsync();
    }
}
