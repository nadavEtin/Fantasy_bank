using Bank;
using GameCore.Utility.Jsons;
using GameEvent.StoryView;
using System;

namespace GameEvent.LoanEvent
{
    public class LoanGameEventData : BaseGameEventData, ILoanGameDataEvent
    {
        public int SuccessChance { get; }
        public int LoanPrice { get; }
        public StoryType Type { get; }
        public Action<bool, IStoryCardView> ResolutionCb => _resolutionCb;

        public LoanGameEventData(int id, string eventText, string eventTitle, string eventResolutionTitle, string eventResolutionText, int countdownDuration, Action<bool, IStoryCardView> resolveCb,
            IBankBalance bankBalance, int loanPrice, int successChance, StoryType eventType,
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

        //public override bool RequirementsMetValidation()
        //{
        //    return _bankBalance.GoldBalance >= LoanPrice;
        //}
    }
}