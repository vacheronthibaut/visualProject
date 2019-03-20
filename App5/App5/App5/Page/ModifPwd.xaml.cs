using Akavache;
using App5.Objet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Reactive.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace App5.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifPwd : ContentPage
	{
		public ModifPwd ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            newPassword.BackgroundColor = Color.White;
            confirmNewPassword.BackgroundColor = Color.White;
            if (newPassword.Text.Equals(confirmNewPassword.Text))
            {
                ChangePwd change = new ChangePwd
                {
                    old_password = oldPwd.Text,
                    new_password = newPassword.Text
                };
                Token token = await BlobCache.LocalMachine.GetObject<Token>("token");
                if (token != null)
                {
                    HttpClient oHttpClient = new HttpClient();
                    oHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);

                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, "https://td-api.julienmialon.com/me/password")
                    {
                        Content = new StringContent(
                                        JsonConvert.SerializeObject(change),
                                        Encoding.UTF8, "application/json")
                    };
                    var response = await oHttpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            Login login = await BlobCache.LocalMachine.GetOrCreateObject<Login>("usercache", null);
                            login.password = newPassword.Text;
                            await BlobCache.LocalMachine.InsertObject("usercache", login, TimeSpan.FromDays(7));
                        }
                        catch (Exception z) { }
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Oups !", "Un problème est survenue ...", "OK");
                    }
                }

            }
            else
            {
                newPassword.BackgroundColor = Color.Red;
                confirmNewPassword.BackgroundColor = Color.Red;
                await DisplayAlert("Attention !", "Votre confirmation de password n'est pas bonne ...", "OK");
            }
        }
    }
}