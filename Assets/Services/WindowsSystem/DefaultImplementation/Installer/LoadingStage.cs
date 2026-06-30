using Cysharp.Threading.Tasks;
using LoadingSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace WindowsSystem.DefaultImplementation.Installer
{
    public class LoadingStage : ILoadingStage
    {
        [Inject]
        public System System;

        public int Order => 0;

        public async ValueTask LoadAsync()
        {
            var config = (Config) await Resources.LoadAsync<Config>(Config.DEFAULT_PATH);

            var dictionary = new Dictionary<Type, AWindow>();
            foreach (var window in config.Windows)
            {
                dictionary[window.GetType()] = window;
            }

            System.Init(dictionary);
        }
    }
}
