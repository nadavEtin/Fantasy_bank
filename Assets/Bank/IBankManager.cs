namespace Bank
{
    public interface IBankManager
    {
        int GoldBalance { get; }
        //bool CheckAvailableGold(int goldAmount);
        bool GetGoldFromBank(int goldAmount);
        void AddGoldToBank(int goldAmount);
    }
}