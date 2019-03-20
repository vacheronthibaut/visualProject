using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Objet
{
    class Place
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int image_id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string imagesource { get; set; }
        public int distance { get; set; }
        public List<Commentaire> comments { get; set; }
        public Place() { }
    }
}
