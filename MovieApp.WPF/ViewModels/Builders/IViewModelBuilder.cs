namespace MovieApp.WPF.ViewModels.Builders
{
    internal interface IViewModelBuilder<T> where T : ViewModelBase
    {
        T Build();
    }
}