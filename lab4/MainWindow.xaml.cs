using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using lab4.Core;
using lab4.Windows;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer searchTimer;
        

        ObservableCollection<Product> SearchResults = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");
            string json = File.ReadAllText(filePath);
            App.Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
            itemsControl.ItemsSource = new ObservableCollection<Product>(App.Products.Where(p => p.Quantity > 0));
            SearchInput.DataContext = this;
            searchTimer = new DispatcherTimer();
            searchTimer.Interval = TimeSpan.FromMilliseconds(500);
            searchTimer.Tick += new EventHandler(SearchTimer_Tick);
            ShoppingCardWindow.UpdateEvent += OnProductQuantityUpdate;
            ManagmentWindow.UpdateEvent += OnProductQuantityUpdate;
        }


        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void minimazeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maxmazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
                WindowState = WindowState.Normal;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ShowProductInfo(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (((FrameworkElement)sender).DataContext is Product product)
                {
                    var productInfoWindow = new ProductInfoWindow(product)
                    {
                        DataContext = product
                    };
                    productInfoWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
            itemsControl.ItemsSource = SearchResults;
        }

        private void ShoppingCart_Click(object sender, RoutedEventArgs e)
        {

            var productInfoWindow = new ShoppingCardWindow();
            productInfoWindow.ShowDialog();
        }

        private void SettingsRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void ManagmentRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var managmentWindow = new ManagmentWindow();
            managmentWindow.ShowDialog();
        }
        private void OnProductQuantityUpdate()
        {
            itemsControl.ItemsSource = null;
            itemsControl.ItemsSource = new ObservableCollection<Product>(App.Products.Where(p => p.Quantity > 0));
            SearchInput.DataContext = this;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(filtersTg_Btn.IsChecked == true)
            {
                tt_discount.Visibility = Visibility.Collapsed;
                tt_all.Visibility = Visibility.Collapsed;
                tt_outOfStock.Visibility = Visibility.Collapsed;
                tt_inStock.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_discount.Visibility = Visibility.Visible;
                tt_all.Visibility = Visibility.Visible;
                tt_outOfStock.Visibility= Visibility.Visible;
                tt_inStock.Visibility= Visibility.Visible;
            }
        }

        private void filtersTg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            contentArea.Opacity = 1;
            itemsControl.IsEnabled = true;
        }

        private void filtersTg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            contentArea.Opacity = 0.3;
            itemsControl.IsEnabled = false;
        }

        private void contentArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filtersTg_Btn.IsChecked = false; 
        }

        private void discountFilter_Selected(object sender, RoutedEventArgs e)
        {
            itemsControl.ItemsSource = new ObservableCollection<Product>(App.Products.Where(p => p.Discount > 0 && p.Quantity > 0));
            SearchInput.DataContext = this;
        }
        private void inStockFilter_Selected(object sender, RoutedEventArgs e)
        {
            OnProductQuantityUpdate();
        }
        private void outOfStockFilter_Selected(object sender, RoutedEventArgs e)
        {
            itemsControl.ItemsSource = new ObservableCollection<Product>(App.Products.Where(p => p.Quantity == 0));
            SearchInput.DataContext = this;
        }
        private void allFilter_Selected(object sender, RoutedEventArgs e)
        {
            itemsControl.ItemsSource = App.Products;
            SearchInput.DataContext = this;
        }
    }
}
