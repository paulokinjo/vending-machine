using FluentAssertions;
using VendingMachine.Domain;

namespace VendingMachineTests
{
    public class MoneySpecs
    {
        [Fact]
        public void SumOfTwoMoneysProducesCorrectResult()
        {
            Money money1 = new(1, 2, 3, 4, 5, 6);
            Money money2 = new(1, 2, 3, 4, 5, 6);

            Money sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void TwoMoneyInstancesEqualIfContainTheSameMoneyAmounts()
        {
            Money money1 = new(1,2,3,4,5,6);    
            Money money2 = new(1,2,3,4,5,6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void TwoMoneyInstancesDoNotEqualIfContainDifferentMoneyAmounts()
        {
            Money dollar = Money.Dollar;
            Money hunderdCents = new(100, 0, 0, 0, 0, 0);

            dollar.Should().NotBe(hunderdCents);
            dollar.GetHashCode().Should().NotBe(hunderdCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void CannotCreateMoneyWithNegativeValue(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            Action action = () => new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0,0,0,0,0,0,0)]
        [InlineData(1,0,0,0,0,0,0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]

        public void AmountIsCalculatedCorrectly(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount,
            decimal expectedAmount)
        {
            Money money = new(oneCentCount, tenCentCount,
                quarterCount, oneDollarCount, fiveDollarCount, twentyDollarCount);

            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void SubtractionOfTwoMoneysProducesCorrectResult()
        {
            Money money1 = new(10, 10, 10, 10, 10, 10);
            Money money2 = new(1, 2, 3, 4, 5, 6);

            Money result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void CannotSubtractMoreThanExists()
        {
            Money money1 = Money.TenCent;
            Money money2 = Money.Cent;

            Action action = () =>
            {
                Money moeny = money1 - money2;
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
