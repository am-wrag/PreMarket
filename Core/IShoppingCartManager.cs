using System.Collections.Generic;
using PreMarket.Product;

namespace PreMarket.Core
{
    public interface IShoppingCartManager
    {
        decimal TotalPrice { get; }
        IList<IProduct> Products { get; }
    }
}