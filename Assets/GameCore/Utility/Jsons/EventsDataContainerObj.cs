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

        //public List<object> regularEvents { get; }
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