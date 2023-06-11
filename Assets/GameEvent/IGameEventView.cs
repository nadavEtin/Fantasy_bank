namespace GameEvent
{
    public interface IGameEventView
    {
        IGameDataEvent EventData { get; }
        bool EventValidation();

        void ActivateEvent();
    }
}