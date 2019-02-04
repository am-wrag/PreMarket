using System.Linq;
using PreMarket.Core;

namespace PreMarket.Cupon
{
    public class DiscountCartCupon : ICartCupon
    {
        private readonly decimal _priceModifyer;

        public DiscountCartCupon(decimal priceModifyer)
        {
            _priceModifyer = priceModifyer;
        }

        public decimal GetTotalPrice(IShoppingCartManager manager)
        {
            return _priceModifyer * manager.Products.Sum(p => p.TotalPrice);
        }

        public ICartCupon Copy()
        {
            return new DiscountCartCupon(_priceModifyer);
        }
    }
}