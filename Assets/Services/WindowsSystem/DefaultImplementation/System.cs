using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace WindowsSystem.DefaultImplementation
{
    public class System : IWindowsSystem
    {
        private readonly Dictionary<Type, AWindow> _windowPrefabs = new();
        private readonly Dictionary<Type, AWindow> _windowInstances = new();
        private Transform _parent;
        private AWindow _currentWindow;

        [Inject]
        public IObjectResolver Resolver;

        ValueTask IWindowsSystem.Open<T>() => Open(typeof(T));

        public ValueTask Close<T>() where T : IWindow => Close(typeof(T));

        public T Get<T>() where T : IWindow => (T)Get(typeof(T));

        internal void Init(Dictionary<Type, AWindow> dictionary)
        {
            _windowPrefabs.Clear();
            _windowPrefabs.AddRange(dictionary);

            _parent = new GameObject("WindowsSystem").transform;
            UnityEngine.Object.DontDestroyOnLoad(_parent.gameObject);
        }

        public async ValueTask Open(Type t)
        {
            if (!TryGetWindow(t, out var wnd)) return;

            var closingTask = CloseCurrent();
            _currentWindow = wnd;
            await closingTask;

            await _currentWindow.Open();
        }

        public async ValueTask Close(Type t)
        {
            if (!TryGetWindow(t, out var wnd)) return;

            if (_currentWindow == wnd) _currentWindow = null;
            await wnd.Close();
        }

        public IWindow Get(Type t)
        {
            TryGetWindow(t, out var wnd);
            return wnd;
        }

        public virtual async UniTask CloseCurrent()
        {
            if (_currentWindow == null) return;

            var closingWnd = _currentWindow;
            _currentWindow = null;

            await closingWnd.Close();
        }


        private bool TryGetWindow(Type type, out AWindow window)
        {
            var result = _windowInstances.TryGetValue(type, out window);
            if (result) return true;

            result = GetPrefab(type, out var prefab);
            if(!result) return false;

            window = Resolver.Instantiate(prefab, _parent);
            _windowInstances[type] = window;
            return true;
        }


        private bool GetPrefab(Type type, out AWindow prefab)
        {
            var result = _windowPrefabs.TryGetValue(type, out prefab);
            if (result) return true;

            foreach(var key in _windowPrefabs.Keys)
            {
                if (type.IsAssignableFrom(key))
                {
                    prefab = _windowPrefabs[key];
                    break;
                }
            }
            return prefab != null;
        }
    }
}
