using Lean.Touch;

namespace GameCore.EventBus
{
    public enum TouchPhase
    {
        Started, Ended
    }
    
    public class TouchEventParams : BaseEventParams
    {
        public TouchPhase Phase;
        public LeanFinger TouchData;

        public TouchEventParams(TouchPhase phase, LeanFinger touchData)
        {
            Phase = phase;
            TouchData = touchData;
        }
    }
}