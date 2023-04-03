using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Uwp.Notifications;
using lab4.Converters;
using lab4.Core;
using System.Windows.Media.Animation;

namespace lab4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductInfoWindow.xaml
    /// </summary>
    public partial class ProductInfoWindow : Window
    {
        public int quontityToCard { get; set; }
        public Product ViewProduct { get; private set; }

        public ProductInfoWindow(Product product)
        {
            InitializeComponent();

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(500);
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            productInfoWindow.BeginAnimation(Window.OpacityProperty, doubleAnimation);
            ViewProduct = product;
            // Создаем экземпляр класса DiscountPriceConverter
            var discountPriceConverter = new DiscountPriceConverter();

            // Добавляем экземпляр класса в ресурсы окна
            this.Resources.Add("DiscountPriceConverter", discountPriceConverter);
            CommandBinding commandBinding = new CommandBinding
            {
                // устанавливаем команду
                Command = ApplicationCommands.Save
            };
            // устанавливаем метод, который будет выполняться при вызове команды
            commandBinding.Executed += AddToCard_Executed;
            commandBinding.Executed += AddOneButton_Executed;
            commandBinding.Executed += RemoveOneFromToCard_Executed;
            commandBinding.Executed += RemoveTenFromToCard_Executed;
            commandBinding.Executed += AddTenToCard_Executed;
            DataContext = ViewProduct;

            // Отображаем информацию о продукте в элементах управления на странице
            productImage.Source = new BitmapImage(new Uri(ViewProduct.ImagePath));
            quontityToCard = int.Parse(QuontityToCard.Text);
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddToCard_Executed(object sender, RoutedEventArgs e)
        {
            if (ViewProduct != null)
            {
                string addToCardMessage = "";
                // Создать экземпляр Product с указанным количеством
                var product = new Product(ViewProduct.Id, ViewProduct.Name, ViewProduct.Description, ViewProduct.Price, ViewProduct.Discount, ViewProduct.Category, ViewProduct.ImagePath, quontityToCard)
                {
                    Quantity = quontityToCard
                };

                bool foundProduct = false;

                foreach (var productFromCard in App.ShoppingCard)
                {
                    if (product.Id == productFromCard.Id)
                    {
                        foundProduct = true;
                        if (product.Quantity + productFromCard.Quantity <= App.Products[product.Id - 1].Quantity)
                        {
                            productFromCard.Quantity += product.Quantity;
                            addToCardMessage = Application.Current.Resources["addToCardMessage"] as string + "\n";
                            break;
                        }
                        else
                        {
                            addToCardMessage = Application.Current.Resources["errorAddToCardMessage"] as string+ " " + App.Products[product.Id - 1].Quantity;
                            break;
                        }
                    }
                }

                if (!foundProduct)
                {
                    if (product.Quantity <= App.Products[product.Id - 1].Quantity)
                    {
                        App.ShoppingCard.Add(product);
                        addToCardMessage = Application.Current.Resources["addToCardMessage"] as string + "\n";
                    }
                    else
                    {
                        addToCardMessage = Application.Current.Resources["errorAddToCardMessage"] as string + "\n";
                    }
                }
                new ToastContentBuilder()
                    .AddText(addToCardMessage)
                    .SetToastDuration(ToastDuration.Short)
                    .Show();
            }
        }

        private void AddOneButton_Executed(object sender, RoutedEventArgs e)
        {
            if(quontityToCard < ViewProduct.Quantity)
            {
                quontityToCard++;
                QuontityToCard.Text = quontityToCard.ToString();
            }
            
        }

        private void RemoveOneFromToCard_Executed(object sender, RoutedEventArgs e)
        {
            if(quontityToCard > 1)
            {
                quontityToCard--;
                QuontityToCard.Text = quontityToCard.ToString();
            }
        }

        private void RemoveTenFromToCard_Executed(object sender, RoutedEventArgs e)
        {
            if(quontityToCard - 10 > 0)
            {
                quontityToCard -= 10;
                QuontityToCard.Text = quontityToCard.ToString();
            }
            else
            {
                quontityToCard = 1;
                QuontityToCard.Text = quontityToCard.ToString();
            }
        }

        private void AddTenToCard_Executed(object sender, RoutedEventArgs e)
        {
            if((int.Parse(QuontityNum.Text) - quontityToCard > 10))
            {
                quontityToCard += 10;
                QuontityToCard.Text = quontityToCard.ToString();
            }
            else
            {
                quontityToCard = int.Parse(QuontityNum.Text);
                QuontityToCard.Text = quontityToCard.ToString();
            }
        }
    }
}
