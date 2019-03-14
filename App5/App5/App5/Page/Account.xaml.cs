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
            if(token == null)
            {

            }
            else
            {
                HttpClient oHttpClient = new HttpClient();
                oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Beare", token.access_token);
                var response = await oHttpClient.GetAsync("https://td-api.julienmialon.com/me");
                string responseString = await response.Content.ReadAsStringAsync();
                Author moi = JObject.Parse(responseString)["data"].ToObject<Author>();
                if (moi != null)
                {
                    informations.BindingContext = moi;
                }
            }
        }
    }
}