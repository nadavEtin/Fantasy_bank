using System;
using Bank;
using GameEvent.EventCardView;

namespace GameEvent
{
    public enum GameEventType
    {
        Loan = 1
    }
    
    public abstract class BaseGameEventData : IGameDataEvent
    {
        public int[] EventRequirements { get; }
        public int ID { get; }

        public abstract bool RequirementsMetValidation();
        public GameEventType EventType { get; }
        public string EventText { get; }
        public string EventTitle { get; }
        public int CountdownDuration { get; }
        protected Action<bool, IGameEventView> _resolutionCb { get; set; }
        protected IBankBalance _bankBalance;
        

        protected BaseGameEventData(int id, string eventText, string eventTitle, int countdownDuration, IBankBalance bankBalance,
            Action<bool, IGameEventView> resolutionCb, GameEventType eventType, int[] eventRequirements)
        {
            _bankBalance = bankBalance;
            ID = id;
            EventText = eventText;
            EventTitle = eventTitle;
            CountdownDuration = countdownDuration;
            _resolutionCb = resolutionCb;
            EventType = eventType;
            EventRequirements = eventRequirements;
        }
    }
}
