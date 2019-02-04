using PreMarket.Core;

namespace PreMarket.Print
{
    public interface IReceiptGenerator
    {
        string Generate(IShoppingCartManager manager);
    }
}