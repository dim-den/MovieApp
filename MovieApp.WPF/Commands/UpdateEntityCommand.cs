using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNet.Identity;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class UpdateEntityCommand : AsyncCommandBase
    {
        private readonly AdminPanelViewModel _adminPanelViewModel;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticator _authenticator;
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_adminPanelViewModel.UpdateEntityID) && _adminPanelViewModel.UpdateEntityID.All(char.IsDigit) && base.CanExecute(parameter);
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
                    var updatedUser = _adminPanelViewModel.User;
                    var user = await _unitOfWork.UserRepository.Get(id);

                    if (user == null)
                    {
                        throw new UserNotFoundException(id.ToString());
                    }

                    if (user.ClientType >= _authenticator.CurrentUser.ClientType ||
                        _adminPanelViewModel.User.ClientType >= _authenticator.CurrentUser.ClientType)
                    {
                        throw new NotEnoughRightsException(_authenticator.CurrentUser.ClientType, user.ClientType);
                    }

                    if (user.Email != updatedUser.Email && await _unitOfWork.UserRepository.GetByEmail(updatedUser.Email) != null)
                    {
                        throw new EmailAlreadyExistsException(updatedUser.Email);
                    }

                    if (user.Username != updatedUser.Username && await _unitOfWork.UserRepository.GetByUsername(updatedUser.Username) != null)
                    {
                        throw new UsernameAlreadyExistsException(updatedUser.Username);
                    }

                    string hashedPassword = null;
                    if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
                        hashedPassword = new PasswordHasher().HashPassword(updatedUser.PasswordHash);

                    user.Username = (string.IsNullOrEmpty(updatedUser.Username) ? user.Username : updatedUser.Username);
                    user.Email = (string.IsNullOrEmpty(updatedUser.Email) ? user.Email : updatedUser.Email);
                    user.PasswordHash = hashedPassword ?? user.PasswordHash;
                    user.ClientType = updatedUser.ClientType;
                    user.Status = (string.IsNullOrEmpty(updatedUser.Status) ? user.Status : updatedUser.Status);
                    user.Name = (string.IsNullOrEmpty(updatedUser.Name) ? user.Name : updatedUser.Name);
                    user.Surname = (string.IsNullOrEmpty(updatedUser.Surname) ? user.Surname : updatedUser.Surname);

                    var res = await _unitOfWork.UserRepository.Update(id, user);
                    var i = _adminPanelViewModel.Users.IndexOf(_adminPanelViewModel.Users.First(e => e.ID == id));
                    _adminPanelViewModel.Users[i] = res;
                }
                else if (index == 1)
                {
                    var updatedFilmReview = _adminPanelViewModel.FilmReview;
                    var filmReview = await _unitOfWork.FilmReviewRepository.Get(id);

                    if (filmReview == null)
                    {
                        throw new EntityNotFoundException(id);
                    }

                    if (updatedFilmReview.UserID != null)
                    {
                        filmReview.UserID = updatedFilmReview.UserID;
                        if (await _unitOfWork.UserRepository.Get(filmReview.UserID.Value) == null)
                        {
                            throw new UserNotFoundException(filmReview.UserID.Value.ToString());
                        }
                    }

                    if (updatedFilmReview.FilmID != null)
                    {
                        filmReview.FilmID = updatedFilmReview.FilmID;
                        if (await _unitOfWork.FilmRepository.Get(filmReview.FilmID.Value) == null)
                        {
                            throw new EntityNotFoundException(filmReview.FilmID.Value);
                        }
                    }

                    filmReview.ReviewText = (string.IsNullOrEmpty(updatedFilmReview.ReviewText) ? filmReview.ReviewText : updatedFilmReview.ReviewText);
                    filmReview.Score = (updatedFilmReview.Score == 0 ? filmReview.Score : updatedFilmReview.Score);
                    filmReview.Date = (updatedFilmReview.Date > new DateTime(1900, 1, 1) ? updatedFilmReview.Date : filmReview.Date);

                    var res = await _unitOfWork.FilmReviewRepository.Update(id, filmReview);
                    var i = _adminPanelViewModel.Reviews.IndexOf(_adminPanelViewModel.Reviews.First(e => e.ID == id));
                    _adminPanelViewModel.Reviews[i] = res;
                }
                else if (index == 2)
                {
                    var updatedFilm = _adminPanelViewModel.Film;
                    var film = await _unitOfWork.FilmRepository.Get(id);

                    if (film == null)
                    {
                        throw new EntityNotFoundException(id);
                    }

                    film.Title = (string.IsNullOrEmpty(updatedFilm.Title) ? film.Title : updatedFilm.Title);
                    film.Description = (string.IsNullOrEmpty(updatedFilm.Description) ? film.Description : updatedFilm.Description);
                    film.Genre = (string.IsNullOrEmpty(updatedFilm.Genre) ? film.Genre : updatedFilm.Genre);
                    film.Country = (string.IsNullOrEmpty(updatedFilm.Country) ? film.Country : updatedFilm.Country);
                    film.Director = (string.IsNullOrEmpty(updatedFilm.Director) ? film.Director : updatedFilm.Director);
                    film.Budget = (updatedFilm.Budget == 0 ? film.Budget : updatedFilm.Budget);
                    film.Fees = (updatedFilm.Fees == 0 ? film.Fees : updatedFilm.Fees);
                    film.ReleaseDate = (updatedFilm.ReleaseDate > new DateTime(1900, 1, 1) ? updatedFilm.ReleaseDate : film.ReleaseDate);
                    film.PosterImageData = (updatedFilm.PosterImageData == null ? film.PosterImageData : updatedFilm.PosterImageData);

                    var res = await _unitOfWork.FilmRepository.Update(id, film);
                    var i = _adminPanelViewModel.Films.IndexOf(_adminPanelViewModel.Films.First(e => e.ID == id));
                    _adminPanelViewModel.Films[i] = res;
                }
                else if(index == 3)
                {
                    var updateFilmCast = _adminPanelViewModel.FilmCast;
                    var filmCast = await _unitOfWork.FilmCastRepository.Get(id);

                    if (filmCast == null)
                    {
                        throw new EntityNotFoundException(id);
                    }

                    if (updateFilmCast.ActorID != null)
                    {
                        filmCast.ActorID = updateFilmCast.ActorID;
                        if (await _unitOfWork.UserRepository.Get(filmCast.ActorID.Value) == null)
                        {
                            throw new EntityNotFoundException(filmCast.ActorID.Value);
                        }
                    }

                    if (updateFilmCast.FilmID != null)
                    {
                        filmCast.FilmID = updateFilmCast.FilmID;
                        if (await _unitOfWork.FilmRepository.Get(filmCast.FilmID.Value) == null)
                        {
                            throw new EntityNotFoundException(filmCast.FilmID.Value);
                        }
                    }

                    filmCast.RoleType = updateFilmCast.RoleType;
                    filmCast.RoleName = (string.IsNullOrEmpty(updateFilmCast.RoleName) ? filmCast.RoleName : updateFilmCast.RoleName);

                    var res = await _unitOfWork.FilmCastRepository.Update(id, filmCast);
                    var i = _adminPanelViewModel.Casts.IndexOf(_adminPanelViewModel.Casts.First(e => e.ID == id));
                    _adminPanelViewModel.Casts[i] = res;
                }
                else if (index == 4)
                {
                    var updatedActor = _adminPanelViewModel.Actor;
                    var actor = await _unitOfWork.ActorRepository.Get(id);

                    if (actor == null)
                    {
                        throw new EntityNotFoundException(id);
                    }

                    actor.Name = (string.IsNullOrEmpty(updatedActor.Name) ? actor.Name : updatedActor.Name);
                    actor.Surname = (string.IsNullOrEmpty(updatedActor.Surname) ? actor.Surname : updatedActor.Surname);
                    actor.Country = (string.IsNullOrEmpty(updatedActor.Country) ? actor.Country : updatedActor.Country);
                    actor.Bday = (updatedActor.Bday > new DateTime(1900, 1, 1) ? updatedActor.Bday : actor.Bday);
                    actor.ImageData = (updatedActor.ImageData == null ? actor.ImageData : updatedActor.ImageData);

                    var res = await _unitOfWork.ActorRepository.Update(id, actor);
                    var i = _adminPanelViewModel.Actors.IndexOf(_adminPanelViewModel.Actors.First(e => e.ID == id));
                    _adminPanelViewModel.Actors[i] = res;
                }

                await _unitOfWork.SaveAsync();
            }
            catch (NotEnoughRightsException exception)
            {
                _adminPanelViewModel.ErrorMessage = $"Not enough rights to update {exception.Changed}";
            }
            catch (EmailAlreadyExistsException exception)
            {
                _adminPanelViewModel.ErrorMessage = $"Email {exception.Email} already exists";
            }
            catch (EntityNotFoundException)
            {
                _adminPanelViewModel.ErrorMessage = $"Wrong entity ID";
            }
            catch (UserNotFoundException)
            {
                _adminPanelViewModel.ErrorMessage = $"Wrong user ID";
            }
            catch (UsernameAlreadyExistsException exception)
            {
                _adminPanelViewModel.ErrorMessage = $"Username {exception.Username} already exists";
            }
            catch (Exception)
            {
                _adminPanelViewModel.ErrorMessage = "Wrong values!";
            }

        }

        public UpdateEntityCommand(AdminPanelViewModel adminPanelViewModel, IUnitOfWork unitOfWork, IAuthenticator authentificator)
        {
            _adminPanelViewModel = adminPanelViewModel;
            _unitOfWork = unitOfWork;
            _authenticator = authentificator;

            _adminPanelViewModel.PropertyChanged += AdminPanelViewModel_PropertyChanged;
        }

        private void AdminPanelViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AdminPanelViewModel.UpdateEntityID))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
