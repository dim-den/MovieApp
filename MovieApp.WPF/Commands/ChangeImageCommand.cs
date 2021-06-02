using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using MovieApp.Domain.Services;
using MovieApp.WPF.State.Authentificator;

namespace MovieApp.WPF.Commands
{
    public class ChangeImageCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeImageCommand(IAuthenticator authenticator, IUnitOfWork unitOfWork)
        {
            _authenticator = authenticator;
            _unitOfWork = unitOfWork;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
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

                    await _unitOfWork.UserRepository.Update(_authenticator.CurrentUser.ID, _authenticator.CurrentUser);

                    await _unitOfWork.SaveAsync();
                }
            }
        }
    }
}