using System;
using System.Collections.Generic;
using GameCore;
using GameCore.EventBus;
using GameCore.ScriptableObjects;
using GameCore.Utility.GeneralClasses;
using GameEvent;
using GameEvent.EventResolution;
using UnityEngine;

namespace Assets.GameEvent.EventResolution
{
    public class EventResolutionViewManager : IEventResolutionViewManager, IDisposable
    {
        private Vector2 _eventResolutionNeutralPos;
        private Stack<IGameDataEvent> _activeEventResolutions;
        private IGameDataEvent _currentlyShownEvent;
        private IBaseFactory _eventResolutionFactory;
        private EventBus _eventBus;
        private IGameDirector _gameDirector;

        private EventResolutionViewManager(IBaseFactory eventResolutionFactory, IGameEventSettings settings, EventBus eventBus, IGameDirector gameDirector)
        {
            _activeEventResolutions = new Stack<IGameDataEvent>();
            _eventResolutionFactory = eventResolutionFactory;
            _eventResolutionNeutralPos = settings.EventResolutionNeutralPos;
            _eventBus = eventBus;
            _gameDirector = gameDirector;

            _eventBus.Subscribe(GameplayEvent.ResolveReadyEvents, ShowEventResolutions);
        }

        public void AddEventResolution(IGameDataEvent data)
        {
            _activeEventResolutions.Push(data);
            
            //_activeEventResolutions.Push(newResolutionView);
        }

        private EventResolutionView CreateEventResolutionView(IGameDataEvent data)
        {
            var newResolutionView = _eventResolutionFactory.Create().GetComponent<EventResolutionView>();
            newResolutionView.Setup(data.EventResolutionTitle, data.EventResolutionMainText, data.ID, ShowEventResolutions);
            return newResolutionView;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe(GameplayEvent.ResolveReadyEvents, ShowEventResolutions);
        }

        private void ApplyEventEffects(int id)
        {


            //continue to the next event
            ShowEventResolutions();
        }

        public void ShowEventResolutions(BaseEventParams baseEventParams = null)
        {
            if (_activeEventResolutions.Count > 0)
            {
                _currentlyShownEvent = _activeEventResolutions.Pop();
                var nextView = CreateEventResolutionView(_currentlyShownEvent);
                nextView.transform.position = _eventResolutionNeutralPos;
            }
            else
            {
                _currentlyShownEvent = null;
                _gameDirector.GamePhaseDone(GamePhases.ResolveReadyEvents);
            }
        }
    }
}