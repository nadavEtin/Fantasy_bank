using System;

namespace Assets.GameEvent.LoanEvent
{
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
        protected override int _loanPrice { get; set; }
        public int LoanPrice => _loanPrice;
        protected override int _successChance { get; set; }
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
