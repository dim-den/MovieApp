using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Commands
{
    public class BrowseImageCommand : ICommand
    {
        private readonly AdminPanelViewModel _adminPanelViewModel;
        public BrowseImageCommand(AdminPanelViewModel adminPanelViewModel)
        {
            _adminPanelViewModel = adminPanelViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Browse image",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF, *WEBP)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF;*.WEBP",
                RestoreDirectory = true
            };

                if (fileDialog.ShowDialog() == true)
            {
                string fileName = fileDialog.FileName;

                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(stream);
                    var image = br.ReadBytes((int)stream.Length);

                    int index = (int)parameter;

                    if (index == 0)
                    {
                        _adminPanelViewModel.User.ImageData = image;
                    }
                    else if (index == 2)
                    {
                        _adminPanelViewModel.Film.PosterImageData = image;
                    }
                    else if(index == 4)
                    {
                        _adminPanelViewModel.Actor.ImageData = image;
                    }
                }
            }
        }

    }
}
