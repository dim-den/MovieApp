using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Moq;
using MovieApp.Domain.Exceptions;
using MovieApp.Domain.Models;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using NUnit.Framework;

namespace MovieApp.Tests.Services.AuthentificationServices
{
    [TestFixture]
    public class AuthentificationServiceTest
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPasswordHasher> _mockPasswordHasher;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _authenticationService = new AuthenticationService(_mockUnitOfWork.Object, _mockPasswordHasher.Object);
        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsUser()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByUsername(expectedUsername)).ReturnsAsync(new User() { Username = expectedUsername });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);

            User user = await _authenticationService.Login(expectedUsername, password);

            string actualUsername = user.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithIncorrectPasswordForExistingUsername_ThrowsInvalidPasswordException()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByUsername(expectedUsername)).ReturnsAsync(new User() { Username = expectedUsername });
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() => _authenticationService.Login(expectedUsername, password));

            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsUserNotFoundException()
        {
            string expectedUsername = "testuser";
            string password = "testpassword";
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByUsername(expectedUsername)).ReturnsAsync((User)null);
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Failed);

            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => _authenticationService.Login(expectedUsername, password));

            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsUser()
        {
            string username = "testregistration";
            string email = "testregistration@gmail.com";
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByUsername(username)).ReturnsAsync((User)null);
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByEmail(email)).ReturnsAsync((User)null);
            _mockUnitOfWork.Setup(s => s.UserRepository.Create(It.IsAny<User>())).ReturnsAsync(new User(){ Username = username });

            var user = await _authenticationService.Register(username, email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.AreEqual(username, user.Username);
        }

        [Test]
        public void Register_WithPasswordsNotMatching_ThrowsPasswordsMismatchException()
        {
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";

            PasswordsMismatchException exception = Assert.ThrowsAsync<PasswordsMismatchException>(
                () => _authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword, It.IsAny<string>(), It.IsAny<string>()));

            string actualPassword = exception.Password;
            string actualConfirmPassword = exception.ConfirmPassword;
            Assert.AreEqual(password, actualPassword);
            Assert.AreEqual(confirmPassword, actualConfirmPassword);
        }

        [Test]
        public void Register_WithAlreadyExistingEmail_ThrowsEmailAlreadyExistsException()
        {
            string email = "test@gmail.com";
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByEmail(email)).ReturnsAsync(new User());

            EmailAlreadyExistsException exception = Assert.ThrowsAsync<EmailAlreadyExistsException>(
                () => _authenticationService.Register(It.IsAny<string>(), email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            string actualEmail = exception.Email;
            Assert.AreEqual(email, actualEmail);
        }

        [Test]
        public void Register_WithAlreadyExistingUsername_ThrowsUsernameAlreadyExistsException()
        {
            string username = "testuser";
            _mockUnitOfWork.Setup(s => s.UserRepository.GetByUsername(username)).ReturnsAsync(new User());

            UsernameAlreadyExistsException exception = Assert.ThrowsAsync<UsernameAlreadyExistsException>(
                () => _authenticationService.Register(username, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            string actualUsername = exception.Username;
            Assert.AreEqual(username, actualUsername);
        }

        [Test]
        public async Task ChangePassword_WithCorrectOldPasswordAndMatchingNewPasswords_ReturnsUserAsync()
        {
            User expectedUser = new User()
            {
                Username = "testuser",
                PasswordHash = "testpassword"
            };
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), expectedUser.PasswordHash)).Returns(PasswordVerificationResult.Success);
            _mockUnitOfWork.Setup(s => s.UserRepository.Update(It.IsAny<int>(), It.IsAny<User>())).ReturnsAsync(new User());

            User actualUser = await _authenticationService.ChangePassword(expectedUser, expectedUser.PasswordHash, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expectedUser.Username, actualUser.Username);
        }

        [Test]
        public void ChangePassword_WithNotMatchingNewPasswords_ThrowsPasswordsMismatchException()
        {
            string newPassword = "testpassword";
            string confirmPassword = "confirmtestpassword";

            PasswordsMismatchException exception = Assert.ThrowsAsync<PasswordsMismatchException>(
                () => _authenticationService.ChangePassword(It.IsAny<User>(), It.IsAny<string>(), newPassword, confirmPassword));

            string actualPassword = exception.Password;
            string actualConfirmPassword = exception.ConfirmPassword;
            Assert.AreEqual(newPassword, actualPassword);
            Assert.AreEqual(confirmPassword, actualConfirmPassword);
        }

        [Test]
        public void ChangePassword_WithIncorrectOldPassword_ThrowsInvalidPasswordException()
        {
            User expectedUser = new User()
            {
                Username = "testuser",
                PasswordHash = "testpassword"
            };
            _mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), expectedUser.PasswordHash)).Returns(PasswordVerificationResult.Failed);

            InvalidPasswordException exception = Assert.ThrowsAsync<InvalidPasswordException>(() =>
                                                    _authenticationService.ChangePassword(expectedUser, expectedUser.PasswordHash, It.IsAny<string>(), It.IsAny<string>()));

            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUser.Username, actualUsername);
        }

    }
}
