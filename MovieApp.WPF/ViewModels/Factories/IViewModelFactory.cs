﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        Task<ViewModelBase> CreateViewModel(object viewType);
    }
}
