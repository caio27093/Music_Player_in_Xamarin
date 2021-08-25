using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace MyMusic
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static Int16 tempo;
        Timer Timer1 = new Timer();
        public MainPage()
        {
            InitializeComponent();

            Timer1.Interval = 1000;
            Timer1.Elapsed += Timer_Tick;
            Timer1.Start();
            // Enable timer.  
            //Timer1.Enabled = true;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tempo++;
            if (tempo == 5)
            {

                Timer1.Close();

                //Application.Current.MainPage = new NavigationPage(new Inicio());

            }
            else
            {

            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
