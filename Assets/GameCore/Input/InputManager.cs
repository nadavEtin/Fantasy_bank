using Assets.Scripts.Utility;
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
            _curFinger = LeanTouch.Fingers[0];
        }

        private void TouchEnded(LeanFinger finger)
        {
            _curFinger = null;
        }
    }
}
