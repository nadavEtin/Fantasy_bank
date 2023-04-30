using Assets.Scripts.Utility;
using Assets.Utility.Jsons;
using GameCore;
using VContainer.Unity;

namespace VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameDirector>();
            builder.Register<EventBus>(Lifetime.Singleton);
            builder.Register<JsonSerialization>(Lifetime.Singleton);
        }
    }
}


