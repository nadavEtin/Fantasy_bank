using System;

namespace GameEvent.LoanEvent
{
    public enum GameEventType
    {
        Loan
    }

    public class LoanGameEventData : BaseGameEventData, IGameDataEvent
    {
        protected override string _eventText { get; set; }
        public string EventText => _eventText;
        protected override string _eventTitle { get; set; }
        public string EventTitle => _eventTitle;
        protected override Action _yesResult { get; set; }
        public Action YesResult => _yesResult;
        protected override Action _noResult { get; set; }
        public Action NoResult => _noResult;
        public GameEventType Type { get; }
        protected override int _loanPrice { get; set; }
        public int LoanPrice => _loanPrice;
        protected override int _successChance { get; set; }
        protected override GameEventType _type { get; set; }
        public int SuccessChance => _successChance;

        public LoanGameEventData(string eventText, string eventTitle, Action yesResult, Action noResult, int loanPrice, int successChance)
        {
            _eventText = eventText;
            _eventTitle = eventTitle;
            _yesResult = yesResult;
            _noResult = noResult;
            _loanPrice = loanPrice;
            _successChance = successChance;
        }
    }
}