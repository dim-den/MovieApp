using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNet.Identity;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class AddEntityCommand : AsyncCommandBase
    {
        private readonly AdminPanelViewModel _adminPanelViewModel;

        private readonly IUnitOfWork _unitOfWork;
        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _adminPanelViewModel.ErrorMessage = string.Empty;

            try
            {
                int index = (int)parameter;
                int id = Convert.ToInt32(_adminPanelViewModel.UpdateEntityID);

                if (index == 0)
                {
                    AuthenticationService authenticationService = new AuthenticationService(_unitOfWork, new PasswordHasher());
                    var user = _adminPanelViewModel.User;
                    user.ID = 0;

                    if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash) ||
                        string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Surname))
                    {
                        throw new Exception();
                    }

                    var res = await authenticationService.Register(user.Username,
                                                         user.Email,
                                                         user.PasswordHash,
                                                         user.PasswordHash,
                                                         user.Name,
                                                         user.Surname);
                    _adminPanelViewModel.Users.Add(res);
                }
                else if (index == 1)
                {
                    var filmReview = _adminPanelViewModel.FilmReview;
                    filmReview.ID = 0;

                    if (string.IsNullOrEmpty(filmReview.ReviewText) || filmReview.Score < 1 || filmReview.Score > 10 || filmReview.Date > DateTime.Now
                        || filmReview.FilmID == null || filmReview.UserID == null)
                    {
                        throw new Exception();
                    }

                    if (await _unitOfWork.UserRepository.Get(filmReview.UserID.Value) == null)
                    {
                        throw new UserNotFoundException(filmReview.UserID.Value.ToString());
                    }

                    if (await _unitOfWork.FilmRepository.Get(filmReview.FilmID.Value) == null)
                    {
                        throw new EntityNotFoundException(filmReview.FilmID.Value);
                    }

                    var res = await _unitOfWork.FilmReviewRepository.Create(filmReview);
                    _adminPanelViewModel.Reviews.Add(res);
                }
                else if (index == 2)
                {
                    var film = _adminPanelViewModel.Film;

                    if (string.IsNullOrEmpty(film.Title) || string.IsNullOrEmpty(film.Description) ||
                    string.IsNullOrEmpty(film.Genre) || string.IsNullOrEmpty(film.Country) ||
                    string.IsNullOrEmpty(film.Director) || film.Fees < 0 || film.Budget < 0)
                    {
                        throw new Exception();
                    }

                    var res = await _unitOfWork.FilmRepository.Create(film);
                    _adminPanelViewModel.Films.Add(res);
                }
                else if (index == 3)
                {
                    var filmCast = _adminPanelViewModel.FilmCast;
                    filmCast.ID = 0;

                    if (string.IsNullOrEmpty(filmCast.RoleName))
                    {
                        throw new Exception();
                    }

                    if (await _unitOfWork.ActorRepository.Get(filmCast.ActorID.Value) == null)
                    {
                        throw new EntityNotFoundException(filmCast.ActorID.Value);
                    }

                    if (await _unitOfWork.FilmRepository.Get(filmCast.FilmID.Value) == null)
                    {
                        throw new EntityNotFoundException(filmCast.FilmID.Value);
                    }

                    var res = await _unitOfWork.FilmCastRepository.Create(filmCast);
                    _adminPanelViewModel.Casts.Add(res);
                }
                else if (index == 4)
                {
                    var actor = _adminPanelViewModel.Actor;
                    actor.ID = 0;

                    if (string.IsNullOrEmpty(actor.Name) || string.IsNullOrEmpty(actor.Surname)
                        || string.IsNullOrEmpty(actor.Country) || actor.Bday > DateTime.Now)
                    {
                        throw new Exception();
                    }

                    var res = await _unitOfWork.ActorRepository.Create(actor);
                    _adminPanelViewModel.Actors.Add(res);
                }

                await _unitOfWork.SaveAsync();
            }
            catch (EmailAlreadyExistsException exception)
            {
                _adminPanelViewModel.ErrorMessage = $"Email {exception.Email} already exists";
            }
            catch (UsernameAlreadyExistsException exception)
            {
                _adminPanelViewModel.ErrorMessage = $"Username {exception.Username} already exists";
            }
            catch (EntityNotFoundException)
            {
                _adminPanelViewModel.ErrorMessage = $"Wrong entity ID";
            }
            catch (UserNotFoundException)
            {
                _adminPanelViewModel.ErrorMessage = $"Wrong user ID";
            }
            catch (Exception)
            {
                _adminPanelViewModel.ErrorMessage = "Wrong values!";
            }
        }

        public AddEntityCommand(AdminPanelViewModel adminPanelViewModel, IUnitOfWork unitOfWork)
        {
            _adminPanelViewModel = adminPanelViewModel;
            _unitOfWork = unitOfWork;
        }
    }
}
