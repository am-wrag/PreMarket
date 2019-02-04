using System.Text;
using PreMarket.Core;

namespace PreMarket.Print
{
    public class SomeReceiptGenerator : IReceiptGenerator
    {
        public string Generate(IShoppingCartManager manager)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Tnx!");
            sb.AppendLine("Name...price...count...summ");

            foreach (var product in manager.Products)
            {
                sb.AppendLine($"{product.Name}...{product.Price}...{product.Count}...{product.TotalPrice}");
            }

            sb.AppendLine($"Total: {manager.TotalPrice}");
            
            return sb.ToString();
        }
    }
}