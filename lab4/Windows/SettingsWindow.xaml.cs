using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace lab4.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        ResourceDictionary darkTheme = new ResourceDictionary
        {
            Source = new Uri("/Themes/DarkBrushes.xaml", UriKind.Relative)
        };
        ResourceDictionary lightTheme = new ResourceDictionary
        {
            Source = new Uri("/Themes/LightBrushes.xaml", UriKind.Relative)
        };
        public SettingsWindow()
        {
            InitializeComponent();
            Closing += SettingsWindow_Closing;
            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;
            languageComboBox.ItemsSource = App.Languages;
            languageComboBox.SelectedItem = App.Language;
            if(lab4.Properties.Settings.Default.CurrentTheme == "Light")
            {
                themeComboBox.SelectedIndex = 0;
            }
            if (lab4.Properties.Settings.Default.CurrentTheme == "Dark")
            {
                themeComboBox.SelectedIndex = 1;
            }
        }
        private void SettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.SettingsRadioButton.IsChecked = false;
                mainWindow.HomeRadioButton.IsChecked = true;
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void LanguageChanged(object sender, EventArgs e)
        {
            languageComboBox.SelectedItem = App.Language;
        }

        private void ChangeLanguageClick(object sender, EventArgs e)
        {
            if (languageComboBox.SelectedItem is CultureInfo lang)
            {
                App.Language = lang;
            }
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (themeComboBox.SelectedItem != null)
            {
                if (themeComboBox.SelectedIndex == 0)
                {
                    SetLightTheme();
                }
                else if (themeComboBox.SelectedIndex == 1)
                {
                    SetDarkTheme();
                }
            }
        }
        internal void SetLightTheme()
        {
            
            Application.Current.Resources.MergedDictionaries.Remove(darkTheme);
            Application.Current.Resources.MergedDictionaries.Add(lightTheme);
            lab4.Properties.Settings.Default.CurrentTheme = "Light";
            lab4.Properties.Settings.Default.Save();
        }

        // Метод для установки темной темы
        internal void SetDarkTheme()
        {
            Application.Current.Resources.MergedDictionaries.Remove(lightTheme);
            Application.Current.Resources.MergedDictionaries.Add(darkTheme);
            lab4.Properties.Settings.Default.CurrentTheme = "Dark";
            lab4.Properties.Settings.Default.Save();
        }
    }
}
