using System;
using System.Collections.Generic;
using Bank;
using GameCore;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameEvent.LoanEvent;
using UnityEngine;

namespace GameEvent
{
    public class EventManager : IGameEventManager, IEventViewAccess
    {
        private IAssetRefs _assetRefs;
        private IInputManager _inputManager;
        private IBankManager _bankManager;
        private EventValidator _eventValidator;
        private Camera _camera;
        private Dictionary<GameEventType, List<IGameEventView>> _pendingEvents;
        private Dictionary<GameEventType, List<IGameEventView>> _approvedEventsOnCountdown;
        private GameObject _eventContainer;

        public EventManager(IAssetRefs assetRefs, IInputManager inputManager, 
            IBankManager bankManager, Camera camera)
        {
            _assetRefs = assetRefs;
            _inputManager = inputManager;
            _bankManager = bankManager;
            _eventValidator = new EventValidator(_bankManager);
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
            geView.Init(_inputManager, this, _bankManager, EventResolution, _camera);

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
            switch (eventData.eventType)
            {
                case GameEventType.Loan:
                    return _eventValidator.LoanEventValidation((LoanGameEventData)eventData);;
                default:
                    return true;
            }
        }
    }
}