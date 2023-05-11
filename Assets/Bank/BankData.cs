namespace Bank
{
    public class BankData : IBankData
    {
        private int _goldBalance;

        public bool RequestGold(int amount)
        {
            return false;
        }

        private int GiveGold(int amount)
        {
            return 1;
        }

        private void GetGold(int amount)
        {
            _goldBalance += amount;
        }
    }
}
