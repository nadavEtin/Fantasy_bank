using Lean.Touch;

namespace GameCore.Input
{
    public interface IInputManager
    {
        LeanFinger RecentTouch { get; }
    }
}