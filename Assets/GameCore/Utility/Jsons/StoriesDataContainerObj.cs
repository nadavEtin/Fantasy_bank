using System;
using System.Collections.Generic;

namespace GameCore.Utility.Jsons
{
    [Serializable]
    public class StoriesDataContainerObj : IEventsDataContainerObj
    {
        public List<DictionaryWrapper<EventDataSerialized>> RegularEvents; //Any change to this parameter names must be reflected in the stories data json file!!
        public List<DictionaryWrapper<LoanStoryDataSerialized>> LoanEvents; //Any change to this parameter names must be reflected in the stories data json file!!

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

        public StoriesDataContainerObj(Dictionary<string, EventDataSerialized> regularEvents,
            Dictionary<string, EventDataSerialized> loanEvents)
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
    //A single unit in a dictionary, or a touple
    public class DictionaryWrapper<T>
    {
        public string key;
        public T value;

        public DictionaryWrapper(string key, T value)
        {
            this.key = key;
            this.value = value;
        }
    }

}