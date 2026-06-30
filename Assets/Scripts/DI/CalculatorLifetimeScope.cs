using Calculator.Model;
using Calculator.Presenter;
using VContainer;
using VContainer.Unity;

namespace Calculator.Adapters
{
    public class CalculatorLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPresenters(builder);
            RegisterPersistentData(builder);

            RegisterSingleton<ApplicationLoadingStage>(builder);

            RegisterSingleton<Application>(builder);
        }

        private static void RegisterPresenters(IContainerBuilder builder)
        {
            WindowsSystem.DefaultImplementation.Installer.Installer.Install(builder);
            RegisterSingleton<PresenterSystemAdapter>(builder);

            RegisterSingleton<PresentersController>(builder);
        }

        private static void RegisterPersistentData(IContainerBuilder builder)
        {
            RegisterSingleton<PersistentData.PlayerPrefs.PersistentData>(builder);
            RegisterSingleton<PersistentDataAdapter>(builder);
        }

        private static void RegisterSingleton<T>(IContainerBuilder builder) => builder.Register<T>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
    }
}
