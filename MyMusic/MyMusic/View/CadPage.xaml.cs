using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadPage : ContentPage
    {
        public CadPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked ( object sender, EventArgs e )
        {
            try
            {
                var page = new View.PopUp.SucessoCad ( );

                await PopupNavigation.Instance.PushAsync ( page );
                Application.Current.MainPage = new NavigationPage ( new LoginPage ( ) );
            }
            catch (Exception ex)
            {

                //throw;
            }

        }
    }
}