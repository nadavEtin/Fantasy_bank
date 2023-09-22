namespace GameEvent.LoanEvent
{
    public interface ILoanGameDataEvent : IGameDataEvent
    {
        int SuccessChance { get; }
        int LoanPrice { get; }
    }
}