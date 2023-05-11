namespace Bank
{
    public interface IBankManager
    {
        int GoldBalance { get; }
        bool GetPermissionToSpendGold(int goldAmount);
    }
}