using Akavache;
using App5.Objet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.Reactive.Linq;
using Newtonsoft.Json;
using Plugin.Media;
using System.IO;
using System.Net.Http.Headers;

namespace App5.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifAccount : ContentPage
	{
        public Profil moi;
        public int dernphoto;

        public ModifAccount ()
		{
			InitializeComponent ();
            Init();
		}

        public async void Init()
        {
            Token token = await BlobCache.LocalMachine.GetObject<Token>("token");

            if (token != null)
            {
                HttpClient oHttpClient = new HttpClient();
                oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);
                var response = await oHttpClient.GetAsync("https://td-api.julienmialon.com/me");
                string responseString = await response.Content.ReadAsStringAsync();
                try
                {
                    moi = JObject.Parse(responseString)["data"].ToObject<Profil>();
                    if (moi != null)
                    {
                        moi.imagesource = "https://td-api.julienmialon.com/images/" + moi.image_id;
                        galerie();
                        modifInformations.BindingContext = moi;
                    }
                }
                catch (Exception e) { }
            }
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
                i.AutomationId = ""+count;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (sender, e) => {
                    i.Opacity = .5;
                    await Task.Delay(200);
                    i.Opacity = 1;
                    moi.image_id = Convert.ToInt32(i.AutomationId);
                    imageprofil.Source = "https://td-api.julienmialon.com/images/" + moi.image_id;
                };
                i.GestureRecognizers.Add(tapGestureRecognizer);
                photoProfil.Children.Add(i);
                dernphoto = count;
                count++;
            }
        }

        private async void modifierprofil(object sender, EventArgs e)
        {
            try
            {
                Token token = await BlobCache.LocalMachine.GetObject<Token>("token");
                HttpClient oHttpClient = new HttpClient();
                oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);

                PathProfil m = new PathProfil
                {
                    first_name = moi.first_name,
                    last_name = moi.last_name,
                    image_id = moi.image_id
                };

                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, "https://td-api.julienmialon.com/me")
                {
                    Content = new StringContent(
                                    JsonConvert.SerializeObject(m),
                                    Encoding.UTF8, "application/json")
                };
                var response = await oHttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Oups !", "Un problème est survenue ...", "OK");
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
            catch (Exception z) { }
        }

        private async void addphoto(object sender, EventArgs e)
        {
            try {
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
                    moi.image_id = dernphoto-1;
                    imageprofil.Source = "https://td-api.julienmialon.com/images/" + moi.image_id;
                    await DisplayAlert("Bravo !", "vous avez importé une image", "OK");
                }
                else
                {
                    await DisplayAlert("Oups !", "une erreur est survenue ... ", "OK");
                }
                
            }
            catch(Exception z) { }
            
        }

        private async void appphoto(object sender, EventArgs e)
        {
            try
            {
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    var media = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Receipts",
                        Name = $"{DateTime.UtcNow}.jpg"
                    };

                    var file = await CrossMedia.Current.TakePhotoAsync(media);
                }
            }
            catch(Exception z) { Console.WriteLine("===================================="+z); }
        }
    }
}