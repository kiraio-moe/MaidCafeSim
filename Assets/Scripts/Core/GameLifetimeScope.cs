using UnityEngine;
using VContainer;
using VContainer.Unity;
using MaidCafe.Core.InputSystem;

namespace MaidCafe.Core
{
    [AddComponentMenu("Maid Cafe/Core/Game Lifetime Scope (DI)")]
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>();
//             builder.UseEntryPoints(entry =>
//             {
//                 entry.Add<InputManager>();
//             });

            builder.Register<InputManager>(Lifetime.Singleton);
        }
    }
}
