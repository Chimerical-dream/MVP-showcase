using System;

namespace Calculator.Presenter
{
    public interface IErrorPresenter : IPresenter
    {
        public event Action OnCloseRequested;
    }
}
