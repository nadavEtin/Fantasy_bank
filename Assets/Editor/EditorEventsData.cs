using System.IO;
using System.Linq;
using GameCore.Utility.Jsons;
using Unity.VisualScripting;
using UnityEngine;

namespace Editor
{
    //[ExecuteAlways]
    public class EditorEventsData
    {
        private string _SA_path;// = $"{Application.dataPath}/StreamingAssets";
        private string _eventsFileName = "EventsData.json";
        private EventsDataContainerObj _eventsDataContainer;

        public EditorEventsData()
        {
            _SA_path = $"{Application.dataPath}/StreamingAssets";
            LoadEventsFromFile($"{_SA_path}/{_eventsFileName}");
        }

        public void Init()
        {
            
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
            else
                _eventsDataContainer.loanEvents.Add(new DictionaryWrapper<LoanEventDataSerialized>(data.id, data));
            WriteDataToFile();
        }

        private void WriteDataToFile()
        {
            var jsonStr = JsonUtility.ToJson(_eventsDataContainer);
            File.WriteAllText($"{_SA_path}/{_eventsFileName}", jsonStr);
        }

        private void LoadEventsFromFile(string path)
        {
            if (File.Exists(path))
            {
                var jsonString = File.ReadAllText(path);
                _eventsDataContainer = JsonUtility.FromJson<EventsDataContainerObj>(jsonString);
            }
            else
                _eventsDataContainer = new EventsDataContainerObj();
        }
    }
}