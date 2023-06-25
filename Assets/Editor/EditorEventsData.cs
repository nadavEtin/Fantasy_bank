using System.IO;
using System.Linq;
using GameCore.Utility.Jsons;
using UnityEngine;

namespace Editor
{
    public class EditorEventsData
    {
        private string _SA_path;// = $"{Application.dataPath}/StreamingAssets";
        private string _eventsFileName = "EventsData.json";
        private string _eventsFilePath;
        private EventsDataContainerObj _eventsDataContainer;

        public EditorEventsData()
        {
            _SA_path = $"{Application.dataPath}/StreamingAssets";
            _eventsFilePath = $"{_SA_path}/{_eventsFileName}";
            LoadEventsFromFile();
        }

        public void SaveEvent(EventDataSerialized data)
        {
            //search for this event by id
            var existingEvent = _eventsDataContainer.regularEvents.FirstOrDefault(e => e.key == data.id);
            
            //replace it if exists otherwise add it
            if (existingEvent != null)
                _eventsDataContainer.regularEvents.Remove(existingEvent);
            else
                _eventsDataContainer.regularEvents.Add(new DictionaryWrapper<EventDataSerialized>(data.id, data));
            WriteDataToFile();
        }

        public void SaveEvent(LoanEventDataSerialized data)
        {
            //search for this event by id
            var existingEvent = _eventsDataContainer.loanEvents.FirstOrDefault(e => e.key == data.id);
            
            //replace it if exists otherwise add it
            if (existingEvent != null)
                _eventsDataContainer.loanEvents.Remove(existingEvent);

            _eventsDataContainer.loanEvents.Add(new DictionaryWrapper<LoanEventDataSerialized>(data.id, data));
            WriteDataToFile();
        }

        public EventDataSerialized LoadSpecificEvent(int idKey)
        {
            return _eventsDataContainer.GetSpecificEvent(idKey);
        }
        
        public EventDataSerialized LoadSpecificEvent(string titleKey)
        {
            return _eventsDataContainer.GetSpecificEvent(titleKey);
        }

        private void WriteDataToFile()
        {
            var jsonStr = JsonUtility.ToJson(_eventsDataContainer, true);
            File.WriteAllText($"{_SA_path}/{_eventsFileName}", jsonStr);
        }

        private void LoadEventsFromFile()
        {
            if (File.Exists(_eventsFilePath))
            {
                var jsonString = File.ReadAllText(_eventsFilePath);
                _eventsDataContainer = JsonUtility.FromJson<EventsDataContainerObj>(jsonString);
            }
            else
                _eventsDataContainer = new EventsDataContainerObj();
        }
    }
}