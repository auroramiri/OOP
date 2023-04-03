using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using lab4.Core;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Threading;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Collections;

namespace lab4.Windows
{
    /// <summary>
    /// Логика взаимодействия для ManagmentWindow.xaml
    /// </summary>
    public partial class ManagmentWindow : Window
    {

        public delegate void UpdateEventHandler();
        public static event UpdateEventHandler UpdateEvent;
        private readonly DispatcherTimer searchTimer;
        readonly ObservableCollection<Product> SearchResults = new ObservableCollection<Product>();
        private Stack<EditItemCommand> _undoStackEdit = new Stack<EditItemCommand>();
        private Stack<EditItemCommand> _redoStackEdit = new Stack<EditItemCommand>();

        public string Message { get; set; }
        public int ProductId { get; set; }
        public ManagmentWindow()
        {
            InitializeComponent();

            managmentList.ItemsSource = App.Products;
            Closing += ManagmentWindow_Closing;

            CommandBinding commandBinding = new CommandBinding
            {
                Command = ApplicationCommands.Save
            };
            commandBinding.Executed += AddOneProduct_Executed;
            commandBinding.Executed += RemoveOneProduct_Executed;
            commandBinding.Executed += SaveButton_Executed;
            commandBinding.Executed += DeleteButton_Executed;
            commandBinding.Executed += CreateButton_Executed;
            SearchInput.DataContext = this;
            searchTimer = new DispatcherTimer();
            searchTimer.Interval = TimeSpan.FromMilliseconds(500);
            searchTimer.Tick += new EventHandler(SearchTimer_Tick);
        }
        private void ManagmentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.ManagmentRadioButton.IsChecked = false;
                mainWindow.HomeRadioButton.IsChecked = true;
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddOneProduct_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product dataObject = (Product)button.DataContext;
            dataObject.Quantity++;
            if (dataObject.Quantity > 0)
            {
                UpdateEvent?.Invoke();
            }
        }

        private void RemoveOneProduct_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product dataObject = (Product)button.DataContext;
            if (dataObject.Quantity > 0)
            {
                dataObject.Quantity--;
                if (dataObject.Quantity == 0)
                {
                    UpdateEvent?.Invoke();
                }
            }
        }
        private void DeleteButton_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product dataObject = (Product)button.DataContext;
            App.Products.Remove(dataObject);
            for (int i = App.ShoppingCard.Count - 1; i >= 0; i--)
            {
                var product = App.ShoppingCard[i];
                if (dataObject.Id == product.Id)
                {
                    App.ShoppingCard.RemoveAt(i);
                }
            }
        }

        private void SaveButton_Executed(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
            Message = Application.Current.Resources["savedMessage"] as string + "\n";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
            var json = JsonConvert.SerializeObject(App.Products);
            File.WriteAllText(filePath, json);
            managmentList.ItemsSource = null;
            managmentList.ItemsSource = App.Products;
            UpdateEvent?.Invoke();
            new ToastContentBuilder()
                    .SetToastDuration(ToastDuration.Short)
                    .AddText(Message)
                    .Show();
        }
        private void ShowProductInfo(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Product product)
            {
                ProductId = product.Id;
                ProductNameTextBox.Text = product.Name;
                ProductCategoryTextBox.Text = product.Category;
                ProductPriceTextBox.Text = product.Price.ToString("G", CultureInfo.InvariantCulture);
                DiscountTextBox.Text = product.Discount.ToString();
                DescriptionTextBox.Text = product.Description;
                QuantityTextBox.Text = product.Quantity.ToString();
                ImageControl.Source = new BitmapImage(new Uri(product.ImagePath));
            } 
        }

        private void ProductPriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true; // игнорируем вводимый символ
            }
            else if (e.Text == ".") // Если введена точка
            {
                // Проверяем, что в текстовом поле еще не было введено точки
                if (((TextBox)sender).Text.Contains("."))
                {
                    e.Handled = true; // игнорируем вводимый символ
                }
            }
        }
        private void UpdateProduct()
        {
            var product = App.Products.FirstOrDefault(p => p.Id == ProductId);
            if (product != null)
            {
                // сохраняем текущее состояние объекта
                var oldProduct = new Product(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Discount,
                    product.Category,
                    product.ImagePath,
                    product.Quantity
                );

                // обновляем свойства объекта
                product.Name = ProductNameTextBox.Text;
                product.Description = DescriptionTextBox.Text;
                product.Price = double.Parse(ProductPriceTextBox.Text, CultureInfo.InvariantCulture);
                product.Discount = int.Parse(DiscountTextBox.Text);
                product.Category = ProductCategoryTextBox.Text;
                product.ImagePath = ImageControl.Source.ToString();
                product.Quantity = int.Parse(QuantityTextBox.Text);

                // создаем и выполняем команду для отмены изменений
                var newProduct = new Product(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Discount,
                    product.Category,
                    product.ImagePath,
                    product.Quantity
                );
                var command = new EditItemCommand(product, oldProduct, newProduct);
                command.Execute();
                _undoStackEdit.Push(command);
            }
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out int result);
        }

        private void ProductNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Zа-яА-Я0-9-:]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DiscountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            SearchResults.Clear();
            string searchText = SearchInput.Text.ToLower();
            foreach (var item in App.Products)
            {
                if (item.Name.ToLower().Contains(searchText))
                {
                    SearchResults.Add(item);
                }
            }
            managmentList.ItemsSource = SearchResults;
        }
        private void ProductNameTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.V) // запрет вставки с помощью Ctrl+V
                || (e.Key == Key.Insert && e.KeyboardDevice.Modifiers == ModifierKeys.Shift) // запрет вставки с помощью Shift+Insert
                || (e.Key == Key.Delete && e.KeyboardDevice.Modifiers == ModifierKeys.Shift)) // запрет вырезки с помощью Shift+Delete
            {
                e.Handled = true;
            }
        }
        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string path = files[0]; // берем первый файл, который был перетащен

                ImageControl.Source = new BitmapImage(new Uri(path));
            }
        }

        private void CreateButton_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Message = Application.Current.Resources["savedMessage"] as string + "\n";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
            var newProduct = new Product
                (
                    App.Products.Last().Id+1, 
                    ProductAddNameTextBox.Text, 
                    DescriptionAddTextBox.Text, 
                    double.Parse(ProductAddPriceTextBox.Text, CultureInfo.InvariantCulture), 
                    int.Parse(DiscountAddTextBox.Text), 
                    ProductAddCategoryTextBox.Text, 
                    ImageAddControl.Source.ToString(), 
                    int.Parse(QuantityAddTextBox.Text)
                );
            foreach (var item in App.Products)
            {
                if (item.Name == newProduct.Name)
                {
                    Message = Application.Current.Resources["productExistsMessage"] as string + "\n";
                    new ToastContentBuilder()
                    .SetToastDuration(ToastDuration.Short)
                    .AddText(Message)
                    .Show();
                    return; // выходим из метода, так как продукт уже существует
                }
            }
            // Если выполнение дошло до этой точки, то продукта нет в списке и мы можем его добавить
            App.Products.Add(newProduct);
            var json = JsonConvert.SerializeObject(App.Products);
            File.WriteAllText(filePath, json);
            managmentList.ItemsSource = null;
            managmentList.ItemsSource = App.Products;
            new ToastContentBuilder()
                    .SetToastDuration(ToastDuration.Short)
                    .AddText(Message)
                    .Show();
        }

        private void OnDropAdd(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string path = files[0]; // берем первый файл, который был перетащен

                ImageAddControl.Source = new BitmapImage(new Uri(path));
            }
        }
        private void undoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_undoStackEdit.Count > 0)
            {
                var command = _undoStackEdit.Pop();
                command.Undo();
                _redoStackEdit.Push(command);
            }
            managmentList.ItemsSource = null;
            managmentList.ItemsSource = App.Products;
        }
        private void redoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_redoStackEdit.Count > 0)
            {
                var command = _redoStackEdit.Pop();
                command.Redo();
                _undoStackEdit.Push(command);
            }
            managmentList.ItemsSource = null;
            managmentList.ItemsSource = App.Products;
        }
    }
}
