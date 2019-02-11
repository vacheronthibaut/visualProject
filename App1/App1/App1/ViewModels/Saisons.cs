using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace App1.Model
{
    class Saisons : ViewModelBase 
    {
        private string _Saisoncurent;
        private DateTime _Date;
        private string _ImageUrl;

        public string Saisoncurent
        {
            get => _Saisoncurent;
            set => SetProperty(ref _Saisoncurent, value);
        }

        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        public DateTime Date
        {
            get => _Date;
            set => setall(value);
        }

        public void setall(DateTime value)
        {
            /*
            L'hiver actuel se terminera le mardi 19 mars 2019 => 2019,3,19
            Le printemps aura lieu du mercredi 20 mars au jeudi 20 juin 2019 => 2019,6,20
            L'été aura lieu du vendredi 21 juin au dimanche 22 septembre 2019 => 2019,9,22
            L'automne aura lieu du lundi 23 septembre au samedi 21 décembre 2019 => 2019,12,21
            */

            _Date = value;
            SetProperty(ref _Date, value);
            DateTime hiver = new DateTime(2019, 3, 19);
            DateTime printemps = new DateTime(2019, 6, 20);
            DateTime ete = new DateTime(2019, 9, 22);
            DateTime automne = new DateTime(2019, 12, 21);

            if (_Date < hiver)
            {
                _Saisoncurent = "hiver";
                SetProperty(ref _Saisoncurent, "hiver");
                _ImageUrl = "Ressourses/winter.jpg";
                SetProperty(ref _ImageUrl, "Ressourses/winter.jpg");
            }
            else
            {
                if (_Date < printemps)
                {
                    _Saisoncurent = "printemps";
                    SetProperty(ref _Saisoncurent, "printemps");
                    _ImageUrl = "Ressourses/spring.jpg";
                    SetProperty(ref _ImageUrl, "Ressourses/spring.jpg");
                }
                else
                {
                    if (_Date < ete)
                    {
                        _Saisoncurent = "ete";
                        SetProperty(ref _Saisoncurent, "ete");
                        _ImageUrl = "Ressourses/summer.jpg";
                        SetProperty(ref _ImageUrl, "Ressourses/summer.jpg");
                    }
                    else
                    {
                        if (_Date < automne)
                        {
                            _Saisoncurent = "automne";
                            SetProperty(ref _Saisoncurent, "automne");
                            _ImageUrl = "Ressourses/autumn.jpg";
                            SetProperty(ref _ImageUrl, "Ressourses/autumn.jpg");
                        }
                        else
                        {
                            _Saisoncurent = "hiver";
                            SetProperty(ref _Saisoncurent, "hiver");
                            _ImageUrl = "Ressourses/winter.jpg";
                            SetProperty(ref _ImageUrl, "Ressourses/winter.jpg");
                        }
                    }
                }
            }
        }

        public Saisons()
        {
            _Date = DateTime.Now;
            setall(_Date);
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            new Saisons();
        }
    }
}
