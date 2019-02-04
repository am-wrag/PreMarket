using System;
using NUnit.Framework;
using PreMarket.Core;
using PreMarket.Cupon;
using PreMarket.Product;

namespace PreMarket.Tests
{
    public class ShoppingCartManagerTests
    {
        [Test]
        public void AddProduct_CorrectProduct_CorrectAdding()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 1M, 10);

            cartManager.AddProduct(product);

            Assert.IsTrue(cartManager.Products.Contains(product));
        }
        [Test]
        public void Undo_UndoAddProduct_ProductIsNotContains()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 1M, 10);

            cartManager.AddProduct(product);
            cartManager.Undo();

            Assert.IsTrue(!cartManager.Products.Contains(product));
        }
        [Test]
        public void Redo_RedoUndoAddProduct_ProductContains()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 1M, 10);

            cartManager.AddProduct(product);
            cartManager.Undo();
            cartManager.Redo();

            Assert.IsTrue(cartManager.Products.Contains(product));
        }

        [Test]
        public void RemoveProduct_ProductIsContains_ProductRemoving()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 1M, 10);

            cartManager.AddProduct(product);
            cartManager.RemoveProduct(product);

            Assert.IsTrue(!cartManager.Products.Contains(product));
        }
        [Test]
        public void RemoveProduct_ProductIsNotContains_ThrowArgumentException()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 1M, 10);

            Assert.Throws<ArgumentException>(() => cartManager.RemoveProduct(product));
        }
        [Test]
        public void ApplyProductCupon_UseCorrectProductCupon_ReturnCorrectProductTotalPrice()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 2M, 10);
            var cupon = new DiscountProductCupon(0.5M, 3);
            cartManager.AddProduct(product);
            cartManager.SetProductCupon(product, cupon);

            Assert.IsTrue(product.TotalPrice == 10M);
        }
        [Test]
        public void Undo_UndoApplyProductCupon_ReturnCorrectProductTotalPrice()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 3M, 10);
            var cupon = new DiscountProductCupon(0.5M, 3);
            cartManager.AddProduct(product);
            cartManager.SetProductCupon(product, cupon);
            cartManager.Undo();

            Assert.IsTrue(product.TotalPrice == 30M);
        }
        [Test]
        public void Redo_RedoUndoApplyProductCupon_ReturnCorrectProductTotalPrice()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 3M, 10);
            var cupon = new DiscountProductCupon(0.5M, 3);
            cartManager.AddProduct(product);
            cartManager.SetProductCupon(product, cupon);
            cartManager.Undo();
            cartManager.Redo();

            Assert.IsTrue(product.TotalPrice == 15M);
        }
        [Test]
        public void ApplyCartCupon_UseCorrectCartDiscountCupon_ReturnCorrectCartTotalPrice()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 2M, 10);
            var cupon = new DiscountCartCupon(0.5M);

            cartManager.AddProduct(product);
            cartManager.SetCartCupon(cupon);

            Assert.IsTrue(cartManager.TotalPrice == 10M);
        }

        [Test]
        public void Undo_UndoApplingCartCupon_ReturnCorrectCartTotalPrice()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 2M, 10);
            var cupon = new DiscountCartCupon(0.5M);

            cartManager.AddProduct(product);
            cartManager.SetCartCupon(cupon);
            cartManager.Undo();

            Assert.IsTrue(cartManager.TotalPrice == 20M);
        }

        [Test]
        public void Redo_RedoUndoApplingCartCupon_ReturnCorrectCartTotalPrice()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 2M, 10);
            var cupon = new DiscountCartCupon(0.5M);

            cartManager.AddProduct(product);
            cartManager.SetCartCupon(cupon);
            cartManager.Undo();
            cartManager.Redo();

            Assert.IsTrue(cartManager.TotalPrice == 10M);
        }

        [Test]
        public void ChangeProductCount_SetCorrectCount_SetNewCount()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 2M, 10);

            cartManager.AddProduct(product);
            cartManager.ChangeProductCount(product, 15);
            
            Assert.IsTrue(product.Count == 15);
        }

        [Test]
        public void ChangeProductCount_SetNegativeCount_ThrowArgumentException()
        {
            var cartManager = new ShoppingCartManager(new CommandManager());

            var product = new VanilaCandy(1, "buble", 2M, 10);

            cartManager.AddProduct(product);
            cartManager.ChangeProductCount(product, 15);

            Assert.Throws<ArgumentException>(() => cartManager.ChangeProductCount(product, -5));
        }

    }
}