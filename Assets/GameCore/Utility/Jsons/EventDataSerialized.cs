using System.Collections.Generic;

namespace GameCore.Utility.Jsons
{
    [System.Serializable]
    public class EventDataSerialized
    {
        public int id;
        public string name;
        public int[] eventRequirements;
        public int eventDuration, type;
        public string text;

        public EventDataSerialized(int id, int eventDuration, int type, List<int> eventRequirements, string name,
            string text)
        {
            this.id = id;
            this.name = name;
            this.eventDuration = eventDuration;
            this.type = type;
            this.eventRequirements = eventRequirements.ToArray();
            this.text = text;
        }
    }

    [System.Serializable]
    public class LoanEventDataSerialized : EventDataSerialized
    {
        public int loanCost, chanceOfSuccess;

        public LoanEventDataSerialized(int id, int eventDuration, int type, List<int> eventRequirements, string name,
            string text, int loanCost, int chanceOfSuccess) : 
            base(id, eventDuration, type, eventRequirements, name, text)
        {
            this.loanCost = loanCost;
            this.chanceOfSuccess = chanceOfSuccess;
        }
    }
}