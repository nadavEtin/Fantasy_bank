using System;
using GameCore.Events;
using Lean.Touch;

namespace GameCore.Input
{
    public class InputManager : IDisposable
    {
        private EventBus _eventBus;
        
        public InputManager(EventBus eventBus) 
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
            _eventBus.Publish(GameplayEvent.TouchStarted, new TouchEventParams(TouchPhase.Started, finger));
        }

        private void TouchEnded(LeanFinger finger)
        {
            _eventBus.Publish(GameplayEvent.TouchEnded, new TouchEventParams(TouchPhase.Ended, finger));
        }

        
    }
}
