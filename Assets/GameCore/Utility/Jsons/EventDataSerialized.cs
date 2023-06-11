namespace GameCore.Utility.Jsons
{
    [System.Serializable]
    public class EventDataSerialized
    {
        public int id, eventDuration, type;
        public int[] eventRequirements;
        public string name, text;

        public EventDataSerialized(int id, int eventDuration, int type, int[] eventRequirements, string name,
            string text)
        {
            this.id = id;
            this.eventDuration = eventDuration;
            this.type = type;
            this.eventRequirements = eventRequirements;
            this.name = name;
            this.text = text;
        }
    }

    [System.Serializable]
    public class LoanEventDataSerialized : EventDataSerialized
    {
        private int loanCost, chanceOfSuccess;

        public LoanEventDataSerialized(int id, int eventDuration, int type, int[] eventRequirements, string name,
            string text, int loanCost, int chanceOfSuccess) : 
            base(id, eventDuration, type, eventRequirements, name, text)
        {
            this.loanCost = loanCost;
            this.chanceOfSuccess = chanceOfSuccess;
        }
    }
}