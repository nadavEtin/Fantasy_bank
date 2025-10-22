using Assets.GameCore.EventEffectsResolver;
using Assets.GameEvent.EventResolution;
using Bank;
using GameCore.DataManagement.Events;
using GameCore.EventBus;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameCore.UI;
using GameCore.Utility.Jsons;
using GameCore.Utility.Screen;
using GameEvent;
using GameEvent.EventCountdown;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private AssetRefs _assetRefs;
    [SerializeField] private GameEventSettings _eventSettings;
    [SerializeField] private StoriesRefs _storyRefs;

    public void InstallBindings(ContainerBuilder builder)
    {
        //scriptables
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_eventSettings, typeof(IGameEventSettings));
        builder.AddSingleton(_storyRefs, typeof(IStoriesRefs));

        builder.AddSingleton(new EventBus(), typeof(IEventBus));
        builder.AddSingleton(new JsonSerialization(), typeof(IJsonSerialization));        
        builder.AddSingleton(new BankManager(), typeof(IBankBalance), typeof(IBankDeposit), typeof(IBankWithdraw));        
        builder.AddSingleton(new ScreenParams(), typeof(ScreenParams));


        builder.AddSingleton(new EventCountdownFactory(), typeof(IEventCountdownFactory));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        //builder.AddSingleton(new EventCountdownManager(), typeof(IAssetRefs));
        //builder.AddSingleton(new EventsData(), typeof(IAssetRefs));
        //builder.AddSingleton(new EventEffectsResolver(), typeof(IAssetRefs));
        //builder.AddSingleton(new EventResolutionViewManager(), typeof(IAssetRefs));


        //builder.AddSingleton(new UiManager(_assetRefs), typeof(IUiManager));
        //builder.AddSingleton(new InputManager(IEventBus), typeof(IAssetRefs));
        //builder.AddSingleton(new StoryEventManager(), typeof(IGameEventManager));
    }
}
