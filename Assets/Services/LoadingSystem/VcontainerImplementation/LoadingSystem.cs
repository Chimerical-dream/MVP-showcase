using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LoadingSystem.VcontainerImplementation
{
    public class LoadingSystem : MonoBehaviour, ILoadingSystem
    {
        [SerializeField]
        private LifetimeScope _scope;

        private void Awake()
        {
            LoadAsync().AsUniTask().Forget();
        }

        public async ValueTask LoadAsync()
        {
            _scope.Build();

            await UniTask.WaitForEndOfFrame();

            ProcessLoadingStages(_scope.Container.Resolve<IReadOnlyList<ILoadingStage>>()).Forget();
        }

        private async UniTaskVoid ProcessLoadingStages(IReadOnlyList<ILoadingStage> stages)
        {
            var sorted = stages.OrderBy(x => x.Order);

            foreach (var stage in sorted)
            {
                await stage.LoadAsync();
            }
        }
    }
}
