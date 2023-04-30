namespace Assets.GameEvent.LoanEvent
{
    public class LoanGameEventData : BaseGameEventData
    {
        protected override string _eventText { get; set; }
        protected override string _eventTitle { get; set; }
        protected override bool _yesResult { get; set; }
        protected override bool _noResult { get; set; }
    }
}
