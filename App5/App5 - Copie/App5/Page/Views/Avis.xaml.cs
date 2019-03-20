using Akavache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json.Linq;
using App5.Objet;

using System.Reactive.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace App5.Page.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Avis : ContentPage
	{
        private ObservableCollection<Place> model;
        private int index;

        public Avis ()
		{
			InitializeComponent ();
            GetAvis();
        }

        public async void GetAvis()
        {
            model = await BlobCache.LocalMachine.GetObject<ObservableCollection<Place>>("model");
            index = await BlobCache.LocalMachine.GetObject<int>("index");
            titre.Text += index;
            affichageAvis();
            
        }

        public void affichageAvis()
        {
            Color[] couleurs = { Color.FromHex("#FFDAB5"), Color.FromHex("#CAFFB9") };
            for(int i = 0; i < model[index].comments.Count;i++)
            {

                var layoutid = new StackLayout() { Orientation = StackOrientation.Horizontal };
                var layouttext = new StackLayout();
                var layouttout = new StackLayout() { BackgroundColor = couleurs[i%2]};
                layoutid.Children.Add(new Label() { Text = model[index].comments[i].author.first_name, FontAttributes = FontAttributes.Bold });
                layoutid.Children.Add(new Label() { Text = model[index].comments[i].author.last_name, FontAttributes = FontAttributes.Bold });
                layouttext.Children.Add(new Label() { Text = model[index].comments[i].date });
                layouttext.Children.Add(new Label() { Text = model[index].comments[i].text });
                layouttout.Children.Add(layoutid);
                layouttout.Children.Add(layouttext);
                listsAvis.Children.Add(layouttout);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //envois commentaire
                Token token = await BlobCache.LocalMachine.GetObject<Token>("token");

                NouveauCommentaire nc = new NouveauCommentaire
                {
                    text = nouveaucommentaire.Text
                };

                string json = JsonConvert.SerializeObject(nc, Formatting.Indented);
                string sContentType = "application/json";

                HttpClient oHttpClient = new HttpClient();
                oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);
                var response = await oHttpClient.PostAsync("https://td-api.julienmialon.com/places/" + model[index].id + "/comments", new StringContent(json.ToString(), Encoding.UTF8, sContentType));
                string responseString = await response.Content.ReadAsStringAsync();
                ResponseEtatHttp responseEtatHttp = JObject.Parse(responseString).ToObject<ResponseEtatHttp>();
                if (responseEtatHttp.is_success)
                {
                    actuAvis();
                    await DisplayAlert("Bravo !", "vous avez post un nouveau commentaire ...", "OK");
                }
                else
                {
                    await DisplayAlert("Attention !", "Un problème est survenu lors du post de votre commentaire ...\n"+responseEtatHttp.error_message+token.access_token, "OK");
                }
            }
            catch(Exception z)
            {
                Console.WriteLine(z);
            }
        }

        public async void actuAvis()
        {
            var client = new System.Net.Http.HttpClient();
            var response2 = await client.GetAsync("https://td-api.julienmialon.com/places/" + model[index].id);
            string listplacesJson2 = await response2.Content.ReadAsStringAsync();
            model[index] = JObject.Parse(listplacesJson2)["data"].ToObject<Place>();
            
            await BlobCache.LocalMachine.InsertObject("model", model, TimeSpan.FromDays(31));
            nouveaucommentaire.Text = "";
            listsAvis.Children.Clear();
            affichageAvis();
        }
    }
}