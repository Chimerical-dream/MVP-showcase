using System;
using System.Collections.Generic;

namespace Calculator.Model
{

    public class Application : IApplication
    {
        private const string HISTORY_SAVE_PATH = "hsp";

        private IPersistentData _persistentData;
        private StringMath.Calculator _calculator = new();
        private List<OperationLog> _operationsHistory;

        public IReadOnlyList<OperationLog> History => _operationsHistory;

        public event Action OnHistoryUpdated;

        public Application(IPersistentData persistentData)
        {
            _persistentData = persistentData;
        }

        public void Init()
        {
            _operationsHistory = _persistentData.Load<List<OperationLog>>(HISTORY_SAVE_PATH) ?? new();
            OnHistoryUpdated?.Invoke();
        }

        public void ProcessInput(string input)
        {
            var result = _calculator.Calculate(input);

            _operationsHistory.Add(new OperationLog { Input = input, Result = result });
            _persistentData.Save(HISTORY_SAVE_PATH, _operationsHistory);

            OnHistoryUpdated?.Invoke();
        }
    }
}
