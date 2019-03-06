using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnexionInscription : TabbedPage
    {
        public ConnexionInscription()
        {
            InitializeComponent();
        }

        private void Connexion(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void Inscription(object sender, EventArgs e)
        {
            CurrentPage = conn;
        }
    }
}