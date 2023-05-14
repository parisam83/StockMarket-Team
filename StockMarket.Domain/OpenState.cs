namespace StockMarket.Domain
{
    internal class OpenState : MarketState
    {
        public OpenState(StockMarketProccessor stockMarketProccessor) : base(stockMarketProccessor)
        {
        }

        public override long EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price)
        {
            return StockMarketProccessor.Enqueue(tradeSide, quantity, price); 
        }

        public override void Cancel(long orderId)
        {
            StockMarketProccessor.Cancel(orderId);
        }

        public override void CloseMarket()
        {
            StockMarketProccessor.Close();
        }

        public override void OpenMarket()
        {
        }
    }
}
