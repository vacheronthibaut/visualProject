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
        public double latitude { get; set; }
        public double longitude { get; set; }
        
        public Place(int i,String t,String d,int im,double la,double lon)
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
