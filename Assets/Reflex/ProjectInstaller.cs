using Assets.GameEvent.EventCardView;
using Bank;
using GameCore;
using GameCore.EventBus;
using GameCore.ScriptableObjects;
using GameCore.Utility.Jsons;
using GameCore.Utility.Screen;
using GameEvent.EventCountdown;
using GameEvent.EventResolution;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    //[SerializeField] private AssetRefs _assetRefs;
    //[SerializeField] private GameEventSettings _eventSettings;
    //[SerializeField] private StoriesRefs _storyRefs;

    //game object for instantiating from non-mono classes
    //private GameObject _instantiaterGameObject;

    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton("working!");


        //_instantiaterGameObject = new GameObject("Instantiater");
        //_instantiaterGameObject.transform.SetParent(transform);
        //builder.AddSingleton(_instantiaterGameObject, typeof(GameObject));


        ////scriptables
        //builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        //builder.AddSingleton(_eventSettings, typeof(IGameEventSettings));
        //builder.AddSingleton(_storyRefs, typeof(IStoriesRefs));

        //builder.AddSingleton(new EventBus(), typeof(IEventBus));
        //builder.AddSingleton(new JsonSerialization(), typeof(IJsonSerialization));
        //builder.AddSingleton(new BankManager(), typeof(IBankBalance), typeof(IBankDeposit), typeof(IBankWithdraw));
        //builder.AddSingleton(new ScreenParams(), typeof(ScreenParams));


        //builder.AddSingleton(new EventCountdownFactory(), typeof(IEventCountdownFactory));
        //builder.AddSingleton(new EventResolutionViewFactory(), typeof(EventResolutionViewFactory));
        //builder.AddSingleton(new StoryViewFactory(), typeof(StoryViewFactory));
        //builder.AddSingleton(new GameDirector(), typeof(GameDirector));






        //builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        //builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        //builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        //builder.AddSingleton(_assetRefs, typeof(IAssetRefs));
        //builder.AddSingleton(new EventCountdownManager(), typeof(IAssetRefs));
        //builder.AddSingleton(new EventsData(), typeof(IAssetRefs));
        //builder.AddSingleton(new EventEffectsResolver(), typeof(IAssetRefs));
        //builder.AddSingleton(new EventResolutionViewManager(), typeof(IAssetRefs));


        //builder.AddSingleton(new UiManager(_assetRefs), typeof(IUiManager));
        //builder.AddSingleton(new InputManager(IEventBus), typeof(IAssetRefs));
        //builder.AddSingleton(new StoryEventManager(), typeof(IGameEventManager));
    }
}
