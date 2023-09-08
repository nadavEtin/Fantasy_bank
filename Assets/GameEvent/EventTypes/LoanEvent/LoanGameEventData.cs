using Bank;
using GameCore.Utility.Jsons;
using GameEvent.EventCardView;
using System;

namespace GameEvent.LoanEvent
{
    public class LoanGameEventData : BaseGameEventData
    {
        public int SuccessChance { get; }
        public int LoanPrice { get; }
        public GameEventType Type { get; }
        public Action<bool, IGameEventView> ResolutionCb => _resolutionCb;

        public LoanGameEventData(int id, string eventText, string eventTitle, string eventResolutionTitle, string eventResolutionText, int countdownDuration, Action<bool, IGameEventView> resolveCb,
            IBankBalance bankBalance, int loanPrice, int successChance, GameEventType eventType,
            int[] eventRequirements) : base(id, eventText, eventTitle, eventResolutionTitle, eventResolutionText, countdownDuration, eventType, eventRequirements, bankBalance,
            resolveCb)
        {
            LoanPrice = loanPrice;
            SuccessChance = successChance;
        }

        public LoanGameEventData(EventDataSerialized eventData, int loanPrice, int successChance) : base(eventData)
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