namespace Bank
{
    public class BankManager : IBankBalance, IBankDeposit, IBankWithdraw
    {
        public int GoldBalance { get; private set; }

        public bool GetGoldFromBank(int goldAmount)
        {
            if (!CheckAvailableGold(goldAmount)) 
                return false;
            
            GoldBalance -= goldAmount;
            return true;
        }

        public void AddGoldToBank(int goldAmount)
        {
            GoldBalance += goldAmount;
        }
        
        private bool CheckAvailableGold(int goldAmount)
        {
            return goldAmount <= GoldBalance;
        }
    }
}