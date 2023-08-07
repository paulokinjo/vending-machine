namespace VendingMachine.Domain;

using static Money;
public sealed class SnackMachine
{

    public Money MoneyInside { get; private set; } = Money.None;
    public Money MoneyInTransaction { get; private set; } = Money.None;

    public void InsertMoney(Money money)
    {
        Money[] consAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        if (!consAndNotes.Contains(money)) 
        {
            throw new InvalidOperationException();
        }

        MoneyInTransaction += money;
    }

    public void ReturnMoney() => MoneyInTransaction = None;

    public void BuySnack()
    {
        MoneyInside += MoneyInTransaction;
        ReturnMoney();
    }
}
