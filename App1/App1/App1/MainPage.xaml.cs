using Xamarin.Forms;
using System.Drawing;
using App1.Model;
using System;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            mydate.Date = DateTime.Now;
            calculall();
        }

        public void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            calculall();
        }

        //monImage
        //resultat
        //mydate
        public void calculall()
        {
            DateTime date = mydate.Date;
            DateTime hiver = new DateTime(2019, 3, 19);
            DateTime printemps = new DateTime(2019, 6, 20);
            DateTime ete = new DateTime(2019, 9, 22);
            DateTime automne = new DateTime(2019, 12, 21);

            if (date < hiver)
            {
                resultat.Text = "hiver";
                monImage.Source = ImageSource.FromFile("Ressources.winter.jpg");
            }
            else
            {
                if (date < printemps)
                {
                    resultat.Text = "printemps";
                }
                else
                {
                    if (date < ete)
                    {
                        resultat.Text = "ete";
                    }
                    else
                    {
                        if (date < automne)
                        {
                            resultat.Text = "automne";
                        }
                        else
                        {
                            resultat.Text = "hiver";
                        }
                    }
                }
            }
        }
    }
}
