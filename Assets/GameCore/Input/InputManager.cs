using Assets.Scripts.Utility;
using GameCore.Events;
using Lean.Touch;

namespace Assets.GameCore
{
    public class InputManager
    {
        private EventBus _eventBus;

        public InputManager(EventBus eventBus) 
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += TouchStarted;
            LeanTouch.OnFingerUp += TouchEnded;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= TouchStarted;
            LeanTouch.OnFingerUp -= TouchEnded;
        }

        private void TouchStarted(LeanFinger finger)
        {
            _eventBus.Publish(GameplayEvent.TouchStarted, new TouchEvent(TouchPhase.Started, finger));
        }

        private void TouchEnded(LeanFinger finger)
        {
            _eventBus.Publish(GameplayEvent.TouchEnded, new TouchEvent(TouchPhase.Ended, finger));
        }
    }
}
