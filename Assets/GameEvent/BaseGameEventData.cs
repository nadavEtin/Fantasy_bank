using System;
using Bank;
using GameCore.Utility.Jsons;
using GameEvent.StoryView;

namespace GameEvent
{
    public enum StoryType
    {
        Other = 0,
        Loan = 1
    }
    
    public abstract class BaseGameEventData : IGameDataEvent
    {
        public int[] EventRequirements { get; }
        public int ID { get; }

        //public abstract bool RequirementsMetValidation();
        public StoryType EventType { get; }
        public string EventText { get; }
        public string EventTitle { get; }
        public int CountdownDuration { get; }
        public string EventResolutionTitle { get; }
        public string EventResolutionMainText { get; }
        protected Action<bool, IStoryCardView> _resolutionCb { get; set; }
        //protected IBankBalance _bankBalance;
        
        protected BaseGameEventData(EventDataSerialized eventData)
        {
            //_bankBalance = bankBalance;
            //_resolutionCb = resolutionCb;

            ID = eventData.id;
            EventText = eventData.text;
            EventTitle = eventData.name;
            EventResolutionTitle = eventData.resolutionName;
            EventResolutionMainText = eventData.resolutionText;
            CountdownDuration = eventData.eventDuration;            
            EventType = (StoryType)eventData.type;
            EventRequirements = eventData.eventRequirements;
        }

        protected BaseGameEventData(int id, string eventText, string eventTitle, string eventResolutionTitle, string eventResolutionMainText, int countdownDuration, StoryType eventType, int[] eventRequirements, 
            IBankBalance bankBalance, Action<bool, IStoryCardView> resolutionCb)
        {
            //_bankBalance = bankBalance;
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
