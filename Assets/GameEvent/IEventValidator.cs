namespace GameEvent
{
    public interface IEventValidator
    {
        void UpdateAvailableEvents();
        void EventCompleted(int id);
    }
}