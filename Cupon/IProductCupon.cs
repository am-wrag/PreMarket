using PreMarket.Product;

namespace PreMarket.Cupon
{
    public interface IProductCupon
    {
        decimal GetTotalPrice(IProduct product);
    }
}