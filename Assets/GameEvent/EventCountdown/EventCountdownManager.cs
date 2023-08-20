using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.EventBus;
using GameCore.EventBus.GameplayEvents;
using GameCore.ScriptableObjects;
using UnityEngine;

namespace GameEvent.EventCountdown
{
    public class EventCountdownManager : IDisposable
    {
        private List<IEventCountdownView> _activeEventCountdowns;
        private readonly EventCountdownFactory _countdownFactory;
        private readonly EventBus _eventBus;
        private readonly IGameEventSettings _settings;
        private readonly Canvas _canvas;
        private readonly float _countdownViewsGap = 15;

        public EventCountdownManager(EventCountdownFactory countdownFactory, EventBus eventBus,
            IGameEventSettings settings, Canvas canvas)
        {
            _activeEventCountdowns = new List<IEventCountdownView>();
            _countdownFactory = countdownFactory;
            _eventBus = eventBus;
            _canvas = canvas;

            //event handling
            _eventBus.Subscribe(GameplayEvent.EventApproved, EventApproved);
            _eventBus.Subscribe(GameplayEvent.NextTurn, NewTurn);
        }

        private void EventApproved(BaseEventParams evParams)
        {
            var eventParams = (EventApprovedParams)evParams;
            var newCdObj = _countdownFactory.Create(_canvas.transform).GetComponent<IEventCountdownView>();
            newCdObj.ObjTransform.SetParent(_canvas.transform);
            //newCdObj.transform.position = 
            newCdObj.Setup(eventParams.EventData);
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
                }
            }
            
            //send all finished countdown ids
            if(finishedCountdownIds.Count > 0)
                _eventBus.Publish(GameplayEvent.EventCountdownDone, new EventCountdownDone(finishedCountdownIds));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.EventApproved, EventApproved);
            _eventBus.Unsubscribe(GameplayEvent.NextTurn, NewTurn);
        }
    }
}