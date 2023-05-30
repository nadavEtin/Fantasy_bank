using System;
using System.Collections.Generic;
using Bank;
using GameCore;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using UnityEngine;

namespace GameEvent
{
    public class GameEventManager : IGameEventManager, IGameEventValidation
    {
        private IAssetRefs _assetRefs;
        private IGameDirector _gameDirector;
        private IBankManager _bankManager;
        private EventValidator _eventValidator;
        private Camera _camera;
        private Dictionary<GameEventType, List<IGameEventView>> _pendingEvents;
        private Dictionary<GameEventType, List<IGameEventView>> _approvedEventsOnCountdown;
        private GameObject _eventContainer;

        public GameEventManager(IAssetRefs assetRefs, IGameDirector gameDirector, IBankManager bankManager,
            Camera camera)
        {
            _assetRefs = assetRefs;
            _gameDirector = gameDirector;
            _bankManager = bankManager;
            _eventValidator = new EventValidator();
            _camera = camera;
            _pendingEvents = new Dictionary<GameEventType, List<IGameEventView>>();
            _approvedEventsOnCountdown = new Dictionary<GameEventType, List<IGameEventView>>();
            foreach (GameEventType type in Enum.GetValues(typeof(GameEventType)))
                _approvedEventsOnCountdown.Add(type, new List<IGameEventView>());
            _eventContainer = new GameObject("GameEventContainer");
        }

        public void CreateGameEvent(GameEventType type)
        {
            var geView = GameObject.Instantiate(_assetRefs.GameEvent, _eventContainer.transform)
                .GetComponent<GameEventView>();
            geView.Init(_gameDirector, _bankManager, EventResolution, _camera);

            if (_pendingEvents.ContainsKey(type) == false)
                _pendingEvents.Add(type, new List<IGameEventView>());
            _pendingEvents[type].Add(geView);
        }

        private void EventResolution(bool approved, IGameEventView curEvent = null)
        {
            if (approved)
                _approvedEventsOnCountdown[curEvent.EventData.Type].Add(curEvent);
        }

        public bool EventValidation(BaseGameEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}