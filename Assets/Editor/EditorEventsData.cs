/*using System.IO;
using System.Linq;
using GameCore.Utility.Jsons;
using UnityEngine;

namespace Editor
{
    public class EditorEventsData
    {
        private string _SA_path;
        private string _eventsFileName = "EventsData.json";
        private string _eventsFilePath;
        private StoriesDataContainerObj _eventsDataContainer;
        private StoriesRefs _storiesRefs;

        public EditorEventsData()
        {
            _storiesRefs = (StoriesRefs)Resources.Load("StoriesRefs");
            //_SA_path = $"{Application.dataPath}/StreamingAssets";
            //_eventsFilePath = $"{_SA_path}/{_eventsFileName}";
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
                _eventsDataContainer.regularEvents.Add(new DictionaryWrapper<IStoryDataSerialized>(data.id, data));
            WriteDataToFile();
        }

        public void SaveEvent(LoanStoryDataSerialized data)
        {
            //search for this event by id
            var existingEvent = _eventsDataContainer.loanEvents.FirstOrDefault(e => e.key == data.id);
            
            //replace it if exists otherwise add it
            if (existingEvent != null)
                _eventsDataContainer.loanEvents.Remove(existingEvent);

            _eventsDataContainer.loanEvents.Add(new DictionaryWrapper<IStoryDataSerialized>(data.id, data));
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
                _eventsDataContainer = JsonUtility.FromJson<StoriesDataContainerObj>(jsonString);
            }
            else
                _eventsDataContainer = new StoriesDataContainerObj();
        }
    }
}*/