using System.Collections.Generic;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using UnityEngine;

namespace GameEvent
{
    public class GameEventManager : IGameEventManager
    {
        private IAssetRefs _assetRefs;
        private Dictionary<GameEventType, GameEventView> _pendingEvents;
        private GameObject _eventContainer;

        public GameEventManager(IAssetRefs assetRefs)
        {
            _pendingEvents = new Dictionary<GameEventType, GameEventView>();
            _eventContainer = new GameObject("GameEventContainer");
            _assetRefs = assetRefs;
        }

        public void CreateGameEvent(GameEventType type)
        {
            var newGe = GameObject.Instantiate(_assetRefs.GameEvent, _eventContainer.transform);
            _pendingEvents.Add(type, newGe.GetComponent<GameEventView>());
        }
    }
}