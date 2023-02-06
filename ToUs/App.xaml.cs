using System;
using System.Threading.Tasks;
using System.Windows;
using ToUs.View.StartView;

namespace ToUs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var startView =  new StartView();
            startView.Show();
            startView.IsVisibleChanged += (s, ev) =>
            {
                if (startView.IsVisible == false && startView.IsLoaded)
                {
                    var mainView = new View.MainView();
                    mainView.Show();
                    //startView.Close();
                }
            };
        }
    }
}
