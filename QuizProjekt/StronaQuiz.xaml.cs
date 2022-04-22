using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuizProjekt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StronaQuiz : ContentPage
    {
        private ObservableCollection<Pytanie> kolekcjaPytan;
        string kategoria;
        int nrPytania = 0;
        int poprawnaOdpowiedz;
        bool czyMoznaDalej;
        bool czyDobraOdp;
        int punkty = 0;
        int liczbaPytan = 5;
        int losowaLiczba;
        Random losowa;

        public StronaQuiz(Kategorie k)
        {
            InitializeComponent();
            BindingContext = this;
            kategoria = k.Kategoria;
            pobierzPytania();
        }
        void pobierzPytania()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var nazwaJson = "QuizProjekt.Pytania.json";
            using (Stream s = assembly.GetManifestResourceStream(nazwaJson))
            using (StreamReader sr = new StreamReader(s))
            {
                var wynik = sr.ReadToEnd();
                List<Pytanie> listaPytan = JsonConvert.DeserializeObject<List<Pytanie>>(wynik);
                for (int i = listaPytan.Count - 1; i >= 0; i--)
                {
                    if (listaPytan[i].Kategoria != kategoria)
                    {
                        listaPytan.RemoveAt(i);
                    }
                }
                kolekcjaPytan = new ObservableCollection<Pytanie>(listaPytan);
            }
            if(kolekcjaPytan.Count > 5)
            {
                kolejnePytanie();
            }
            else
            {
                pytanie.Text = "Niestety, brak quizów w tej kategorii";
                odpowiedz1.IsVisible = false;
                odpowiedz2.IsVisible = false;
                odpowiedz3.IsVisible = false;
                odpowiedz4.IsVisible = false;
                dalejButton.IsVisible = false;
            }
        }
        void kolejnePytanie()
        {
            losowa = new Random();
            losowaLiczba = losowa.Next(kolekcjaPytan.Count-1);
            pytanie.Text = kolekcjaPytan[losowaLiczba].TrescPytania;
            poprawnaOdpowiedz = kolekcjaPytan[losowaLiczba].PoprawnaOdpowiedz;
            odpowiedz1.IsEnabled = true;
            odpowiedz2.IsEnabled = true;
            odpowiedz3.IsEnabled = true;
            odpowiedz4.IsEnabled = true;
            poprawnaOdpowiedz = kolekcjaPytan[losowaLiczba].PoprawnaOdpowiedz;
            odpowiedz1.Text = kolekcjaPytan[losowaLiczba].Odpowiedzi[0];
            odpowiedz2.Text = kolekcjaPytan[losowaLiczba].Odpowiedzi[1];
            odpowiedz3.Text = kolekcjaPytan[losowaLiczba].Odpowiedzi[2];
            odpowiedz4.Text = kolekcjaPytan[losowaLiczba].Odpowiedzi[3];
            kolekcjaPytan.RemoveAt(losowaLiczba);
        }
        private async void Button_ClickedDalej(object sender, EventArgs e)
        {
            if(czyMoznaDalej)
            {
                odpowiedz1.BackgroundColor = Color.White;
                odpowiedz2.BackgroundColor = Color.White;
                odpowiedz3.BackgroundColor = Color.White;
                odpowiedz4.BackgroundColor = Color.White;
                czyMoznaDalej = false;
                if(czyDobraOdp)
                {
                    punkty++;
                    czyDobraOdp = false;
                }
                if(nrPytania+1 < liczbaPytan)
                {
                    nrPytania++;
                    kolejnePytanie();
                }
                else
                {
                    koniecQuizu();
                }
            }
            else
            {
                await DisplayAlert("Błąd", "Aby przejść dalej musisz zaznaczyć odpowiedź", "Ok");
            }
        }
        private void Button_ClickedWyjscie(object sender, EventArgs e)
        {
            App.Current.MainPage = new WyborQuiz();
        }

        private void odpowiedz1_Clicked(object sender, EventArgs e)
        {
            czyMoznaDalej = true;
            odpowiedz2.IsEnabled = false;
            odpowiedz3.IsEnabled = false;
            odpowiedz4.IsEnabled = false;
            odpowiedz1.BackgroundColor = Color.Green;
            if (poprawnaOdpowiedz == 0)
            {
                odpowiedz1.BackgroundColor = Color.Green;
                czyDobraOdp = true;
            }
            else
            {
                odpowiedz1.BackgroundColor = Color.Red;
            }
        }

        private void odpowiedz2_Clicked(object sender, EventArgs e)
        {
            czyMoznaDalej = true;
            odpowiedz1.IsEnabled = false;
            odpowiedz3.IsEnabled = false;
            odpowiedz4.IsEnabled = false;
            if (poprawnaOdpowiedz == 1)
            {
                odpowiedz2.BackgroundColor = Color.Green;
                czyDobraOdp = true;
            }
            else
            {
                odpowiedz2.BackgroundColor = Color.Red;
            }
        }

        private void odpowiedz3_Clicked(object sender, EventArgs e)
        {
            czyMoznaDalej = true;
            odpowiedz1.IsEnabled = false;
            odpowiedz2.IsEnabled = false;
            odpowiedz4.IsEnabled = false;
            if (poprawnaOdpowiedz == 2)
            {
                odpowiedz3.BackgroundColor = Color.Green;
                czyDobraOdp = true;
            }
            else
            {
                odpowiedz3.BackgroundColor = Color.Red;
            }
        }

        private void odpowiedz4_Clicked(object sender, EventArgs e)
        {
            czyMoznaDalej = true;
            odpowiedz1.IsEnabled = false;
            odpowiedz2.IsEnabled = false;
            odpowiedz3.IsEnabled = false;
            if (poprawnaOdpowiedz == 3)
            {
                odpowiedz4.BackgroundColor = Color.Green;
                czyDobraOdp = true;
            }
            else
            {
                odpowiedz4.BackgroundColor = Color.Red;
            }
        }
        void koniecQuizu()
        {
            dalejButton.IsEnabled = false;
            podsumowanie1.Text = "Podsumowanie: ";
            podsumowanie2.Text = "Wynik: " + punkty + "/" + liczbaPytan;
        }
    }
}