using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FFImageLoading.Forms;
using MyMusic.Models;
using MyMusic.ViewModels;
using Newtonsoft.Json;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusic.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicaAlbum : MasterDetailPage
    {

        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
        MusViewModel m = new MusViewModel();
        List<Musica> sourceMp3;
        bool isplay = false;
        bool isfirst = true;
        int idmusica = 0;
        int idalbum = 0;
        public static int quantidade;
        string name = String.Empty;
        string artist = String.Empty;
        public static int index = 0;
        string source = String.Empty;
        WebClient wc = new WebClient();
        string cover = String.Empty;
        List<Musica> listaDeMusicas = new List<Musica>();


        public MusicaAlbum(int id_album, string name, string artist, string cover)
        {
            InitializeComponent();
            this.BindingContext = new MusicalbumModelView(id_album);
            qtdeMusicas.Text = quantidade + " Músicas";
            idalbum = id_album;
            albName.Text = name;
            imgCover.Source = cover;
            player.PlaybackEnded += Player_PlaybackEnded;


        }


        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Albums());
        }
        private void TapGestureRecognizerMusicas(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Musicas());
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Application.Current.MainPage = new NavigationPage(new Home());
        }

        public async Task ConsultaMusica()
        {

            var teste = await Service.MusicaService.ConsultarMusicas();

            //List<Album> listaDeAlbums = new List<Album>();
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

                            }

                        }
                    }


                }
            }

        }

        public async Task ConsultaMusicaSurce()
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
                                source = xaudio;
                                name = xname;
                                artist = xartist;
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
                await ConsultaMusicaSurce();
                idmusica = Convert.ToInt32(tapGesture.CommandParameter);


                    List<Musica> _T = await m.ConsultaTodasAlbum(idalbum);

                await ConsultaMusicaSurce();
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

                    Application.Current.MainPage = new NavigationPage(new View.PlayerMusic(Convert.ToString(imgCover.Source), albName.Text, name, artist, _T, ind));


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
            // Do stuff
        }

        async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {

            try
            {
                Image frame = (Image)sender;
                TapGestureRecognizer tapGesture = (TapGestureRecognizer)frame.GestureRecognizers[0];

                idmusica = Convert.ToInt32(tapGesture.CommandParameter);
                await ConsultaMusica();

                List<Musica> _T = await m.ConsultaTodasAlbum(idalbum);


                await ConsultaMusicaSurce();
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
                Application.Current.MainPage = new NavigationPage(new View.PlayerMusic(Convert.ToString(imgCover.Source), albName.Text, name, artist, await m.ConsultaSources(idalbum),ind));

                //await DisplayAlert("Boa","Musica "+id+"Selecionada", "Ok");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }

        private void Player_PlaybackEnded(object sender, EventArgs e)
        {
            if (sourceMp3.Count >(++index))
            {
                index = 0;
            }
            else
            {
                index++;
            }
            Stream fileStream = wc.OpenRead(sourceMp3[index].audio);
            player.Load(fileStream);
            isplay = true;
            player.Play();
        }
        async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            if (isfirst)
            {
                isfirst = false;
                sourceMp3 = await m.ConsultaSources(idalbum);
                Stream fileStream = wc.OpenRead(sourceMp3[index].audio);
                player.Load(fileStream);
                Toca();
            }
            else
            {
                Toca();
            }

        }

        public void Toca(){

            if (isplay)
            {

                isplay = false;
                player.Pause();
                playAll.Source = "icplaybranco";


            }
            else
            {

                isplay = true;
                player.Play();
                playAll.Source = "icpausebranco";

            }



        }


    }
}