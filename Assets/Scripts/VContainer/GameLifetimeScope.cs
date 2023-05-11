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
            builder.Register<JsonSerialization>(Lifetime.Singleton);
            builder.Register<IUiManager, UiManager>(Lifetime.Scoped);
            builder.RegisterInstance<IAssetRefs, AssetRefs>(_assetRefs);
            builder.Register<InputManager>(Lifetime.Singleton);
            builder.Register<IBankManager, BankManager>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<Canvas>();
        }
    }
}


