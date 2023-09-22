using Bank;
using GameEvent;

namespace Assets.GameCore.EventEffectsResolver
{
    public class EventEffectsResolver : IEventEffectsResolver
    {
        private IBankBalance _bankBalance;
        private IBankDeposit _bankDeposit;
        private IBankWithdraw _bankWithdraw;

        public EventEffectsResolver(IBankBalance bankBalance, IBankDeposit bankDeposit, IBankWithdraw bankWithdraw)
        {
            _bankBalance = bankBalance;
            _bankDeposit = bankDeposit;
            _bankWithdraw = bankWithdraw;
        }

        public void ResolveEvent(IGameDataEvent eventData)
        {

        }

        public void GainGold(int amount)
        {
            _bankDeposit.AddGoldToBank(amount);
        }
    }
}