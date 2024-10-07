using System;
using MvImage.Models;
using Prism.Ioc;
using Prism.Mvvm;

namespace MvImage.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private TextWrapper title = new ();

        [Obsolete("このコンストラクタはデザイン時に利用するためのものです。明示的に呼び出さないでください。")]
        public MainWindowViewModel()
        {
            FileListViewModel = new DummyFileListViewModel();
        }

        public MainWindowViewModel(IContainerProvider containerProvider)
        {
            FileListViewModel = containerProvider.Resolve<IFileListViewModel>();
        }

        public IFileListViewModel FileListViewModel { get; init; }

        public TextWrapper Title { get => title; set => SetProperty(ref title, value); }
    }
}