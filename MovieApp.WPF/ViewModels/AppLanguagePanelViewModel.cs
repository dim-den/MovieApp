using System.Windows.Input;
using MovieApp.WPF.Commands;

namespace MovieApp.WPF.ViewModels
{
    public class AppLanguagePanelViewModel : ViewModelBase
    {
        public ICommand ChangeAppLanguageCommand { get; set; }

        public bool IsRuCultrureNow => System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU";
        public bool IsEnCultrureNow => !IsRuCultrureNow;

        public AppLanguagePanelViewModel()
        {
            ChangeAppLanguageCommand = new ChangeAppLanguageCommand();
        }
    }
}