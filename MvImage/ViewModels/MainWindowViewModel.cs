using MvImage.Models;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private TextWrapper title;

        public TextWrapper Title { get => title; set => SetProperty(ref title, value); }
    }
}