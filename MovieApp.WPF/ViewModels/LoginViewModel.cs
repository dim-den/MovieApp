using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.WPF.Commands;

namespace MovieApp.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand ChangeViewCommand { get; set; }

        public LoginViewModel(MainViewModel mainView)
        {
            ChangeViewCommand = new ChangeViewCommand(mainView);
        }
    }
}
