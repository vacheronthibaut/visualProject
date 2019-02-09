using Xamarin.Forms;
using System.Drawing;
using App1.Model;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Saisons();
        }
    }
}
