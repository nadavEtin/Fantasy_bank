using GameEvent;

namespace Assets.GameEvent.EventResolution
{
    public interface IEventResolutionViewManager
    {
        void AddEventResolution(IGameDataEvent data);
    }
}