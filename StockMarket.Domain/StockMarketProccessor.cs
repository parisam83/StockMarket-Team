using System.Text.Json.Nodes;

namespace StockMarket.Domain
{
    public class StockMarketProccessor
    {
        private long lastOrderId;
        private readonly PriorityQueue<Order, Order> buyOrders = new PriorityQueue<Order, Order>();
        private readonly PriorityQueue<Order, Order> sellOrders = new PriorityQueue<Order, Order>();

        public StockMarketProccessor(long lastOrderId = 0)
        {
            this.lastOrderId = lastOrderId;
        }

        public void EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price)
        {
            Interlocked.Increment(ref lastOrderId);
            var order = new Order(lastOrderId, tradeSide, quantity, price);
            if (tradeSide == TradeSide.Buy) proccessBuyOrder(order);
            else proccessSellOrder(order);
        }

        private void proccessBuyOrder(Order order)
        {
            
        }

        private void proccessSellOrder(Order order)
        {

        }
    }
}