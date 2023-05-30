using System.Collections.Generic;
using System.Linq;

namespace GameEvent
{
    public class EventValidator : IEventValidator
    {
        private List<int> _completedEvents;
        private List<BaseGameEventData> _allEvents;
        private List<BaseGameEventData> _availableEventsPool;
        
        

        public EventValidator()
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
                //all event requirements for event "eve" have been completed
                if (eve.EventRequirements.All(req => _completedEvents.Contains(req)))
                {
                    _availableEventsPool.Add(eve);
                    _allEvents.Remove(eve);
                }
            }
        }
    }
}