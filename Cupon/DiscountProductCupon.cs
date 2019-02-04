using PreMarket.Product;

namespace PreMarket.Cupon
{
    public class DiscountProductCupon : IProductCupon
    {
        private readonly decimal _priceModifier;
        private readonly int _minimumProductCount;

        public DiscountProductCupon(decimal priceModifyer, int minimumProductCount)
        {
            _priceModifier = priceModifyer;
            _minimumProductCount = minimumProductCount;
        }

        public decimal GetTotalPrice(IProduct productInfo)
        {
            var totalPriceWithNoDiscount = productInfo.Price * productInfo.Count;

            return productInfo.Count >= _minimumProductCount 
                ? totalPriceWithNoDiscount * _priceModifier
                : totalPriceWithNoDiscount;
        }

        public IProductCupon Copy()
        {
            return new DiscountProductCupon(_priceModifier, _minimumProductCount);
        }
    }
}