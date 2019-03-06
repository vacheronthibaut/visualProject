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

namespace App5
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Place> model;

        public MainPage()
        {
            InitializeComponent();
            recupInformation();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detail = new Detail();
            detail.BindingContext = e.Item;
            Navigation.PushAsync(detail);
        }
        public async void recupInformation()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("https://td-api.julienmialon.com/places");
            string listplacesJson = await response.Content.ReadAsStringAsync();

            model = JObject.Parse(listplacesJson)["data"].ToObject<ObservableCollection<Place>>();
           
            for (int i = 0; i < model.Count; i++)
            {
                model[i].imagesource = "https://td-api.julienmialon.com/images/" + model[i].image_id;
                calculdistance(i);
            }
            listView.ItemsSource = model;
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
    }
}
