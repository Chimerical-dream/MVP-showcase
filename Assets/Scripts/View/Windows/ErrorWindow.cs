using Calculator.Presenter;
using System;
using UnityEngine;
using UnityEngine.UI;
using WindowsSystem.DefaultImplementation;

namespace Calculator.View.Windows
{
    public class ErrorWindow : AWindow, IErrorPresenter
    {
        public event Action OnCloseRequested;

        [SerializeField]
        private Button _closeButton;

        protected override void Awake()
        {
            base.Awake();
            _closeButton.onClick.AddListener(HandleCloseClick);
        }

        private void HandleCloseClick()
        {
            OnCloseRequested.Invoke();
        }
    }
}
