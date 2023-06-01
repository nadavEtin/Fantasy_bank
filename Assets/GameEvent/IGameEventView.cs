namespace GameEvent
{
    public interface IGameEventView
    {
        IGameDataEvent EventData { get; }
        void ActivateEvent();
    }
}