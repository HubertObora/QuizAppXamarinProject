using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizProjekt
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_ClickedStart(object sender, EventArgs e)
        {
            App.Current.MainPage = new WyborQuiz();
        }
        private void Button_ClickedWyjscie(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void Button_ClickedInfo(object sender, EventArgs e)
        {
            App.Current.MainPage = new InfoA();
        }
    }
}
