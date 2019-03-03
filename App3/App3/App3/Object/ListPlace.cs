using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Object
{
    public class ListPlace
    {
        public List<Place> listep { get; set; }
        
        public ListPlace(){}
        public ListPlace(List<Place> l)
        {
            listep = l;
        }
    }
}
