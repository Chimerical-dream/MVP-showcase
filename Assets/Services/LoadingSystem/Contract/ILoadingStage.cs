using System.Threading.Tasks;

namespace LoadingSystem
{
    public interface ILoadingStage
    {
        int Order { get; }
        ValueTask LoadAsync();
    }
}
