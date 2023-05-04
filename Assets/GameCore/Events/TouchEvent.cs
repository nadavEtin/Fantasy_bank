using Assets.Scripts.Utility;
using Lean.Touch;

namespace GameCore.Events
{
    public enum TouchPhase
    {
        Started, Ended
    }
    
    public class TouchEvent : BaseEventParams
    {
        public TouchPhase Phase;
        public LeanFinger TouchData;

        public TouchEvent(TouchPhase phase, LeanFinger touchData)
        {
            Phase = phase;
            TouchData = touchData;
        }
    }
}