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
    public partial class WyborQuiz : ContentPage
    {
        private ObservableCollection<Kategorie> kolekcjaKategorii;
        public WyborQuiz()
        {
            InitializeComponent();
            BindingContext = this;
            pobierzKategorie();

        }
        void pobierzKategorie()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var nazwaJson = "QuizProjekt.Kategorie.json";
            using (Stream s = assembly.GetManifestResourceStream(nazwaJson))
            using (StreamReader sr = new StreamReader(s))
            {
                var wynik = sr.ReadToEnd();
                List<Kategorie> listaKategorii = JsonConvert.DeserializeObject<List<Kategorie>>(wynik);
                kolekcjaKategorii = new ObservableCollection<Kategorie>(listaKategorii);
                myListView.ItemsSource = kolekcjaKategorii;
            }
        }
        private void Button_ClickedPowrot(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

        private void myListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var wybranaKategoria = e.Item as Kategorie;
            ((ListView)sender).SelectedItem = null;
            App.Current.MainPage = new StronaQuiz(wybranaKategoria);
        }
    }
}