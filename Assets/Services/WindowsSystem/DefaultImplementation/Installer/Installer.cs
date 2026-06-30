using VContainer;

namespace WindowsSystem.DefaultImplementation.Installer
{
    public static class Installer
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<System>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<LoadingStage>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
