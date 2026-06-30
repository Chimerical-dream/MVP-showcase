using Calculator.Model;
using Calculator.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using WindowsSystem.DefaultImplementation;

namespace Calculator.View.Windows
{
    public class InputWindow : AWindow, IInputPresenter
    {
        [SerializeField]
        private Button _submitButton;
        [SerializeField]
        private TextMeshProUGUI _historyText;
        [SerializeField]
        private TMP_InputField _inputField;

        [Inject]
        public IPersistentData PersistentData;

        private const string PERSISTENT_INPUT_PATH = "InputWindow/InputField";

        public event Action<string> OnInputSubmitted;
        private int _lastDisplayedLogLength;

        protected override void Awake()
        {
            base.Awake();
            _submitButton.onClick.AddListener(HandleSubmitClick);

            _inputField.text = PersistentData.Load<string>(PERSISTENT_INPUT_PATH);
            _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        private void OnInputFieldValueChanged(string value)
        {
            PersistentData.Save(PERSISTENT_INPUT_PATH, value);
            var saved = PersistentData.Load<string>(PERSISTENT_INPUT_PATH);

        }

        private void HandleSubmitClick() => OnInputSubmitted.Invoke(_inputField.text);

        public void UpdateHistory(IReadOnlyList<OperationLog> history)
        {
            if (history.Count <= _lastDisplayedLogLength) return;

            StringBuilder sb = new StringBuilder();
            foreach (OperationLog log in history.Skip(_lastDisplayedLogLength))
            {
                var result = log.Result.Type == StringMath.Data.ResultType.Error ? "ERROR" : log.Result.Value;
                sb.Append($"{log.Input}={result}\n");
            }
            _historyText.text += sb.ToString();

            _lastDisplayedLogLength = history.Count;
        }
    }
}
