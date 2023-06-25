namespace GameEvent.EventCardView
{
    public interface IGameEventView
    {
        IGameDataEvent EventData { get; }
        bool EventValidation();

        void ActivateEvent();
    }
}