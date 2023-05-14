namespace StockMarket.Domain
{
    internal abstract class MarketState : IStockMarketProccessor
    {
        protected StockMarketProccessor StockMarketProccessor { get; set; }
        protected MarketState(StockMarketProccessor stockMarketProccessor) 
        {
            StockMarketProccessor = stockMarketProccessor;
        }

        public virtual IEnumerable<Order> Orders => throw new NotImplementedException();

        public virtual IEnumerable<Trade> Trades => throw new NotImplementedException();

        public virtual void Cancel(long orderId)
        {
            throw new NotImplementedException();
        }

        public virtual void CloseMarket()
        {
            throw new NotImplementedException();
        }

        public virtual long EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price)
        {
            throw new NotImplementedException();
        }

        public virtual void OpenMarket()
        {
            throw new NotImplementedException();
        }
    }
}
