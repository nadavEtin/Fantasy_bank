using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace GameCore.Utility.Jsons
{
    [Serializable]
    public class EventsDataContainerObj : IEventsDataContainerObj
    {
        public List<DictionaryWrapper<EventDataSerialized>> regularEvents; // { get; }
        public List<DictionaryWrapper<LoanEventDataSerialized>> loanEvents;

        public EventsDataContainerObj()
        {
            regularEvents = new List<DictionaryWrapper<EventDataSerialized>>();
            loanEvents = new List<DictionaryWrapper<LoanEventDataSerialized>>();
        }

        public EventsDataContainerObj(List<DictionaryWrapper<EventDataSerialized>> regularEvents,
            List<DictionaryWrapper<LoanEventDataSerialized>> loanEvents)
        {
            this.regularEvents = regularEvents;
            this.loanEvents = loanEvents;
        }

        public EventDataSerialized GetSpecificEvent(int idKey)
        {
            var res = regularEvents.Find(e => e.value.id == idKey);
            if (res != null)
                return res.value;
            var res2 = loanEvents.Find(e => e.value.id == idKey);
            return res2?.value;
        }
        
        public EventDataSerialized GetSpecificEvent(string nameKey)
        {
            var res = regularEvents.Find(e => e.value.name == nameKey);
            if (res != null)
                return res.value;
            var res2 = loanEvents.Find(e => e.value.name == nameKey);
            return res2?.value;
        }
    }

    [Serializable]
    public class DictionaryWrapper<T>
    {
        public int key;
        public T value;

        public DictionaryWrapper(int key, T value)
        {
            this.key = key;
            this.value = value;
        }
    }
}