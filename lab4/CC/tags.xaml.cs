using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using lab4.Core;

namespace lab4.CC
{
    /// <summary>
    /// Логика взаимодействия для tags.xaml
    /// </summary>
    public partial class tags : UserControl
    {
        public tags()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register(
        "Products", typeof(ObservableCollection<Product>), typeof(tags));

        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(tags));

        public string SortProperty
        {
            get { return (string)GetValue(SortPropertyProperty); }
            set { SetValue(SortPropertyProperty, value); }
        }

        public static readonly DependencyProperty SortPropertyProperty =
            DependencyProperty.Register("SortProperty", typeof(string), typeof(tags));

        private bool isSortingDescending = false;
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)?.Tag?.ToString();
            if (tag != null && Products != null)
            {
                var sortedProducts = new ObservableCollection<Product>();
                if (!isSortingDescending)
                {
                    sortedProducts = new ObservableCollection<Product>(
                        Products.OrderBy(p => p.GetType().GetProperty(tag)?.GetValue(p)));
                    isSortingDescending = true;
                }
                else
                {
                    sortedProducts = new ObservableCollection<Product>(
                        Products.OrderByDescending(p => p.GetType().GetProperty(tag)?.GetValue(p)));
                    isSortingDescending = false;
                }
                Products.Clear();
                foreach (var product in sortedProducts)
                {
                    Products.Add(product);
                }
            }
            if ((sender as Button)?.FindName("Path") is Path path)
            {
                var angle = isSortingDescending ? 0 : 180;
                var rotateAnimation = new DoubleAnimation(angle, TimeSpan.FromSeconds(0.2));
                path.RenderTransformOrigin = new Point(0.5, 0.5);
                path.RenderTransform = new RotateTransform();
                path.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            }
        }
    }
}
