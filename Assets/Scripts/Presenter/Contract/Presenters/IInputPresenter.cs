using System;
using System.Collections.Generic;
using Calculator.Model;

namespace Calculator.Presenter
{
    public interface IInputPresenter : IPresenter
    {
        event Action<string> OnInputSubmitted;

        void UpdateHistory(IReadOnlyList<OperationLog> history);
    }
}
