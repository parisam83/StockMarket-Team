namespace StockMarket.Domain.Tests
{
    public class StockMarketProccessorTests
    {
        [Fact]
        public void EnqueueOrder_Should_Proccess_SellOrder_When_BuyOrder_Is_Already_Enqueued_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor(); // System Under Test

            // Act
            sut.EnqueueOrder();

            // Assert


        }
    }
}