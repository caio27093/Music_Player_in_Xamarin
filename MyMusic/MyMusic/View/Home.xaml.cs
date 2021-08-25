using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MyMusic.Models;
using MyMusic.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FFImageLoading.Forms;

namespace MyMusic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : MasterDetailPage
    {
        public static List<Album> listaDeAlbums = new List<Album>();
        int id= 0;
        string name = string.Empty;
        string artist = string.Empty;
        string cover = string.Empty;
        MusViewModel m = new MusViewModel();


        int idmusica = 0;
        int idalb = 0;
        string source = string.Empty;
        string albname = string.Empty;

        public async Task ConsultaMusica()
        {

            var teste = await Service.MusicaService.ConsultarMusicas();

            List<Musica> listaDeMusicas = new List<Musica>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Music");

            foreach (XmlNode retorno in retornoMusica.SelectNodes("Music"))
            {
                foreach (XmlNode musica in retorno.SelectNodes("Music"))
                {
                    int xid = 0;
                    int xid_album = 0;
                    string xname = string.Empty;
                    string xartist = string.Empty;
                    string xcover = string.Empty;
                    string xcolor = string.Empty;
                    string xaudio = string.Empty;
                    bool xfavorito = false;
                    string ximage = string.Empty;

                    if (musica.SelectSingleNode("id") != null) xid = Convert.ToInt32(musica.SelectSingleNode("id").InnerText);
                    if (musica.SelectSingleNode("id") != null) xid_album = Convert.ToInt32(musica.SelectSingleNode("id_album").InnerText);
                    if (musica.SelectSingleNode("name") != null) xname = musica.SelectSingleNode("name").InnerText;
                    if (musica.SelectSingleNode("artist") != null) xartist = musica.SelectSingleNode("artist").InnerText;
                    if (musica.SelectSingleNode("cover") != null) xcover = musica.SelectSingleNode("cover").InnerText;
                    if (musica.SelectSingleNode("color") != null) xcolor = (musica.SelectSingleNode("color").InnerText);
                    if (musica.SelectSingleNode("audio") != null) xaudio = musica.SelectSingleNode("audio").InnerText;
                    if (musica.SelectSingleNode("isFavorite") != null) xfavorito = Convert.ToBoolean(musica.SelectSingleNode("isFavorite").InnerText);


                    var contador = listaDeMusicas.Where(x => x.name == xname).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (idmusica == xid)
                            {
                                if (xfavorito == true)
                                {
                                    ximage = "coracaonormal";
                                }
                                else
                                {
                                    ximage = "coracaobazul";
                                }
                                name = xname;
                                artist = xartist;
                                source = xaudio;
                                cover = xcover;
                                idalb = xid_album;

                            }

                        }
                    }


                }
            }

        }
        public Home()
        {

            InitializeComponent();
            this.BindingContext = new AlbunsViewModel();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new View.Albums());
        }

        private void TapGestureRecognizerMusicas(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new View.Musicas());
        }
        async void OnTapped(object sender, EventArgs e)
        {

            try
            {
                CachedImage frame = (CachedImage)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                 id = Convert.ToInt32( tapGesture.CommandParameter);
                await ConsultaAlbum();

                Application.Current.MainPage = new NavigationPage(new View.MusicaAlbum(id,name,artist,cover));

                //await DisplayAlert("Boa","Musica "+id+"Selecionada", "Ok");
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
            // Do stuff
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

        public async Task ConsultaAlb()
        {



            var teste = await Service.AlbumService.ConsultarAlbums();

            List<Album> listaDeAlbums = new List<Album>();
            XmlDocument retornoMusica = new XmlDocument();
            retornoMusica = (XmlDocument)JsonConvert.DeserializeXmlNode(Convert.ToString(teste), "Album");

            int id = 0;
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
                    if (musica.SelectSingleNode("cover") != null) xcover = musica.SelectSingleNode("cover").InnerText;


                    var contador = listaDeAlbums.Where(x => x.name == xname).ToList();
                    if (contador != null)
                    {
                        if (contador.Count() == 0)
                        {
                            if (xid == idalb)
                            {
                                cover = xcover;
                                albname = xname;
                            }




                        }
                    }


                }
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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

        private void TapGestureRecognizer_Tapped_(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Home());
        }

        async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            try
            {
                StackLayout frame = (StackLayout)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                idmusica = Convert.ToInt32(tapGesture.CommandParameter);
                await ConsultaMusica();
                await ConsultaAlb();

                await Toc();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
        public async Task Toc()
        {

            List<Musica> _T = await m.ConsultaFavoritas();

            int ind = 0;
            int i = 0;
            int a = 0;
            while (i < 10)
                 {
                    if (_T[ind].audio == source)
                    {

                    i=11;
                     }
                    else
                    {
                        ind++;
                    }
                a++;
                 }

            Application.Current.MainPage = new NavigationPage(new View.PlayerMusic("https://i.imgur.com/ftiXHvX.png", "Favoritas", name, artist, _T, ind));
        }

        async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            try
            {
                Image frame = (Image)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                idmusica = Convert.ToInt32(tapGesture.CommandParameter);
                await ConsultaMusica();
                await ConsultaAlb();

                await Toc();

                //await DisplayAlert("Boa","Musica "+id+"Selecionada", "Ok");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
    }
}