using System.ComponentModel;

namespace lab4.Core
{
    public class Product : INotifyPropertyChanged
    {
        private int _quantity;
        private double _price;
        private int _discount;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    if (Discount > 0)
                    {
                        DiscountedPrice = _price * (100 - Discount) / 100;
                    }
                    else
                    {
                        DiscountedPrice = _price;
                    }
                    OnPropertyChanged("Price");
                    OnPropertyChanged("DiscountedPrice");
                }
            }
        }

        public int Discount
        {
            get { return _discount; }
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    if (_discount > 0)
                    {
                        DiscountedPrice = Price * (100 - _discount) / 100;
                    }
                    else
                    {
                        DiscountedPrice = Price;
                    }
                    OnPropertyChanged("Discount");
                    OnPropertyChanged("DiscountedPrice");
                }
            }
        }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public double DiscountedPrice { get; set; }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Product(int id, string name, string description, double price, int discount, string category, string imagepath, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Discount = discount;
            ImagePath = imagepath;
            Description = description;
            Category = category;
            if (Discount > 0)
            {
                DiscountedPrice = Price * (100 - Discount) / 100;
            }
            else
            {
                DiscountedPrice = Price;
            }
            Quantity = quantity;
        }
    }
}
