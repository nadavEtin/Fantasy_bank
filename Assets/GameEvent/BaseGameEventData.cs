using System;
using GameEvent.LoanEvent;

namespace GameEvent
{
    public abstract class BaseGameEventData
    {
        public int[] EventRequirements => _eventRequirements;
        public int ID => _id;

        public abstract void RequirementsMetValidation();

        protected abstract int[] _eventRequirements { get; set; }
        protected abstract int _id { get; set; }
        protected abstract string _eventText { get; set; }
        protected abstract string _eventTitle { get; set; }
        protected abstract Action<bool, IGameEventView> _resolutionCb { get; set; }
        protected abstract GameEventType _type { get; set; }

        protected BaseGameEventData(int id, string eventText, string eventTitle,
            Action<bool, IGameEventView> resolutionCb,
            GameEventType type, int[] eventRequirements)
        {
            _id = id;
            _eventText = eventText;
            _eventTitle = eventTitle;
            _resolutionCb = resolutionCb;
            _type = type;
            _eventRequirements = eventRequirements;
        }
    }
}
