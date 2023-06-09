﻿namespace StockMarket.Domain
{
    internal abstract class BaseComparer : IComparer<Order>
    {
        public virtual int Compare(Order? x, Order? y) // if you put ? after variable name, ...
        {
            var result = SpecificCompare(x, y);
            if (result != 0) return result;
            if (x.Id > y.Id) return -1;
            else if (x.Id < y.Id) return 1;
            return 0;
        }

        protected abstract int SpecificCompare(Order? x, Order? y);
    }
}