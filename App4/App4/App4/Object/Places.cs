using System;
using System.Collections.Generic;
using System.Text;

namespace App4.Pages
{
    public class Places
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int image_id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string imagesource { get; set; }
        public int distance { get; set; }
        public Places() { }
    }
}
