using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MovieApp.Domain.Models;
using MovieApp.Domain.Exceptions;

namespace MovieApp.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _unitOfWork.UserRepository.GetByUsername(username);

            if (user == null)
            {
                throw new UserNotFoundException(username);
            }

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return user;
        }

        public async Task<User> Register(string username, string email, string password, string confirmPassword, string name, string surname)
        {
            if (password != confirmPassword)
            {
                throw new PasswordsMismatchException(password, confirmPassword);
            }

            User userAccount = await _unitOfWork.UserRepository.GetByEmail(email);
            if (userAccount != null)
            {
                throw new EmailAlreadyExistsException(email);
            }

            User userUsername = await _unitOfWork.UserRepository.GetByUsername(username);
            if (userUsername != null)
            {
                throw new UsernameAlreadyExistsException(username);
            }

            string hashedPassword = _passwordHasher.HashPassword(password);

            User user = new()
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Name = name,
                Surname = surname,
                ClientType = ClientType.User
            };

            var result = await _unitOfWork.UserRepository.Create(user);

            await _unitOfWork.SaveAsync();

            return result;
        }
    }
}
