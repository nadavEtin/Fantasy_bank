namespace GameEvent
{
    public interface IGameDataEvent
    {
        StoryType EventType { get; }
        int ID { get; }
        string EventText { get; }
        string EventTitle { get; }
        string EventResolutionTitle { get; }
        string EventResolutionMainText { get; }
        int CountdownDuration { get; }
        int[] EventRequirements { get; }
        //bool RequirementsMetValidation();
    }
}
