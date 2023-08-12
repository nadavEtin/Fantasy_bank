using GameCore.ScriptableObjects;
using GameCore.UI;
using Bank;
using DG.Tweening;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameEvent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameCore
{
    public class GameDirector : IGameDirector, IStartable
    {
        private readonly EventBus.EventBus _eventBus;

        private readonly IAssetRefs _assetRefs;
        private readonly Canvas _canvas;
        private readonly IGameEventManager _geManager;

        private readonly Camera _camera;
        private readonly IBankBalance _bankBalance;
        private readonly IUiManager _uiManager;
        
        //test
        private IObjectResolver _resolver;

        public GameDirector(EventBus.EventBus bus, IAssetRefs assetRefs, IBankBalance bankBalance,
            IGameEventManager eventManager, Canvas canvas, Camera camera, IUiManager uiManager, IObjectResolver resolver)
        {
            _assetRefs = assetRefs;
            _canvas = canvas;
            _camera = camera;
            _eventBus = bus;
            _bankBalance = bankBalance;
            _uiManager = uiManager;
            _geManager = eventManager;


            _resolver = resolver;
            DOTween.Init();
            GameEventCreate();
        }
        

        #region Game Flow

        private void AdvanceTurn()
        {
            _eventBus.Publish(GameplayEvent.NextTurn, new NextTurnEventParams());
        }

        private void GameEventCreate()
        {
            //_resolver.Instantiate(_assetRefs.EventCountdown);
            
            
            //_geManager.CreateGameEvent(GameEventType.Loan);

            _resolver.Instantiate(_assetRefs.EventResolutionScreen);
        }

        #endregion

        public void Start()
        {
            //THIS IS NEEDED TO CALL THIS OBJ'S CONSTRUCTOR AFTER REGISTERING IN LIFETIME SCOPE
        }
    }
}