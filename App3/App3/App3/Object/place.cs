using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Object
{
    public class Place
    {
        public int id { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public int image_id { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        
        public Place(int i,String t,String d,int im,float la,float lon)
        {
            id = i;
            title = t;
            description = d;
            image_id = im;
            latitude = la;
            longitude = lon;
        }

        public Place() { }
    }
}
