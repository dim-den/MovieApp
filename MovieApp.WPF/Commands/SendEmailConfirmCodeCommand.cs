﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class SendEmailConfirmCodeCommand : AsyncCommandBase
    {
        private int _verificationCodeLength;
        private readonly IEmailService _emailService;
        private readonly SettingsViewModel _settingsViewModel;
        public SendEmailConfirmCodeCommand(SettingsViewModel settingViewModel, int verificationCodeLength)
        {
            _emailService = new EmailService();
            _settingsViewModel = settingViewModel;
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

                _settingsViewModel.ExpectedVerificationCode = await _emailService.SendMessage(_settingsViewModel.CurrentUser, body, subject, _verificationCodeLength);
            }
            catch (Exception)
            {
                _settingsViewModel.ErrorMessage = "An error occured while sending message, check for Internet connection";
            }
        }
    }
}
