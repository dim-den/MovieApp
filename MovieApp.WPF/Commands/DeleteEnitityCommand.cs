using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class DeleteEnitityCommand : AsyncCommandBase
    {
        private readonly AdminPanelViewModel _adminPanelViewModel;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;

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
                    var user = await _unitOfWork.UserRepository.Get(id);

                    if (user.ClientType >= _authenticator.CurrentUser.ClientType)
                    {
                        throw new NotEnoughRightsException(_authenticator.CurrentUser.ClientType, user.ClientType);
                    }

                    await _unitOfWork.UserRepository.Delete(id);
                    _adminPanelViewModel.Users.Remove(_adminPanelViewModel.Users.FirstOrDefault(e => e.ID == id));
                }
                else if (index == 1)
                {
                    await _unitOfWork.FilmReviewRepository.Delete(id);
                    _adminPanelViewModel.Reviews.Remove(_adminPanelViewModel.Reviews.FirstOrDefault(e => e.ID == id));
                }
                else if (index == 2)
                {
                    await _unitOfWork.FilmRepository.Delete(id);
                    _adminPanelViewModel.Films.Remove(_adminPanelViewModel.Films.FirstOrDefault(e => e.ID == id));
                }
                else if (index == 3)
                {
                    await _unitOfWork.FilmCastRepository.Delete(id);
                    _adminPanelViewModel.Casts.Remove(_adminPanelViewModel.Casts.FirstOrDefault(e => e.ID == id));
                }
                else if (index == 4)
                {
                    await _unitOfWork.ActorRepository.Delete(id);
                    _adminPanelViewModel.Actors.Remove(_adminPanelViewModel.Actors.FirstOrDefault(e => e.ID == id));
                }

                await _unitOfWork.SaveAsync();
            }
            catch (NotEnoughRightsException exception)
            {
                _adminPanelViewModel.ErrorMessage = $"Not enough rights to delete {exception.Changed}";
            }
            catch (Exception)
            {
                _adminPanelViewModel.ErrorMessage = "Wrong entity ID to delete!";
            }
        }

        public DeleteEnitityCommand(AdminPanelViewModel adminPanelViewModel, IUnitOfWork unitOfWork, IAuthenticator authentificator)
        {
            _adminPanelViewModel = adminPanelViewModel;
            _unitOfWork = unitOfWork;
            _authenticator = authentificator;

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