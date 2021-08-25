using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyMusic.Models;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMusic.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerMusic : MasterDetailPage
    {
        public PlayerMusic(string cover,string albName,string musname,string artista, List<Musica> sourcemusica,int indexInit)
        {
            InitializeComponent(); 
            BindingContext = this;
            MyStringProperty = "00:00";
            imgCover.Source = cover.Replace("Uri: ","");
            lblAlbum.Text = albName;
            lblmus.Text = musname;
            indexPrincipal = indexInit;
            lblartista.Text = artista;
            allSources = sourcemusica;
            Stream fileStream = wc.OpenRead(sourcemusica[indexInit].audio);
            player.Load(fileStream);

            player.PlaybackEnded += Player_PlaybackEnded;

            var segundos = (Convert.ToInt16( player.Duration)+1);
            sliderMus.Maximum = segundos;

            t.Elapsed += new System.Timers.ElapsedEventHandler(this.AumentarTempo);
            t.Interval = 1000;
            int hor = (int)(segundos / (60 * 60));

            int min = (int)((segundos - (hor * 60 * 60)) / 60);

            int seg = (int)(segundos - (hor * 60 * 60) - (min * 60));
            string minutos = string.Empty;
            string segundost = string.Empty;

            if (min <=10)
            {
                minutos = String.Format("0{0}", min);
            }
            else
            {

                minutos = String.Format("{0}", min);
            }
            if (seg <= 10)
            {
                segundost = String.Format("0{0}", seg);
            }
            else
            {

                segundost = String.Format("{0}", seg);
            }

            lblTempoMusica.Text = String.Format("{0}:{1}", minutos, segundost);



            _timer = new System.Timers.Timer();
            _timer.AutoReset = true;
            _timer.Interval = 1000; // Intervalo em milésimos
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(AumentaSlider);
            




        }
        private void sliderMus_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            segundos++;

            int hor = (int)(segundos / (60 * 60));

            int min = (int)((segundos - (hor * 60 * 60)) / 60);

            int seg = (int)(segundos - (hor * 60 * 60) - (min * 60));
            string minutos = string.Empty;
            string segundost = string.Empty;

            if (min <= 10)
            {
                minutos = String.Format("0{0}", min);
            }
            else
            {

                minutos = String.Format("{0}", min);
            }
            if (seg <= 10)
            {
                segundost = String.Format("0{0}", seg);
            }
            else
            {

                segundost = String.Format("{0}", seg);
            }

            MyStringProperty = String.Format("{0}:{1}", minutos, segundost);

        }
        public void AumentarTempo(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            tempo++;


            lbltimer.Text = tempo.ToString();

            _timer.Enabled = true;
        }
        public  void AumentaSlider(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            sliderMus.Value++;




            _timer.Enabled = true;
        }
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Musicas());
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
        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            int quantidade = allSources.Count;
            --quantidade;
            if (indexPrincipal== quantidade)
            {
                indexPrincipal = 0;
            }
            else
            {
                indexPrincipal++;
            }
            Stream fileStream = wc.OpenRead(allSources[indexPrincipal].audio);

            lblmus.Text = allSources[indexPrincipal].name;
            lblartista.Text = allSources[indexPrincipal].artist;
            player.Load(fileStream);
            MyStringProperty = "00:00";



            var segundosn = (Convert.ToInt16(player.Duration) + 1);
            sliderMus.Maximum = segundosn;

            int hor = (int)(segundosn / (60 * 60));

            int min = (int)((segundosn - (hor * 60 * 60)) / 60);

            int seg = (int)(segundosn - (hor * 60 * 60) - (min * 60));
            string minutos = string.Empty;
            string segundost = string.Empty;

            if (min <= 10)
            {
                minutos = String.Format("0{0}", min);
            }
            else
            {

                minutos = String.Format("{0}", min);
            }
            if (seg <= 10)
            {
                segundost = String.Format("0{0}", seg);
            }
            else
            {

                segundost = String.Format("{0}", seg);
            }

            lblTempoMusica.Text = String.Format("{0}:{1}", minutos, segundost);

            isplay = true;
            sliderMus.Value = 0;
            segundos = 0;
            player.Play();

        }
        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            int quantidade = allSources.Count;
            --quantidade;
            if (indexPrincipal == 0)
            {
                indexPrincipal = quantidade;
            }
            else
            {
                indexPrincipal--;
            }
            Stream fileStream = wc.OpenRead(allSources[indexPrincipal].audio);
            lblmus.Text = allSources[indexPrincipal].name;
            lblartista.Text = allSources[indexPrincipal].artist;
            player.Load(fileStream);
            MyStringProperty = "00:00";



            var segundosn = (Convert.ToInt16(player.Duration) + 1);
            sliderMus.Maximum = segundosn;

            int hor = (int)(segundosn / (60 * 60));

            int min = (int)((segundosn - (hor * 60 * 60)) / 60);

            int seg = (int)(segundosn - (hor * 60 * 60) - (min * 60));
            string minutos = string.Empty;
            string segundost = string.Empty;

            if (min <= 10)
            {
                minutos = String.Format("0{0}", min);
            }
            else
            {

                minutos = String.Format("{0}", min);
            }
            if (seg <= 10)
            {
                segundost = String.Format("0{0}", seg);
            }
            else
            {

                segundost = String.Format("{0}", seg);
            }

            lblTempoMusica.Text = String.Format("{0}:{1}", minutos, segundost);

            isplay = true;
            sliderMus.Value = 0;
            segundos = 0;
            player.Play();

        }
        private void Player_PlaybackEnded(object sender, EventArgs e)
        {
            /*int quantidade = allSources.Count;
            --quantidade;
            if (indexPrincipal == quantidade)
            {
                indexPrincipal = 0;
            }
            else
            {
                indexPrincipal++;
            }
            Stream fileStream = wc.OpenRead(allSources[indexPrincipal].audio);

            lblmus.Text = allSources[indexPrincipal].name;
            lblartista.Text = allSources[indexPrincipal].artist;
            player.Load(fileStream);
            MyStringProperty = "00:00";



            var segundosn = (Convert.ToInt16(player.Duration) + 1);
            sliderMus.Maximum = segundosn;

            int hor = (int)(segundosn / (60 * 60));

            int min = (int)((segundosn - (hor * 60 * 60)) / 60);

            int seg = (int)(segundosn - (hor * 60 * 60) - (min * 60));
            string minutos = string.Empty;
            string segundost = string.Empty;

            if (min <= 10)
            {
                minutos = String.Format("0{0}", min);
            }
            else
            {

                minutos = String.Format("{0}", min);
            }
            if (seg <= 10)
            {
                segundost = String.Format("0{0}", seg);
            }
            else
            {

                segundost = String.Format("{0}", seg);
            }

            lblTempoMusica.Text = String.Format("{0}:{1}", minutos, segundost);

            isplay = true;
            sliderMus.Value = 0;
            segundos = 0;
            player.Play();*/
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (isplay)
            {

                isplay = false;
                _timer.Enabled = false;
                playimg.Source = "icplaybranco";
                player.Pause();


            }
            else
            {

                isplay = true;
                _timer.Enabled = true;
                playimg.Source = "icpausebranco";
                player.Play();
            }
        }
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
        public System.Timers.Timer t = new System.Timers.Timer();
        public static System.Timers.Timer _timer;
        public static List<Musica> allSources;
        WebClient wc = new WebClient();
        public string MyStringProperty
        {
            get { return myStringProperty; }
            set
            {
                myStringProperty = value;
                OnPropertyChanged(nameof(MyStringProperty)); // Notify that there was a change on this property
            }
        }
        private string myStringProperty;
        public static int indexPrincipal;
        bool isplay = false;
        int segundos = 0;
        int tempo = 0;
    }
}