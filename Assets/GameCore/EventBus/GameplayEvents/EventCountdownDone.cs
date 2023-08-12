using System.Collections.Generic;
using GameEvent;

namespace GameCore.EventBus.GameplayEvents
{
    public class EventCountdownDone : BaseEventParams
    {
        public List<IGameDataEvent> CompletedEventsData { get; }

        public EventCountdownDone(List<IGameDataEvent> completedEventsData)
        {
            CompletedEventsData = completedEventsData;
        }
    }
}