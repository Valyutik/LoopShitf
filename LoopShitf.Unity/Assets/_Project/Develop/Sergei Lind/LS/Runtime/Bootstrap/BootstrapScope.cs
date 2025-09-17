using Sergei_Lind.LS.Runtime.Utilities;
using Sergei_Lind.LS.Runtime.Input;
using VContainer.Unity;
using VContainer;

namespace Sergei_Lind.LS.Runtime.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(this);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LoadingService>(Lifetime.Scoped);
            builder.Register<ConfigContainer>(Lifetime.Singleton);
            builder.Register<InputService>(Lifetime.Singleton).As<InputService, IInput>();

            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}
