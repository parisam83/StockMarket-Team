namespace StockMarket.Domain
{
    internal class CloseState : MarketState
    {
        public CloseState(StockMarketProccessor stockMarketProccessor) : base(stockMarketProccessor)
        {
        }

        public override long EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price)
        {
            throw new NotImplementedException(); 
        }

        public override void Cancel(long orderId)
        {
        }

        public override void CloseMarket()
        {
        }

        public override void OpenMarket()
        {
            StockMarketProccessor.Open();
        }
    }
}
