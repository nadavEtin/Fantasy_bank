namespace Assets.GameEvent
{
    public abstract class BaseGameEventData
    {
        protected abstract string _eventText { get; set; }
        protected abstract string _eventTitle { get; set; }
        protected abstract bool _yesResult { get; set; }
        protected abstract bool _noResult { get; set; }
    }
}
