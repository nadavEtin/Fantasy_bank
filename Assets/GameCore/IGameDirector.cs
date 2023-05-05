using Assets.Scripts.Utility;
using GameCore.Events;

namespace GameCore
{
    public interface IGameDirector
    {
        TouchEventParams RecentTouch { get; }
    }
}