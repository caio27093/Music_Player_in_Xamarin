using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FFImageLoading.Forms;
using MyMusic.Models;
using MyMusic.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusic.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Albums : MasterDetailPage
    {

        int id = 0;
        string name = string.Empty;
        string artist = string.Empty;
        string cover = string.Empty;
        public Albums()
        {
            InitializeComponent();
            this.BindingContext = new AlbunsViewModel();
        }


        public async Task ConsultaAlbum()
        {

            var teste = await Service.AlbumService.ConsultarAlbums();

            List<Album> listaDeAlbums = new List<Album>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Album");

            foreach (XmlNode retorno in retornoMusica.SelectNodes("Album"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Album"))
                {
                    int xid = 0;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    string xcover = string.Empty;

                    if (musica.SelectSingleNode("id") != null) xid = Convert.ToInt32(musica.SelectSingleNode("id").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = musica.SelectSingleNode("name").InnerText;
                    if (musica.SelectSingleNode("artist") != null) xartist = musica.SelectSingleNode("artist").InnerText;
                    if (musica.SelectSingleNode("cover") != null) xcover = musica.SelectSingleNode("cover-high").InnerText;


                    var contador = listaDeAlbums.Where(x => x.name == xname).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (id == xid)
                            {
                                name = xname;
                                artist = xartist;
                                cover = xcover;

                            }
                        }
                    }


                }
            }

        }

        async void OnTapped(object sender, EventArgs e)
        {

            try
            {
                CachedImage frame = (CachedImage)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                id = Convert.ToInt32(tapGesture.CommandParameter);
                await ConsultaAlbum();

                Application.Current.MainPage = new NavigationPage(new View.MusicaAlbum(id, name, artist, cover));

                //await DisplayAlert("Boa","Musica "+id+"Selecionada", "Ok");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
            // Do stuff
        }

        private async void TapGestureRecognizer_TappedLab(object sender, EventArgs e)
        {
            try
            {
                Label frame = (Label)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                int id = Convert.ToInt32(tapGesture.CommandParameter);

                await ConsultaAlbum();

                Application.Current.MainPage = new NavigationPage(new View.MusicaAlbum(id, name, artist, cover));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Home());
        }

        private void TapGestureRecognizerMusicas(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Musicas());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Albums());
        }
    }
}