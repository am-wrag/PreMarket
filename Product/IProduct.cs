using PreMarket.Cupon;

namespace PreMarket.Product
{
    public interface IProduct
    {
        string Name { get; }
        decimal Price { get; }
        int Count { get; set; }
        decimal TotalPrice { get; }
        IProductCupon Cupon { get; }
        void SetCupon(IProductCupon cupon);
    }
}