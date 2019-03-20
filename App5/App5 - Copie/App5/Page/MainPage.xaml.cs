using Akavache;
using App5.Objet;
using App5.Page;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace App5
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Place> model;

        public MainPage()
        {
            InitializeComponent();
            Init();
            recupInformation();
        }

        public async void Init()
        {
            try
            {
                model = await BlobCache.LocalMachine.GetObject<ObservableCollection<Place>>("model");
                listView.ItemsSource = model;
            }
            catch (Exception e) { }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await getIndex((e.Item as Place).id);
            var detail = new Detail();
            detail.BindingContext = e.Item;
            await Navigation.PushAsync(detail);
        }

        public async void recupInformation()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("https://td-api.julienmialon.com/places");
            string listplacesJson = await response.Content.ReadAsStringAsync();

            model = JObject.Parse(listplacesJson)["data"].ToObject<ObservableCollection<Place>>();
           
            for (int i = 0; i < model.Count; i++)
            {
                var response2 = await client.GetAsync("https://td-api.julienmialon.com/places/" + model[i].id);
                string listplacesJson2 = await response2.Content.ReadAsStringAsync();
                model[i] = JObject.Parse(listplacesJson2)["data"].ToObject<Place>();
                model[i].imagesource = "https://td-api.julienmialon.com/images/" + model[i].image_id;
                calculdistance(i);
            }
            triModel();
            listView.ItemsSource = model;
            await BlobCache.LocalMachine.InsertObject("model", model, TimeSpan.FromDays(31));
        }

        public async void calculdistance(int i)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                model[i].distance = (int)Location.CalculateDistance(location, new Location(model[i].latitude, model[i].longitude), DistanceUnits.Kilometers);
            }
            catch (FeatureNotSupportedException e)
            {

            }
            catch (FeatureNotEnabledException e)
            {

            }
            catch (PermissionException e)
            {

            }
            catch (Exception e)
            {

            }
        }

        private void PageAccount(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Account());
        }

        private void PageAddPlace(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPlace());
        }

        public void triModel()
        {
            int min;
            int indexmove;
            for (int i = 0; i < model.Count-1; i++)
            {
                min = model[i].distance;
                indexmove = i;
                for(int j = i+1; j < model.Count; j++)
                {
                    if (min >= model[j].distance)
                    {
                        min = model[j].distance;
                        indexmove = j;
                    }
                }
                if(i != indexmove)
                {
                    Place temp = model[i];
                    model[i] = model[indexmove];
                    model[indexmove] = temp;
                }
            }
        }

        public async Task getIndex(int id)
        {

            int index = 0;
            model = await BlobCache.LocalMachine.GetObject<ObservableCollection<Place>>("model");
            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].id == id)
                {
                    index = i;
                }
            }
            await BlobCache.LocalMachine.InsertObject("index", index, TimeSpan.FromDays(31));
        }

        private void Miseajours_Clicked(object sender, EventArgs e)
        {
            recupInformation();
        }
    }
}
