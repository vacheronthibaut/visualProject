using System;
using Storm.Mvvm;
using System.Collections.Generic;
using System.Text;

namespace App3.ViewModel
{
    public class DetailleModel : ViewModelBase
    {
        private String _titre;
        private String _image;
        private String _description;

        public String Titre
        {
            get => _titre;
            set => SetProperty(ref _titre, value);
        }

        public String Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public String Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DetailleModel()
        {
            
            //_titre = lieu.titre;
            //_image = lieu.image_id;
            //_description = lieu.description;
        }
    }
}
