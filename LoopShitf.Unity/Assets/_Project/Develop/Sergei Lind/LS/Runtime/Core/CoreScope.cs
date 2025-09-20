using Sergei_Lind.LS.Runtime.Core.Player.Ring;
using Sergei_Lind.LS.Runtime.Core.Player;
using Sergei_Lind.LS.Runtime.Core.Enemy;
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
            builder.Register<RingFactory>(Lifetime.Singleton);

            builder.Register<EnemyFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EnemySpawner>().AsSelf();
        }
    }
}