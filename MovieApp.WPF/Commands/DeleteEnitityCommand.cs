using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class DeleteEnitityCommand : AsyncCommandBase
    {
        private readonly AdminPanelViewModel _adminPanelViewModel;
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_adminPanelViewModel.DeleteEntityID) && _adminPanelViewModel.DeleteEntityID.All(char.IsDigit) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _adminPanelViewModel.ErrorMessage = string.Empty;

            try
            {
                int index = (int)parameter;
                int id = Convert.ToInt32(_adminPanelViewModel.DeleteEntityID);

                if (index == 0)
                {
                    await _adminPanelViewModel.UnitOfWork.UserRepository.Delete(id);
                }
                else if (index == 1)
                {
                    await _adminPanelViewModel.UnitOfWork.FilmReviewRepository.Delete(id);
                }
                else if (index == 2)
                {
                    await _adminPanelViewModel.UnitOfWork.FilmRepository.Delete(id);
                }
                else if (index == 3)
                {
                    await _adminPanelViewModel.UnitOfWork.FilmCastRepository.Delete(id);
                }
                else if (index == 4)
                {
                    await _adminPanelViewModel.UnitOfWork.ActorRepository.Delete(id);
                }

                await _adminPanelViewModel.UnitOfWork.SaveAsync();
            }
            catch(Exception)
            {
                _adminPanelViewModel.ErrorMessage = "Wrong entity ID to delete!";
            }

        }

        public DeleteEnitityCommand(AdminPanelViewModel adminPanelViewModel)
        {
            _adminPanelViewModel = adminPanelViewModel;

            _adminPanelViewModel.PropertyChanged += AdminPanelViewModel_PropertyChanged;
        }

        private void AdminPanelViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AdminPanelViewModel.DeleteEntityID))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
