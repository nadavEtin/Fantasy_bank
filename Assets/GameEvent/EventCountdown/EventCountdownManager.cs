using System;
using System.Collections.Generic;
using System.Linq;
using Assets.GameEvent.EventResolution;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.ScriptableObjects;
using GameCore.UI;
using GameCore.Utility.GeneralClasses;
using UnityEngine;

namespace GameEvent.EventCountdown
{
    public class EventCountdownManager : IDisposable
    {
        private List<IEventCountdownView> _activeEventCountdowns;
        private readonly EventCountdownFactory _countdownFactory;
        private readonly EventsManager _eventBus;
        private readonly IGameEventSettings _settings;
        private readonly IUiManager _uiManager;
        private readonly float _countdownViewsGap = 15;
        private IEventResolutionViewManager _eventResolutionManager;
        private RectTransform _coundownContainer;

        public EventCountdownManager(EventCountdownFactory countdownFactory, IEventResolutionViewManager eventResolutionViewManager, EventsManager eventBus, IGameEventSettings settings, IUiManager uiManager)
        {
            _activeEventCountdowns = new List<IEventCountdownView>();
            _countdownFactory = countdownFactory;
            _eventBus = eventBus;
            _uiManager = uiManager;
            _settings = settings;
            _eventResolutionManager = eventResolutionViewManager;
            _coundownContainer = uiManager.CanvasRefs.StoryEventCountdownHolder;

            //event handling
            _eventBus.Subscribe(GameplayEvent.StoryEventApproved, EventApproved);
            _eventBus.Subscribe(GameplayEvent.NextTurn, NewTurn);
        }

        private void EventApproved(BaseEventParams evParams)
        {
            var eventParams = (EventApprovedParams)evParams;
            var newCdObj = _countdownFactory.Create(_uiManager.Canvas.transform).GetComponent<IEventCountdownView>();            
            newCdObj.Setup(eventParams.EventData);
            newCdObj.ObjTransform.SetParent(_coundownContainer);
            //newCdObj.ObjTransform.SetParent(_canvas.transform);

            _activeEventCountdowns.Add(newCdObj);
            SortCountdownViews();
        }

        /*private void ResolveCountdownEvent(IGameDataEvent data)
        {
            
        }*/

        private void SortCountdownViews()
        {
            _activeEventCountdowns = _activeEventCountdowns.OrderBy(c => c.CountdownDuration).ToList();
            var spacing = _activeEventCountdowns[0].ObjTransform.rect.size.y * 1.2f;
            for (int i = 0; i < _activeEventCountdowns.Count; i++)
            {
                _activeEventCountdowns[i].ObjTransform.anchoredPosition = new Vector2(0, 0 + spacing * i);
            }
        }

        private void NewTurn(BaseEventParams evParams)
        {
            var finishedCountdownIds = new List<IGameDataEvent>();
            
            foreach (var activeEventCountdown in _activeEventCountdowns)
            {
                activeEventCountdown.ReduceCountdown(_settings.DefaultEventCountReduction);
                if (activeEventCountdown.CountdownDuration <= 0)
                {
                    _activeEventCountdowns.Remove(activeEventCountdown);
                    activeEventCountdown.CountdownDone();
                    finishedCountdownIds.Add(activeEventCountdown.EventData);

                    //add finished countdown to event resolution
                    _eventResolutionManager.AddEventResolution(activeEventCountdown.EventData);
                }
            }
            
            //send all finished countdown ids
            if(finishedCountdownIds.Count > 0)
                _eventBus.Publish(GameplayEvent.EventCountdownDone, new EventCountdownDone(finishedCountdownIds));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.StoryEventApproved, EventApproved);
            _eventBus.Unsubscribe(GameplayEvent.NextTurn, NewTurn);
        }
    }
}