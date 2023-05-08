using Assets.Scripts.Utility;
using GameCore.Events;
using GameCore.ScriptableObjects;
using Lean.Touch;
using VContainer;
using VContainer.Unity;

namespace Assets.GameCore
{
    public class InputManager
    {
        private EventBus _eventBus;

        [Inject]
        public InputManager(EventBus eventBus, IAssetRefs assetRefs, IObjectResolver container) 
        {
            _eventBus = eventBus;
            container.Instantiate(assetRefs.GameEvent);
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
            _eventBus.Publish(GameplayEvent.TouchStarted, new TouchEventParams(TouchPhase.Started, finger));
        }

        private void TouchEnded(LeanFinger finger)
        {
            _eventBus.Publish(GameplayEvent.TouchEnded, new TouchEventParams(TouchPhase.Ended, finger));
        }
    }
}
