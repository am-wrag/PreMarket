using PreMarket.Cupon;

namespace PreMarket.Product
{
    public abstract class CandyProductBase : IProduct
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Count { get; set; }
        public decimal TotalPrice => Cupon?.GetTotalPrice(this) ?? Price * Count;
        public IProductCupon Cupon { get; private set; }

        protected CandyProductBase(string name, decimal price, int count = 1)
        {
            Price = price;
            Name = name;
            Count = count;
        }
        public void SetCupon(IProductCupon cupon)
        {
            Cupon = cupon;
        }
    }
}