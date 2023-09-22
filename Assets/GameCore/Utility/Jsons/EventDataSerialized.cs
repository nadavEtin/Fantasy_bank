using System.Collections.Generic;

namespace GameCore.Utility.Jsons
{
    [System.Serializable]
    public class EventDataSerialized //: IStoryDataSerialized
    {
        public int id, eventDuration, type;
        public string name, resolutionName, text, resolutionText;
        //public string resolutionName { private set; get; }
        public int[] eventRequirements;
        //public int eventDuration { private set; get; }
        //public int type { private set; get; }
        //public string text { private set; get; }
        //public string resolutionText { private set; get; }

    public EventDataSerialized(int id, int eventDuration, int type, List<int> eventRequirements, string name, string text, string resolutionName, string resolutionText)
        {
            this.id = id;
            this.name = name;
            this.resolutionName = resolutionName;
            this.eventRequirements = eventRequirements.ToArray();
            this.eventDuration = eventDuration;
            this.type = type;
            this.text = text;
            this.resolutionText = resolutionText;
        }
    }

    [System.Serializable]
    public class LoanStoryDataSerialized : EventDataSerialized
    {
        public int loanCost, chanceOfSuccess;

        public LoanStoryDataSerialized(int id, int eventDuration, int type, List<int> eventRequirements, string name,
            string text, string resolutionName, string resolutionText, int loanCost, int chanceOfSuccess) :
            base(id, eventDuration, type, eventRequirements, name, text, resolutionName, resolutionText)
        {
            this.loanCost = loanCost;
            this.chanceOfSuccess = chanceOfSuccess;
        }
    }
}