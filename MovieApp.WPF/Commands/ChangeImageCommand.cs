using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using MovieApp.Domain.Models;
using MovieApp.EntityFramework.Services;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.Commands
{
    public class ChangeImageCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        public ChangeImageCommand(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Browse image",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF",
                RestoreDirectory = true
            };

            if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;

                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(stream);
                    var image = br.ReadBytes((int)stream.Length);

                    var currentUser = _authenticator.CurrentUser;
                    currentUser.ImageData = image;

                    _authenticator.CurrentUser = currentUser;

                    GenericDataService<User> dataService = new GenericDataService<User>();
                    await dataService.Update(_authenticator.CurrentUser.ID, _authenticator.CurrentUser);
                }   
            }
        }
    }
}
