using Sergei_Lind.LS.Runtime.Core.Player.Ring;
using Sergei_Lind.LS.Runtime.Core.Player;
using Sergei_Lind.LS.Runtime.Core.Enemy;
using Sergei_Lind.LS.Runtime.Core.UI;
using VContainer.Unity;
using UnityEngine;
using VContainer;

namespace Sergei_Lind.LS.Runtime.Core
{
    public sealed class CoreScope : LifetimeScope
    {
        [Header("Root Transforms")]
        [SerializeField] private RootTransform rootTransform;
        [SerializeField] private EnemyRootTransform enemyRootTransform;
        
        [Header("UI")]
        [SerializeField] private GameOverPanelView gameOverPanel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoreFlow>();
            builder.RegisterComponent(rootTransform);
            builder.RegisterComponent(enemyRootTransform);
            
            builder.Register<PlayerViewFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerController>().AsSelf();
            builder.Register<RingFactory>(Lifetime.Singleton);

            builder.Register<EnemyFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EnemySpawner>().AsSelf();

            builder.RegisterComponent(gameOverPanel);
            builder.Register<GameUIController>(Lifetime.Singleton);
            
            builder.Register<GameSession>(Lifetime.Singleton);
        }
    }
}