using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App3.ViewModel
{
    class MainPageModel
    {

        public string title { get; set; }
        public string description { get; set; }
        public int image_id { get; set; }
        public string imagesource { get; set; }
        public int distance { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public MainPageModel()
        {
        }
    }
}
