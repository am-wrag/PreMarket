using System;
using System.Collections.Generic;
using System.Linq;
using PreMarket.Cupon;
using PreMarket.Product;

namespace PreMarket.Core
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        public IList<IProduct> Products { get; } = new List<IProduct>();

        public decimal TotalPrice => _cartCupon?.GetTotalPrice(this) ?? Products.Sum(p => p.TotalPrice);

        private readonly ICommandManager _commandManager;
        private ICartCupon _cartCupon;

        public ShoppingCartManager(ICommandManager commandManager)
        {
            _commandManager = commandManager;
        }        

        public void AddProduct(IProduct product)
        {
            _commandManager
                .ExecuteCommand(
                    () => AddProductOperation(product), 
                    () => RemoveProductOperation(product));
        }

        public void RemoveProduct(IProduct product)
        {
            if (!Products.Contains(product))
            {
                throw new ArgumentException("Removing product exception. Product not contains!");
            }

            _commandManager
                .ExecuteCommand(
                () => RemoveProductOperation(product),
                () => AddProductOperation(product));
        }

        public void SetProductCupon(IProduct product, IProductCupon cupon)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var lastUsedCuppon = product.Cupon?.Copy();

            _commandManager
                .ExecuteCommand(
                () => product.SetCupon(cupon),
                () => product.SetCupon(lastUsedCuppon));
        }
        
        public void ChangeProductCount(IProduct product, int newCount)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (newCount < 0)
            {
                throw new ArgumentException("Negative product count value!");
            }

            var lastProductCount = product.Count;

            _commandManager
                .ExecuteCommand(
                    () => product.Count = newCount,
                    () => product.Count = lastProductCount);
        }      

        public void SetCartCupon(ICartCupon cartCupon)
        {
            var lastUsedCuppon = _cartCupon?.Copy();

            _commandManager.ExecuteCommand(
                () => _cartCupon = cartCupon,
                () => _cartCupon = lastUsedCuppon);
        }

        public void Undo()
        {
            _commandManager.Undo();
        }

        public void Redo()
        {
            _commandManager.Redo();
        }

        private void AddProductOperation(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Products.Add(product);
        }

        private void RemoveProductOperation(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Products.Remove(product);
        }
    }
}