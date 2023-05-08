using Assets.GameCore;
using Assets.Scripts.Utility;
using Assets.Utility.Jsons;
using GameCore;
using GameCore.ScriptableObjects;
using GameCore.UI;
using UnityEngine;
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
            builder.RegisterComponentInHierarchy<Canvas>();
        }
    }
}


