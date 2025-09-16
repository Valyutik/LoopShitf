using Sergei_Lind.LS.Runtime.Core.Player;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Sergei_Lind.LS.Runtime.Core
{
    public sealed class CoreScope : LifetimeScope
    {
        [SerializeField] private RootTransform rootTransform;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoreFlow>();
            builder.RegisterComponent(rootTransform);
            builder.Register<PlayerFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerController>().AsSelf();
        }
    }
}