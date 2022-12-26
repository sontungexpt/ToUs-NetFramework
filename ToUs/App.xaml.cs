using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToUs.View.AuthenticateView;

namespace ToUs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var authenticateView =  new AuthenticateView();
            authenticateView.Show();
            authenticateView.IsVisibleChanged += (s, ev) =>
            {
                if (authenticateView.IsVisible == false && authenticateView.IsLoaded)
                {
                    var mainView = new View.MainView();
                    mainView.Show();
                    authenticateView.Close();
                }
            };
        }
    }
}
