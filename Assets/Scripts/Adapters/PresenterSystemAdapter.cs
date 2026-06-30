
using Calculator.Presenter;
using System.Threading.Tasks;
using WindowsSystem;

namespace Calculator.Adapters
{
    public class PresenterSystemAdapter : IPresenterSystem
    {
        private readonly IWindowsSystem _system;

        public PresenterSystemAdapter(IWindowsSystem system)
        {
            _system = system;
        }

        public ValueTask Close<T>() where T : IPresenter
        {
            return _system.Close(typeof(T));
        }

        public T Get<T>() where T : IPresenter
        {
            return (T) _system.Get(typeof(T));
        }

        public ValueTask Open<T>() where T : IPresenter
        {
            return _system.Open(typeof(T));
        }
    }
}
