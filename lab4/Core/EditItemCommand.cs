using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab4.Core
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
    }
    public class EditItemCommand : ICommand
    {
        private readonly Product _product;
        private readonly Product _oldProduct;
        private readonly Product _newProduct;

        public EditItemCommand(Product product, Product oldProduct, Product newProduct)
        {
            _product = product;
            _oldProduct = oldProduct;
            _newProduct = newProduct;
        }

        public void Execute()
        {
            _product.Name = _newProduct.Name;
            _product.Description = _newProduct.Description;
            _product.Price = _newProduct.Price;
            _product.Discount = _newProduct.Discount;
            _product.Category = _newProduct.Category;
            _product.ImagePath = _newProduct.ImagePath;
            _product.Quantity = _newProduct.Quantity;
        }

        public void Undo()
        {
            _product.Name = _oldProduct.Name;
            _product.Description = _oldProduct.Description;
            _product.Price = _oldProduct.Price;
            _product.Discount = _oldProduct.Discount;
            _product.Category = _oldProduct.Category;
            _product.ImagePath = _oldProduct.ImagePath;
            _product.Quantity = _oldProduct.Quantity;
        }

        public void Redo()
        {
            _product.Name = _newProduct.Name;
            _product.Description = _newProduct.Description;
            _product.Price = _newProduct.Price;
            _product.Discount = _newProduct.Discount;
            _product.Category = _newProduct.Category;
            _product.ImagePath = _newProduct.ImagePath;
            _product.Quantity = _newProduct.Quantity;
        }
    }
}
