using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            string conexao = CrossConnectivity.Current.IsConnected ? "Connected" : "Disconnected";
            if (conexao == "Connected")
            {
                Login();
            }
            else
            {
                var page = new View.PopUp.PopupInternetConnection();

                await PopupNavigation.Instance.PushAsync(page);
            }
        }


        async void Login()
        {

            var page = new View.PopUp.PopupBoasVindas();

            await PopupNavigation.Instance.PushAsync(page);
            Application.Current.MainPage = new NavigationPage(new Home());
        }



}
}