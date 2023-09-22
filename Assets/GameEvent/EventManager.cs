using System;
using System.Collections.Generic;
using Bank;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameEvent.EventCardView;
using UnityEngine;

namespace GameEvent
{
    public class EventManager : IGameEventManager, IDisposable
    {
        private IAssetRefs _assetRefs;
        private readonly IInputManager _inputManager;
        private readonly IBankBalance _bankBalance;
        private IEventValidator _eventValidator;
        private Camera _camera;
        private EventBus _eventBus;

        //events that are being showed to the player in this round, usually 1-5 events
        private List<IGameEventView> _currentRoundEvents;
        private Dictionary<StoryType, List<IGameEventView>> _pendingEvents;
        private Dictionary<StoryType, List<IGameEventView>> _approvedEventsOnCountdown;
        private readonly GameObject _eventContainer;

        public EventManager(IAssetRefs assetRefs, IInputManager inputManager,
            IBankBalance bankBalance, Camera camera, EventBus eventBus)
        {
            _assetRefs = assetRefs;
            _inputManager = inputManager;
            _bankBalance = bankBalance;
            _currentRoundEvents = new List<IGameEventView>();
            _eventValidator = new StoryValidator();
            _camera = camera;
            _eventBus = eventBus;
            _pendingEvents = new Dictionary<StoryType, List<IGameEventView>>();
            _approvedEventsOnCountdown = new Dictionary<StoryType, List<IGameEventView>>();

            foreach (StoryType type in Enum.GetValues(typeof(StoryType)))
                _approvedEventsOnCountdown.Add(type, new List<IGameEventView>());
            _eventContainer = new GameObject("GameEventContainer");
            EventSubscriptions();
        }

        public void EventValidation(IGameEventView view)
        {
            _eventValidator.EventValidationEntry(view);
        }

        public void CreateGameEvent(StoryType type)
        {
            //TODO: instantiate through the lifetime resolver and inject the needed vars, replace init method with constructor
            var geView = GameObject.Instantiate(_assetRefs.StoryView, _eventContainer.transform)
                .GetComponent<GameEventView>();
            geView.Init(_inputManager, _bankBalance, EventResolution, _camera, _eventBus);

            if (_pendingEvents.ContainsKey(type) == false)
                _pendingEvents.Add(type, new List<IGameEventView>());
            _pendingEvents[type].Add(geView);
        }

        private void CountdownResolution(BaseEventParams events)
        {
            var eventParams = (EventCountdownDone)events;
            foreach (var eventData in eventParams.CompletedEventsData)
            {
                var currentEventView = _approvedEventsOnCountdown[eventData.EventType]
                    .Find(e => e.EventData.ID == eventData.ID);
                if (currentEventView != null)
                {
                    //show event result screen
                }
            }
        }

        private void NewTurn(BaseEventParams eventParams)
        {
            _currentRoundEvents = _eventValidator.GetEventsForCurrentTurn();
            NextEvent();
        }

        private void NextEvent()
        {
            if (_currentRoundEvents.Count > 0)
            {
                var curEvent = _currentRoundEvents[0];
                curEvent.ActivateEvent();
            }
            else
            {
                //Advance to the next round?
            }
        }

        private void EventResolution(bool approved, IGameEventView curEvent = null)
        {
            if (approved && curEvent != null)
                _approvedEventsOnCountdown[curEvent.EventData.EventType].Add(curEvent);
        }

        private void EventSubscriptions()
        {
            _eventBus.Subscribe(GameplayEvent.EventCountdownDone, CountdownResolution);
            _eventBus.Subscribe(GameplayEvent.NextTurn, NewTurn);
            _eventBus.Subscribe(GameplayEvent.GameStart, NewTurn);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.EventCountdownDone, CountdownResolution);
            _eventBus.Unsubscribe(GameplayEvent.NextTurn, NewTurn);
        }
    }
}