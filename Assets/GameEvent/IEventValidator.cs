using GameEvent.LoanEvent;

namespace GameEvent
{
    public interface IEventValidator
    {
        void UpdateAvailableEvents();
        void EventCompleted(int id);
        bool LoanEventValidation(LoanGameEventData eventData);
    }
}