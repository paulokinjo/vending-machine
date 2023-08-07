using FluentAssertions;
using VendingMachine.Domain;

namespace VendingMachineTests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void ReturnMoneyEmptiesMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.FiveDollar );

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void InsertMoneyGoesToMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Money.Cent);
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void CannotInsertMoreThanOneCoinOrNoteAtAtime()
        {
            var snackMachine = new SnackMachine();
            var twoCent = Money.Cent + Money.Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MoneyInTransactionGoesToMoneyInsideAfterPurchase()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Money.Dollar);
            snackMachine.InsertMoney(Money.Dollar);

            snackMachine.BuySnack();

            snackMachine.MoneyInTransaction.Should().Be(Money.None);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }
    }
}
