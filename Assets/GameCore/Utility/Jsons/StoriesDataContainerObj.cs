using System;
using System.Collections.Generic;

namespace GameCore.Utility.Jsons
{
    [Serializable]
    public class StoriesDataContainerObj : IEventsDataContainerObj
    {
        public List<DictionaryWrapper<EventDataSerialized>> RegularEvents; // { get; }
        public List<DictionaryWrapper<LoanStoryDataSerialized>> LoanEvents;

        public StoriesDataContainerObj()
        {
            RegularEvents = new List<DictionaryWrapper<EventDataSerialized>>();
            LoanEvents = new List<DictionaryWrapper<LoanStoryDataSerialized>>();
        }

        /*public StoriesDataContainerObj(List<DictionaryWrapper<EventDataSerialized>> regularEvents,
            List<DictionaryWrapper<LoanStoryDataSerialized>> loanEvents)
        {
            this.RegularEvents = regularEvents;
            this.LoanEvents = loanEvents;
        }*/

        public StoriesDataContainerObj(Dictionary<int, EventDataSerialized> regularEvents,
            Dictionary<int, EventDataSerialized> loanEvents)
        {
            RegularEvents = new List<DictionaryWrapper<EventDataSerialized>>();
            LoanEvents = new List<DictionaryWrapper<LoanStoryDataSerialized>>();
            foreach (var storyKvp in regularEvents)
            {
                RegularEvents.Add(new DictionaryWrapper<EventDataSerialized>(storyKvp.Key, storyKvp.Value));
            }

            foreach (var loanKvp in loanEvents)
            {
                LoanEvents.Add(new DictionaryWrapper<LoanStoryDataSerialized>(loanKvp.Key, (LoanStoryDataSerialized)loanKvp.Value));
            }
        }

        public EventDataSerialized GetSpecificEvent(int idKey)
        {
            var res = RegularEvents.Find(e => e.value.id == idKey);
            if (res != null)
                return res.value;
            var res2 = LoanEvents.Find(e => e.value.id == idKey);
            return res2?.value;
        }

        public EventDataSerialized GetSpecificEvent(string nameKey)
        {
            var res = RegularEvents.Find(e => e.value.name == nameKey);
            if (res != null)
                return res.value;
            var res2 = LoanEvents.Find(e => e.value.name == nameKey);
            return res2?.value;
        }
    }

    [Serializable]
    //A single unit in a dictionaru, or a touple
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