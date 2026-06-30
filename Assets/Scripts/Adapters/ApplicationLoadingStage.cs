using Calculator.Model;
using Calculator.Presenter;
using Cysharp.Threading.Tasks;
using LoadingSystem;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Calculator.Adapters
{
    public class ApplicationLoadingStage : ILoadingStage
    {
        public int Order => int.MaxValue;

        private readonly Application _application;
        private readonly PresentersController _presentersController;

        public ApplicationLoadingStage(Application application, PresentersController presentersController)
        {
            _application = application;
            _presentersController = presentersController;
        }

        public async ValueTask LoadAsync()
        {
            _application.Init();
            await UniTask.WaitForEndOfFrame();
            _presentersController.Init();

            await UniTask.WaitForEndOfFrame();

            SceneManager.LoadScene(1);
        }
    }
}
