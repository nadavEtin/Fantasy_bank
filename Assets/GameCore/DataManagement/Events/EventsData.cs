using GameCore.Utility.Jsons;

namespace GameCore.DataManagement.Events
{
    public class EventsData
    {
        private IJsonSerialization _jsonSerialization;

        private EventsData(IJsonSerialization jsonSerialization)
        {
            _jsonSerialization = jsonSerialization;
        }

        public static void SaveEvent(EventDataSerialized data)
        {
            
        }
    }
}