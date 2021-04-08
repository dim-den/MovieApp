using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class ChangeViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public ChangeViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "ToLogin")
            {
                viewModel.CurrentViewModel = new LoginViewModel(viewModel);
            }
            else if (parameter.ToString() == "ToRegister")
            {
                viewModel.CurrentViewModel = new RegisterViewModel(viewModel);
            }
        }
    }
}
