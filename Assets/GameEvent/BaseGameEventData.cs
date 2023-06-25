﻿using System;
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
        public int[] EventRequirements { get; protected set; }
        public int ID { get; protected set; }

        public abstract bool RequirementsMetValidation();
        public GameEventType EventType { get; protected set; }
        protected string _eventText { get; set; }
        protected string _eventTitle { get; set; }
        protected Action<bool, IGameEventView> _resolutionCb { get; set; }
        protected IBankBalance _bankBalance;
        

        protected BaseGameEventData(int id, string eventText, string eventTitle, IBankBalance bankBalance,
            Action<bool, IGameEventView> resolutionCb, GameEventType eventType, int[] eventRequirements)
        {
            _bankBalance = bankBalance;
            ID = id;
            _eventText = eventText;
            _eventTitle = eventTitle;
            _resolutionCb = resolutionCb;
            EventType = eventType;
            EventRequirements = eventRequirements;
        }
    }
}
