using System.Collections.Generic;
using PreMarket.Product;

namespace PreMarket.Core
{
    public interface IShoppingCartManager
    {
        int TotalPrice { get; }
        IList<IProduct> Products { get; }
    }
}