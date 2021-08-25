using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyMusic.ViewModels;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MyMusic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {

            //this.BindingContext = new AlbunsViewModel();
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new CadPage());
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

    }
}