using App4.Pages;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
//using Newtonsoft.Json.Linq;
//using Xamarin.Essentials;

namespace App4
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Places> model;
        private int malongitude;

        public MainPage()
        {
            InitializeComponent();
            //recupInformation();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detail = new Detail();
            detail.BindingContext = e.Item;
            Navigation.PushAsync(detail);
        }
        /*
        public async void recupInformation()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("https://td-api.julienmialon.com/places");
            string listplacesJson = await response.Content.ReadAsStringAsync();

            model = JObject.Parse(listplacesJson)["data"].ToObject<ObservableCollection<Places>>();
            for(int i = 0; i < model.Count; i++)
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
            catch(FeatureNotEnabledException e)
            {

            }
            catch(PermissionException e)
            {

            }
            catch (Exception e)
            {

            }
        }*/
    }
}
