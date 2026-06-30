using System;
using System.Collections.Generic;

namespace Calculator.Model
{
    public interface IApplication
    {
        void Init();

        void ProcessInput(string input);

        IReadOnlyList<OperationLog> History { get; }
        public event Action OnHistoryUpdated;
    }
}
