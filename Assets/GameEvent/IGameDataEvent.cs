namespace GameEvent
{
    public interface IGameDataEvent
    {
        GameEventType EventType { get; }
        int ID { get; }
        string EventText { get; }
        string EventTitle { get; }
        string EventResolutionTitle { get; }
        string EventResolutionMainText { get; }
        int CountdownDuration { get; }
        bool RequirementsMetValidation();
    }
}
