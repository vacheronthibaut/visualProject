using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Objet
{
    public class ResponseEtatHttp
    {
        public Boolean is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
        public ResponseEtatHttp() { }

    }
}
