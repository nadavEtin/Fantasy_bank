namespace GameEvent
{
    public interface IGameDataEvent
    {
        GameEventType EventType { get; }
        int ID { get; }
        string EventText { get; }
        string EventTitle { get; }
        int CountdownDuration { get; }
        bool RequirementsMetValidation();
    }
}
