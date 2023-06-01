using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bank;
using GameEvent.LoanEvent;

namespace GameEvent
{
    public class EventValidator : IEventValidator
    {
        private IBankManager _bankManager;
        
        private List<int> _completedEvents;
        private List<BaseGameEventData> _allEvents;
        private List<BaseGameEventData> _availableEventsPool;

        public EventValidator(IBankManager bankManager)
        {
            _completedEvents = new List<int>();
            _allEvents = new List<BaseGameEventData>();
            _availableEventsPool = new List<BaseGameEventData>();
        }

        public void EventCompleted(int id)
        {
            _completedEvents.Add(id);
            _availableEventsPool.Remove(_availableEventsPool.FirstOrDefault(a => a.ID == id));
            UpdateAvailableEvents();
        }

        public void UpdateAvailableEvents()
        {
            foreach (var eve in _allEvents)
            {
                if (EventRequirementsMet(eve.EventRequirements))
                {
                    _availableEventsPool.Add(eve);
                    _allEvents.Remove(eve);
                }
            }
        }

        public bool EventValidationEntry(BaseGameEventData eventData)
        {
            switch (eventData.eventType)
            {
                case GameEventType.Loan:
                    return LoanEventValidation((LoanGameEventData)eventData);
            }

            return false;
        }

        public bool LoanEventValidation(LoanGameEventData eventData)
        {
            if (EventRequirementsMet(eventData.EventRequirements) == false)
                return false;

            return eventData.LoanPrice >= _bankManager.GoldBalance;
        }

        private bool EventRequirementsMet(IReadOnlyCollection<int> requirements)
        {
            if (requirements == null || requirements.Count == 0)
                return true;
            
            //all event requirements for event "eve" have been completed
            return requirements.All(req => _completedEvents.Contains(req));
        }
    }
}