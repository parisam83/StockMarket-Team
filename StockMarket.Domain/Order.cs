namespace StockMarket.Domain
{
    public class Order
    {
        private long id;
        private TradeSide tradeSide;
        private decimal quantity;
        private decimal price;

        internal Order(long id, TradeSide tradeSide, decimal quantity, decimal price)
        {
            this.id = id;
            this.tradeSide = tradeSide;
            this.quantity = quantity;
            this.price = price;
        }

        public long Id { get => id; set => id = value; }
        public decimal Price { get => price; set => price = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
        public TradeSide TradeSide { get => tradeSide; set => tradeSide = value; }
    }
}