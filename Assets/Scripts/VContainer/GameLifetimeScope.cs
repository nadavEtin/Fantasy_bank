using Bank;
using GameCore;
using GameCore.DataManagement.Events;
using GameCore.EventBus;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameCore.UI;
using GameCore.Utility.Jsons;
using GameCore.Utility.Screen;
using GameEvent;
using UnityEngine;
using VContainer.Unity;

namespace VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetRefs _assetRefs;
        [SerializeField] private GameEventSettings _eventSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameDirector>();
            builder.Register<EventBus>(Lifetime.Singleton);
            builder.Register<JsonSerialization>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UiManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance<IAssetRefs, AssetRefs>(_assetRefs);
            builder.Register<InputManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BankManager>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EventManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventValidator>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventsData>(Lifetime.Singleton);
            builder.Register<ScreenParams>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<Canvas>();
            builder.RegisterComponentInHierarchy<Camera>();
            
            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<EventsData>();
                container.Resolve<ScreenParams>();
            });
        }
    }
}


