using System;
using System.Collections.Generic;
using System.Text;

namespace App3
{
    class Lieu
    {
        public String nomLieu { get; set; }

        public String descLieu { get; set; }

        public Lieu() { }

        public Lieu(String n,String d)
        {
            nomLieu = n;
            descLieu = d;
        }
    }
}
