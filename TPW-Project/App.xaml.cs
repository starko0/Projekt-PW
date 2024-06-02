using System.Configuration;
using System.Data;
using System.Windows;
using TPW_Project.ViewModel;

namespace TPW_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainController()
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
