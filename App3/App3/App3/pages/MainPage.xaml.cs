using App3.pages;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using App3.ViewModel;
using Xamarin.Essentials;
using System;

namespace App3
{
    public partial class MainPage : ContentPage
    {

        private ObservableCollection<MainPageModel> model;
        private int malongitude;


        public MainPage()
        {
            InitializeComponent();
            GetJson();
            listView.ItemTapped += ListView_ItemTapped1;
        }
        

        private void ListView_ItemTapped1(object sender, ItemTappedEventArgs e)
        {
            var pagesuiv = new Detail();
            pagesuiv.BindingContext = e.Item;
            Navigation.PushAsync(pagesuiv);
        }

        public async void GetJson()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync("https://td-api.julienmialon.com/places");
            string listeplacejson = await response.Content.ReadAsStringAsync();

            model = JObject.Parse(listeplacejson)["data"].ToObject<ObservableCollection<MainPageModel>>();
            for(int i = 0; i < model.Count; i++)
            {
                model[i].imagesource = "https://td-api.julienmialon.com/images/" + model[i].image_id;
                distance(i);
            }
            
            listView.ItemsSource = model;
        }

        public async void distance(int i)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                
                model[i].distance = (int)Location.CalculateDistance(location, new Location(model[i].latitude, model[i].longitude), DistanceUnits.Kilometers);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
