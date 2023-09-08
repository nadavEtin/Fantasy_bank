using System;
using Bank;
using GameCore.Utility.Jsons;
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
        public string EventResolutionTitle { get; }
        public string EventResolutionMainText { get; }
        protected Action<bool, IGameEventView> _resolutionCb { get; set; }
        protected IBankBalance _bankBalance;
        
        protected BaseGameEventData(EventDataSerialized eventData)
        {

        }

        protected BaseGameEventData(int id, string eventText, string eventTitle, string eventResolutionTitle, string eventResolutionMainText, int countdownDuration, GameEventType eventType, int[] eventRequirements, 
            IBankBalance bankBalance, Action<bool, IGameEventView> resolutionCb)
        {
            _bankBalance = bankBalance;
            ID = id;
            EventText = eventText;
            EventTitle = eventTitle;
            EventResolutionTitle = eventResolutionTitle;
            EventResolutionMainText = eventResolutionMainText;
            CountdownDuration = countdownDuration;
            _resolutionCb = resolutionCb;
            EventType = eventType;
            EventRequirements = eventRequirements;
        }
    }
}
