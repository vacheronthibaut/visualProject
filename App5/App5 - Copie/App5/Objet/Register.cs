using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Objet
{
    public class Register
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public Register(string e,string f,string l,string p)
        {
            email = e;
            first_name = f;
            last_name = l;
            password = p;
        }
        public Register() { }
    }
}
