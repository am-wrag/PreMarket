using NUnit.Framework;
using PreMarket.Core;
using PreMarket.Cupon;
using PreMarket.Print;
using PreMarket.Product;

namespace PreMarket.Tests
{
    public class SomeReceiptGeneratorTest
    {
        [Test]
        public void GenerateReceipt_GenerateWithCorrectCartPasrameters_ReturnCorrectText()
        {
            const string expectedResult = "Tnx!\r\nName...price...count...summ\r\nbuble...1...10...8,0\r\nTotal: 4,00\r\n";

            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 1M, 10);

            cartManager.AddProduct(product);

            cartManager.SetProductCupon(product, new DiscountProductCupon(0.8M, 3));

            cartManager.SetCartCupon(new DiscountCartCupon(0.5M));

            var result = new SomeReceiptGenerator().Generate(cartManager);

            Assert.IsTrue(result == expectedResult);
        }
    }
}