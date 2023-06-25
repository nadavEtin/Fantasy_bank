using GameEvent.EventCardView;

namespace GameEvent
{
    public interface IEventValidator
    {
        void UpdateAvailableEvents();
        void EventCompleted(int id);
        bool EventValidationEntry(IGameEventView eventView);
    }
}