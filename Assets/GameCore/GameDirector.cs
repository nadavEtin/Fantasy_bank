using GameCore.Events;
using GameCore.ScriptableObjects;
using GameCore.UI;
using System;
using Bank;
using GameEvent;
using GameEvent.LoanEvent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameCore
{
    public class GameDirector : IStartable, IDisposable, IGameDirector
    {
        private readonly EventBus _eventBus;
        
        private readonly IAssetRefs _assetRefs;
        private readonly Canvas _canvas;
        private readonly IGameEventManager _geManager;
        
        private readonly Camera _camera;
        
        //should be inited by vcontainer
        private readonly IBankManager _bankManager;
        private readonly IUiManager _uiManager;
        
        public TouchEventParams RecentTouch { get; private set; }
        
        public GameDirector(EventBus bus, IAssetRefs assetRefs, Canvas canvas, 
            Camera camera, IUiManager uiManager)
        {
            _assetRefs = assetRefs;
            _canvas = canvas;
            _camera = camera;
            _eventBus = bus;
            _bankManager = new BankManager(_eventBus);
            _uiManager = uiManager;
            _geManager = new GameEventManager(_assetRefs, this, _bankManager, camera);
            
            GameEventCreate();
        }

        private void TouchEventListener(BaseEventParams eventParams)
        {
            RecentTouch = (TouchEventParams)eventParams;
        }

        public void Start()
        {
            _eventBus.Subscribe(GameplayEvent.TouchStarted, TouchEventListener);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.TouchStarted, TouchEventListener);
        }

        #region Game Flow

        private IObjectResolver _resolver;

        private void GameEventCreate()
        {
            //_geManager.CreateGameEvent(GameEventType.Loan);
        }

        #endregion
    }
}