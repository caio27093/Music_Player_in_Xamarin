
using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Linq;


namespace MyMusic
{
    public class Splash : ContentPage
    {

        Image splashScreen;
        bool usuarioLogado;


        public Splash(bool usuarioLogado)
        {


            //Localizacao.Consultametros();

            NavigationPage.SetHasNavigationBar(this, false);
            this.usuarioLogado = usuarioLogado;

            var layout = new AbsoluteLayout();
            splashScreen = new Image
            {
                Source = "fonebranco"
            };

            AbsoluteLayout.SetLayoutFlags(splashScreen, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashScreen, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            layout.Children.Add(splashScreen);

            BackgroundColor = Color.FromHex("#34495E");
            Content = layout;
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            await splashScreen.ScaleTo(1, 10000);


            Application.Current.MainPage = new NavigationPage(new Inicio());

        }


    }
}
