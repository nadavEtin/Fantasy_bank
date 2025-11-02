using System;
using GameCore.EventBus;
using Lean.Touch;

namespace GameCore.Input
{
    public class InputManager : IDisposable, IInputManager
    {
        private readonly EventsManager _eventBus;
        
        public LeanFinger RecentTouch { get; private set; }
        
        public InputManager(EventsManager eventBus) 
        {
            _eventBus = eventBus;
            LeanTouch.OnFingerDown += TouchStarted;
            LeanTouch.OnFingerUp += TouchEnded;
        }

        public void Dispose()
        {
            LeanTouch.OnFingerDown -= TouchStarted;
            LeanTouch.OnFingerUp -= TouchEnded;
        }

        private void TouchStarted(LeanFinger finger)
        {
            RecentTouch = finger;
            _eventBus.Publish(GameplayEvent.TouchStarted, new TouchEventParams(TouchPhase.Started, finger));
        }

        private void TouchEnded(LeanFinger finger)
        {
            RecentTouch = finger;
            _eventBus.Publish(GameplayEvent.TouchEnded, new TouchEventParams(TouchPhase.Ended, finger));
        }

        
    }
}
