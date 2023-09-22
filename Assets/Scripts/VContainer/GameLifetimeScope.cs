using Assets.GameCore.EventEffectsResolver;
using Assets.GameEvent.EventResolution;
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
using GameEvent.EventCountdown;
using GameEvent.EventResolution;
using UnityEngine;
using VContainer.Unity;

namespace VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetRefs _assetRefs;
        [SerializeField] private GameEventSettings _eventSettings;
        [SerializeField] private StoriesRefs _storyRefs;

        private IContainerBuilder _containerBuilder;

        protected override void Configure(IContainerBuilder builder)
        {
            _containerBuilder = builder;
            
            builder.RegisterInstance<IAssetRefs, AssetRefs>(_assetRefs);
            builder.RegisterInstance<IGameEventSettings, GameEventSettings>(_eventSettings);
            builder.RegisterInstance<IStoriesRefs, StoriesRefs>(_storyRefs);
            
            builder.RegisterEntryPoint<GameDirector>();
            builder.Register<EventBus>(Lifetime.Singleton);
            builder.Register<JsonSerialization>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UiManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BankManager>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EventManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StoryValidator>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventResolutionViewManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventEffectsResolver>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EventsData>(Lifetime.Singleton);
            builder.Register<ScreenParams>(Lifetime.Singleton);
            builder.Register<EventCountdownManager>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<Canvas>();
            builder.RegisterComponentInHierarchy<Camera>();
            
            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<EventsData>();
                container.Resolve<ScreenParams>();
                container.Resolve<EventCountdownManager>();
            });
            
            Factories();
        }

        private void Factories()
        {
            _containerBuilder.Register<EventCountdownFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            _containerBuilder.Register<EventResolutionViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            //TODO: check if this is needed after using implemented intefaces
            /*_containerBuilder.RegisterBuildCallback(container =>
            {
                container.Resolve<EventCountdownFactory>();
                container.Resolve<EventResolutionViewFactory>();

            });*/
        }
    }
}


