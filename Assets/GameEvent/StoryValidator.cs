using Bank;
using GameEvent.EventCardView;
using GameEvent.LoanEvent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEvent
{
    public class StoryValidator : IEventValidator
    {
        private IBankBalance _bankBalance;

        private List<int> _completedEvents;
        private List<IGameEventView> _unavailableEvents;
        private List<IGameEventView> _availableEventsPool;

        public StoryValidator()
        {
            _completedEvents = new List<int>();
            _unavailableEvents = new List<IGameEventView>();
            _availableEventsPool = new List<IGameEventView>();
        }

        public void EventCompleted(int id)
        {
            _completedEvents.Add(id);
            _availableEventsPool.Remove(_availableEventsPool.FirstOrDefault(a => a.EventData.ID == id));
            UpdateAvailableEvents();
        }

        public void UpdateAvailableEvents()
        {
            foreach (var eve in _unavailableEvents)
            {
                if (EventRequirementsMet(eve.EventData.EventRequirements))
                {
                    //Add the event to the pool of available events
                    _availableEventsPool.Add(eve);

                    //Remove the event from the general pool of unavailable events
                    _unavailableEvents.Remove(eve);
                }
            }
        }

        public bool EventValidationEntry(IGameEventView eventView)
        {
            var res = false;
            if (GeneralEventValidation(eventView))
            {
                switch (eventView.EventData.EventType)
                {
                    case StoryType.Loan:
                        res = LoanTypeValidation(eventView.EventData);
                        break;
                    default:
                        break;
                }
            }

            return res;
        }

        public List<IGameEventView> GetEventsForCurrentTurn()
        {
            Random random = new Random();
            var result = new List<IGameEventView>();
            UpdateAvailableEvents();
            for (int i = 0; i < 3; i++)
            {
                if (_availableEventsPool.Count > 0)
                {
                    var rnd = random.Next(_availableEventsPool.Count - 1);
                    result.Add(_availableEventsPool[rnd]);
                    _availableEventsPool.RemoveAt(rnd);
                }
                else
                {
                    //Debug.Log("out of events");
                    break;
                }
            }

            return result;
        }

        private bool GeneralEventValidation(IGameEventView eventView)
        {
            return EventRequirementsMet(eventView.EventData.EventRequirements);
        }

        private bool EventRequirementsMet(IReadOnlyCollection<int> requirements)
        {
            if (requirements == null || requirements.Count == 0)
                return true;

            //all event requirements for event "eve" have been completed
            return requirements.All(req => _completedEvents.Contains(req));
        }

        private bool LoanTypeValidation(IGameDataEvent eventData)
        {
            var loanData = (ILoanGameDataEvent)eventData;
            return _bankBalance.GoldBalance >= loanData.LoanPrice;
        }
    }
}