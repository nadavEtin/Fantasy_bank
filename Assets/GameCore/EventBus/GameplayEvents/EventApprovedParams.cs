using GameEvent;

namespace GameCore.EventBus.GameplayEvents
{
    public class EventApprovedParams : BaseEventParams
    {
        public IGameDataEvent EventData;

        public EventApprovedParams(IGameDataEvent eventData)
        {
            EventData = eventData;
        }
    }
}