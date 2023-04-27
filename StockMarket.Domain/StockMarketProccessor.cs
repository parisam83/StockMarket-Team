using StackExchange.Redis;
using System.Text.Json.Nodes;

namespace StockMarket.Domain
{
    public class StockMarketProccessor
    {
        private long lastOrderId;
        private long lastTradeId;
        public readonly List<Order> Orders;
        public readonly List<Trade> Trades;
        private readonly PriorityQueue<Order, Order> buyOrders;
        private readonly PriorityQueue<Order, Order> sellOrders;

        public StockMarketProccessor(long lastOrderId = 0)
        {
            this.lastOrderId = lastOrderId;
            this.Orders = new List<Order>();
            this.Trades = new List<Trade>();
            this.buyOrders = new PriorityQueue<Order, Order>(new MaxComparer());
            this.sellOrders = new PriorityQueue<Order, Order>(new MinComparer());
        }

        public void EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price)
        {
            Interlocked.Increment(ref lastOrderId);
            var order = new Order(lastOrderId, tradeSide, quantity, price);
            Orders.Add(order);

            if (tradeSide == TradeSide.Buy)proccessBuyOrder(order);
            else proccessSellOrder(order);
        }

        private void proccessBuyOrder(Order order)
        {
            while (sellOrders.Count > 0 && order.Quantity > 0 && sellOrders.Peek().Price <= order.Price)
            {
                makeTrade(order, sellOrders.Peek());
                if (sellOrders.Peek().Quantity == 0)
                    sellOrders.Dequeue();
            }
            if (order.Quantity > 0) buyOrders.Enqueue(order, order);
        }

        private void proccessSellOrder(Order order)
        {
            while (buyOrders.Count > 0 && order.Quantity > 0 && buyOrders.Peek().Price >= order.Price)
            {
                makeTrade(buyOrders.Peek(), order);
                if (buyOrders.Peek().Quantity == 0)
                    buyOrders.Dequeue();
            }
            if (order.Quantity > 0) sellOrders.Enqueue(order, order);
        }

        private void makeTrade(Order buyOrder, Order sellOrder)
        {
            decimal quantityToDecrease = Math.Min(sellOrder.Quantity, buyOrder.Quantity);

            Interlocked.Increment(ref lastTradeId);
            var trade = new Trade(lastTradeId, buyOrder.Id, sellOrder.Id, quantityToDecrease, sellOrder.Price);
            Trades.Add(trade);

            buyOrder.DecreaseQuantity(quantityToDecrease);
            sellOrder.DecreaseQuantity(quantityToDecrease);
        }
    }
}