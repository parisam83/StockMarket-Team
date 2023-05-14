using FluentAssertions;
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
            var buyOrderId = sut.EnqueueOrder(tradeSide: TradeSide.Buy, quantity: 1, price: 1500);


            // Act
            var sellOrderId = sut.EnqueueOrder(tradeSide: TradeSide.Sell, quantity: 2, price: 1400);

            // Assert
            Assert.Equal(2, sut.Orders.Count());
            Assert.Equal(1, sut.Trades.Count());
            sut.Orders.First().Should().BeEquivalentTo(new
            {
                TradeSide = TradeSide.Buy,
                Quantity = 0M,
                Price = 1500M
            });
            sut.Orders.Last().Should().BeEquivalentTo(new
            {
                TradeSide = TradeSide.Sell,
                Quantity = 1M,
                Price = 1400M
            });
            sut.Trades.First().Should().BeEquivalentTo(new
            {
                BuyOrderId = buyOrderId,
                SellOrderId = sellOrderId,
                Quantity = 1M,
                Price = 1400M
            });
        }

        [Fact]
        public void EnqueueOrder_Should_Proccess_BuyOrder_When_SellOrder_Is_Already_Enqueued_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor();
            var sellOrderId = sut.EnqueueOrder(tradeSide: TradeSide.Sell, quantity: 1, price: 1400);

            // Act
            var buyOrderId = sut.EnqueueOrder(tradeSide: TradeSide.Buy, quantity: 1, price: 1500);

            // Assert
            Assert.Equal(2, sut.Orders.Count());
            Assert.Equal(1, sut.Trades.Count());
            sut.Orders.First().Should().BeEquivalentTo(new
            {
                TradeSide = TradeSide.Sell,
                Quantity = 0M,
                Price = 1400M
            });
            sut.Orders.Last().Should().BeEquivalentTo(new
            {
                TradeSide = TradeSide.Buy,
                Quantity = 0M,
                Price = 1500M
            });
            sut.Trades.First().Should().BeEquivalentTo(new
            {
                BuyOrderId = buyOrderId,
                SellOrderId = sellOrderId,
                Quantity = 1M,
                Price = 1400M
            });
        }

        [Fact]
        public void EnqueueOrder_Should_Proccess_SellOrder_When_Multiple_BuyOrders_Are_Already_Enqueued_Test()
        {

        }

        [Fact]
        public void EnqueueOrder_Should_Proccess_BuyOrder_When_Multiple_SellOrders_Are_Already_Enqueued_Test()
        {

        }

        [Fact]
        public void EnqueueOrder_Should_Proccess_SellOrder_When_Some_BuyOrders_Are_Matched_Enqueued_Test() 
        { 

        }

        [Fact]
        public void EnqueueOrder_Should_Proccess_BuyOrder_When_Some_SellOrders_Are_Matched_Enqueued_Test()
        {

        }

        [Fact]
        public void EnqueueOrder_Should_Not_Proccess_BuyOrder_When_No_SellOrders_Are_Matched_Test()
        {

        }

        [Fact]
        public void EnqueueOrder_Should_Not_Proccess_SellOrder_When_No_BuyOrders_Are_Matched_Test()
        {

        }

        [Fact]
        public void EnqueueOrder_Should_Proccess_BuyOrder_When_Demand_Is_More_Than_Supply_Test()
        {

        }

        [Fact]
        public void CancelOrder_Should_Cancel_Order_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor();
            var orderId = sut.EnqueueOrder(tradeSide: TradeSide.Sell, quantity: 1, price: 1400);

            // Act
            sut.CancelOrder(orderId);

            // Assert
            sut.Orders.First().Should().BeEquivalentTo(new
            {
                isCanceled = true
            });
        }

        [Fact]
        public void CancelOrder_Should_Not_Process_Order_When_Peeked_MatchingOrder_Is_Canceled_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor();
            var orderId = sut.EnqueueOrder(tradeSide: TradeSide.Sell, quantity: 1, price: 1400);

            // Act
            sut.CancelOrder(orderId);

            // Assert
            Assert.Empty(sut.Trades);
        }

        [Fact]
        public void CloseMarket_Should_Close_StockMarket_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor();

            // Act
            sut.CloseMarket();

            // Assert
            Assert.Equal(sut.state, StockMarketState.close);
        }

        [Fact]
        public void EnqueueOrder_Should_Not_Work_When_StockMarket_Is_Closed_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor();
            sut.CloseMarket();

            // Act
            void act() => sut.EnqueueOrder(tradeSide: TradeSide.Buy, quantity: 1, price: 1500);

            // Assert
            Assert.Throws<NotImplementedException>(act);
        }

        [Fact]
        public void CancelOrder_Should_Not_Work_When_StockMarket_Is_Closed_Test()
        {
            // Arrange
            var sut = new StockMarketProccessor();
            var orderId = sut.EnqueueOrder(tradeSide: TradeSide.Buy, quantity: 1, price: 1500);
            sut.CloseMarket();

            // Act
            sut.CancelOrder(orderId);

            // Assert
            sut.Orders.First().Should().BeEquivalentTo(new
            {
                isCanceled = false
            });
        }

        [Fact]
        public void 
        // TODO: state logic tests!

        [Fact]
        public void ModifyOrder_Test()
        {
            // buy order
            // sell order : doesn't match with sell order
            // buy order modifies so that the orders match
        }

        [Fact]
        public void ModifyOrder_Should_Not_Work_When_StockMarket_Is_Closed_Test()
        {

        }

        [Fact]
        public void ModifyOrder_Should_Work_When_StockMarket_Is_Opened_Test()
        {

        }
    }
}