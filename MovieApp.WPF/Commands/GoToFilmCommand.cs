using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.WPF.Commands
{
    public class GoToFilmCommand : AsyncCommandBase
    {

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            int i = 52;
            Actor film = (Actor)parameter;  
        }

        public GoToFilmCommand()
        {
        }
    }
}
