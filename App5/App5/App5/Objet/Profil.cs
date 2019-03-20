using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Objet
{
    public class Profil
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int image_id { get; set; }
        public string imagesource { get; set; }
        public Profil() { }
    }
}
