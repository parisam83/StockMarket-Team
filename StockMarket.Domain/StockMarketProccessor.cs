using StackExchange.Redis;
using System.Text.Json.Nodes;

namespace StockMarket.Domain
{
    public class StockMarketProccessor
    {
        private long lastOrderId;
        private long lastTradeId;
        public readonly List<Order> orders;
        public readonly List<Trade> trades;
        private readonly PriorityQueue<Order, Order> buyOrders;
        private readonly PriorityQueue<Order, Order> sellOrders;

        public IEnumerable<Order> Orders => orders;
        public IEnumerable<Trade> Trades => trades;
        public StockMarketProccessor(long lastOrderId = 0)
        {
            this.lastOrderId = lastOrderId;
            this.orders = new List<Order>();
            this.trades = new List<Trade>();
            this.buyOrders = new PriorityQueue<Order, Order>(new MaxComparer());
            this.sellOrders = new PriorityQueue<Order, Order>(new MinComparer());
        }

        public long EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price)
        {
            Interlocked.Increment(ref lastOrderId);
            var order = new Order(lastOrderId, tradeSide, quantity, price);
            orders.Add(order);

            if (tradeSide == TradeSide.Buy) proccessBuyOrder(order);
            else proccessSellOrder(order);
            return order.Id;
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
            trades.Add(trade);

            buyOrder.DecreaseQuantity(quantityToDecrease);
            sellOrder.DecreaseQuantity(quantityToDecrease);
        }
    }
}