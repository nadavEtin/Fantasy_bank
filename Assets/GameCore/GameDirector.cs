using Assets.Scripts.Utility;
using GameCore.Events;
using GameCore.ScriptableObjects;
using GameCore.UI;
using Lean.Touch;
using System;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public class GameDirector : IStartable, IDisposable, IGameDirector
    {
        private readonly EventBus _eventBus;
        private readonly IUiManager _uiManager;
        private readonly IAssetRefs _assetRefs;
        
        public TouchEvent RecentTouch { get; private set; }
        
        public GameDirector(EventBus bus, IAssetRefs assetRefs)
        {
            //var assetRefs = Resources.Load("AssetRefs");
            _assetRefs = assetRefs;
            
            _eventBus = bus;
            _uiManager = new UiManager(assetRefs);
        }

        private void TouchEventListener(BaseEventParams eventParams)
        {
            RecentTouch = (TouchEvent)eventParams;
        }

        public TouchEvent GetTouchData(BaseEventParams eventParams)
        {
            var touchData = (TouchEvent)eventParams;
            return touchData;
        }
        
        public void Start()
        {
            _eventBus.Subscribe(GameplayEvent.TouchStarted, TouchEventListener);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.TouchStarted, TouchEventListener);
        }
    }
}