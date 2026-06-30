using System.Threading.Tasks;

namespace Calculator.Presenter
{
    public interface IPresenterSystem
    {
        ValueTask Open<T>() where T : IPresenter;
        ValueTask Close<T>() where T : IPresenter;

        T Get<T>() where T : IPresenter;
    }
}
