using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MyMusic.Models;
using MyMusic.ViewModels;
using Newtonsoft.Json;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusic.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Musicas : MasterDetailPage
    {
        int idmusica = 0;
        int idalb = 0;
        string name = string.Empty;
        string artist   =string.Empty;
        string source=string.Empty;
        string cover = string.Empty;
        string albname = string.Empty;
        MusViewModel m = new MusViewModel();




        public Musicas()
        {
            InitializeComponent();
            this.BindingContext = new MusViewModel();
        }


        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Albums());
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Home());
        }

        private void TapGestureRecognizerMusicas(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Musicas());
        }
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
        public async Task ConsultaAlbum()
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
                            if (xid ==idalb)
                            {
                                cover = xcover;
                                albname = xname;
                            }




                        }
                    }


                }
            }

        }
        async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            try
            {
                StackLayout frame = (StackLayout)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                idmusica = Convert.ToInt32(tapGesture.CommandParameter);
                await ConsultaMusica();
                await ConsultaAlbum();

                List<Musica> _T = await m.ConsultaTodas();

                int ind = 0;
                int i = 0;
                int a = 0;
                while (i < 10)
                {
                    if (_T[ind].audio == source)
                    {

                        i = 11;
                    }
                    else
                    {
                        ind++;
                    }
                    a++;
                }

                Application.Current.MainPage = new NavigationPage(new View.PlayerMusic("https://i.imgur.com/To3qLwq.png", "Músicas", name, artist, _T,ind));



            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
        async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            try
            {
                Image frame = (Image)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                idmusica = Convert.ToInt32(tapGesture.CommandParameter);
                await ConsultaMusica();
                await ConsultaAlbum();

                List<Musica> _T = await m.ConsultaTodas();

                int ind = 0;
                int i = 0;
                int a = 0;
                while (i < 10)
                {
                    if (_T[ind].audio == source)
                    {

                        i = 11;
                    }
                    else
                    {
                        ind++;
                    }
                    a++;
                }


                Application.Current.MainPage = new NavigationPage(new View.PlayerMusic("https://i.imgur.com/To3qLwq.png", "Músicas", name, artist, _T,ind));

                //await DisplayAlert("Boa","Musica "+id+"Selecionada", "Ok");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
    }
}