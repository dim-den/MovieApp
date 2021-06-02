using System;
using System.Threading.Tasks;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class SendEmailConfirmCodeCommand : AsyncCommandBase
    {
        private int _verificationCodeLength;
        private readonly IEmailService _emailService;
        private readonly EmailConfirmPanelViewModel _emailConfirmPanelViewModel;

        public SendEmailConfirmCodeCommand(EmailConfirmPanelViewModel emailConfirmPanelViewModel, IEmailService emailService, int verificationCodeLength)
        {
            _emailService = emailService;
            _emailConfirmPanelViewModel = emailConfirmPanelViewModel;
            _verificationCodeLength = verificationCodeLength;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                string body = "<p>Hey {0}, type this verification code in application to confirm your email:</p>" +
                              "<b>{1}</b><br>" +
                              "<p>Thank you for using our app!</p>";
                string subject = "Confirm your email";

                _emailConfirmPanelViewModel.ExpectedVerificationCode = await _emailService.SendMessage(_emailConfirmPanelViewModel.CurrentUser, body, subject, _verificationCodeLength);
            }
            catch (Exception)
            {
                _emailConfirmPanelViewModel.ErrorMessage = "An error occured while sending message, check for Internet connection";
            }
        }
    }
}