using Bank;
using GameCore;
using GameCore.Events;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameCore.UI;
using UnityEngine;
using Utility.Jsons;
using VContainer.Unity;

namespace VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetRefs _assetRefs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameDirector>();
            builder.Register<EventBus>(Lifetime.Singleton);
            builder.Register<JsonSerialization>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UiManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance<IAssetRefs, AssetRefs>(_assetRefs);
            builder.RegisterEntryPoint<InputManager>();
            builder.Register<BankManager>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<Canvas>();
            builder.RegisterComponentInHierarchy<Camera>();
        }
    }
}


