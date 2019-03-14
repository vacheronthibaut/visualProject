using Akavache;
using App5.Objet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Reactive.Linq;

namespace App5.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnexionInscription : TabbedPage
    {
        public ConnexionInscription()
        {
            InitializeComponent();
            init();
        }

        public async void init()
        {
            try
            {
                Login login = await BlobCache.LocalMachine.GetOrCreateObject<Login>("usercache", null);

                if (login != null)
                {
                    //DisplayAlert("Connexion", "connexion automatique ...","Annuler");
                    emailc.Text = "Connexion automatique";
                    pwdc.Text = "Connexion automatique";
                    isCheck.Checked = true;
                    string json = JsonConvert.SerializeObject(login, Formatting.Indented);
                    string sContentType = "application/json";
                    HttpClient oHttpClient = new HttpClient();
                    var response = await oHttpClient.PostAsync("https://td-api.julienmialon.com/auth/login", new StringContent(json.ToString(), Encoding.UTF8, sContentType));
                    string responseString = await response.Content.ReadAsStringAsync();
                    Token token = JObject.Parse(responseString)["data"].ToObject<Token>();
                    await BlobCache.LocalMachine.InsertObject("token", token, TimeSpan.FromSeconds(token.expires_in));
                    await Navigation.PushAsync(new MainPage());
                }
            }
            catch (Exception e) {
                try
                {
                    string res = await BlobCache.LocalMachine.GetOrCreateObject<string>("lastuser", null);
                    if (res != null)
                    {
                        emailc.Text = res;
                    }
                }
                catch(Exception z){ }
            }
            
        }

        private async void Connexion(object sender, EventArgs e)
        {
            emailc.BackgroundColor = Color.White;
            pwdc.BackgroundColor = Color.White;
            Boolean err = false;
            string messageErr = "";

            if(emailc.Text == null) { emailc.BackgroundColor = Color.Red; err = true; messageErr += "il manque votre email ...\n"; }
            if(pwdc.Text == null) { pwdc.BackgroundColor = Color.Red; err = true; messageErr += "il manque votre passeword ...\n"; }
            if (err)
            {
                await DisplayAlert("Alert", messageErr, "OK");
            }
            else
            {
                Login login = new Login
                {
                    email = emailc.Text,
                    password = pwdc.Text
                };
                string json = JsonConvert.SerializeObject(login, Formatting.Indented);
                string sContentType = "application/json";
                HttpClient oHttpClient = new HttpClient();
                var response = await oHttpClient.PostAsync("https://td-api.julienmialon.com/auth/login", new StringContent(json.ToString(), Encoding.UTF8, sContentType));
                string responseString = await response.Content.ReadAsStringAsync();

                ResponseEtatHttp responseEtatHttp = JObject.Parse(responseString).ToObject<ResponseEtatHttp>();
                if (responseEtatHttp.is_success)
                {
                    emailc.Text = null;
                    pwdc.Text = null;
                    Token token = JObject.Parse(responseString)["data"].ToObject<Token>();
                    await BlobCache.LocalMachine.InsertObject("token", token, TimeSpan.FromSeconds(token.expires_in));
                    await BlobCache.LocalMachine.InsertObject("lastuser", login.email, TimeSpan.FromDays(7));
                    if (isCheck.Checked)
                    {
                        await BlobCache.LocalMachine.InsertObject("usercache",login, TimeSpan.FromDays(7));
                    }
                    else
                    {
                        await BlobCache.LocalMachine.Invalidate("usercache");
                    }
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Erreur", responseEtatHttp.error_message, "OK");
                }
                    
            }
        }

        private async void Inscription(object sender, EventArgs e)
        {
            string messageErreur = "";
            Boolean err = false;
            //init
            fname.BackgroundColor = Color.White;
            lname.BackgroundColor = Color.White;
            email.BackgroundColor = Color.White;
            pwd.BackgroundColor = Color.White;
            cpwd.BackgroundColor = Color.White;

            if (fname.Text == null) { messageErreur += "il manque votre first name ...\n"; err = true; fname.BackgroundColor = Color.Red; }
            if (lname.Text == null) { messageErreur += "il manque votre last name ...\n"; err = true; lname.BackgroundColor = Color.Red; }
            if (email.Text == null) { messageErreur += "il manque votre email ...\n"; err = true; email.BackgroundColor = Color.Red; }
            if (pwd.Text == null) { messageErreur += "il manque votre password ...\n"; err = true; pwd.BackgroundColor = Color.Red; }
            if (cpwd.Text == null) { messageErreur += "il manque votre confirmation de password ...\n"; err = true; cpwd.BackgroundColor = Color.Red; }
            if(pwd.Text != null && cpwd.Text == null)
            {
                if (!pwd.Text.Equals(cpwd.Text)) { messageErreur += "votre password et votre confirmation de password ne sont pas pareil ...\n"; err = true; }
            }
            if (err) { await DisplayAlert("Alert", messageErreur, "OK"); }
            else
            {
                Register register = new Register
                {
                    email = email.Text,
                    first_name = fname.Text,
                    last_name = lname.Text,
                    password = pwd.Text
                };
                string json = JsonConvert.SerializeObject(register,Formatting.Indented);
                string sContentType = "application/json";
                HttpClient oHttpClient = new HttpClient();
                var response = await oHttpClient.PostAsync("https://td-api.julienmialon.com/auth/register", new StringContent(json.ToString(), Encoding.UTF8, sContentType));
                string responseString = await response.Content.ReadAsStringAsync();
                
                ResponseEtatHttp responseEtatHttp = JObject.Parse(responseString).ToObject<ResponseEtatHttp>();
                if (responseEtatHttp.is_success)
                {
                    emailc.Text = email.Text;
                    fname.Text = null;
                    lname.Text = null;
                    email.Text = null;
                    pwd.Text = null;
                    cpwd.Text = null;

                    await DisplayAlert("Inscription Reussie", "vous vous êtes inscrit,\nconnectez vous maintenant !", "OK");
                    Token token = JObject.Parse(responseString)["data"].ToObject<Token>();
                    CurrentPage = conn;
                }
                else
                {
                    await DisplayAlert("Erreur", responseEtatHttp.error_message, "OK");
                }
            }
        }
    }
}