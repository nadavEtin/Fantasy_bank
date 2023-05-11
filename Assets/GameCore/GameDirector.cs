using GameCore.Events;
using GameCore.ScriptableObjects;
using GameCore.UI;
using Lean.Touch;
using System;
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
        private readonly IUiManager _uiManager;
        private readonly IAssetRefs _assetRefs;
        private readonly Canvas _canvas;
        private readonly GameEventManager _geManager;
        
        public TouchEventParams RecentTouch { get; private set; }
        
        public GameDirector(EventBus bus, IAssetRefs assetRefs, Canvas canvas, IObjectResolver scope)
        {
            _assetRefs = assetRefs;
            _canvas = canvas;
            _eventBus = bus;
            _uiManager = new UiManager(assetRefs, canvas);
            _geManager = new GameEventManager(_assetRefs);
            
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
            _geManager.CreateGameEvent(GameEventType.Loan);
        }

        #endregion
    }
}