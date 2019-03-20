using Akavache;
using App5.Objet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Reactive.Linq;

namespace App5.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Account : ContentPage
	{
		public Account ()
		{
			InitializeComponent ();
            Init();
		}

        public async void Init()
        {
            Token token = await BlobCache.LocalMachine.GetObject<Token>("token");

            if(token != null)
            {
                HttpClient oHttpClient = new HttpClient();
                oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);
                var response = await oHttpClient.GetAsync("https://td-api.julienmialon.com/me");
                string responseString = await response.Content.ReadAsStringAsync();
                try
                {
                    Profil moi = JObject.Parse(responseString)["data"].ToObject<Profil>();
                    if (moi != null)
                    {
                        moi.first_name = "First name : " + moi.first_name;
                        moi.last_name = "Last name : " + moi.last_name;
                        moi.email = "Email : " + moi.email;
                        moi.imagesource = "https://td-api.julienmialon.com/images/" + moi.image_id;
                        informations.BindingContext = moi;
                    }
                }
                catch (Exception e) { }
            }
        }

        private void modifprof(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ModifAccount());
        }

        private void modifpwd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ModifPwd());
        }

        private void Miseajours_Clicked(object sender, EventArgs e)
        {
            Init();
        }
    }
}