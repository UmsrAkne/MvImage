using System.Windows;
using MvImage.ViewModels;
using MvImage.Views;
using Prism.Ioc;

namespace MvImage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            #if RELEASE
            containerRegistry.RegisterSingleton<IFileListViewModel, FileListViewModel>();
            #else
            containerRegistry.RegisterSingleton<IFileListViewModel, DummyFileListViewModel>();
            #endif
        }
    }
}