using System;

namespace Assets.GameEvent
{
    public abstract class BaseGameEventData
    {
        protected abstract string _eventText { get; set; }
        protected abstract string _eventTitle { get; set; }
        protected abstract Action _yesResult { get; set; }
        protected abstract Action _noResult { get; set; }
        protected abstract int _loanPrice { get; set; }
        protected abstract int _successChance { get; set; }
    }
}
