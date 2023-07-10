using System.Collections.Generic;

namespace GameCore.EventBus.GameplayEvents
{
    public class EventCountdownDone : BaseEventParams
    {
        public List<int> CompletedEventIds { get; private set; }

        public EventCountdownDone(List<int> completedEventIds)
        {
            CompletedEventIds = completedEventIds;
        }
    }
}