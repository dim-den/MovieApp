using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.WPF.ViewModels.Builders
{
    interface IViewModelBuilder<T> where T : ViewModelBase
    {
        T Build();
    }
}
