using Akavache;
using App5.Objet;
using Plugin.Media;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace App5.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPlace : ContentPage
	{
        public PostPlace postplace;
        public int dernphoto;


        public AddPlace ()
		{
			InitializeComponent ();
            Init();
		}

        public async void Init()
        {
            try
            {
                galerie();
                postplace = new PostPlace();
                postplace.image_id = -1;
                buttonMap.BackgroundColor = Color.White;
                buttonPosition.BackgroundColor = Color.White;
                carte.IsVisible = true;
                position.IsVisible = false;
                Location location = await Geolocation.GetLastKnownLocationAsync();
                Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(10)));

                map.HeightRequest = 200;
                mymap.Children.Add(map);
            }
            catch(Exception e) { }
        }

        private void parLaMap(object sender, EventArgs e)
        {
            buttonMap.BackgroundColor = Color.LightCyan;
            buttonPosition.BackgroundColor = Color.White;
            carte.IsVisible = true;
            position.IsVisible = false;
        }

        private void parLaPosition(object sender, EventArgs e)
        {
            buttonMap.BackgroundColor = Color.White;
            buttonPosition.BackgroundColor = Color.LightCyan;
            carte.IsVisible = false;
            position.IsVisible = true;
        }

        private async void addphoto(object sender, EventArgs e)
        {
            try
            {
                var media = CrossMedia.Current;
                var file = await media.PickPhotoAsync();

                var upfilebytes = File.ReadAllBytes(file.Path);
                Token token = await BlobCache.LocalMachine.GetObject<Token>("token");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                baContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                content.Add(baContent, "File", "filename.ext");
                var response = await client.PostAsync("https://td-api.julienmialon.com/images", content);
                if (response.IsSuccessStatusCode)
                {
                    photoProfil.Children.Clear();
                    await galerie();
                    postplace.image_id = dernphoto - 1;
                    imageprofil.Source = "https://td-api.julienmialon.com/images/" + postplace.image_id;
                    await DisplayAlert("Bravo !", "vous avez importé une image", "OK");
                }
                else
                {
                    await DisplayAlert("Oups !", "une erreur est survenue ... ", "OK");
                }

            }
            catch (Exception z) { }

        }

        public async Task galerie()
        {
            int count = 1;
            Boolean ok = true;
            while (ok)
            {
                try
                {
                    HttpClient oHttpClient = new HttpClient();
                    var response = await oHttpClient.GetAsync("https://td-api.julienmialon.com/images/" + count);
                    if (!response.IsSuccessStatusCode)
                    {
                        ok = false;
                    }
                }
                catch (Exception e) { ok = false; }
                Image i = new Image
                {
                    HeightRequest = 150,
                    Source = "https://td-api.julienmialon.com/images/" + count
                };
                i.AutomationId = "" + count;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (sender, e) => {
                    i.Opacity = .5;
                    await Task.Delay(200);
                    i.Opacity = 1;
                    postplace.image_id = Convert.ToInt32(i.AutomationId);
                    imageprofil.Source = "https://td-api.julienmialon.com/images/" + postplace.image_id;
                };
                i.GestureRecognizers.Add(tapGestureRecognizer);
                photoProfil.Children.Add(i);
                dernphoto = count;
                count++;
            }
        }

        private async void Ajout(object sender, EventArgs e)
        {
            Boolean ok = true;
            titre.BackgroundColor = Color.White;
            description.BackgroundColor = Color.White;
            latitude.BackgroundColor = Color.White;
            longitude.BackgroundColor = Color.White;

            if (titre.Text == null) { ok = false; titre.BackgroundColor = Color.Red; }
            if(description.Text == null) { ok = false; description.BackgroundColor = Color.Red; }
            if(postplace.image_id == -1) { ok = false; imageprofil.BackgroundColor = Color.Red; }
            if(latitude.Text == null) { ok = false; latitude.BackgroundColor = Color.Red; }
            if(longitude.Text == null) { ok = false; longitude.BackgroundColor = Color.Red; }
            if (ok)
            {
                try
                {
                    Token token = await BlobCache.LocalMachine.GetObject<Token>("token");
                    PostPlace p = new PostPlace
                    {
                        title = titre.Text,
                        description = description.Text,
                        image_id = postplace.image_id,
                        latitude = Convert.ToDouble(latitude.Text),
                        longitude = Convert.ToDouble(longitude.Text)
                    };

                    string json = JsonConvert.SerializeObject(p, Formatting.Indented);
                    string sContentType = "application/json";

                    HttpClient oHttpClient = new HttpClient();
                    oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);
                    var response = await oHttpClient.PostAsync("https://td-api.julienmialon.com/places", new StringContent(json.ToString(), Encoding.UTF8, sContentType));
                    string responseString = await response.Content.ReadAsStringAsync();
                    ResponseEtatHttp responseEtatHttp = JObject.Parse(responseString).ToObject<ResponseEtatHttp>();
                    if (responseEtatHttp.is_success)
                    {
                        DisplayAlert("bravo !", "vous avez posté un nouveau lieu ...\nActualiser la page pour le voir !", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Oups !", "attention un champs n'est pas valide", "OK");
                    }
                }
                catch (Exception z)
                {
                    await DisplayAlert("Oups !", "attention un champs n'est pas valide", "OK");
                }
            }
        }
    }
}