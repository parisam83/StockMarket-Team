using Xunit;

namespace StockMarket.Domain.Tests
{
    public class StockMarketProccessorTests
    {
        [Fact]
        public void EnqueueOrder_Should_Proccess_SellOrder_When_BuyOrder_Is_Already_Enqueued_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor(); // System Under Test
            sut.EnqueueOrder(tradeSide: TradeSide.Buy, quantity: 1, price: 1500);


            // Act
            sut.EnqueueOrder(tradeSide: TradeSide.Sell, quantity: 2, price: 1400);

            // Assert
            Assert.Equal(2, sut.Orders.Count());
            Assert.Equal(1, sut.Trades.Count());
        }
    }
}