using System.Collections.Generic;
using System.Linq;
using Bank;

namespace GameEvent
{
    public class EventValidator : IEventValidator
    {
        private IBankBalance _bankBalance;
        
        private List<int> _completedEvents;
        private List<IGameEventView> _allEvents;
        private List<IGameEventView> _availableEventsPool;

        public EventValidator()
        {
            _completedEvents = new List<int>();
            _allEvents = new List<IGameEventView>();
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
            foreach (var eve in _allEvents)
            {
                if (eve.EventData.RequirementsMetValidation())
                {
                    _availableEventsPool.Add(eve);
                    _allEvents.Remove(eve);
                }
            }
        }

        public bool EventValidationEntry(IGameEventView eventView)
        {
            var res = eventView.EventValidation();
            var yarp = GeneralEventValidation(eventView);

            return false;
        }

        private bool GeneralEventValidation(IGameEventView eventView)
        {
            return true;
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