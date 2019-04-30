using PreMarket.Core;

namespace PreMarket.Cupon
{
    public interface ICartCupon
    {
        decimal GetTotalPrice(IShoppingCartManager cartManaget);
    }
}