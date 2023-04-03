using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using lab4.Core;
using lab4.Windows;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static object ViewModel { get; internal set; }
        public static ObservableCollection<Product> ShoppingCard { get; } = new ObservableCollection<Product>();
        public static ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        private static readonly List<CultureInfo> m_Languages = new List<CultureInfo>();

        private string Theme { get; set; }
        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            InitializeComponent();
            LanguageChanged += App_LanguageChanged;

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("en-US")); //Нейтральная культура для этого проекта
            m_Languages.Add(new CultureInfo("ru-RU"));
            SettingsWindow settingsWindow = new SettingsWindow();
            Theme = lab4.Properties.Settings.Default.CurrentTheme;
            if(Theme != null )
            {
                if(Theme == "Light")
                {
                    settingsWindow.SetLightTheme();
                }
                else
                {
                    settingsWindow.SetDarkTheme();
                }
            }
            Language = lab4.Properties.Settings.Default.DefaultLanguage;
        }
        //Евент для оповещения всех окон приложения
        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(string.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                ResourceDictionary oldDict = (from d in Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Current.Resources.MergedDictionaries.Remove(oldDict);
                    Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(dict);
                }

                //4. Вызываем евент для оповещения всех окон.
                LanguageChanged(Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            lab4.Properties.Settings.Default.DefaultLanguage = Language;
            lab4.Properties.Settings.Default.Save();
        }
    }
    
}
