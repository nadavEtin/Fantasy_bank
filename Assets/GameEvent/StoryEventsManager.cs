using System;
using System.Collections.Generic;
using Assets.GameEvent.EventCardView;
using Bank;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.Input;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using GameCore.Utility.Jsons;
using GameEvent.StoryView;
using UnityEngine;

namespace GameEvent
{
    public class StoryEventsManager : IGameEventManager, IDisposable
    {
        private IAssetRefs _assetRefs;
        private readonly IInputManager _inputManager;
        private readonly IBankBalance _bankBalance;
        private IStoryValidator _storyValidator;
        private StoryViewFactory _storyViewFactory;
        private Camera _camera;
        private EventsManager _eventBus;

        //events that are being showed to the player in this round, usually 1-5 events
        private List<IGameDataEvent> _currentRoundEvents;
        private Dictionary<StoryType, List<IGameDataEvent>> _pendingEvents;
        private Dictionary<StoryType, List<IGameDataEvent>> _approvedEventsOnCountdown;
        private readonly GameObject _eventContainer;

        public StoryEventsManager(IAssetRefs assetRefs, IInputManager inputManager, StoryViewFactory storyViewFactory,
            IBankBalance bankBalance, IStoriesRefs storiesRefs, Camera camera, EventsManager eventBus)
        {
            _assetRefs = assetRefs;
            _inputManager = inputManager;
            _bankBalance = bankBalance;
            _storyViewFactory = storyViewFactory;
            _currentRoundEvents = new List<IGameDataEvent>();
            _storyValidator = new StoryValidator(bankBalance, storiesRefs);
            _camera = camera;
            _eventBus = eventBus;
            _pendingEvents = new Dictionary<StoryType, List<IGameDataEvent>>();
            _approvedEventsOnCountdown = new Dictionary<StoryType, List<IGameDataEvent>>();

            foreach (StoryType type in Enum.GetValues(typeof(StoryType)))
                _approvedEventsOnCountdown.Add(type, new List<IGameDataEvent>());
            _eventContainer = new GameObject("GameEventContainer");
            EventSubscriptions();
        }

        public void EventValidation(IGameDataEvent data)
        {
            _storyValidator.StoryEventValidationEntry(data);
        }        

        private void CountdownResolution(BaseEventParams events)
        {
            var eventParams = (EventCountdownDone)events;
            foreach (var eventData in eventParams.CompletedEventsData)
            {
                var currentEventView = _approvedEventsOnCountdown[eventData.EventType]
                    .Find(e => e.ID == eventData.ID);
                if (currentEventView != null)
                {
                    //show event result screen
                }
            }
        }

        private void GameStart(BaseEventParams eventParams = null)
        {
            _storyValidator.GameStart();
            NewTurn();
        }

        private void NewTurn(BaseEventParams eventParams = null)
        {
            _currentRoundEvents = _storyValidator.GetStoriesForCurrentTurn();
            NextStory();
        }

        private void NextStory()
        {
            if (_currentRoundEvents.Count > 0)
            {
                var curStory = _currentRoundEvents[0];
                var curStoryView = _storyViewFactory.Create(_eventContainer.transform);
                var validation = _storyValidator.StoryEventValidationEntry(curStory);
                curStoryView.GetComponent<IStoryCardView>().Init(curStory, EventResolution);
                curStoryView.GetComponent<IStoryCardView>().ActivateEvent(validation);
            }
            else
            {
                //Advance to the next round?
            }
        }

        private void EventResolution(bool approved, IGameDataEvent curEvent = null)
        {
            if (approved && curEvent != null)
                _approvedEventsOnCountdown[curEvent.EventType].Add(curEvent);
        }

        private void EventSubscriptions()
        {
            _eventBus.Subscribe(GameplayEvent.EventCountdownDone, CountdownResolution);
            _eventBus.Subscribe(GameplayEvent.NextTurn, NewTurn);
            _eventBus.Subscribe(GameplayEvent.GameStart, GameStart);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.EventCountdownDone, CountdownResolution);
            _eventBus.Unsubscribe(GameplayEvent.NextTurn, GameStart);
        }
    }
}