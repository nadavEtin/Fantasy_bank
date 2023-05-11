using GameCore.Events;
using VContainer;

namespace Bank
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BankManager : IBankManager
    {
        private EventBus _eventBus;
        public int GoldBalance { get; private set; }

        [Inject]
        public BankManager(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public bool GetPermissionToSpendGold(int goldAmount)
        {
            if (goldAmount > GoldBalance)
                return false;
            
            GoldBalance -= goldAmount;
            _eventBus.Publish(GameplayEvent.GoldBalanceChanged, new SingleParamInt(GoldBalance));
            return true;
        }
    }
}