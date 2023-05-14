namespace StockMarket.Domain
{
    public interface IStockMarketProccessor
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<Trade> Trades { get; }

        void Cancel(long orderId);
        void CloseMarket();
        long EnqueueOrder(TradeSide tradeSide, decimal quantity, decimal price);
        void OpenMarket();
    }
}