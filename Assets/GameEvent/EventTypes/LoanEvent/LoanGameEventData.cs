using System;
using Bank;
using GameEvent.EventCardView;

namespace GameEvent.LoanEvent
{
    public class LoanGameEventData : BaseGameEventData
    {
        public int SuccessChance { get; }
        public int LoanPrice { get; }
        public GameEventType Type { get; }
        public Action<bool, IGameEventView> ResolutionCb => _resolutionCb;

        public LoanGameEventData(int id, string eventText, string eventTitle, int countdownDuration, Action<bool, IGameEventView> resolveCb,
            IBankBalance bankBalance, int loanPrice, int successChance, GameEventType eventType,
            int[] eventRequirements) : base(id, eventText, eventTitle, countdownDuration, bankBalance, resolveCb, eventType,
            eventRequirements)
        {
            LoanPrice = loanPrice;
            SuccessChance = successChance;
        }
        
        public override bool RequirementsMetValidation()
        {
            return _bankBalance.GoldBalance >= LoanPrice;
        }
    }
}