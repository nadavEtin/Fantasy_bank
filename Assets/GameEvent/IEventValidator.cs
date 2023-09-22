using GameEvent.EventCardView;
using System.Collections.Generic;

namespace GameEvent
{
    public interface IEventValidator
    {
        List<IGameEventView> GetEventsForCurrentTurn();
        void UpdateAvailableEvents();
        void EventCompleted(int id);
        bool EventValidationEntry(IGameEventView eventView);
    }
}