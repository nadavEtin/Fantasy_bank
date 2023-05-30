using System;

namespace GameEvent.LoanEvent
{
    public enum GameEventType
    {
        Loan
    }

    public class LoanGameEventData : BaseGameEventData, IGameDataEvent
    {
        public int SuccessChance { get; }
        public int LoanPrice { get; }
        public GameEventType Type { get; }


        public override void RequirementsMetValidation()
        {
            throw new NotImplementedException();
        }

        protected override int[] _eventRequirements { get; set; }
        protected override int _id { get; set; }
        protected override string _eventText { get; set; }
        protected override string _eventTitle { get; set; }
        protected override Action<bool, IGameEventView> _resolutionCb { get; set; }
        protected override GameEventType _type { get; set; }

        public string EventText => _eventText;
        public string EventTitle => _eventTitle;
        public Action<bool, IGameEventView> ResolutionCb => _resolutionCb;

        public LoanGameEventData(int id, string eventText, string eventTitle, Action<bool, IGameEventView> resolveCb,
            int loanPrice, int successChance, GameEventType type, int[] eventRequirements) : base(id, eventText,
            eventTitle, resolveCb, type, eventRequirements)
        {
            LoanPrice = loanPrice;
            SuccessChance = successChance;
        }
    }
}