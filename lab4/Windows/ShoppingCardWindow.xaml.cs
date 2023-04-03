using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Newtonsoft.Json;
using lab4.Converters;
using lab4.Core;
using System;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.Notifications;

namespace lab4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCartWindow.xaml
    /// </summary>
    public partial class ShoppingCardWindow : Window
    {
        public string Message { get; set; }
        public delegate void UpdateEventHandler();
        public static event UpdateEventHandler UpdateEvent;
        public ShoppingCardWindow()
        {
            InitializeComponent();
            var discountPriceConverter = new DiscountPriceConverter();
            // Добавляем экземпляр класса в ресурсы окна
            this.Resources.Add("DiscountPriceConverter", discountPriceConverter);
            shoppingList.ItemsSource = App.ShoppingCard;
            CommandBinding commandBinding = new CommandBinding
            {
                Command = ApplicationCommands.Save
            };
            commandBinding.Executed += CheckoutButton_Executed;
            commandBinding.Executed += AddOneButton_Executed;
            commandBinding.Executed += RemoveOneFromToCard_Executed;
            commandBinding.Executed += DeleteButton_Executed;
            UpdateTotalPrice();
            UpdateTotalCount();
            App.ShoppingCard.CollectionChanged += Items_CollectionChanged;
            Closing += ShoppingCardWindow_Closing;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShoppingCardWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.ShoppingCardRadioButton.IsChecked = false;
                mainWindow.HomeRadioButton.IsChecked = true;
            }
        }
        private void UpdateTotalPrice()
        {
            double totalPrice = 0;

            foreach (var item in shoppingList.ItemsSource)
            {
                if (item is Product itemType)
                {
                    totalPrice += itemType.DiscountedPrice * itemType.Quantity;
                }
            }

            totalPriceBlock.Text = totalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private void UpdateTotalCount()
        {
            double totalCount = 0;

            foreach (var item in shoppingList.ItemsSource)
            {
                if (item is Product itemType)
                {
                    totalCount += itemType.Quantity;
                }
            }

            totalCountBlock.Text = totalCount.ToString();
        }
        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateTotalPrice();
            UpdateTotalCount();
        }
        private void DeleteButton_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product dataObject = (Product)button.DataContext;
            App.ShoppingCard.Remove(dataObject);
        }

        private string ValidateCard()
        {
            Message = "";

            if (App.ShoppingCard == null || App.ShoppingCard.Count == 0)
            {
                Message += Application.Current.Resources["emptyCardListMessage"] as string + "\n";
            }
            if (MastercardRadioButton.IsChecked == false && VisaRadioButton.IsChecked == false && CreditRadioButton.IsChecked == false)
            {
                Message += Application.Current.Resources["noSelectedCardMessage"] as string + "\n";
            }
            if (!CardNumberTextBox.IsMaskCompleted)
            {
                Message += Application.Current.Resources["emptyCardNumberMessage"] as string + "\n";
            }
            if (!ExpireDateTextBox.IsMaskCompleted)
            {
                Message += Application.Current.Resources["emptyCardExpireDateMessage"] as string + "\n";
            }
            string input = ExpireDateTextBox.Text;
            bool isValidDate = DateTime.TryParseExact(input, "MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);

            if (isValidDate && date >= DateTime.Now)
            {
                // Введенная дата корректна
                ExpireDateTextBox.Text = input; // сохраняем введенное значение
            }
            else
            {
                // Введенная дата некорректна
                Message += Application.Current.Resources["wrongCardExpireDateMessage"] as string + "\n";
                ExpireDateTextBox.Clear(); // очищаем поле
            }
            if (!CVVTextBox.IsMaskCompleted)
            {
                Message += Application.Current.Resources["emptyCardCVVMessage"] as string + "\n";
            }
            return Message;
        }
        private void CheckoutButton_Executed(object sender, RoutedEventArgs e)
        {
            ValidateCard();
            if (string.IsNullOrEmpty(Message))
            {
                Message = Application.Current.Resources["checkoutMessage"] as string + "\n";
                string checkFile = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".json";

                var paymentInfo = new PaymentInfo
                {
                    PaymentData = new PaymentData
                    {
                        CardType = MastercardRadioButton.IsChecked == true ? "Mastercard" :
                                   VisaRadioButton.IsChecked == true ? "Visa" :
                                   CreditRadioButton.IsChecked == true ? "Credit" : "",
                        CardNumber = CardNumberTextBox.Text,
                        ExpireDate = ExpireDateTextBox.Text,
                        CVV = CVVTextBox.Text,
                        TotalPrice = totalPriceBlock.Text,
                        ProductQuantity = int.Parse(totalCountBlock.Text)
                    },
                    ShoppingList = App.ShoppingCard
                };
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
                foreach (var item in App.ShoppingCard)
                {
                    var product = App.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (product != null)
                    {
                        product.Quantity -= item.Quantity;
                        if(product.Quantity == 0)
                        {
                            UpdateEvent?.Invoke();
                        }
                    }
                }
                var json = JsonConvert.SerializeObject(App.Products);
                File.WriteAllText(filePath, json);
                var checkJson = JsonConvert.SerializeObject(paymentInfo);
                File.WriteAllText(checkFile, checkJson);
                MastercardRadioButton.IsChecked = false;
                VisaRadioButton.IsChecked = false;
                CreditRadioButton.IsChecked = false;
                CardNumberTextBox.Text = string.Empty;
                ExpireDateTextBox.Text = string.Empty;
                CVVTextBox.Text = string.Empty;
                App.ShoppingCard.Clear();
                UpdateTotalPrice();
                UpdateTotalCount();
            }
            new ToastContentBuilder()
                    .SetToastDuration(ToastDuration.Short)
                    .AddText(Message)
                    .Show();
        }
        private void AddOneButton_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product dataObject = (Product)button.DataContext;
            var productInAllProducts = App.Products.FirstOrDefault(p => p.Name == dataObject.Name);
            if (productInAllProducts != null)
            {
                // Ограничить количество товара в корзине максимальным количеством из списка всех товаров
                if (dataObject.Quantity < productInAllProducts.Quantity)
                {
                    dataObject.Quantity++;
                    UpdateTotalPrice();
                    UpdateTotalCount();
                }
            }
        }
        private void RemoveOneFromToCard_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product dataObject = (Product)button.DataContext;
            if (dataObject.Quantity > 1)
            {
                dataObject.Quantity--;
            }
            UpdateTotalPrice();
            UpdateTotalCount();
        }
    }
}
