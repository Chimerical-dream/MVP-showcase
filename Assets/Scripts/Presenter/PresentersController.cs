using Calculator.Model;
using System.Linq;

namespace Calculator.Presenter
{
    public class PresentersController
    {
        private IPresenterSystem _presenterSystem;
        private IApplication _application;

        public PresentersController(IPresenterSystem presenterSystem, IApplication application)
        {
            _presenterSystem = presenterSystem;
            _application = application;
        }

        public void Init()
        {
            DisplayInputPresenter();
            _application.OnHistoryUpdated += OnHistoryUpdated;
        }

        public void OnHistoryUpdated()
        {
            if(_application.History.Last().Result.Type == StringMath.Data.ResultType.Error)
            {
                DisplayErrorPresenter();
                return;
            }

            var inputPresenter = _presenterSystem.Get<IInputPresenter>();

            inputPresenter.UpdateHistory(_application.History);
        }

        private void DisplayInputPresenter()
        {
            var presenter = _presenterSystem.Get<IInputPresenter>();

            presenter.UpdateHistory(_application.History);

            presenter.OnInputSubmitted -= OnInputSubmitted;
            presenter.OnInputSubmitted += OnInputSubmitted;

            _presenterSystem.Open<IInputPresenter>();

            return;
            void OnInputSubmitted(string input)
            {
                _application.ProcessInput(input);
            }
        }

        private void DisplayErrorPresenter()
        {
            var presenter = _presenterSystem.Get<IErrorPresenter>();
            presenter.OnCloseRequested -= OnCloseRequested;
            presenter.OnCloseRequested += OnCloseRequested;

            _presenterSystem.Open<IErrorPresenter>();

            return;
            void OnCloseRequested()
            {
                _presenterSystem.Close<IErrorPresenter>();
                DisplayInputPresenter();
            }
        }


    }
}
