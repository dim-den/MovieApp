using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieApp.WPF.Commands
{
    public class ChangeAppLanguageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string culture = (string)parameter;

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            ResourceDictionary dict = new ResourceDictionary();
            switch (culture)
            {
                case "ru-RU":
                    dict.Source = new Uri(String.Format(@"/Resources/Localization/lang.{0}.xaml", culture), UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri(@"/Resources/Localization/lang.xaml", UriKind.Relative);
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }


}

