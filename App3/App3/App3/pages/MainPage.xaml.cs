using App3.Object;
using App3.pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            getJson();
            //listView.ItemsSource = ListeLieux.LL;
            listView.ItemTapped += ListView_ItemTapped1;
        }

        public async void getJson()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("https://td-api.julienmialon.com/places");
            string listeplacejson = await response.Content.ReadAsStringAsync();

            List<Place> listp = JObject.Parse(listeplacejson)["data"].ToObject<List<Place>>();
            //Console.WriteLine("===================================maliste : " + listp[0].title + "=============================");


            /*
            ListPlace l = new ListPlace();
            if (listeplacejson != "")
            {
                l = JsonConvert.DeserializeObject<ListPlace>(listeplacejson);
            }
            */

            //listView.ItemsSource = l2.listep;

        }

        private void ListView_ItemTapped1(object sender, ItemTappedEventArgs e)
        {
            
            var pagesuiv = new Detail();
            Navigation.PushAsync(pagesuiv);
        }
    }
}
